using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_Easybar.DTO
{
    public class Producto_dto
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

    }
}