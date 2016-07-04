using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CoreWebAPIServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AADController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "World" };
        }

    }
}
