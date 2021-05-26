using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raporter.Models
{
    public class Uzytkownicy
    {
        public int UzytkownicyID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int OddzialyID { get; set; }
        public int ID_funkcji { get; set; }

        public virtual Oddzialy Oddzialy { get; set; }

    }
}