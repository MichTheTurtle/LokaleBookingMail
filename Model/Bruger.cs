using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Brugere")]
    public class Bruger
    {

        public int BrugerID { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rettidhed { get; set; }
    }
}
