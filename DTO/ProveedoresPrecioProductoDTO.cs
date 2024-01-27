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
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string? Obra { get; set; }
        public string? Serie { get; set; }
        public string NombObra { get; set; }
        public int IdArticuloProveedor { get; set; }
    }
}
