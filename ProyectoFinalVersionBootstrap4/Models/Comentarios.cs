//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinalVersionBootstrap4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comentarios
    {
        public int Id_comentario { get; set; }
        public Nullable<int> cod_user { get; set; }
        public Nullable<int> Id_establecimiento { get; set; }
        public string Comentario { get; set; }
        public Nullable<decimal> Calificacion { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
        public virtual establecimientos establecimientos { get; set; }
    }
}
