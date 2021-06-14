using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raporter.Models;

namespace Raporter.ViewModels
{
    public class RaportsAdmViewModel
    {
        public Uzytkownicy uzytkownik { get; set; }
        public Oddzialy oddzial { get; set; }
        public Raporty raport { get; set; }
        public Funkcje funkcja { get; set; }
    }
}