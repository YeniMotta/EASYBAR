using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_Easybar.DTO
{
    public class Pedido_dto
    {
        public int id_pedido { get; set; }
        public int id_cliente { get; set; }
        public int id_bar { get; set; }
        public decimal valor_pedido { get; set; }
        public bool estado_pedido { get; set; }
        public string descripcion_pedido { get; set; }      

    }
}