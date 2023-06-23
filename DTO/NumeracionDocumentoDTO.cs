using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NumeracionDocumentoDTO
    {
        
        public int IdNumeracionDocumento { get; set; }
        public int IdBase { get; set; }
        public int IdTipoDocumento { get; set; } 
        public string? Serie { get; set; }
        public int NumeracionInicial { get; set; }
        public string? Base { get; set; }
        public string? TipoDocumento { get; set; }
        public bool Estado { get; set; }
    }
}
