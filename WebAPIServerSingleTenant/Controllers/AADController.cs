using System.Collections.Generic;
using System.Web.Http;

namespace WebAPIServerSingleTenant.Controllers
{
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
