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
    public class adminController : ApiController
    {
        private db_easybarEntities db = new db_easybarEntities();

        // GET: api/admin
        [HttpGet]
        public List<Admin_dto> ConsultarTodosAdmin()
        {
            var admin = db.administrador_bar.Select(a => new Admin_dto
            {
                id_admin = a.id_admin,
                cedula_a = a.cedula_a,
                nombre_completo_a = a.nombre_completo_a,
                correo_a = a.correo_a,
                Telefono_a =a.Telefono_a
            });
            return admin.ToList();
        }

        // GET: api/admin/5
        [HttpGet]
        [ResponseType(typeof(administrador_bar))]
        public IHttpActionResult ConsultarxIdAdmin(int id)
        {
            Admin_dto administrador_consultado = db.administrador_bar.Where(a => a.id_admin.Equals(id)).Select(a => new Admin_dto
            {
                id_admin = a.id_admin,
                cedula_a = a.cedula_a,
                nombre_completo_a = a.nombre_completo_a,
                correo_a = a.correo_a,
                Telefono_a = a.Telefono_a
            }).FirstOrDefault();         
            if (administrador_consultado == null)
            {
                return NotFound();
            }

            return Ok(administrador_consultado);
        }

        // PUT: api/admin/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult ActualizarAdmin(Admin_dto administrador_bar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (administrador_bar.id_admin == null || administrador_bar.id_admin == 0)
            {
                return BadRequest();
            }

            administrador_bar Admin_Actualizar = db.administrador_bar.Where(a => a.id_admin.Equals(administrador_bar.id_admin)).FirstOrDefault();

            Admin_Actualizar.id_rol = administrador_bar.id_rol;
            Admin_Actualizar.cedula_a = administrador_bar.cedula_a;
            Admin_Actualizar.nombre_completo_a = administrador_bar.nombre_completo_a;
            Admin_Actualizar.correo_a = administrador_bar.correo_a;
            Admin_Actualizar.Telefono_a = administrador_bar.Telefono_a;
            Admin_Actualizar.fecha_adicion_a = administrador_bar.fecha_adicion_a;
            Admin_Actualizar.estado_admin = true;


            db.Entry(Admin_Actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!administrador_barExists(administrador_bar.id_admin))
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

        // POST: api/admin
        [HttpPost]
        [ResponseType(typeof(administrador_bar))]
        public IHttpActionResult InsertarAdmin(Admin_dto administrador_bar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            administrador_bar administrador_nuevo = new administrador_bar();

            administrador_nuevo.id_rol = 2;
            administrador_nuevo.cedula_a = administrador_bar.cedula_a;
            administrador_nuevo.nombre_completo_a = administrador_bar.nombre_completo_a;
            administrador_nuevo.correo_a = administrador_bar.correo_a;
            administrador_nuevo.Telefono_a = administrador_bar.Telefono_a;
            administrador_nuevo.contraseña_a = administrador_bar.contraseña_a;
            administrador_nuevo.fecha_adicion_a = DateTime.Now;
            administrador_nuevo.estado_admin = false;

            db.administrador_bar.Add(administrador_nuevo);
            db.SaveChanges();
            return Ok("Administrador creado existosamente");
        }

        [HttpPut]

        public IHttpActionResult ModificarEstadoAdmin(Admin_dto administrador_bar)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (administrador_bar.id_admin == null || administrador_bar.id_admin == 0)
            {
                return BadRequest();
            }


            administrador_bar Admin_actualizar = db.administrador_bar.Where(a =>a.id_admin.Equals(administrador_bar.id_admin)).FirstOrDefault();

            Admin_actualizar.estado_admin  = administrador_bar.estado_admin;
            ;

            db.Entry(Admin_actualizar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!administrador_barExists(administrador_bar.id_admin))
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

        private bool administrador_barExists(int id)
        {
            return db.administrador_bar.Count(e => e.id_admin == id) > 0;
        }
    }
}