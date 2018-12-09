using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebAPISignedJWTs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //Metadata for ADFS shown, but this can be substituted with either the Azure AD v2 common endpoint:
                //https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration or the tenant-specific version:
                //https://login.microsoftonline.com/{tenant}/v2.0/.well-known/openid-configuration
                options.MetadataAddress = "https://adfs.contoso.com/adfs/.well-known/openid-configuration";
                options.Validate();

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //For ADFS the access token issuer is not the same as the issuer attribute in the metadata so we need
                    //this additional parameter to pass validation
                    ValidIssuer = "http://adfs.contoso.com/adfs/services/trust",
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = "https://contoso.com/api",
                    RequireSignedTokens = true,
                    ValidateActor = true,
                };
            });

            services.AddAuthorization(options =>
            {
                //Policy for accepting only cert-based auth
                options.AddPolicy("Certificate", policy =>
                    policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        ((c.Type == System.Security.Claims.ClaimTypes.AuthenticationMethod &&
                        c.Value == "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/tlsclient") ||
                        (c.Type == System.Security.Claims.ClaimTypes.AuthenticationMethod &&
                        c.Value == "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/x509"))
                        )));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();            
            app.UseMvc();
        }
    }
}
