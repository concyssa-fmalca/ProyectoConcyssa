using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TipoDocumentoDTO
    {
        public int IdTipoDocumento { get; set; }
        public string Codigo { get; set; }
        public string TipoDocumento { get; set; }
        public bool Estado { get; set; }
    }
}
