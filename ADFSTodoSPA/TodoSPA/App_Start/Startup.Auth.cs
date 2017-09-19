using Owin;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.OAuth;

namespace ADFSTodoSPA
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {            
            var tvps = new TokenValidationParameters
            {
                // In this app, the TodoListClient and TodoListService
                // are represented using the same Application Id - we use
                // the Application Id to represent the audience, or the
                // intended recipient of tokens.

                ValidAudience = ConfigurationManager.AppSettings["ida:Audience"],

                // In a real application, you might use issuer validation to
                // verify that the user's organization (if applicable) has
                // signed up for the app.  Here, we'll just turn it off.

                ValidateIssuer = false,
            };

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = new Microsoft.Owin.Security.Jwt.JwtFormat(tvps, new OpenIdConnectCachingSecurityTokenProvider("https://login.contoso.com/adfs/.well-known/openid-configuration")),
            });
        }

    }
}
