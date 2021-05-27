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
    public class RaportyController : Controller
    {
        private RaporterContext db = new RaporterContext();

        // GET: Raporty
        public ActionResult Index()
        {
            var raporties = db.Raporties.Include(r => r.Uzytkownicy);
            return View(raporties.ToList());
        }

        // GET: Raporty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raporty raporty = db.Raporties.Find(id);
            if (raporty == null)
            {
                return HttpNotFound();
            }
            return View(raporty);
        }

        // GET: Raporty/Create
        public ActionResult Create()
        {
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie");
            return View();
        }

        // POST: Raporty/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RaportyID,DataRaportu,Temat,Tresc,UzytkownicyID")] Raporty raporty)
        {
            if (ModelState.IsValid)
            {
                db.Raporties.Add(raporty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie", raporty.UzytkownicyID);
            return View(raporty);
        }

        // GET: Raporty/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raporty raporty = db.Raporties.Find(id);
            if (raporty == null)
            {
                return HttpNotFound();
            }
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie", raporty.UzytkownicyID);
            return View(raporty);
        }

        // POST: Raporty/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RaportyID,DataRaportu,Temat,Tresc,UzytkownicyID")] Raporty raporty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raporty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie", raporty.UzytkownicyID);
            return View(raporty);
        }

        // GET: Raporty/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raporty raporty = db.Raporties.Find(id);
            if (raporty == null)
            {
                return HttpNotFound();
            }
            return View(raporty);
        }

        // POST: Raporty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Raporty raporty = db.Raporties.Find(id);
            db.Raporties.Remove(raporty);
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
