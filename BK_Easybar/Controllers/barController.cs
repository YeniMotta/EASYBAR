using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BK_Easybar.DTO;
using BK_Easybar.Models;

namespace BK_Easybar.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class barController : ApiController
    {
        private db_easybarEntities db = new db_easybarEntities();

        // GET: api/bar
        [HttpGet]
        public List<Bar_dto> consultarBares()
        {
            var bar = db.bar.Select(b => new Bar_dto
            {
                id_bar =b.id_bar,
                id_admin = b.id_admin,
                nombre_bar = b.nombre_bar,
                nit_bar = b.nit_bar,
                Direccion_bar = b.Direccion_bar,
                Telefono_bar1 = b.Telefono_bar1,
                Telefono_bar2 = b.Telefono_bar2,
                Logo_bar = b.Logo_bar,
                link_washap = b.link_washap,
                link_facebook = b.link_facebook,
                link_instagram = b.link_instagram,
                correo_b = b.correo_b,
                estado_bar = b.estado_bar,
                Horario = b.Horario
            });
            return bar.ToList();
        }

        // GET: api/bar/5
        [HttpGet]
        [ResponseType(typeof(bar))]
        public IHttpActionResult ConsultaxBar(int id)
        {
            Bar_dto bar_consultado = db.bar.Where(b => b.id_bar.Equals(id)).Select(b => new Bar_dto
            {
                id_bar = b.id_bar,
                id_admin = b.id_admin,
                nombre_bar = b.nombre_bar,
                nit_bar = b.nit_bar,
                Direccion_bar = b.Direccion_bar,
                Telefono_bar1 = b.Telefono_bar1,
                Telefono_bar2 = b.Telefono_bar2,
                Logo_bar = b.Logo_bar,
                link_washap = b.link_washap,
                link_facebook = b.link_facebook,
                link_instagram = b.link_instagram,
                correo_b = b.correo_b,
                estado_bar = b.estado_bar,
                Horario = b.Horario

            }).FirstOrDefault();

            if (bar_consultado == null)
            {
                return NotFound();
            }

            return Ok(bar_consultado);
        }

        // PUT: api/bar/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult ActualizarBar(Bar_dto bar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bar.id_bar == null || bar.id_bar == 0)
            {
                return Ok("Bar NO EXISTENTE"); ;
            }
           


            bar bar_actualizar = db.bar.Where(b => b.id_bar.Equals(bar.id_bar)).FirstOrDefault();
            bar_actualizar.id_admin = bar.id_admin;
            bar_actualizar.nombre_bar = bar.nombre_bar;
            bar_actualizar.nit_bar = bar.nit_bar;
            bar_actualizar.Direccion_bar = bar.Direccion_bar;
            bar_actualizar.Telefono_bar1 = bar.Telefono_bar1;
            bar_actualizar.Telefono_bar2 = bar.Telefono_bar2;
            bar_actualizar.Logo_bar = bar.Logo_bar;
            bar_actualizar.link_washap = bar.link_washap;
            bar_actualizar.link_facebook = bar.link_facebook;
            bar_actualizar.link_instagram = bar.link_instagram;
            bar_actualizar.correo_b = bar.correo_b;
            bar_actualizar.estado_bar = true;
            bar_actualizar.Horario = bar.Horario;
            bar_actualizar.fecha_adicion = DateTime.Now;

            db.Entry(bar_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!barExists(bar.id_bar))
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


        // POST: api/bar
        [HttpPost]
        [ResponseType(typeof(bar))]
        public IHttpActionResult InsertarBar(Bar_dto bar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bar bar_nuevo = new bar();
            bar_nuevo.id_admin = bar.id_admin;
            bar_nuevo.nombre_bar = bar.nombre_bar;
            bar_nuevo.nit_bar = bar.nit_bar;
            bar_nuevo.Direccion_bar = bar.Direccion_bar;
            bar_nuevo.Telefono_bar1 = bar.Telefono_bar1;
            bar_nuevo.Telefono_bar2 = bar.Telefono_bar2;
            bar_nuevo.Logo_bar = bar.Logo_bar;
            bar_nuevo.link_washap = bar.link_washap;
            bar_nuevo.link_facebook = bar.link_facebook;
            bar_nuevo.link_instagram = bar.link_instagram;
            bar_nuevo.correo_b = bar.correo_b;
            bar_nuevo.estado_bar = false;
            bar_nuevo.Horario = bar.Horario;
            bar_nuevo.fecha_adicion = DateTime.Now;


            db.bar.Add(bar_nuevo);
            db.SaveChanges();
            return Ok("El Bar se creo exitosamente");
        }
        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        [HttpPut]

        public IHttpActionResult ModificarEstadoBar(Bar_dto bar)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bar.id_bar == null || bar.id_bar == 0)
            {
                return BadRequest();
            }
            

            bar bar_actualizar = db.bar.Where(b => b.id_bar.Equals(bar.id_bar)).FirstOrDefault();

            if (bar_actualizar.id_bar == null) { return BadRequest(); }

            bar_actualizar.estado_bar = bar.estado_bar;

            db.Entry(bar_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!barExists(bar.id_bar))
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

        private bool barExists(int id)
        {
            return db.bar.Count(e => e.id_bar == id) > 0;
        }

    }
}