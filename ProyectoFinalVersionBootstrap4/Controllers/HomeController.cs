using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinalVersionBootstrap4.Models;

namespace ProyectoFinalVersionBootstrap4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Usuario, string Password)
        {
            try
            {
                /*usamos en using para que utilize de manera correcta la entidad, es decir 
                Libere la memoria utilizada al terminar la conexion*/
                using (ProduccionTuBajonSVEntities entidad = new ProduccionTuBajonSVEntities())
                {
                    //creamos un variable tipo var para almacenar la consulta
                    /*Creamos la consulta, en donde d= alias para la tabla usuario, usando el metodo
                    FirstorDefault para que devuelva null  en caso no encuentre datos que coincidan
                     con la contrasenia y usuario ingresado*/
                    var objusuario = (from d in entidad.USUARIO
                                      where d.EMAIL == Usuario.Trim()
                                      && d.PASSWORD == Password.Trim()
                                      select d).FirstOrDefault();
                    //Validamos si el usuario es nulo
                    if (objusuario == null)
                    {
                        ViewBag.Error = "Usuario y/o contrasenia no validos";
                        return View();
                    }
                    else 
                    {
                        //Verificamos si es administrador o usuario
                        if (objusuario.COD_ROL.ToString() == "1")
                        {
                            //redireccionamos a la direccion 
                            Session["Administrador"] = objusuario;
                            ViewBag.Usuario = objusuario.NOMBRE.ToString();
                            return RedirectToAction("Index", "Administrador");
                        }
                        else {
                            //redireccionamos a la direccion 
                            Session["Usuario"] = objusuario;
                            ViewBag.Usuario = objusuario.NOMBRE.ToString();
                            return RedirectToAction("Index", "Home");
                        }
                    
                    }
                    
                   
                }
               
              
            }
            catch (Exception)
            {
                ViewBag.Error = "Usuario no valido";
                return View();
            }

        }

        public ActionResult CerrarSesion() 
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
       
    }
}