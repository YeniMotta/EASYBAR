using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_Easybar.DTO
{
    public class cliente_dto
    {
        public int id_cliente { get; set; }
        public int id_rol { get; set; }
        public int cedula { get; set; }
        public string nombre_completo { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string Barrio_localidad { get; set; }
        public string Telefono { get; set; }
        public string contraseña { get; set; }
        public System.DateTime fecha_adicion { get; set; }
        public bool estado_cliente { get; set; }
    }
}