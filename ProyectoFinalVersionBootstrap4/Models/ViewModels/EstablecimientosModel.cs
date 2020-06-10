using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalVersionBootstrap4.Models
{
    public class EstablecimientosModel
    {
        [Required]
        public int Id_establecimiento { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required(ErrorMessage="Ingrese el nombre del establecimiento")]
        public string Nombre_tienda { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [RegularExpression("[0-9]{8}")]
        public string Telefono { get; set; }
        public Nullable<int> Categoria { get; set; }
        public string Tipo_entrega { get; set; }
        [Required]
        public Nullable<decimal> Precio { get; set; }
        public string Horario { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> latitud { get; set; }
        public Nullable<decimal> longitud { get; set; }
    }
    
}