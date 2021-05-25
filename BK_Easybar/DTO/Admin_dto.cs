using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_Easybar.DTO
{
    public class Admin_dto
    {
        public int id_admin { get; set; }
        public int id_rol { get; set; }
        public int cedula_a { get; set; }
        public string nombre_completo_a { get; set; }
        public string correo_a { get; set; }
        public string Telefono_a { get; set; }
        public string contraseña_a { get; set; }
        public System.DateTime fecha_adicion_a { get; set; }
        public bool estado_admin { get; set; }

    }
}