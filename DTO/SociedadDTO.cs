using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SociedadDTO
    {
        public int IdSociedad { get; set; }
        public string NombreSociedad { get; set; }
        public string NumeroDocumento { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
