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
    public class DetalleFacturasController : ApiController
    {
        private InstituteEntities db = new InstituteEntities();

        // GET: api/DetalleFacturas
        public IQueryable<tblDetalleFactura> GettblDetalleFacturas()
        {
            return db.tblDetalleFacturas;
        }

        // GET: api/DetalleFacturas/5
        [ResponseType(typeof(tblDetalleFactura))]
        public async Task<IHttpActionResult> GettblDetalleFactura(int id)
        {
            tblDetalleFactura tblDetalleFactura = await db.tblDetalleFacturas.FindAsync(id);
            if (tblDetalleFactura == null)
            {
                return NotFound();
            }

            return Ok(tblDetalleFactura);
        }

        // PUT: api/DetalleFacturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblDetalleFactura(int id, tblDetalleFactura tblDetalleFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDetalleFactura.IdDetalleFactura)
            {
                return BadRequest();
            }

            db.Entry(tblDetalleFactura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDetalleFacturaExists(id))
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

        // POST: api/DetalleFacturas
        [ResponseType(typeof(tblDetalleFactura))]
        public async Task<IHttpActionResult> PosttblDetalleFactura(tblDetalleFactura tblDetalleFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblDetalleFacturas.Add(tblDetalleFactura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblDetalleFactura.IdDetalleFactura }, tblDetalleFactura);
        }

        // DELETE: api/DetalleFacturas/5
        [ResponseType(typeof(tblDetalleFactura))]
        public async Task<IHttpActionResult> DeletetblDetalleFactura(int id)
        {
            tblDetalleFactura tblDetalleFactura = await db.tblDetalleFacturas.FindAsync(id);
            if (tblDetalleFactura == null)
            {
                return NotFound();
            }

            db.tblDetalleFacturas.Remove(tblDetalleFactura);
            await db.SaveChangesAsync();

            return Ok(tblDetalleFactura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblDetalleFacturaExists(int id)
        {
            return db.tblDetalleFacturas.Count(e => e.IdDetalleFactura == id) > 0;
        }
    }
}