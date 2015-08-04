using System.Collections.Generic;
using System.Web.Http;

namespace WebAPIServerSingleTenant.Controllers
{
    //Comment out the line below if you want to allow API usage in browser
    [HostAuthentication("OAuth2Bearer")]
    [Authorize]
    public class AADController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Hello()
        {
            return new string[] { "World" };
        }
    }
}
