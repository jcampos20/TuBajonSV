//importar controlador
using ProyectoFinalVersionBootstrap4.Controllers;
using ProyectoFinalVersionBootstrap4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//importar esta libreria
using System.Web.Mvc;
namespace ProyectoFinalVersionBootstrap4.Filters
{
    public class VerificarSesion: ActionFilterAttribute//heredar ActionFiltrs
    {
        //Sirve para validar si esta logueado o no, para que no pueda 
        //navegar por las paginas
        private USUARIO objetoUsuario;

        //crear metodo sobrecargado
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                //traemola sesion del controlador
                //Validamos si el usuario es administrador o cliente
                if ((USUARIO)HttpContext.Current.Session["Usuario"] == null)
                {
                    objetoUsuario = (USUARIO)HttpContext.Current.Session["Administrador"];

                }
                else
                {
                    objetoUsuario = (USUARIO)HttpContext.Current.Session["Usuario"];

                }
                if (objetoUsuario == null)
                {
                    if (filterContext.Controller is HomeController == false)
                    {
                        //si alguien quiere entrar a url no permitira si no está logueado
                        //genera el filtro si no ha iniciado la sesion
                        filterContext.HttpContext.Response.Redirect("~/Home/Index");
                    }
                }
            }
            catch(Exception)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Index");
                //se valida para que no se pueda navegar libremente
            }
            //Despues ir a al FilterConfig en la carpeta appStart
        }
    }
}