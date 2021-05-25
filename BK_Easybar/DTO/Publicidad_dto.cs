using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_Easybar.DTO
{
    public class Publicidad_dto
    {
        public int id_publicidad { get; set; }
        public int id_bar { get; set; }
        public string nombre_publicidad { get; set; }
        public string tiempo_publicidad { get; set; }
        public string Mensaje { get; set; }
        public Nullable<int> valor { get; set; }
        public string Imagen_publicidad { get; set; }
        public bool Estado_publicidad { get; set; }
    }
}