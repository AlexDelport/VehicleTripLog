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
    public class VeLogDataAPIController : ApiController
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: api/VeLogDataAPI
        public IQueryable<VeLogData> GetVeLogDatas()
        {
            return db.VeLogDatas;
        }

        // GET: api/VeLogDataAPI/5
        [ResponseType(typeof(VeLogData))]
        public IHttpActionResult GetVeLogData(int id)
        {
            VeLogData veLogData = db.VeLogDatas.Find(id);
            if (veLogData == null)
            {
                return NotFound();
            }

            return Ok(veLogData);
        }

        // PUT: api/VeLogDataAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVeLogData(int id, VeLogData veLogData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veLogData.Id)
            {
                return BadRequest();
            }

            db.Entry(veLogData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeLogDataExists(id))
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

        // POST: api/VeLogDataAPI
        [ResponseType(typeof(VeLogData))]
        public IHttpActionResult PostVeLogData(VeLogData veLogData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VeLogDatas.Add(veLogData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = veLogData.Id }, veLogData);
        }

        // DELETE: api/VeLogDataAPI/5
        [ResponseType(typeof(VeLogData))]
        public IHttpActionResult DeleteVeLogData(int id)
        {
            VeLogData veLogData = db.VeLogDatas.Find(id);
            if (veLogData == null)
            {
                return NotFound();
            }

            db.VeLogDatas.Remove(veLogData);
            db.SaveChanges();

            return Ok(veLogData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VeLogDataExists(int id)
        {
            return db.VeLogDatas.Count(e => e.Id == id) > 0;
        }
    }
}