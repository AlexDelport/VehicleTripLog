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
    public class VelogUsersAPIController : ApiController
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: api/VelogUsersAPI
        public IQueryable<tblvelogUser> GettblvelogUsers()
        {
            return db.tblvelogUsers;
        }

        // GET: api/VelogUsersAPI/5
        [ResponseType(typeof(tblvelogUser))]
        public IHttpActionResult GettblvelogUser(int id)
        {
            tblvelogUser tblvelogUser = db.tblvelogUsers.Find(id);
            if (tblvelogUser == null)
            {
                return NotFound();
            }

            return Ok(tblvelogUser);
        }

        // PUT: api/VelogUsersAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblvelogUser(int id, tblvelogUser tblvelogUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblvelogUser.Id)
            {
                return BadRequest();
            }

            db.Entry(tblvelogUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblvelogUserExists(id))
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

        // POST: api/VelogUsersAPI
        [ResponseType(typeof(tblvelogUser))]
        public IHttpActionResult PosttblvelogUser(tblvelogUser tblvelogUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblvelogUsers.Add(tblvelogUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblvelogUser.Id }, tblvelogUser);
        }

        // DELETE: api/VelogUsersAPI/5
        [ResponseType(typeof(tblvelogUser))]
        public IHttpActionResult DeletetblvelogUser(int id)
        {
            tblvelogUser tblvelogUser = db.tblvelogUsers.Find(id);
            if (tblvelogUser == null)
            {
                return NotFound();
            }

            db.tblvelogUsers.Remove(tblvelogUser);
            db.SaveChanges();

            return Ok(tblvelogUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblvelogUserExists(int id)
        {
            return db.tblvelogUsers.Count(e => e.Id == id) > 0;
        }
    }
}