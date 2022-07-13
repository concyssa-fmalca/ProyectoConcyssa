using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RubroProveedorDTO
    {
        public int IdRubroProveedor { get; set; }
        public int IdSociedad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
    }
}
