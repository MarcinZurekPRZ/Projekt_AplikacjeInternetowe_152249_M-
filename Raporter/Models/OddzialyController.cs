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
    public class OddzialyController : Controller
    {
        private RaporterContext db = new RaporterContext();

        // GET: Oddzialy
        public ActionResult Index()
        {
            return View(db.Oddzialies.ToList());
        }

        // GET: Oddzialy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oddzialy oddzialy = db.Oddzialies.Find(id);
            if (oddzialy == null)
            {
                return HttpNotFound();
            }
            return View(oddzialy);
        }

        // GET: Oddzialy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oddzialy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OddzialyID,NazwaOddzialu,SymbolOddzialu")] Oddzialy oddzialy)
        {
            if (ModelState.IsValid)
            {
                db.Oddzialies.Add(oddzialy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oddzialy);
        }

        // GET: Oddzialy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oddzialy oddzialy = db.Oddzialies.Find(id);
            if (oddzialy == null)
            {
                return HttpNotFound();
            }
            return View(oddzialy);
        }

        // POST: Oddzialy/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OddzialyID,NazwaOddzialu,SymbolOddzialu")] Oddzialy oddzialy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oddzialy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oddzialy);
        }

        // GET: Oddzialy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oddzialy oddzialy = db.Oddzialies.Find(id);
            if (oddzialy == null)
            {
                return HttpNotFound();
            }
            return View(oddzialy);
        }

        // POST: Oddzialy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oddzialy oddzialy = db.Oddzialies.Find(id);
            db.Oddzialies.Remove(oddzialy);
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
