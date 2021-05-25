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
using BK_Easybar.DTO;
using BK_Easybar.Models;
using System.Web.Http.Cors;

namespace BK_Easybar.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class productoController : ApiController
    {
        private db_easybarEntities db = new db_easybarEntities();

        // GET: api/producto
        [HttpGet]
        public List<Producto_dto> ConsultarProductos()
        {
            var producto = db.producto.Select(p => new Producto_dto
            {
                id_producto = p.id_producto,
                id_bar = p.id_bar,
                producto_especial = p.producto_especial,
                nombre_producto = p.nombre_producto,
                categoria = p.categoria,
                descripcion_producto = p.descripcion_producto,
                disponibilidad = p.disponibilidad,
                cantidad = p.cantidad,
                precio = p.precio,
                imagen_producto = p.imagen_producto,
                estado_producto = p.estado_producto
            });
            return producto.ToList();
        }

        // GET: api/producto/5
        [HttpGet]
        [ResponseType(typeof(producto))]
        public IHttpActionResult ConsultarProductoxId(int id)
        {
            Producto_dto producto_consultado = db.producto.Where(p => p.id_producto.Equals(id)).Select(p => new Producto_dto
            {
                id_producto = p.id_producto,
                id_bar = p.id_bar,
                producto_especial = p.producto_especial,
                nombre_producto = p.nombre_producto,
                categoria = p.categoria,
                descripcion_producto = p.descripcion_producto,
                disponibilidad = p.disponibilidad,
                cantidad = p.cantidad,
                precio = p.precio,
                imagen_producto = p.imagen_producto,
                estado_producto = p.estado_producto

            }).FirstOrDefault();
            if (producto_consultado == null)
            {
                return NotFound();
            }

            return Ok(producto_consultado);
        }

        // PUT: api/producto/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult ActualizarProducto(Producto_dto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            producto producto_actualizar = db.producto.Where(p => p.id_producto.Equals(producto.id_producto)).FirstOrDefault();
            producto validar_idbar = db.producto.Where(B => B.id_bar.Equals(producto.id_bar)).FirstOrDefault();

            if (producto_actualizar == null)
            {
                return BadRequest("No se encontró el producto solicitado");
            }
            if (validar_idbar == null)
            {
                return BadRequest("No se encontró el bar solicitado");
            }

            producto_actualizar.id_bar = producto.id_bar;
            producto_actualizar.producto_especial = producto.producto_especial;
            producto_actualizar.nombre_producto = producto.nombre_producto;
            producto_actualizar.categoria = producto.categoria;
            producto_actualizar.descripcion_producto = producto.descripcion_producto;
            producto_actualizar.disponibilidad = producto.disponibilidad;
            producto_actualizar.cantidad = producto.cantidad;
            producto_actualizar.precio = producto.precio;
            producto_actualizar.imagen_producto = producto.imagen_producto;
            producto_actualizar.estado_producto = true;


            db.Entry(producto_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoExists(producto.id_producto))
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

        // POST: api/producto
        [HttpPost]
        [ResponseType(typeof(producto))]
        public IHttpActionResult InsertarProducto(producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            producto producto_nuevo = new producto();
            producto_nuevo.id_bar = producto.id_bar;
            producto_nuevo.producto_especial = producto.producto_especial;
            producto_nuevo.nombre_producto = producto.nombre_producto;
            producto_nuevo.categoria = producto.categoria;
            producto_nuevo.descripcion_producto = producto.descripcion_producto;
            producto_nuevo.disponibilidad = producto.disponibilidad;
            producto_nuevo.cantidad = producto.cantidad;
            producto_nuevo.precio = producto.precio;
            producto_nuevo.imagen_producto = producto.imagen_producto;
            producto_nuevo.estado_producto = true;

            db.producto.Add(producto_nuevo);
            db.SaveChanges();

            return Ok("El producto se creo exitosamente");
        }


        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        [HttpPut]

        public IHttpActionResult ModificarEstadoProducto(Producto_dto producto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (producto.id_producto == null || producto.id_producto == 0)
            {
                return BadRequest();
            }


            producto Prodcuto_actualizar = db.producto.Where(p => p.id_producto.Equals(producto.id_producto)).FirstOrDefault();

            Prodcuto_actualizar.estado_producto = producto.estado_producto;

            db.Entry(Prodcuto_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productoExists(producto.id_producto))
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

        [ResponseType(typeof(void))]
        [HttpPut]

               
    protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productoExists(int id)
        {
            return db.producto.Count(e => e.id_producto == id) > 0;
        }
    }
}