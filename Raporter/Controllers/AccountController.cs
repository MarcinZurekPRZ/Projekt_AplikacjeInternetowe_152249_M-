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


            var login = db.Uzytkownicies.Where(a => a.Imie.Equals(avm.uzytkownik.Imie));

            //if (avm.uzytkownik.Imie.Equals("tak") && avm.uzytkownik.Nazwisko.Equals("Ćwikla"))
            if((db.Uzytkownicies.Where(a => a.Imie.Equals(avm.uzytkownik.Imie) && a.Nazwisko.Equals(avm.uzytkownik.Nazwisko)).FirstOrDefault()) != null)
            {
                Session["Imie"] = avm.uzytkownik.Imie;
                Session["Nazwisko"] = avm.uzytkownik.Nazwisko;

                return View("Welcome");
            }
            else
            {
                ViewBag.Error = "Nieprawidłowe dane";
                
                return View("Index");
            }
            
        }
    }
}