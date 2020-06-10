using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinalVersionBootstrap4.Models;
using ProyectoFinalVersionBootstrap4.Filters;

namespace ProyectoFinalVersionBootstrap4.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        [Autorizacion(codOper:1)]
        public ActionResult Index()
        {
            return View();
        }
        [Autorizacion(codOper: 2)]
        public ActionResult Establecimientos()
        {
            //creamos una lista del tipo categorias para almacenar la consulta
            var listaidestablecimientos = new List<int>();
            var listaSelect = new List<categorias>();
            using (ProduccionTuBajonSVEntities modelo = new ProduccionTuBajonSVEntities()) 
            {
                //realizamos la consulta a la tabla categorias

                listaSelect = modelo.categorias.ToList();
                
            }

            //Devolvemos la lista con la consulta a la vista
            ViewBag.Milista = listaSelect;

            return View();
           
        }
        [Autorizacion(codOper: 2)]
        [HttpPost]
        public ActionResult Establecimientos(EstablecimientosModel obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ProduccionTuBajonSVEntities modelo = new ProduccionTuBajonSVEntities())
                    {
                        //realizamos la consulta a la tabla categorias
                       var listaidestablecimientos = (from d in modelo.establecimientos
                                                   select d.Id_establecimiento).ToList();
                        //Creamos una lista tipo input select para que se muestre
                        if (listaidestablecimientos.Count() < 1)
                        {
                            ViewBag.Correlativo = 1;

                        }
                        else
                        {
                            ViewBag.Correlativo = listaidestablecimientos.Max();

                        }
                        var tablaestablecimientos = new establecimientos();
                        tablaestablecimientos.Id_establecimiento = ViewBag.Correlativo;
                        tablaestablecimientos.Imagen = "";
                        tablaestablecimientos.Nombre_tienda = obj.Nombre_tienda;
                        tablaestablecimientos.Direccion = obj.Direccion;
                        tablaestablecimientos.Telefono = obj.Telefono;
                        tablaestablecimientos.Categoria = obj.Categoria;
                        tablaestablecimientos.Tipo_entrega = obj.Tipo_entrega;
                        tablaestablecimientos.Precio = obj.Precio;
                        tablaestablecimientos.Horario = obj.Horario;
                        tablaestablecimientos.Descripcion = obj.Descripcion;
                        tablaestablecimientos.latitud = obj.latitud;
                        tablaestablecimientos.longitud = obj.longitud;

                        modelo.establecimientos.Add(tablaestablecimientos);
                        modelo.SaveChanges();


                    }
                }
                catch 
                {
                    ViewBag.Error("registro no completado");
                    return View();
                }


            }

            ViewBag.Exito="Registro Completo";
            return View();
        
        }
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


    }
}