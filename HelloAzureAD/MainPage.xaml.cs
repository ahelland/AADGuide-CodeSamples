using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

using static System.Environment;

namespace HelloAzureAD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const string aadInstance = "https://login.microsoftonline.com/";
        const string ResourceId = "https://graph.windows.net/";
        const string tenant = "contoso.onmicrosoft.com";
        const string clientId = "copy-from-Azure-Portal";
        private static AuthenticationContext authContext = null;
        private static Uri redirectURI = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += (s, e) =>
            {
                var currentPackage = Windows.ApplicationModel.Package.Current;
                var packageFamilyName = currentPackage.Id.FamilyName;

                //Uncomment if you want to have a popup with the PFN
                //new MessageDialog("PFN = " + packageFamilyName).ShowAsync();
                Debug.WriteLine("PFN = " + packageFamilyName);
            };
        }

        /// <summary>
        /// Gets a token, calls the Graph API, and prints out the results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HelloAzureAD_Click(object sender, RoutedEventArgs e)
        {
            var authResult = await GetToken();
            string token = authResult?.AccessToken;
            if (token == null)
            {
                MessageDialog dialog = new MessageDialog($"If the error continues, please contact your administrator.", "Sorry, an error occurred while signing you in.");
                await dialog.ShowAsync();
            }
            if (token != null)
            {
                var user = await GetUserInfo(authResult.TenantId, authResult.UserInfo.UniqueId, token);
                lblHelloAADOutput.Text = "";
                lblHelloAADOutput.Text += $"Display name: {user.displayName}" + NewLine;
                lblHelloAADOutput.Text += $"UPN: {user.userPrincipalName}" + NewLine;
                lblHelloAADOutput.Text += $"Mobile phone: {user.mobile}" + NewLine;
            }
        }

        /// <summary>
        /// Acquires a token from Azure AD to be used in subsequent calls to the Graph API
        /// </summary>
        /// <returns>Bearer token</returns>
        private static async Task<AuthenticationResult> GetToken()
        {
            var authority = $"{aadInstance}{tenant}";
            authContext = new AuthenticationContext(authority);

            AuthenticationResult result = null;
            try
            {
                //PromptBehavior
                //Auto == prompt only when necessary (use cached token if it exists)
                //Always == useful for debug as you will always have to authenticate
                var authParms = new PlatformParameters(PromptBehavior.Auto, false);
                result = await authContext.AcquireTokenAsync(ResourceId, clientId, redirectURI, authParms);

            }
            catch (Exception x)
            {
                if (x.Message == "User canceled authentication")
                {
                    // Do nothing
                }
                return null;
            }

            return result;
        }

        /// <summary>
        /// Performs a "raw" GET request against the Graph API to retrieve a user object
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static async Task<AADUser> GetUserInfo(string tenantId, string userId, string token)
        {
            //Fixed 401 from previous build - newer Graph API in use now
            string graphRequest = $"https://graph.windows.net/{tenantId}/users/{userId}?api-version=1.6";
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", token);
            var response = await client.GetAsync(new Uri(graphRequest));

            string content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<AADUser>(content);
            return user;
        }
    }

    /// <summary>
    /// A "reduced" user object containing only a few attributes
    /// </summary>
    public class AADUser
    {
        public string displayName { get; set; }
        public string userPrincipalName { get; set; }
        public string mobile { get; set; }
    }
}
