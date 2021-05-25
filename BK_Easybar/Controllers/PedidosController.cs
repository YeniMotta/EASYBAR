using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BK_Easybar.Models;
using BK_Easybar.DTO;

namespace BK_Easybar.Controllers
{
    public class PedidosController : ApiController
    {
        private db_easybarEntities db = new db_easybarEntities();

        // GET: api/Pedidos
        [HttpGet]
        public List<Pedido_dto> consultarPedidos()
        {
            var pedido = db.Pedidos.Select(p => new Pedido_dto
            {
                id_pedido = p.id_pedido,
                id_cliente = p.id_cliente,
                id_bar = p.id_bar,
                valor_pedido = p.valor_pedido,
                estado_pedido = p.estado_pedido,
                descripcion_pedido = p.descripcion_pedido
            });
            return pedido.ToList();
        }

        // GET: api/Pedidos/5

        [ResponseType(typeof(Pedidos))]
        [HttpGet]
        public IHttpActionResult ConsultaxPedido(int id)
        {
            Pedido_dto pedido_consultado = db.Pedidos.Where(p => p.id_pedido.Equals(id)).Select(p => new Pedido_dto
            {
                id_pedido = p.id_pedido,
                id_cliente = p.id_cliente,
                id_bar = p.id_bar,
                valor_pedido = p.valor_pedido,
                estado_pedido = p.estado_pedido,
                descripcion_pedido = p.descripcion_pedido

            }).FirstOrDefault();

            if (pedido_consultado == null)
            {
                return NotFound();
            }

            return Ok(pedido_consultado);
        }


        // PUT: api/Pedidos/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult ActualizarPedidos(Pedido_dto Pedidos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Pedidos.id_pedido == null || Pedidos.id_pedido == 0)
            {
                return BadRequest();
            }

            Pedidos pedido_actualizar = db.Pedidos.Where(P => P.id_pedido.Equals(Pedidos.id_pedido)).FirstOrDefault();

            pedido_actualizar.id_cliente = Pedidos.id_cliente;
            pedido_actualizar.id_bar = Pedidos.id_bar;
            pedido_actualizar.valor_pedido = Pedidos.valor_pedido;
            pedido_actualizar.estado_pedido = Pedidos.estado_pedido;
            pedido_actualizar.descripcion_pedido = Pedidos.descripcion_pedido;

            db.Entry(pedido_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidosExists(Pedidos.id_pedido))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Pedidos
        [HttpPost]
        [ResponseType(typeof(Pedidos))]
        public IHttpActionResult InsertarPedidos(Pedido_dto Pedidos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Pedidos pedido_nuevo = new Pedidos();
            pedido_nuevo.id_cliente = Pedidos.id_cliente;
            pedido_nuevo.id_bar = Pedidos.id_bar;
            pedido_nuevo.valor_pedido = Pedidos.valor_pedido;
            pedido_nuevo.estado_pedido = Pedidos.estado_pedido;
            pedido_nuevo.descripcion_pedido = Pedidos.descripcion_pedido;

            db.Pedidos.Add(pedido_nuevo);
            db.SaveChanges();
            return Ok("El Pedido se creo exitosamente");
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidosExists(int id)
        {
            return db.Pedidos.Count(e => e.id_pedido == id) > 0;
        }
    }
}