using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace CoreWebAPIClient
{

    public sealed partial class MainPage : Page
    {
        const string aadInstance = "https://login.microsoftonline.com/";
        const string tenantId = "tenant-guid";
        const string clientId = "client-guid";
        const string baseApiUrl = "localhost:44399";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void HelloWebApi_Click(object sender, RoutedEventArgs e)
        {
            var authResult = await GetToken();
            string token = authResult?.Token;

            if (token == null)
            {
                MessageDialog dialog = new MessageDialog("If the error continues, please contact your administrator.", "Sorry, an error occurred while signing you in.");
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

        private static async Task<AuthenticationResult> GetToken()
        {                      
            var clientApp = new PublicClientApplication(clientId);

            string[] scopes = { clientId };

            AuthenticationResult result = null;
            try
            {
                //If you want to go straight to the branded login page
                //result = await clientApp.AcquireTokenAsync(scopes,"",UiOptions.SelectAccount,string.Empty,null,authority,null);
                //var authority = $"{aadInstance}{tenantId}";

                //To go to the generic login page (which will dynamically change/redirect)
                result = await clientApp.AcquireTokenAsync(scopes, "", UiOptions.SelectAccount, string.Empty);
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
