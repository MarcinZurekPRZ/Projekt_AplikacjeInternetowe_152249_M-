using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raporter.Models
{
    public class Funkcje
    {
        public int FunkcjeID { get; set; }
        public string  NazwaFunkcji { get; set; }

        public virtual ICollection<Uzytkownicy> Uzytkownicy { get; set; }
    }
}