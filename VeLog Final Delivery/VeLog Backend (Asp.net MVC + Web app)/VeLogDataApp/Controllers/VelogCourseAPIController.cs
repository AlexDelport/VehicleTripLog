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
    public class VelogCourseAPIController : ApiController
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: api/VelogCourseAPI
        public IQueryable<tblvelogCours> GettblvelogCourses()
        {
            return db.tblvelogCourses;
        }

        // GET: api/VelogCourseAPI/5
        [ResponseType(typeof(tblvelogCours))]
        public IHttpActionResult GettblvelogCours(int id)
        {
            tblvelogCours tblvelogCours = db.tblvelogCourses.Find(id);
            if (tblvelogCours == null)
            {
                return NotFound();
            }

            return Ok(tblvelogCours);
        }

        // PUT: api/VelogCourseAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblvelogCours(int id, tblvelogCours tblvelogCours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblvelogCours.Id)
            {
                return BadRequest();
            }

            db.Entry(tblvelogCours).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblvelogCoursExists(id))
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

        // POST: api/VelogCourseAPI
        [ResponseType(typeof(tblvelogCours))]
        public IHttpActionResult PosttblvelogCours(tblvelogCours tblvelogCours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblvelogCourses.Add(tblvelogCours);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblvelogCours.Id }, tblvelogCours);
        }

        // DELETE: api/VelogCourseAPI/5
        [ResponseType(typeof(tblvelogCours))]
        public IHttpActionResult DeletetblvelogCours(int id)
        {
            tblvelogCours tblvelogCours = db.tblvelogCourses.Find(id);
            if (tblvelogCours == null)
            {
                return NotFound();
            }

            db.tblvelogCourses.Remove(tblvelogCours);
            db.SaveChanges();

            return Ok(tblvelogCours);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblvelogCoursExists(int id)
        {
            return db.tblvelogCourses.Count(e => e.Id == id) > 0;
        }
    }
}