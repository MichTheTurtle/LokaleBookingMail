using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Lokaler")]
    public class Lokale
    {
        public int LokaleID { get; set; }
        public string LokaleNavn { get; set; }
    }
}
