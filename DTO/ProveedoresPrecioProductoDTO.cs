using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProveedoresPrecioProductoDTO
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public decimal PrecioNacional { get; set; }
        public decimal PrecioExtranjero { get; set; }
        public string DescripcionArticulo { get; set; }
    }
}
