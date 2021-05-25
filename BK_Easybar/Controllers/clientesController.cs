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
    public class clientesController : ApiController
    {
        private db_easybarEntities db = new db_easybarEntities();

        // GET: api/clientes
       [HttpGet]
        public  List<cliente_dto> consultarTodos()
        {
            
            var clientes = db.cliente.Select(c => new cliente_dto
            {
                id_cliente = c.id_cliente,
                cedula = c.cedula,
                correo = c.correo,
                Barrio_localidad = c.Barrio_localidad,
                direccion = c.direccion,
                nombre_completo = c.nombre_completo,
                Telefono = c.Telefono

            }); ;

            return clientes.ToList();
        }

        // GET: api/clientes/5
        [ResponseType(typeof(cliente))]
        [HttpGet]
        public IHttpActionResult ConsultarId(int id)
        {
         
            cliente_dto cliente_consultado = db.cliente.Where(c => c.id_cliente.Equals(id)).Select(c => new cliente_dto
            {
                id_cliente = c.id_cliente,
                cedula = c.cedula,
                correo = c.correo,
                Barrio_localidad = c.Barrio_localidad,
                direccion = c.direccion,
                nombre_completo = c.nombre_completo,
                Telefono = c.Telefono

            }).FirstOrDefault();
            if (cliente_consultado == null)
            {
                return NotFound();
            }

            return Ok(cliente_consultado);
        }

        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult ActualizarCliente(cliente_dto cliente)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cliente.id_cliente == null || cliente.id_cliente==0)
            {
                return BadRequest();
            }


            cliente Cliente_actualizar = db.cliente.Where(c => c.id_cliente.Equals(cliente.id_cliente)).FirstOrDefault();

            Cliente_actualizar.id_rol = cliente.id_rol;
            Cliente_actualizar.cedula = cliente.cedula;
            Cliente_actualizar.nombre_completo = cliente.nombre_completo;
            Cliente_actualizar.correo = cliente.correo;
            Cliente_actualizar.direccion = cliente.direccion;
            Cliente_actualizar.Barrio_localidad = cliente.Barrio_localidad;
            Cliente_actualizar.Telefono = cliente.Telefono;
            Cliente_actualizar.contraseña = cliente.contraseña;
            Cliente_actualizar.fecha_adicion = DateTime.Now;
            Cliente_actualizar.estado_cliente = true;

            db.Entry(Cliente_actualizar).State = EntityState.Modified;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(cliente.id_cliente))
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

        // POST: api/clientes
        [ResponseType(typeof(cliente))]
        [HttpPost]
        public IHttpActionResult InsertarCliente(cliente_dto cliente)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cliente cliente_nuevo = new cliente();
            cliente_nuevo.id_rol = cliente.id_rol;
            cliente_nuevo.cedula = cliente.cedula;
            cliente_nuevo.nombre_completo = cliente.nombre_completo;
            cliente_nuevo.correo = cliente.correo;
            cliente_nuevo.direccion = cliente.direccion;
            cliente_nuevo.Barrio_localidad = cliente.Barrio_localidad;
            cliente_nuevo.Telefono = cliente.Telefono;
            cliente_nuevo.contraseña = cliente.contraseña;
            cliente_nuevo.fecha_adicion = DateTime.Now;
            cliente_nuevo.estado_cliente = true;



            db.cliente.Add(cliente_nuevo);
            db.SaveChanges();
            return Ok("El usuario se creo exitosamente");           
        }

       

        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        [HttpPut]

        public IHttpActionResult ModificarEstadoCliente(cliente_dto cliente)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cliente.id_cliente == null || cliente.id_cliente == 0)
            {
                return BadRequest();
            }


            cliente Cliente_actualizar = db.cliente.Where(c => c.id_cliente.Equals(cliente.id_cliente)).FirstOrDefault();

            Cliente_actualizar.estado_cliente = cliente.estado_cliente;

            db.Entry(Cliente_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(cliente.id_cliente))
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(int id)
        {
            return db.cliente.Count(e => e.id_cliente == id) > 0;
        }
    }
}