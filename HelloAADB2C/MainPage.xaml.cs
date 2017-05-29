using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Identity.Client;
using static System.Environment;
using System.Diagnostics;

namespace HelloAADB2C
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void HelloAADB2C_Click(object sender, RoutedEventArgs e)
        {
            var authResult = await GetTokenAsync();
            string token = authResult?.IdToken;

            if (token == null)
            {
                MessageDialog dialog = new MessageDialog($"If the error continues, please contact your administrator.", "Sorry, an error occurred while signing you in.");
                await dialog.ShowAsync();
            }

            if (token != null)
            {                
                var user = GetUserInfo(authResult);
                lblHelloAADB2COutput.Text = "";
                lblHelloAADB2COutput.Text += $"Display name: {user.Name}" + NewLine;
                lblHelloAADB2COutput.Text += $"Identity Provider: {user.IdentityProvider}" + NewLine;
                lblHelloAADB2COutput.Text += $"Identifier: {user.Identifier}" + NewLine;                
            }
        }

        private static async Task<AuthenticationResult> GetTokenAsync()
        {
            AuthenticationResult authResult = null;
            try
            {
                authResult = await App.PublicClientApp.AcquireTokenAsync(App.ApiScopes, GetUserByPolicy(App.PublicClientApp.Users, App.PolicySignUpSignIn), UIBehavior.SelectAccount, string.Empty, null, App.Authority);                
            }

            catch (MsalServiceException ex)
            {
                try
                {
                    if (ex.Message.Contains("AADB2C90118"))
                    {
                        authResult = await App.PublicClientApp.AcquireTokenAsync(App.ApiScopes, GetUserByPolicy(App.PublicClientApp.Users, App.PolicySignUpSignIn), UIBehavior.SelectAccount, string.Empty, null, App.AuthorityResetPassword);
                        return authResult;
                    }
                    else
                    {
                        Debug.WriteLine($"Something went wrong: {ex.Message}");
                        return null;
                    }
                }

                catch (Exception)
                {
                    Debug.WriteLine($"Something went wrong: {ex.Message}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            return authResult;
        }

        private static AADB2CUser GetUserInfo(AuthenticationResult authResult)
        {
            var user = new AADB2CUser { Name = authResult.User.Name, Identifier = authResult.User.Identifier, IdentityProvider = authResult.User.IdentityProvider };

            return user;
        }

        public class AADB2CUser
        {
            public string Name { get; set; }
            public string Identifier { get; set; }
            public string IdentityProvider { get; set; }
        }

        private static IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower())) return user;
            }
            return null;
        }

        private static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
