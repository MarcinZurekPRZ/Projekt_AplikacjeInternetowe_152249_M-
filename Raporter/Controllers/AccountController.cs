using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raporter.ViewModels;
using Raporter.Data;
using Raporter.Models;
using System.Net;


namespace Raporter.Controllers
{
    public class AccountController : Controller
    {
        RaporterContext db = new RaporterContext();

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            RaporterContext db = new RaporterContext();

            if((db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).FirstOrDefault()) != null)
            {
                var id = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).Select(a => new { Id = a.UzytkownicyID }).FirstOrDefault();
                var oddzial = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).Select(a => new { Id = a.OddzialyID }).FirstOrDefault();
                var funkcja = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).Select(a => new { Id = a.FunkcjeID }).FirstOrDefault();
                Session["Login"] = avm.uzytkownik.Login;
                Session["Haslo"] = avm.uzytkownik.Haslo;
                Session["UserID"] = id.Id;
                Session["FunkcjaID"] = funkcja.Id;
                Session["OddzialID"] = oddzial.Id;

                if (funkcja.Id.ToString() == 1.ToString())
                {
                    return RedirectToAction("ViewRap", "UserRaportsView");
                  }
                  else if(funkcja.Id.ToString() == 2.ToString())
                {
                    return RedirectToAction("ViewRap_kier", "UserRaportsView");
                }
                  else if (funkcja.Id.ToString() == 3.ToString())
                {
                    return RedirectToAction("ViewRap_Adm", "UserRaportsView");
                }
                else
                {
                    ViewBag.Error = "Niepoprawne dane oddzialu uzytkownika";

                    return View("Index");
                }


            }
            else
            {
                ViewBag.Error = "Nieprawidłowe dane !";
                
                return View("Index");
            }
            
        }



        public ActionResult Logout()
        {

            Session.Remove("Login");
            Session.Remove("Haslo");
            Session.Remove("UserID");
            Session.Remove("FunkcjaID");

            ViewBag.SuccessMessage = "Poprawne wylogowanie !";

            return View("Index");
            

        }

        public ActionResult Create()
        {
            ViewBag.OddzialyID = new SelectList(db.Oddzialies, "OddzialyID", "NazwaOddzialu");
            ViewBag.FunkcjeID = new SelectList(db.Funkcjes, "FunkcjeID", "NazwaFunkcji");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UzytkownicyID,Imie,Nazwisko,OddzialyID,FunkcjeID,Haslo,Login")] Uzytkownicy uzytkownicy)
        {
            if (ModelState.IsValid)
            {
                db.Uzytkownicies.Add(uzytkownicy);
                db.SaveChanges();
                return RedirectToAction("ViewUrz", "Account");
            }

            ViewBag.OddzialyID = new SelectList(db.Oddzialies, "OddzialyID", "NazwaOddzialu", uzytkownicy.OddzialyID);
            return View("Create");
        }


        public ActionResult ViewUrz()
        {
            
            List<Uzytkownicy> uzytkownik = db.Uzytkownicies.ToList();
            List<Funkcje> funkcja = db.Funkcjes.ToList();
            List<Oddzialy> oddzial = db.Oddzialies.ToList();

            var query = from uz in uzytkownik
                        join fun in funkcja on uz.FunkcjeID equals fun.FunkcjeID into table1
                        from fun in table1.ToList()
                        join od in oddzial on uz.OddzialyID equals od.OddzialyID into table2
                        from od in table2.ToList()
                        select new UsersListViewModel
                        {
                            uzytkownik = uz,
                            oddzial = od,
                            funkcja = fun

                        };
        
         return View(query);

        }


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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uzytkownicy uzytkownicy = db.Uzytkownicies.Find(id);
            db.Uzytkownicies.Remove(uzytkownicy);
            db.SaveChanges();
            return RedirectToAction("ViewUrz", "Account");
        }
    }
}