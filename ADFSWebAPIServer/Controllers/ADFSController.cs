using System.Collections.Generic;
using System.Web.Http;

namespace ADFSWebAPIServer.Controllers
{
    //Comment out the line below if you want to allow API usage in browser
    [HostAuthentication("OAuth2Bearer")]
    [Authorize]
    public class ADFSController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Hello()
        {
            return new string[] { "World" };
        }
    }
}
