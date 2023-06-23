using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TipoRegistroDTO
    {
        public int IdTipoRegistro { get; set; }
        public string? NombTipoRegistro { get; set; }
        public bool Estado { get; set; }
        public int IdSociedad { get; set; }
    }
}
