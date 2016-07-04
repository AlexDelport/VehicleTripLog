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
    public class VelogCampusAPIController : ApiController
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: api/VelogCampusAPI
        public IQueryable<tblvelogCampu> GettblvelogCampus()
        {
            return db.tblvelogCampus;
        }

        // GET: api/VelogCampusAPI/5
        [ResponseType(typeof(tblvelogCampu))]
        public IHttpActionResult GettblvelogCampu(int id)
        {
            tblvelogCampu tblvelogCampu = db.tblvelogCampus.Find(id);
            if (tblvelogCampu == null)
            {
                return NotFound();
            }

            return Ok(tblvelogCampu);
        }

        // PUT: api/VelogCampusAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblvelogCampu(int id, tblvelogCampu tblvelogCampu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblvelogCampu.Id)
            {
                return BadRequest();
            }

            db.Entry(tblvelogCampu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblvelogCampuExists(id))
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

        // POST: api/VelogCampusAPI
        [ResponseType(typeof(tblvelogCampu))]
        public IHttpActionResult PosttblvelogCampu(tblvelogCampu tblvelogCampu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblvelogCampus.Add(tblvelogCampu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblvelogCampu.Id }, tblvelogCampu);
        }

        // DELETE: api/VelogCampusAPI/5
        [ResponseType(typeof(tblvelogCampu))]
        public IHttpActionResult DeletetblvelogCampu(int id)
        {
            tblvelogCampu tblvelogCampu = db.tblvelogCampus.Find(id);
            if (tblvelogCampu == null)
            {
                return NotFound();
            }

            db.tblvelogCampus.Remove(tblvelogCampu);
            db.SaveChanges();

            return Ok(tblvelogCampu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblvelogCampuExists(int id)
        {
            return db.tblvelogCampus.Count(e => e.Id == id) > 0;
        }
    }
}