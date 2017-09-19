using System.Data.Entity;
using System.Web.Http;
using ADFSTodoSPA.DAL;

namespace ADFSTodoSPA
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new TodoListServiceInitializer());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
