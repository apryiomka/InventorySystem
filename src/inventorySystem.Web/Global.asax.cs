using System.Web;
using System.Web.Http;
using inventorySystem.Web.App_Start;

namespace inventorySystem.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IoC.Initialize();
        }
    }
}
