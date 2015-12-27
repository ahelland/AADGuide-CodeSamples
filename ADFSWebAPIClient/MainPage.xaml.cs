using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace ADFSWebAPIClient
{
    public sealed partial class MainPage : Page
    {
        const string aadInstance = "https://adfs.contoso.local/adfs/";
        const string ResourceId = "https://aadguide.azurewebsites.net/WebAPI";
        const string clientId = "copy-from-ADFS-server";
        const string baseApiUrl = "localhost:44320";
        private static AuthenticationContext authContext = null;
        private static Uri redirectURI = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void HelloWebApi_Click(object sender, RoutedEventArgs e)
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
                HttpClient client = new HttpClient();
                string apiRequest = $"https://{baseApiUrl}/api/ADFS";
                client.DefaultRequestHeaders.Authorization = new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", token);
                var response = await client.GetAsync(new Uri(apiRequest));
                string content = await response.Content.ReadAsStringAsync();
                lblHelloAPIOutput.Text = content.ToString();
            }
        }

        /// <summary>
        /// Acquires a token from ADFS
        /// </summary>
        /// <returns>Bearer token</returns>
        private static async Task<AuthenticationResult> GetToken()
        {
            var authority = $"{aadInstance}";
            authContext = new AuthenticationContext(authority, false);

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
    }
}
