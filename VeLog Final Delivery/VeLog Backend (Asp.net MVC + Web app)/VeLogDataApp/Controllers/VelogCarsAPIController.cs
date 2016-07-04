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
using VeLogDataApp.Models;

namespace VeLogDataApp.Controllers
{
    public class VelogCarsAPIController : ApiController
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: api/VelogCarsAPI
        public IQueryable<tblVelogCar> GettblVelogCars()
        {
            return db.tblVelogCars;
        }

        // GET: api/VelogCarsAPI/5
        [ResponseType(typeof(tblVelogCar))]
        public IHttpActionResult GettblVelogCar(int id)
        {
            tblVelogCar tblVelogCar = db.tblVelogCars.Find(id);
            if (tblVelogCar == null)
            {
                return NotFound();
            }

            return Ok(tblVelogCar);
        }

        // PUT: api/VelogCarsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblVelogCar(int id, tblVelogCar tblVelogCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblVelogCar.Id)
            {
                return BadRequest();
            }

            db.Entry(tblVelogCar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblVelogCarExists(id))
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

        // POST: api/VelogCarsAPI
        [ResponseType(typeof(tblVelogCar))]
        public IHttpActionResult PosttblVelogCar(tblVelogCar tblVelogCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblVelogCars.Add(tblVelogCar);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblVelogCar.Id }, tblVelogCar);
        }

        // DELETE: api/VelogCarsAPI/5
        [ResponseType(typeof(tblVelogCar))]
        public IHttpActionResult DeletetblVelogCar(int id)
        {
            tblVelogCar tblVelogCar = db.tblVelogCars.Find(id);
            if (tblVelogCar == null)
            {
                return NotFound();
            }

            db.tblVelogCars.Remove(tblVelogCar);
            db.SaveChanges();

            return Ok(tblVelogCar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblVelogCarExists(int id)
        {
            return db.tblVelogCars.Count(e => e.Id == id) > 0;
        }
    }
}