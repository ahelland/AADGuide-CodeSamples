using System.Configuration;
//Usings for Web API auth
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
//Usings for the Web-based login
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IdentityModel.Claims;
using WebAPIServerSingleTenant.Models;

namespace WebAPIServerSingleTenant
{
    public partial class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientID"];
        private static string appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];

        public static readonly string Authority = aadInstance + tenantId;       

        // This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
        string graphResourceId = "https://graph.windows.net";

        public void ConfigureAuth(IAppBuilder app)
        {
            #region WebAPI
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Tenant = tenant,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = resourceId
                    },
                });
            #endregion

            #region Web-based login
            ApplicationDbContext db = new ApplicationDbContext();

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = Authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,

                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        // If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
                        AuthorizationCodeReceived = (context) =>
                        {
                            var code = context.Code;
                            ClientCredential credential = new ClientCredential(clientId, appKey);
                            string signedInUserID = context.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = 
                            new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(Authority, new ADALTokenCache(signedInUserID));
                            AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(
                            code, new System.Uri(System.Web.HttpContext.Current.Request.Url.GetLeftPart(System.UriPartial.Path)), credential, graphResourceId);

                            return System.Threading.Tasks.Task.FromResult(0);
                        }
                    }
                });
            #endregion
        }
    }
}
