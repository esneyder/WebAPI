using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Tikect
    {
        public int IdFactura { get; set; }
        public string Cliente { get; set; }
        public string Camarero { get; set; }
        public string Ubicacion { get; set; }
        public string FechaFactura { get; set; }
    }
}
