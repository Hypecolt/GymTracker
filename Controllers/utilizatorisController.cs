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
    public class utilizatorisController : Controller
    {
        private ProiectDAWEntities db = new ProiectDAWEntities();

        // GET: utilizatoris
        public ActionResult Index()
        {
            var utilizatoris = db.utilizatoris.Include(u => u.contact).Include(u => u.goal);
            return View(utilizatoris.ToList());
        }

        // GET: utilizatoris/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilizatori utilizatori = db.utilizatoris.Find(id);

            List<(string email,string tlf,string goal)> uzertlf = new List<(string, string, string)>();
            var contact = db.contacts.Where(contacts => contacts.id_contact == utilizatori.id_contact);
            var corp = db.goals.Where(goalz => goalz.id_goal == utilizatori.id_goal).First().tip_corp;

            foreach (var item in contact)
            {   
                //var aid = (db.contacts.First(x => x.id_contact == item.id_contact).id_contact);
                //var uzrmail = (db.contacts.First(x => x.id_contact == item.id_contact));
                //ViewBag.aidi = aid;
                uzertlf.Add((item.email, item.nr_telefon, corp));
                
            }
            ViewBag.contactuzr = uzertlf;
            ViewBag.numeom = utilizatori.user_name;

            if (utilizatori == null)
            {
                return HttpNotFound();
            }
            return View(utilizatori);
        }

        // GET: utilizatoris/Create
        public ActionResult Create()
        {
            ViewBag.id_contact = new SelectList(db.contacts, "id_contact", "nr_telefon");
            ViewBag.id_goal = new SelectList(db.goals, "id_goal", "tip_corp");
            return View();
        }

        // POST: utilizatoris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_utilizator,user_name,password,id_goal,id_contact")] utilizatori utilizatori)
        {
            if (ModelState.IsValid)
            {
                utilizatori.id_utilizator = Guid.NewGuid();
                db.utilizatoris.Add(utilizatori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_contact = new SelectList(db.contacts, "id_contact", "nr_telefon", utilizatori.id_contact);
            ViewBag.id_goal = new SelectList(db.goals, "id_goal", "tip_corp", utilizatori.id_goal);
            return View(utilizatori);
        }

        // GET: utilizatoris/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilizatori utilizatori = db.utilizatoris.Find(id);
            if (utilizatori == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_contact = new SelectList(db.contacts, "id_contact", "nr_telefon", utilizatori.id_contact);
            ViewBag.id_goal = new SelectList(db.goals, "id_goal", "tip_corp", utilizatori.id_goal);
            return View(utilizatori);
        }

        // POST: utilizatoris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_utilizator,user_name,password,id_goal,id_contact")] utilizatori utilizatori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizatori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_contact = new SelectList(db.contacts, "id_contact", "nr_telefon", utilizatori.id_contact);
            ViewBag.id_goal = new SelectList(db.goals, "id_goal", "tip_corp", utilizatori.id_goal);
            return View(utilizatori);
        }

        // GET: utilizatoris/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilizatori utilizatori = db.utilizatoris.Find(id);
            if (utilizatori == null)
            {
                return HttpNotFound();
            }
            return View(utilizatori);
        }

        // POST: utilizatoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            utilizatori utilizatori = db.utilizatoris.Find(id);
            db.utilizatoris.Remove(utilizatori);
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
