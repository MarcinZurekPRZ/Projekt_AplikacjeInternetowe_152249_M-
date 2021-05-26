using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raporter.Models
{
    public class Oddzialy
    {
        public int OddzialyID { get; set; }
        public string NazwaOddzialu { get; set; }
        public string SymbolOddzialu { get; set; }

        public virtual ICollection<Uzytkownicy> Uzytkownicy { get; set; }
    }
}