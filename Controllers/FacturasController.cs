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
    public class FacturasController : ApiController
    {
        private InstituteEntities db = new InstituteEntities();

        // GET: api/Facturas
        public IQueryable<tblFactura> GettblFacturas()
        {
            return db.tblFacturas;
        }

        // GET: api/Facturas/5
        [ResponseType(typeof(tblFactura))]
        public async Task<IHttpActionResult> GettblFactura(int id)
        {
            tblFactura tblFactura = await db.tblFacturas.FindAsync(id);
            if (tblFactura == null)
            {
                return NotFound();
            }

            return Ok(tblFactura);
        }

        // PUT: api/Facturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblFactura(int id, tblFactura tblFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblFactura.IdFactura)
            {
                return BadRequest();
            }

            db.Entry(tblFactura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblFacturaExists(id))
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

        // POST: api/Facturas
        [ResponseType(typeof(tblFactura))]
        public async Task<IHttpActionResult> PosttblFactura(tblFactura tblFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblFacturas.Add(tblFactura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblFactura.IdFactura }, tblFactura);
        }

        // DELETE: api/Facturas/5
        [ResponseType(typeof(tblFactura))]
        public async Task<IHttpActionResult> DeletetblFactura(int id)
        {
            tblFactura tblFactura = await db.tblFacturas.FindAsync(id);
            if (tblFactura == null)
            {
                return NotFound();
            }

            db.tblFacturas.Remove(tblFactura);
            await db.SaveChangesAsync();

            return Ok(tblFactura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblFacturaExists(int id)
        {
            return db.tblFacturas.Count(e => e.IdFactura == id) > 0;
        }
    }
}