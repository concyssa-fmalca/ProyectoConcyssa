using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ModeloDTO
    {
        public int MyProperty { get; set; }
        public int IdModelo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public int IdSociedad { get; set; }
    }
}
