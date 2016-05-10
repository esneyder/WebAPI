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
    public class CocineroesController : ApiController
    {
        private InstituteEntities db = new InstituteEntities();

        // GET: api/Cocineroes
        public IQueryable<tblCocinero> GettblCocineroes()
        {
            return db.tblCocineroes;
        }

        // GET: api/Cocineroes/5
        [ResponseType(typeof(tblCocinero))]
        public async Task<IHttpActionResult> GettblCocinero(int id)
        {
            tblCocinero tblCocinero = await db.tblCocineroes.FindAsync(id);
            if (tblCocinero == null)
            {
                return NotFound();
            }

            return Ok(tblCocinero);
        }

        // PUT: api/Cocineroes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCocinero(int id, tblCocinero tblCocinero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCocinero.IdCocinero)
            {
                return BadRequest();
            }

            db.Entry(tblCocinero).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCocineroExists(id))
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

        // POST: api/Cocineroes
        [ResponseType(typeof(tblCocinero))]
        public async Task<IHttpActionResult> PosttblCocinero(tblCocinero tblCocinero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCocineroes.Add(tblCocinero);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCocinero.IdCocinero }, tblCocinero);
        }

        // DELETE: api/Cocineroes/5
        [ResponseType(typeof(tblCocinero))]
        public async Task<IHttpActionResult> DeletetblCocinero(int id)
        {
            tblCocinero tblCocinero = await db.tblCocineroes.FindAsync(id);
            if (tblCocinero == null)
            {
                return NotFound();
            }

            db.tblCocineroes.Remove(tblCocinero);
            await db.SaveChangesAsync();

            return Ok(tblCocinero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCocineroExists(int id)
        {
            return db.tblCocineroes.Count(e => e.IdCocinero == id) > 0;
        }
    }
}