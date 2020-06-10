using ProyectoFinalVersionBootstrap4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ProyectoFinalVersionBootstrap4.Filters
{
    //Es para los metodos a usar del atributo
    //Se puede usar los multiples metodos para que esten desasctivados
    //y dependiendo el usuario la oeracion que vaya hacer funcione
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Autorizacion:AuthorizeAttribute
    {
        private USUARIO objetoUsuario;
        private ProduccionTuBajonSVEntities data = new Models.ProduccionTuBajonSVEntities();
        private int Cod_Operacion;

        public Autorizacion(int codOper = 0)//inicializado en 0 para que tenga acceso a todos los modulos
        {
            Cod_Operacion = codOper;
            //en base a parametros será operacion que se hará
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string nombreOperac = "";
            string nombreModulo = "";
            try
            {
                //Validamos si el usuario es administrador o cliente
                if ((USUARIO)HttpContext.Current.Session["Usuario"] == null)
                {
                    objetoUsuario = (USUARIO)HttpContext.Current.Session["Administrador"];

                }
                else {
                    objetoUsuario = (USUARIO)HttpContext.Current.Session["Usuario"];

                }
                
                var lstOperaciones = from opr in data.ROL_OPERA
                                     where opr.COD_ROL == objetoUsuario.COD_ROL
                                     && opr.COD_OPERA == Cod_Operacion
                                     select opr;
                if (lstOperaciones.ToList().Count() == 0)
                {
                    //Obtener nombre operacion
                    var objOperar = data.OPERACIONES.Find(Cod_Operacion);
                    //si no se sabe si es int16-Int32 se pone sigo de interrogacion
                    int? cod_modum = objOperar.COD_MOD;
                    nombreOperac = getNombreOperacion(Cod_Operacion);
                    //obtener nombre modulo
                    nombreModulo = getNombreModulo(cod_modum);
                    //CREAR CONTROLADOR DE ERROR 
                    filterContext.Result = new RedirectResult("~/Error/NoAutorizado?operacion="+nombreOperac+",modulo="+nombreModulo+",error= No_Autorizado");
              
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/NoAutorizado?operacion=" + nombreOperac + ",modulo=" + nombreModulo + ",error= No_Autorizado" + ex.Message);

            }

           // base.OnAuthorization(filterContext);
        }

        private string getNombreModulo(int? cod_modum)
        {
            var modulo = from op in data.MODULO
                         where op.COD_MOD == Cod_Operacion
                         select op.NOM_MOD;
            string nombreMOD;
            try
            {
                nombreMOD = modulo.First().ToString();
            }
            catch (Exception)
            {
                nombreMOD = "";
            }
            return nombreMOD;
        }

        private string getNombreOperacion(int cod_Operacion)
        {
            var operar = from op in data.OPERACIONES
                         where op.COD_OPERA == Cod_Operacion
                         select op.NOM_OPERA;
            string nombreOp;
            try
            {
                nombreOp = operar.First();
            }
            catch (Exception)
            {
                nombreOp = "";
            }
            return nombreOp;
        }
    }
}