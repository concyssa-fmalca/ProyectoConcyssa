using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ArticuloStockDTO
    {
        public int IdArticuloStock { get; set; }
        public int IdArticulo { get; set; }
        
        public int IdAlmacen { get; set; }
        public decimal Stock { get; set; }
        public decimal PrecioPromedio { get; set; }

        public string NombArticulo { get; set; }
        public string NombAlmacen { get; set; }
    }
}
