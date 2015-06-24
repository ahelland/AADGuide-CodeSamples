using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Environment;

namespace GraphTreeView
{
    public partial class GraphTreeView : Form
    {
        public GraphTreeView()
        {
            InitializeComponent();
        }

        private void btnGetTenantDetails_Click(object sender, EventArgs e)
        {
            string currentDateTime = DateTime.Now.ToUniversalTime().ToString();

            #region Setup Active Directory Client
            ActiveDirectoryClient activeDirectoryClient;
            try
            {
                activeDirectoryClient = AuthenticationHelper.GetActiveDirectoryClientAsApplication();
            }
            catch (AuthenticationException ex)
            {
                if (ex.InnerException != null)
                {
                    //You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                    //InnerException Message will contain the HTTP error status codes mentioned in the link above                                       
                    txtOutput.Text += $"Error detail: {ex.InnerException.Message}";
                }
                return;
            }

            #endregion

            #region TenantDetails            
            VerifiedDomain initialDomain = new VerifiedDomain();
            VerifiedDomain defaultDomain = new VerifiedDomain();
            ITenantDetail tenant = null;
            txtOutput.Text += "Retrieving Tenant Details" + NewLine;
            try
            {
                List<ITenantDetail> tenantsList = activeDirectoryClient.TenantDetails
                    .Where(tenantDetail => tenantDetail.ObjectId.Equals(Constants.TenantId))
                    .ExecuteAsync().Result.CurrentPage.ToList();
                if (tenantsList.Count > 0)
                {
                    tenant = tenantsList.First();
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text += $"Error getting TenantDetails {ex.Message} {ex.InnerException?.Message}" + NewLine;
            }
            if (tenant == null)
            {
                txtOutput.Text += "Tenant not found" + NewLine;
            }
            else
            {
                TenantDetail tenantDetail = (TenantDetail)tenant;
                txtDetails.Text = $"Display Name: {tenantDetail.DisplayName}" + NewLine;

                // Get the Tenant's Verified Domains 
                initialDomain = tenantDetail.VerifiedDomains.First(x => x.Initial.HasValue && x.Initial.Value);
                txtDetails.Text += $"Initial Domain Name: {initialDomain.Name}" + NewLine;
                defaultDomain = tenantDetail.VerifiedDomains.First(x => x.@default.HasValue && x.@default.Value);
                txtDetails.Text += $"Default Domain Name: {defaultDomain.Name}" + NewLine;

                // Get Tenant's Tech Contacts
                foreach (string techContact in tenantDetail.TechnicalNotificationMails)
                {
                    txtDetails.Text += $"Tech Contact: {techContact}" + NewLine;
                }

                txtDetails.Text += $"Street: {tenantDetail.Street}" + NewLine;
                txtDetails.Text += $"Zip: {tenantDetail.PostalCode}" + NewLine;
                txtDetails.Text += $"City: {tenantDetail.City}" + NewLine;
                txtDetails.Text += $"State: {tenantDetail.State}" + NewLine;
                txtDetails.Text += $"Country: {tenantDetail.Country}" + NewLine;
                txtDetails.Text += $"Phone: {tenantDetail.TelephoneNumber }" + NewLine;

                txtDetails.Text += $"Dirsync: {tenantDetail.DirSyncEnabled}" + NewLine;
                txtDetails.Text += $"Last Dirsync: {tenantDetail.CompanyLastDirSyncTime}" + NewLine;
            }

            #endregion
        }

        private void btnLoadGroups_Click(object sender, EventArgs e)
        {
            string currentDateTime = DateTime.Now.ToUniversalTime().ToString();

            #region Setup Active Directory Client
            
            ActiveDirectoryClient activeDirectoryClient;
            try
            {
                activeDirectoryClient = AuthenticationHelper.GetActiveDirectoryClientAsApplication();
            }
            catch (AuthenticationException ex)
            {
                if (ex.InnerException != null)
                {
                    //You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                    //InnerException Message will contain the HTTP error status codes mentioned in the link above                                       
                    txtOutput.Text += $"Error detail: {ex.InnerException.Message}";
                }
                return;
            }

            #endregion

            #region Get list of Groups
            
            try
            {
                txtOutput.Text += $"***** Starting GetGroups at {currentDateTime} *****" + NewLine;                
                var count = 0;
                var allGroups = activeDirectoryClient.Groups.OrderBy(group => group.DisplayName).ExecuteAsync().Result;

                do
                {
                    List<IGroup> subGroups = allGroups.CurrentPage.ToList();
                    foreach (IGroup group in subGroups)
                    {
                        if (!tenantTree.Nodes[0].Nodes.ContainsKey(group.ObjectId))
                        {
                            tenantTree.Nodes[0].Nodes.Add(group.ObjectId, group.DisplayName);
                        }
                        count++;
                    }
                    allGroups = allGroups.GetNextPageAsync().Result;
                } while (allGroups != null);

                txtOutput.Text += $"Group Count: {count}" + NewLine;

            }
            catch (Exception ex)
            {
                txtOutput.Text += $"Error getting groups: {ex.Message} {ex.InnerException?.Message}" + NewLine;
            }

            #endregion

            currentDateTime = DateTime.Now.ToUniversalTime().ToString();
            txtOutput.Text += $"***** Ending GetGroups at {currentDateTime} *****" + NewLine;
        }

        private void btnLoadUsers_Click(object sender, EventArgs e)
        {           
            string currentDateTime = DateTime.Now.ToUniversalTime().ToString();
            var selectedGroup = tenantTree.SelectedNode;
            var groupId = selectedGroup.Name;

            #region Setup Active Directory Client
            
            ActiveDirectoryClient activeDirectoryClient;
            try
            {
                activeDirectoryClient = AuthenticationHelper.GetActiveDirectoryClientAsApplication();
            }
            catch (AuthenticationException ex)
            {
                if (ex.InnerException != null)
                {
                    //You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                    //InnerException Message will contain the HTTP error status codes mentioned in the link above                                       
                    txtOutput.Text += $"Error detail: {ex.InnerException.Message}";
                }
                return;
            }

            #endregion

            #region Get users for a given group

            try
            {
                txtOutput.Text += $"***** Starting GetUsers at {currentDateTime} *****" + NewLine;              

                IGroup groups = activeDirectoryClient.Groups.Where(d => d.ObjectId == groupId).ExecuteSingleAsync().Result;
                Group group = (Group)groups;

                IGroupFetcher groupFetcher = group;
                var members = groupFetcher.Members.ExecuteAsync().Result;

                var count = 0;

                if (members.CurrentPage.Count != 0)
                {
                    do
                    {
                        var memberObjects = members.CurrentPage.ToList();
                        foreach (IDirectoryObject member in memberObjects)
                        {
                            if (member is User)
                            {
                                User user = member as User;

                                if (!selectedGroup.Nodes.ContainsKey(user.ObjectId))
                                {
                                    selectedGroup.Nodes.Add(user.ObjectId, user.DisplayName);
                                }
                                count++;
                            }
                            members = members.GetNextPageAsync().Result;
                        }
                    } while (members != null);

                    txtOutput.Text += $"User Count (for selected group): {count}" + NewLine;
                }
                if (count == 0)
                {
                    txtOutput.Text += "There doesn't seem to be any users in this group." + NewLine;
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text += $"Error getting users: {ex.Message} {ex.InnerException?.Message}" + NewLine;                
            }

            #endregion

            currentDateTime = DateTime.Now.ToUniversalTime().ToString();
            txtOutput.Text += $"***** Ending GetUsers at {currentDateTime} *****" + NewLine;
        }

        private void btnGroupDetails_Click(object sender, EventArgs e)
        {
            var selectedGroup = tenantTree.SelectedNode;
            var groupId = selectedGroup.Name;

            #region Setup Active Directory Client
            ActiveDirectoryClient activeDirectoryClient;
            try
            {
                activeDirectoryClient = AuthenticationHelper.GetActiveDirectoryClientAsApplication();
            }
            catch (AuthenticationException ex)
            {
                if (ex.InnerException != null)
                {
                    txtOutput.Text += $"Error detail: {ex.InnerException.Message}";
                }
                return;
            }
            #endregion

            try
            {
                IGroup groups = activeDirectoryClient.Groups.Where(d => d.ObjectId == groupId).ExecuteSingleAsync().Result;
                Group group = (Group)groups;

                txtDetails.Text = $"Group name: {group.DisplayName}" + NewLine;
                txtDetails.Text += $"Last DirSync {group.LastDirSyncTime}";
            }
            catch
            {
                txtOutput.Text += "Oops, did you forget to select a group first?" + NewLine;
            }
        }

        private void btnUserDetails_Click(object sender, EventArgs e)
        {
            var selectedUser = tenantTree.SelectedNode;
            var userId = selectedUser.Name;

            #region Setup Active Directory Client
            ActiveDirectoryClient activeDirectoryClient;
            try
            {
                activeDirectoryClient = AuthenticationHelper.GetActiveDirectoryClientAsApplication();
            }
            catch (AuthenticationException ex)
            {
                if (ex.InnerException != null)
                {
                    txtOutput.Text += $"Error detail: {ex.InnerException.Message}";
                }
                return;
            }
            #endregion

            try
            {
                IUser users = activeDirectoryClient.Users.Where(d => d.ObjectId == userId).ExecuteSingleAsync().Result;
                User user = (User)users;

                txtDetails.Text = $"User name: {user.DisplayName}" + NewLine;
                txtDetails.Text += $"Mail address: {user.Mail}" + NewLine;
                txtDetails.Text += $"Mobile phone: {user.Mobile}";
            }
            catch
            {
                txtOutput.Text += "Oops, did you forget to select a user first?" + NewLine;
            }
        }
    }
}
