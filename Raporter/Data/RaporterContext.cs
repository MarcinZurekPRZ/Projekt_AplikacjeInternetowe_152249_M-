using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Raporter.Data
{
    public class RaporterContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RaporterContext() : base("name=RaporterContext")
        {
        }

        public System.Data.Entity.DbSet<Raporter.Models.Oddzialy> Oddzialies { get; set; }

        public System.Data.Entity.DbSet<Raporter.Models.Uzytkownicy> Uzytkownicies { get; set; }

        public System.Data.Entity.DbSet<Raporter.Models.Funkcje> Funkcjes { get; set; }

        public System.Data.Entity.DbSet<Raporter.Models.Raporty> Raporties { get; set; }
    }
}
