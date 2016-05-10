using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    public class ClientesController : ApiController
    {
        private InstituteEntities db = new InstituteEntities();

        // GET: api/Clientes
        public IQueryable<tblCliente> GettblClientes()
        {
            return db.tblClientes;
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(tblCliente))]
        public async Task<IHttpActionResult> GettblCliente(int id)
        {
            tblCliente tblCliente = await db.tblClientes.FindAsync(id);
            if (tblCliente == null)
            {
                return NotFound();
            }

            return Ok(tblCliente);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCliente(int id, tblCliente tblCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCliente.IdCliente)
            {
                return BadRequest();
            }

            db.Entry(tblCliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clientes
        [ResponseType(typeof(tblCliente))]
        public async Task<IHttpActionResult> PosttblCliente(tblCliente tblCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblClientes.Add(tblCliente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCliente.IdCliente }, tblCliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(tblCliente))]
        public async Task<IHttpActionResult> DeletetblCliente(int id)
        {
            tblCliente tblCliente = await db.tblClientes.FindAsync(id);
            if (tblCliente == null)
            {
                return NotFound();
            }

            db.tblClientes.Remove(tblCliente);
            await db.SaveChangesAsync();

            return Ok(tblCliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblClienteExists(int id)
        {
            return db.tblClientes.Count(e => e.IdCliente == id) > 0;
        }
    }
}