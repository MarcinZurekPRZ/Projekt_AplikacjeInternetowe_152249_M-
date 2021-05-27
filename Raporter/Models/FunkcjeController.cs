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
    public class FunkcjeController : Controller
    {
        private RaporterContext db = new RaporterContext();

        // GET: Funkcje
        public ActionResult Index()
        {
            return View(db.Funkcjes.ToList());
        }

        // GET: Funkcje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funkcje funkcje = db.Funkcjes.Find(id);
            if (funkcje == null)
            {
                return HttpNotFound();
            }
            return View(funkcje);
        }

        // GET: Funkcje/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funkcje/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FunkcjeID,NazwaFunkcji")] Funkcje funkcje)
        {
            if (ModelState.IsValid)
            {
                db.Funkcjes.Add(funkcje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funkcje);
        }

        // GET: Funkcje/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funkcje funkcje = db.Funkcjes.Find(id);
            if (funkcje == null)
            {
                return HttpNotFound();
            }
            return View(funkcje);
        }

        // POST: Funkcje/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FunkcjeID,NazwaFunkcji")] Funkcje funkcje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funkcje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funkcje);
        }

        // GET: Funkcje/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funkcje funkcje = db.Funkcjes.Find(id);
            if (funkcje == null)
            {
                return HttpNotFound();
            }
            return View(funkcje);
        }

        // POST: Funkcje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funkcje funkcje = db.Funkcjes.Find(id);
            db.Funkcjes.Remove(funkcje);
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
