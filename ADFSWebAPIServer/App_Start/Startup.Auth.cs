using System.Configuration;
//Usings for Web API auth
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
//Usings for the Web-based login
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace ADFSWebAPIServer
{
    public partial class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientID"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];
        private static string ADFSServer = ConfigurationManager.AppSettings["ida:ADFSServer"];
        private static string ADFSDiscoveryDoc = ADFSServer + "adfs/.well-known/openid-configuration";
        private static string FederationMetadata = ADFSServer + "federationmetadata/2007-06/federationmetadata.xml";


        public void ConfigureAuth(IAppBuilder app)
        {
            #region WebAPI
            app.UseActiveDirectoryFederationServicesBearerAuthentication(
                new ActiveDirectoryFederationServicesBearerAuthenticationOptions
                {
                    MetadataEndpoint = FederationMetadata,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = resourceId,
                        AuthenticationType = "OAuth2Bearer"
                    }
                }
            );
            #endregion

            #region Web-based login            
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    MetadataAddress = ADFSDiscoveryDoc,
                    RedirectUri = postLogoutRedirectUri,
                    Resource = resourceId,
                    PostLogoutRedirectUri = postLogoutRedirectUri,

                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        AuthenticationFailed = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Error?message=" + context.Exception.Message);
                            return System.Threading.Tasks.Task.FromResult(0);
                        }
                    }
                });
            #endregion
        }
    }
}