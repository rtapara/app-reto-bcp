using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app_reto_bcp.Models
{
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1)]
        public string monedaOrigen { get; set; }
        [Required]
        [StringLength(1)]
        public string monedaDestino { get; set; }
        [Required]
        public double tipoCambio { get; set; }
    }
}
