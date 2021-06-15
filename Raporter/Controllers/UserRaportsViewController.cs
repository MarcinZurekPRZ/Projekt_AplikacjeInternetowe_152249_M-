using System;

using System.Data;
using System.Data.Entity;
using System.Net;

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
        readonly RaporterContext db = new RaporterContext();
        // GET: UserRaportsView
        public ActionResult ViewRap()
        {
            

            var id = Session["UserID"];
            List<Raporty> raportylist = db.Raporties.Where(a => a.UzytkownicyID.ToString() == id.ToString()).ToList();

            ViewData["RaportyUzytkownika"] =  raportylist ;
            Session["TypWidoku"] = "ViewRap";


            return View(ViewData["RaportyUzytkownika"]);
        }

        public ActionResult ViewRap_kier()
        {
            var oddzialID = Session["OddzialID"];
            Session["TypWidoku"] = "ViewRap_kier";

            List<Uzytkownicy> uzytkownik = db.Uzytkownicies.ToList();
            List<Raporty> raport = db.Raporties.ToList();
            List<Oddzialy> oddzial= db.Oddzialies.ToList();

            var query = from rap in raport
                        join uz in uzytkownik on rap.UzytkownicyID equals uz.UzytkownicyID into table1
                        from uz in table1.ToList()
                        join od in oddzial on uz.OddzialyID equals od.OddzialyID into table2
                        from od in table2.ToList()
                        where od.OddzialyID.ToString() == oddzialID.ToString()
                        select new RaportsKierViewModel
                        {
                            raport=rap,
                            uzytkownik=uz,
                            oddzial=od

                        };

            return View(query);
        }
        public ActionResult ViewRap_Adm()
        {
            Session["TypWidoku"] = "ViewRap_Adm";

            List<Uzytkownicy> uzytkownik = db.Uzytkownicies.ToList();
            List<Raporty> raport = db.Raporties.ToList();
            List<Oddzialy> oddzial = db.Oddzialies.ToList();
            List<Funkcje> funkcj = db.Funkcjes.ToList();
            
            var oddzialID = Session["OddzialID"];

            var query = from uz in uzytkownik
                        join rap in raport  on uz.UzytkownicyID equals rap.UzytkownicyID into table1
                        from rap in table1.ToList()
                        join od in oddzial on uz.OddzialyID equals od.OddzialyID into table2
                        from od in table2.ToList()
                        join fn in funkcj on uz.FunkcjeID equals fn.FunkcjeID into table3
                        from fn in table3.ToList()
                        //where od.OddzialyID.ToString() == oddzialID.ToString()
                        select new RaportsAdmViewModel
                        {
                            raport = rap,
                            uzytkownik = uz,
                            oddzial = od,
                            funkcja = fn

                        };
        

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
                if (!((string)@Session["TypWidoku"] == ("ViewRap_Adm")))
                {
                    raporty.UzytkownicyID = (int)Session["UserID"];
                }    
                db.Raporties.Add(raporty);
                db.SaveChanges();
                return RedirectToAction((string)@Session["TypWidoku"], "UserRaportsView");
            }

            return View("CreateRap");
        }


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
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Login", raporty.UzytkownicyID);
            Session["Temp_UzytkownicyID"] = raporty.UzytkownicyID;
            return View(raporty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RaportyID,DataRaportu,Temat,Tresc,UzytkownicyID")] Raporty raporty)
        {
            if (ModelState.IsValid)
            {

                if (!((string)@Session["TypWidoku"] == ("ViewRap_Adm")))
                {
                    raporty.UzytkownicyID = (int)Session["Temp_UzytkownicyID"];
                    Session.Remove("Temp_UzytkownicyID");
                }

                db.Entry(raporty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction((string)@Session["TypWidoku"], "UserRaportsView");
            }
            ViewBag.UzytkownicyID = new SelectList(db.Uzytkownicies, "UzytkownicyID", "Imie", raporty.UzytkownicyID);
            return View(raporty);
        }

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
            return RedirectToAction((string)@Session["TypWidoku"], "UserRaportsView");
        }

    }
}