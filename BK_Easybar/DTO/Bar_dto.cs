using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_Easybar.DTO
{
    public class Bar_dto
    {

        public int id_bar { get; set; }
        public int id_admin { get; set; }
        public string nit_bar { get; set; }
        public string nombre_bar { get; set; }
        public string Direccion_bar { get; set; }
        public string Telefono_bar1 { get; set; }
        public string Telefono_bar2 { get; set; }
        public string Logo_bar { get; set; }
        public string link_washap { get; set; }
        public string link_facebook { get; set; }
        public string link_instagram { get; set; }
        public string correo_b { get; set; }
        public bool estado_bar { get; set; }
        public string Horario { get; set; }
        public System.DateTime fecha_adicion { get; set; }


    }
}