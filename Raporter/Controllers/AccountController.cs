using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raporter.ViewModels;
using Raporter.Data;

namespace Raporter.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            RaporterContext db = new RaporterContext();


            //var login = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login));

            if((db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).FirstOrDefault()) != null)
            {
                var id = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).Select(a => new { Id = a.UzytkownicyID }).FirstOrDefault();
                var funkcja = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).Select(a => new { Id = a.FunkcjeID }).FirstOrDefault();
                Session["Login"] = avm.uzytkownik.Login;
                Session["Haslo"] = avm.uzytkownik.Haslo;
                Session["UserID"] = id.Id;
                Session["FunkcjaID"] = funkcja.Id;

                //return RedirectToAction("ViewRap", "UserRaportsView");
                //return View("Welcome");
                if(funkcja.Id.ToString() == 1.ToString())
                {
                    return RedirectToAction("ViewRap", "UserRaportsView");
                  }
                  else if(funkcja.Id.ToString() == 2.ToString())
                {
                    return RedirectToAction("ViewRap_kier", "UserRaportsView");
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
    }
}