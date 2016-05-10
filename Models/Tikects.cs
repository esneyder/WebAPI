using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
   public class Tikects
    {
        public int IdDetalleFactura { get; set; }
        public string FechaFactura { get; set; }
        public string Cocinero{ get; set; }
        public string Plato { get; set; }
        public decimal Importe { get; set; }
    }
}
