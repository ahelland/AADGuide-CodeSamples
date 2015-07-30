using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace WebAPIClientSingleTenant
{
    public sealed partial class MainPage : Page
    {
        //Replace 'Contoso' with your tenant name
        const string aadInstance = "https://login.microsoftonline.com/";
        const string ResourceId = "https://contoso.onmicrosoft.com/WebAPIServerSingleTenant";
        const string tenant = "contoso.onmicrosoft.com";
        const string clientId = "copy-from-Azure-Portal";
        const string baseApiUrl = "localhost:44300";
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
                string apiRequest = $"https://{baseApiUrl}/api/AAD";
                client.DefaultRequestHeaders.Authorization = new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", token);
                var response = await client.GetAsync(new Uri(apiRequest));
                string content = await response.Content.ReadAsStringAsync();
                lblHelloAPIOutput.Text = content.ToString();
            }
        }

        /// <summary>
        /// Acquires a token from Azure AD
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
    }
}
