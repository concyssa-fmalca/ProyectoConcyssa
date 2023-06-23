using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TipoCambioDTO
    {
        public int IdMoneda { get; set; }
        public string? Moneda { get; set; }
        public int IdSociedad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TipoCambioCompra { get; set; }
        public decimal TipoCambioVenta { get; set; }
        public string? Origen { get; set; }
       
    }
    public class TipoCambioSunatDTO
    {
        public double compra { get; set; }
        public double venta { get; set; }
        public string? origen { get; set; }
        public string? moneda { get; set; }
        public string? fecha { get; set; }
    }
}
