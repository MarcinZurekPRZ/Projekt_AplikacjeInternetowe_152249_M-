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
        // GET: UserRaportsView
        public ActionResult ViewRap(UserRaportsViewModel rap)
        {
            RaporterContext db = new RaporterContext();

            var id = Session["UserID"];
            List<Raporty> raportylist = db.Raporties.Where(a => a.UzytkownicyID.ToString() == id.ToString()).ToList();
            //db.Raporties.ToList();
            //List <Raporty> raportylist = db.Raporties.Select(raport => new Raporty(){
            //  UzytkownicyID = 1
            //}).ToList();

            ViewData["RaportyUzytkownika"] =  raportylist ;
                

            return View(ViewData["RaportyUzytkownika"]);
        }
        
    }
}