using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProiectDAWcod.Data;

namespace ProiectDAWcod.Controllers
{
    public class utilizatori_exercitiiController : Controller
    {
        private ProiectDAWEntities db = new ProiectDAWEntities();

        // GET: utilizatori_exercitii
        public ActionResult Index()
        {
            var utilizatori_exercitii = db.utilizatori_exercitii.Include(u => u.exercitii).Include(u => u.utilizatori);
            return View(utilizatori_exercitii.ToList());
        }

        // GET: utilizatori_exercitii/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilizatori_exercitii utilizatori_exercitii = db.utilizatori_exercitii.Find(id);
            if (utilizatori_exercitii == null)
            {
                return HttpNotFound();
            }
            return View(utilizatori_exercitii);
        }

        // GET: utilizatori_exercitii/Create
        public ActionResult Create()
        {
            ViewBag.id_exercitiu = new SelectList(db.exercitiis, "id_exercitiu", "nume_exercitiu");
            ViewBag.id_utilizator = new SelectList(db.utilizatoris, "id_utilizator", "user_name");
            return View();
        }

        // POST: utilizatori_exercitii/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_utilizatori_exercitii,id_utilizator,id_exercitiu")] utilizatori_exercitii utilizatori_exercitii)
        {
            if (ModelState.IsValid)
            {
                utilizatori_exercitii.id_utilizatori_exercitii = Guid.NewGuid();
                db.utilizatori_exercitii.Add(utilizatori_exercitii);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_exercitiu = new SelectList(db.exercitiis, "id_exercitiu", "nume_exercitiu", utilizatori_exercitii.id_exercitiu);
            ViewBag.id_utilizator = new SelectList(db.utilizatoris, "id_utilizator", "user_name", utilizatori_exercitii.id_utilizator);
            return View(utilizatori_exercitii);
        }

        // GET: utilizatori_exercitii/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilizatori_exercitii utilizatori_exercitii = db.utilizatori_exercitii.Find(id);
            if (utilizatori_exercitii == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_exercitiu = new SelectList(db.exercitiis, "id_exercitiu", "nume_exercitiu", utilizatori_exercitii.id_exercitiu);
            ViewBag.id_utilizator = new SelectList(db.utilizatoris, "id_utilizator", "user_name", utilizatori_exercitii.id_utilizator);
            return View(utilizatori_exercitii);
        }

        // POST: utilizatori_exercitii/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_utilizatori_exercitii,id_utilizator,id_exercitiu")] utilizatori_exercitii utilizatori_exercitii)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizatori_exercitii).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_exercitiu = new SelectList(db.exercitiis, "id_exercitiu", "nume_exercitiu", utilizatori_exercitii.id_exercitiu);
            ViewBag.id_utilizator = new SelectList(db.utilizatoris, "id_utilizator", "user_name", utilizatori_exercitii.id_utilizator);
            return View(utilizatori_exercitii);
        }

        // GET: utilizatori_exercitii/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilizatori_exercitii utilizatori_exercitii = db.utilizatori_exercitii.Find(id);
            if (utilizatori_exercitii == null)
            {
                return HttpNotFound();
            }
            return View(utilizatori_exercitii);
        }

        // POST: utilizatori_exercitii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            utilizatori_exercitii utilizatori_exercitii = db.utilizatori_exercitii.Find(id);
            db.utilizatori_exercitii.Remove(utilizatori_exercitii);
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
