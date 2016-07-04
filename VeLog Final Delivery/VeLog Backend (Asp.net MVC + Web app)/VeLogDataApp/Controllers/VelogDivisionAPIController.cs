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
    public class VelogDivisionAPIController : ApiController
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: api/VelogDivisionAPI
        public IQueryable<tblvelogDivision> GettblvelogDivisions()
        {
            return db.tblvelogDivisions;
        }

        // GET: api/VelogDivisionAPI/5
        [ResponseType(typeof(tblvelogDivision))]
        public IHttpActionResult GettblvelogDivision(int id)
        {
            tblvelogDivision tblvelogDivision = db.tblvelogDivisions.Find(id);
            if (tblvelogDivision == null)
            {
                return NotFound();
            }

            return Ok(tblvelogDivision);
        }

        // PUT: api/VelogDivisionAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblvelogDivision(int id, tblvelogDivision tblvelogDivision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblvelogDivision.Id)
            {
                return BadRequest();
            }

            db.Entry(tblvelogDivision).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblvelogDivisionExists(id))
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

        // POST: api/VelogDivisionAPI
        [ResponseType(typeof(tblvelogDivision))]
        public IHttpActionResult PosttblvelogDivision(tblvelogDivision tblvelogDivision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblvelogDivisions.Add(tblvelogDivision);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblvelogDivision.Id }, tblvelogDivision);
        }

        // DELETE: api/VelogDivisionAPI/5
        [ResponseType(typeof(tblvelogDivision))]
        public IHttpActionResult DeletetblvelogDivision(int id)
        {
            tblvelogDivision tblvelogDivision = db.tblvelogDivisions.Find(id);
            if (tblvelogDivision == null)
            {
                return NotFound();
            }

            db.tblvelogDivisions.Remove(tblvelogDivision);
            db.SaveChanges();

            return Ok(tblvelogDivision);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblvelogDivisionExists(int id)
        {
            return db.tblvelogDivisions.Count(e => e.Id == id) > 0;
        }
    }
}