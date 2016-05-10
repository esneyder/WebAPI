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
    public class MesasController : ApiController
    {
        private InstituteEntities db = new InstituteEntities();

        // GET: api/Mesas
        public IQueryable<tblMesa> GettblMesas()
        {
            return db.tblMesas;
        }

        // GET: api/Mesas/5
        [ResponseType(typeof(tblMesa))]
        public async Task<IHttpActionResult> GettblMesa(int id)
        {
            tblMesa tblMesa = await db.tblMesas.FindAsync(id);
            if (tblMesa == null)
            {
                return NotFound();
            }

            return Ok(tblMesa);
        }

        // PUT: api/Mesas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblMesa(int id, tblMesa tblMesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblMesa.IdMesa)
            {
                return BadRequest();
            }

            db.Entry(tblMesa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblMesaExists(id))
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

        // POST: api/Mesas
        [ResponseType(typeof(tblMesa))]
        public async Task<IHttpActionResult> PosttblMesa(tblMesa tblMesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblMesas.Add(tblMesa);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblMesa.IdMesa }, tblMesa);
        }

        // DELETE: api/Mesas/5
        [ResponseType(typeof(tblMesa))]
        public async Task<IHttpActionResult> DeletetblMesa(int id)
        {
            tblMesa tblMesa = await db.tblMesas.FindAsync(id);
            if (tblMesa == null)
            {
                return NotFound();
            }

            db.tblMesas.Remove(tblMesa);
            await db.SaveChangesAsync();

            return Ok(tblMesa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblMesaExists(int id)
        {
            return db.tblMesas.Count(e => e.IdMesa == id) > 0;
        }
    }
}