using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Raporter.Models
{
    public class Uzytkownicy
    {
        public int UzytkownicyID { get; set; }
        [Display(Name = "Login")]
        public string Imie { get; set; }
        [Display(Name = "Haslo")]
        public string Nazwisko { get; set; }
        public int OddzialyID { get; set; }
        public int FunkcjeID { get; set; }

        public virtual Oddzialy Oddzialy { get; set; }

    }
}