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
    public class CamareroesController : ApiController
    {
        private InstituteEntities db = new InstituteEntities();

        // GET: api/Camareroes
        public IQueryable<tblCamarero> GettblCamareroes()
        {
            return db.tblCamareroes;
        }

        // GET: api/Camareroes/5
        [ResponseType(typeof(tblCamarero))]
        public async Task<IHttpActionResult> GettblCamarero(int id)
        {
            tblCamarero tblCamarero = await db.tblCamareroes.FindAsync(id);
            if (tblCamarero == null)
            {
                return NotFound();
            }

            return Ok(tblCamarero);
        }

        // PUT: api/Camareroes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCamarero(int id, tblCamarero tblCamarero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCamarero.IdCamarero)
            {
                return BadRequest();
            }

            db.Entry(tblCamarero).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCamareroExists(id))
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

        // POST: api/Camareroes
        [ResponseType(typeof(tblCamarero))]
        public async Task<IHttpActionResult> PosttblCamarero(tblCamarero tblCamarero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCamareroes.Add(tblCamarero);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCamarero.IdCamarero }, tblCamarero);
        }

        // DELETE: api/Camareroes/5
        [ResponseType(typeof(tblCamarero))]
        public async Task<IHttpActionResult> DeletetblCamarero(int id)
        {
            tblCamarero tblCamarero = await db.tblCamareroes.FindAsync(id);
            if (tblCamarero == null)
            {
                return NotFound();
            }

            db.tblCamareroes.Remove(tblCamarero);
            await db.SaveChangesAsync();

            return Ok(tblCamarero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCamareroExists(int id)
        {
            return db.tblCamareroes.Count(e => e.IdCamarero == id) > 0;
        }
    }
}