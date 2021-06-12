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

        public ActionResult ViewRap_kier()
        {
            var funkcja = Session["FunkcjaID"];
            List<Uzytkownicy> uzytkownik = db.Uzytkownicies.ToList();
            List<Raporty> raport = db.Raporties.ToList();
            List<Oddzialy> oddzial= db.Oddzialies.ToList();

            var query = from rap in raport
                        join uz in uzytkownik on rap.UzytkownicyID equals uz.UzytkownicyID into table1
                        from uz in table1.ToList()
                        join od in oddzial on uz.OddzialyID equals od.OddzialyID into table2
                        from od in table2.ToList()
                        where od.OddzialyID.ToString() == funkcja.ToString()
                        select new RaportsKierViewModel
                        {
                            raport=rap,
                            uzytkownik=uz,
                            oddzial=od
                            //uz.Imie,
                            //uz.Nazwisko,
                            //rap.DataRaportu,
                            //rap.Temat,
                            //rap.Tresc,
                            //od.NazwaOddzialu
                        };

            //var result = query.ToList();

            //ViewData["RaportyUzytkownika"] = result;


            //return View(ViewData["RaportyUzytkownika"]);
            return View(query);
        }

        public ActionResult Create()
        {
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Login");
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

            //ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie", raporty.UzytkownicyID);
            return View("CreateRap");
            //return View("CreateRap");
        }

    }
}