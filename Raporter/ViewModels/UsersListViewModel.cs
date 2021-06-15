using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raporter.Models;

namespace Raporter.ViewModels
{
    public class UsersListViewModel
    {
        public Uzytkownicy uzytkownik { get; set; }
        public Funkcje funkcja { get; set; }
        public Oddzialy oddzial { get; set; }
    }
}
