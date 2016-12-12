using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using static System.Environment;

namespace AboutMePasswordGrant
{
    public partial class AboutMeForm : Form
    {
        public AboutMeForm()
        {
            InitializeComponent();
        }

        private async void btnAction_Click(object sender, EventArgs e)
        {
            var domain = txtAADDomainName.Text;
            var user = txtUsername.Text;
            var pw = txtPassword.Text;
            var clientId = txtClientId.Text;
            var resource = txtResource.Text;

            HttpClient client = new HttpClient();

            string requestUrl = $"https://login.microsoftonline.com/{domain}/oauth2/token";

            string request_content = $"grant_type=password&resource={resource}&client_id={clientId}&username={user}&password={pw}&scope=openid";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            try
            {
                request.Content = new StringContent(request_content, Encoding.UTF8, "application/x-www-form-urlencoded");
            }
            catch (Exception x)
            {
                var msg = x.Message;
                txtOutput.Text = $"Something went wrong: {msg}";
            }
            HttpResponseMessage response = await client.SendAsync(request);

            string responseString = await response.Content.ReadAsStringAsync();
            GenericToken token = JsonConvert.DeserializeObject<GenericToken>(responseString);
            var at = token.access_token;

            var me = await GetUserInfo(at);

            txtOutput.Text = $"Display Name:{me.displayName}{NewLine}Upn:{me.userPrincipalName}{NewLine}Preferred Language:{me.preferredLanguage}";

        }

        private static async Task<AADUser> GetUserInfo(string token)
        {
            string graphRequest = $"https://graph.microsoft.com/v1.0/me/";
            var authHeader = "Bearer " + token;
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authHeader);
            var response = await client.GetAsync(new Uri(graphRequest));

            string content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<AADUser>(content);
            return user;
        }

        internal class GenericToken
        {
            public string token_type { get; set; }
            public string scope { get; set; }
            public string resource { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public string id_token { get; set; }
            public string expires_in { get; set; }
        }

        //A selection of attributes to include in deserialization - there are more available
        internal class AADUser
        {
            public string displayName { get; set; }
            public string givenName { get; set; }
            public string surname { get; set; }
            public string mail { get; set; }
            public string preferredLanguage { get; set; }
            public string userPrincipalName { get; set; }
            public string mobilePhone { get; set; }
        }

    }
}
