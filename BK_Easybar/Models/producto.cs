//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BK_Easybar.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class producto
    {
        public int id_producto { get; set; }
        public int id_bar { get; set; }
        public Nullable<bool> producto_especial { get; set; }
        public string nombre_producto { get; set; }
        public string categoria { get; set; }
        public string descripcion_producto { get; set; }
        public bool disponibilidad { get; set; }
        public string cantidad { get; set; }
        public string precio { get; set; }
        public string imagen_producto { get; set; }
        public bool estado_producto { get; set; }
    
        public virtual bar bar { get; set; }
    }
}
