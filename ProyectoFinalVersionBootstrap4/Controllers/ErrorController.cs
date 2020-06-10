using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalVersionBootstrap4.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult NoAutorizado(string operacion, string modulo, string errorMensaje)
        {
            ViewBag.operacion = operacion;
            ViewBag.modulo = modulo;
            ViewBag.error = errorMensaje;
            return View();
        }
    }
}