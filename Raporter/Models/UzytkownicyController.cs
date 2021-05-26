using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Raporter.Data;

namespace Raporter.Models
{
    public class UzytkownicyController : Controller
    {
        private RaporterContext db = new RaporterContext();

        // GET: Uzytkownicy
        public ActionResult Index()
        {
            var uzytkownicies = db.Uzytkownicies.Include(u => u.Oddzialy);
            return View(uzytkownicies.ToList());
        }

        // GET: Uzytkownicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownicy uzytkownicy = db.Uzytkownicies.Find(id);
            if (uzytkownicy == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownicy);
        }

        // GET: Uzytkownicy/Create
        public ActionResult Create()
        {
            ViewBag.OddzialyID = new SelectList(db.Oddzialies, "OddzialyID", "NazwaOddzialu");
            return View();
        }

        // POST: Uzytkownicy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UzytkownicyID,Imie,Nazwisko,OddzialyID,ID_funkcji")] Uzytkownicy uzytkownicy)
        {
            if (ModelState.IsValid)
            {
                db.Uzytkownicies.Add(uzytkownicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OddzialyID = new SelectList(db.Oddzialies, "OddzialyID", "NazwaOddzialu", uzytkownicy.OddzialyID);
            return View(uzytkownicy);
        }

        // GET: Uzytkownicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownicy uzytkownicy = db.Uzytkownicies.Find(id);
            if (uzytkownicy == null)
            {
                return HttpNotFound();
            }
            ViewBag.OddzialyID = new SelectList(db.Oddzialies, "OddzialyID", "NazwaOddzialu", uzytkownicy.OddzialyID);
            return View(uzytkownicy);
        }

        // POST: Uzytkownicy/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UzytkownicyID,Imie,Nazwisko,OddzialyID,ID_funkcji")] Uzytkownicy uzytkownicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uzytkownicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OddzialyID = new SelectList(db.Oddzialies, "OddzialyID", "NazwaOddzialu", uzytkownicy.OddzialyID);
            return View(uzytkownicy);
        }

        // GET: Uzytkownicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownicy uzytkownicy = db.Uzytkownicies.Find(id);
            if (uzytkownicy == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownicy);
        }

        // POST: Uzytkownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uzytkownicy uzytkownicy = db.Uzytkownicies.Find(id);
            db.Uzytkownicies.Remove(uzytkownicy);
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
