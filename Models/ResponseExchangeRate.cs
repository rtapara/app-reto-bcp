using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_reto_bcp.Models
{
    public class ResponseExchangeRate
    {
        public ResponseMessage response { get; set; }

        public double monto { get; set; }
        public double montoCambiado { get; set; }
        public string monedaOrigen { get; set; }
        public string monedaDestino { get; set; }
        public double tipoCambio { get; set; }

    }
}
