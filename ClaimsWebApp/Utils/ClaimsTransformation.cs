using System.Security.Claims;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace ClaimsWebApp.Utils
{
    public class ClaimsTransformationMiddleware
    {       
        readonly Func<IDictionary<string, object>, Task> _next;       

        public ClaimsTransformationMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }
        
        public async Task Invoke(IDictionary<string, object> env)
        {
            ClaimsPrincipal incomingPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated == true)
            {
                ClaimsIdentity claimsIdentity = incomingPrincipal.Identity as ClaimsIdentity;
        
                string tenantId = incomingPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
                
                //Replace with tenant id for xyz.onmicrosoft.net
                string adminTenant = "1234-abcd-GUID";

                if (tenantId == adminTenant)
                {
                    ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, "AADGuide"));
                }
                
                Thread.CurrentPrincipal = incomingPrincipal;        
            }
            await _next(env);
        }        
    }
}