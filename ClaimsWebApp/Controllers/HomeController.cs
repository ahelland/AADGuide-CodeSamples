using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace ClaimsWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;

            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            return View();
        } 
              
        public ActionResult AddClaim()
        {
            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;

            //ClaimType, Value, ValueType, Issuer
            Claim localClaim = new Claim(ClaimTypes.Webpage, "http://localhost", ClaimValueTypes.String, "AADGuide");

            ClaimsPrincipal.Current.Identities.First().AddClaim(localClaim);
            return View("Index");
        }

        public ActionResult RemoveClaim()
        {
            Claim customClaim = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Webpage);

            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;

            ClaimsPrincipal.Current.Identities.First().RemoveClaim(customClaim);
            return View("Index");
        }
    }
}