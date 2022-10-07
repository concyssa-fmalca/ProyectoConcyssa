using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StockArticuloAlmacenDTO
    {
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }

        public string Descripcion { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockMaximo { get; set; }
        public decimal StockAlerta { get; set; }
        public decimal StockAlmacen { get; set; }
    }
}
