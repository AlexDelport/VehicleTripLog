using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeLogDataApp.Models;

namespace VeLogDataApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private VelogDataContext db = new VelogDataContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.tblvelogUsers.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblvelogUser tblvelogUser = db.tblvelogUsers.Find(id);
            if (tblvelogUser == null)
            {
                return HttpNotFound();
            }
            return View(tblvelogUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Role")] tblvelogUser tblvelogUser)
        {
            if (ModelState.IsValid)
            {
                db.tblvelogUsers.Add(tblvelogUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblvelogUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblvelogUser tblvelogUser = db.tblvelogUsers.Find(id);
            if (tblvelogUser == null)
            {
                return HttpNotFound();
            }
            return View(tblvelogUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Role")] tblvelogUser tblvelogUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblvelogUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblvelogUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblvelogUser tblvelogUser = db.tblvelogUsers.Find(id);
            if (tblvelogUser == null)
            {
                return HttpNotFound();
            }
            return View(tblvelogUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblvelogUser tblvelogUser = db.tblvelogUsers.Find(id);
            db.tblvelogUsers.Remove(tblvelogUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
