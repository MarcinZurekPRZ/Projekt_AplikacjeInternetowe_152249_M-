using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raporter.Data;
using Raporter.ViewModels;
using Raporter.Models;





namespace Raporter.Controllers
{
    public class UserRaportsViewController : Controller
    {
        RaporterContext db = new RaporterContext();
        // GET: UserRaportsView
        public ActionResult ViewRap()
        {
            

            var id = Session["UserID"];
            List<Raporty> raportylist = db.Raporties.Where(a => a.UzytkownicyID.ToString() == id.ToString()).ToList();

            ViewData["RaportyUzytkownika"] =  raportylist ;
                

            return View(ViewData["RaportyUzytkownika"]);
        }

        public ActionResult Create()
        {
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie");
            return View("CreateRap");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RaportyID,DataRaportu,Temat,Tresc,UzytkownicyID")] Raporty raporty)
        {
            if (ModelState.IsValid)
            {
                db.Raporties.Add(raporty);
                db.SaveChanges();
                return RedirectToAction("ViewRap", "UserRaportsView");
                //return RedirectToAction("Index");
            }

            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie", raporty.UzytkownicyID);
            return View("CreateRap");
            //return View("CreateRap");
        }

    }
}