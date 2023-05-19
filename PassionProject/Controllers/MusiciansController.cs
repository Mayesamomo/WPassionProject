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
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class MusiciansController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Musicians
        public IQueryable<Musician> GetMusicians()
        {
            return db.Musicians;
        }

        // GET: api/Musicians/5
        [ResponseType(typeof(Musician))]
        public async Task<IHttpActionResult> GetMusician(int id)
        {
            Musician musician = await db.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            return Ok(musician);
        }

        // PUT: api/Musicians/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMusician(int id, Musician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != musician.Id)
            {
                return BadRequest();
            }

            db.Entry(musician).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicianExists(id))
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

        // POST: api/Musicians
        [ResponseType(typeof(Musician))]
        public async Task<IHttpActionResult> PostMusician(Musician musician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Musicians.Add(musician);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = musician.Id }, musician);
        }

        // DELETE: api/Musicians/5
        [ResponseType(typeof(Musician))]
        public async Task<IHttpActionResult> DeleteMusician(int id)
        {
            Musician musician = await db.Musicians.FindAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            db.Musicians.Remove(musician);
            await db.SaveChangesAsync();

            return Ok(musician);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MusicianExists(int id)
        {
            return db.Musicians.Count(e => e.Id == id) > 0;
        }
    }
}