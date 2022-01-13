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
    public class exercitiisController : Controller
    {
        private ProiectDAWEntities db = new ProiectDAWEntities();

        // GET: exercitiis
        public ActionResult Index()
        {
            return View(db.exercitiis.ToList());
        }

        // GET: exercitiis/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercitii exercitii = db.exercitiis.Find(id);

            List<string> uzeri = new List<string>();
            List<string> nume_egz = new List<string>();
            var utilizatori = db.utilizatori_exercitii.Where(utilizatori_exercitii => utilizatori_exercitii.id_exercitiu == exercitii.id_exercitiu);
            var exercitiu = db.utilizatori_exercitii.Where(utilizatori_exercitii => utilizatori_exercitii.id_exercitiu == exercitii.id_exercitiu);
            foreach (var item in utilizatori)
            {
                uzeri.Add(db.utilizatoris.First(x => x.id_utilizator == item.id_utilizator).user_name);
            }
            foreach (var item in exercitiu)
            {
                nume_egz.Add(db.exercitiis.First(x => x.id_exercitiu == item.id_exercitiu).nume_exercitiu);
            }
            ViewBag.uzeri = uzeri;
            ViewBag.nume_egz = nume_egz;

            if (exercitii == null)
            {
                return HttpNotFound();
            }
            return View(exercitii);
        }

        // GET: exercitiis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: exercitiis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_exercitiu,nume_exercitiu,descriere")] exercitii exercitii)
        {
            if (ModelState.IsValid)
            {
                exercitii.id_exercitiu = Guid.NewGuid();
                db.exercitiis.Add(exercitii);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exercitii);
        }

        // GET: exercitiis/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercitii exercitii = db.exercitiis.Find(id);
            if (exercitii == null)
            {
                return HttpNotFound();
            }
            return View(exercitii);
        }

        // POST: exercitiis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_exercitiu,nume_exercitiu,descriere")] exercitii exercitii)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercitii).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exercitii);
        }

        // GET: exercitiis/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exercitii exercitii = db.exercitiis.Find(id);

            if (exercitii == null)
            {
                return HttpNotFound();
            }
            return View(exercitii);
        }

        // POST: exercitiis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            exercitii exercitii = db.exercitiis.Find(id);
            db.exercitiis.Remove(exercitii);
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
