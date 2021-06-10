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


            var login = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login));

            //if (avm.uzytkownik.Imie.Equals("tak") && avm.uzytkownik.Nazwisko.Equals("Ćwikla"))
            if((db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).FirstOrDefault()) != null)
            {
                var id = db.Uzytkownicies.Where(a => a.Login.Equals(avm.uzytkownik.Login) && a.Haslo.Equals(avm.uzytkownik.Haslo)).Select(a => new { Id = a.UzytkownicyID }).FirstOrDefault();
                Session["Login"] = avm.uzytkownik.Login;
                Session["Haslo"] = avm.uzytkownik.Haslo;
                Session["UserID"] = id.Id;

                //return View("Welcome");
                return RedirectToAction("ViewRap", "UserRaportsView");
            }
            else
            {
                ViewBag.Error = "Nieprawidłowe dane";
                
                return View("Index");
            }
            
        }
    }
}