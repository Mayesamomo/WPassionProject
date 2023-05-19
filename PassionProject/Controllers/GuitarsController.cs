using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class GuitarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
     
        // GET: api/Guitar/ListGuitar
        [HttpGet]
        public IEnumerable<GuitarDTO> ListGuitars()
        {
            List<Guitar> guitars = db.Guitars.ToList();
            List<GuitarDTO> guitarDto = new List<GuitarDTO>();

            foreach (Guitar guitar in guitars)
            {
                GuitarDTO dto = new GuitarDTO
                {
                    Id = guitar.Id,
                    BrandName = guitar.BrandName,
                    NumberOfStrings = guitar.NumberOfStrings,
                    CategoryId = guitar.CategoryId,
                    MusicianId = guitar.MusicianId,
                    Color = guitar.Color
                };

                guitarDto.Add(dto);
            }

            return guitarDto;
        }

        // GET: api/Guitar/FindGuitar/1
        [ResponseType(typeof(Guitar))]
        [HttpGet]
        public IHttpActionResult FindGuitar(int id)
        {
            Guitar guitar = db.Guitars.Find(id);
            if (guitar == null)
            {
                return NotFound();
            }

            GuitarDTO guitarDto = new GuitarDTO()
            {
                Id = guitar.Id,
                BrandName = guitar.BrandName,
                NumberOfStrings = guitar.NumberOfStrings,
                CategoryId = guitar.CategoryId,
                MusicianId = guitar.MusicianId,
                Color = guitar.Color
            };

            return Ok(guitarDto);
        }

        // POST: api/GuitarData/AddGuitar
        [ResponseType(typeof(Guitar))]
        [HttpPost]
        public IHttpActionResult AddGuitar(Guitar guitar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = new ApplicationDbContext())
            {
                db.Guitars.Add(guitar);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = guitar.Id }, guitar);
        }




        // POST: api/Guitar/UpdateGuitar/
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateGuitar(int id, Guitar guitar)
        {
            Debug.WriteLine("I have reached the update guitar method!");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != guitar.Id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter: " + id);
                Debug.WriteLine("POST parameter: " + guitar.Id);
                Debug.WriteLine("POST parameter: " + guitar.BrandName);
                Debug.WriteLine("POST parameter: " + guitar.NumberOfStrings);
                Debug.WriteLine("POST parameter: " + guitar.CategoryId);
                Debug.WriteLine("POST parameter: " + guitar.MusicianId);
                Debug.WriteLine("POST parameter: " + guitar.Color);
                return BadRequest();
            }

            using (var db = new ApplicationDbContext())
            {
                db.Entry(guitar).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuitarExists(id))
                    {
                        Debug.WriteLine("Guitar not found");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        

        // POST: api/GuitarData/DeleteGuitar/5
        [ResponseType(typeof(Guitar))]
        [HttpPost]
        public IHttpActionResult DeleteGuitar(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var guitar = db.Guitars.Find(id);
                if (guitar == null)
                {
                    return NotFound();
                }

                db.Guitars.Remove(guitar);
                db.SaveChanges();

                return Ok();
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuitarExists(int id)
        {
            return db.Guitars.Any(e => e.Id == id);
        }

    }
}
