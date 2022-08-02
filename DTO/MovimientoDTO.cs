using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MovimientoDTO
    {
        public int IdMovimiento { get; set; }
        public int ObjType { get; set; }
        public int IdMoneda { get; set; }
        public string CodMoneda { get; set; }
        public decimal  TipoCambio { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdListaPrecios { get; set; }
        public string Referencia { get; set; }
        public string Comentario { get; set; }
    }
}
