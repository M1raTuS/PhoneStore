using PhoneStore.Domain.Entities;
using PhoneStore.WebUI.Infrastructure.Binder;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhoneStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelIBinder());
        }
    }
}
