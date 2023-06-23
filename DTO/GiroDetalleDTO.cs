using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiroDetalleDTO
    {
        public int IdGiro { get; set; }
        public int IdGiroDetalle { get; set; }
        public int IdTipoDocumento { get; set; }
        public string? TipoDocumento { get; set; }

        public int IdMoneda { get; set; }
        public string? Moneda { get; set; }
        public decimal Monto { get; set; }
        public decimal Rendicion { get; set; }
        public string? Documento { get; set; }
        public string? Proveedor { get; set; }
        public int IdProveedor  { get; set; }
        public string? Serie  { get; set; }
        public string? NroDocumento { get; set; }
        public string? Comentario { get; set; }
        public string? Anexo { get; set; }
        public bool Estado { get; set; }

    }
}
