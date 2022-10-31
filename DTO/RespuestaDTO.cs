using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RespuestaDTO
    {
        public string Result { get; set; } = "";
        public string Mensaje { get; set; } = "";
        public string Base64ArchivoPDF { get; set; } = "";

    }
}
