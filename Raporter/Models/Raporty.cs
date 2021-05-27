using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raporter.Models
{
    public class Raporty
    {
        public int RaportyID { get; set; }
        public DateTime DataRaportu { get; set; }
        public string Temat { get; set; }
        public string Tresc { get; set; }
        public int UzytkownicyID { get; set; }

        public virtual Uzytkownicy Uzytkownicy  { get; set; }
    }
}