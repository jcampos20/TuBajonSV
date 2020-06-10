using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalVersionBootstrap4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificarSesion());
        }
    }
}
