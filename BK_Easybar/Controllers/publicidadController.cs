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
    public class publicidadController : ApiController
    {
        private db_easybarEntities db = new db_easybarEntities();

        // GET: api/publicidad

        [HttpGet]
        public List<Publicidad_dto> ConsultarPublicidad()
        {
            var publicidad = db.publicidad.Select(p => new Publicidad_dto
            {
                id_bar = p.id_bar,
                nombre_publicidad = p.nombre_publicidad,
                tiempo_publicidad = p.tiempo_publicidad,
                Mensaje = p.Mensaje,
                valor = p.valor,
                Imagen_publicidad = p.Imagen_publicidad,
                Estado_publicidad = p.Estado_publicidad
            });
            return publicidad.ToList();
        }

        // GET: api/publicidad/5
        [HttpGet]
        [ResponseType(typeof(publicidad))]
        public IHttpActionResult ConsultarPublicidadxId(int id)
        {
            Publicidad_dto publicidad_consultada = db.publicidad.Where(p => p.id_publicidad.Equals(id)).Select(p => new Publicidad_dto
            {
                id_bar = p.id_bar,
                nombre_publicidad = p.nombre_publicidad,
                tiempo_publicidad = p.tiempo_publicidad,
                Mensaje = p.Mensaje,
                valor = p.valor,
                Imagen_publicidad = p.Imagen_publicidad,
                Estado_publicidad = p.Estado_publicidad

            }).FirstOrDefault();
            if (publicidad_consultada == null)
            {
                return NotFound();
            }
            return Ok(publicidad_consultada);
}

        // PUT: api/publicidad/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult ActualizarPublicidad(Publicidad_dto publicidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (publicidad.id_publicidad == null || publicidad.id_publicidad == 0)
            {
                return BadRequest();
            }

            publicidad publicidad_Actualizar = db.publicidad.Where(p => p.id_publicidad.Equals(publicidad.id_publicidad)).FirstOrDefault();

            publicidad_Actualizar.id_bar = publicidad.id_bar;
            publicidad_Actualizar.nombre_publicidad = publicidad.nombre_publicidad;
            publicidad_Actualizar.tiempo_publicidad = publicidad.tiempo_publicidad;
            publicidad_Actualizar.valor = publicidad.valor;
            publicidad_Actualizar.Mensaje = publicidad.Mensaje;
            publicidad_Actualizar.Imagen_publicidad = publicidad.Imagen_publicidad;
            publicidad_Actualizar.Estado_publicidad = publicidad.Estado_publicidad;


            db.Entry(publicidad_Actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!publicidadExists(publicidad.id_publicidad))
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

        // POST: api/publicidad
        [HttpPost]
        [ResponseType(typeof(publicidad))]
        public IHttpActionResult Insertarpublicidad(Publicidad_dto publicidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            publicidad publicidad_nueva = new publicidad();
            
            publicidad_nueva.id_bar = publicidad.id_bar;
            publicidad_nueva.nombre_publicidad = publicidad.nombre_publicidad;
            publicidad_nueva.tiempo_publicidad = publicidad.tiempo_publicidad;
            publicidad_nueva.valor = publicidad.valor; 
            publicidad_nueva.Mensaje = publicidad.Mensaje;
            publicidad_nueva.Imagen_publicidad = publicidad.Imagen_publicidad;
            publicidad_nueva.Estado_publicidad = publicidad.Estado_publicidad;


            db.publicidad.Add(publicidad_nueva);
            db.SaveChanges();

            return Ok("Publicidad creada exitosamente");
        }

        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        [HttpPut]

        public IHttpActionResult ModificarEstadoPublicidad(Publicidad_dto publicidad)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (publicidad.id_publicidad == null || publicidad.id_publicidad == 0)
            {
                return BadRequest();
            }


            publicidad publicidad_actualizar = db.publicidad.Where(c => c.id_publicidad.Equals(publicidad.id_publicidad)).FirstOrDefault();

            publicidad_actualizar.Estado_publicidad = publicidad.Estado_publicidad;

            db.Entry(publicidad_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!publicidadExists(publicidad.id_publicidad))
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

        private bool publicidadExists(int id)
        {
            return db.publicidad.Count(e => e.id_publicidad == id) > 0;
        }
    }
}