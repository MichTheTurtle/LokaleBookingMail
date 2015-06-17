using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Context: DbContext
    {

        public Context() : base() 
        {
 
        }

        public DbSet<Bruger> Brugere { get; set; }
        public DbSet<Lokale> Lokaler { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
