using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MonedaDTO
    {
        public int IdMoneda { get; set; }
        public int IdSociedad { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Base { get; set; }
        public bool Estado { get; set; }
    }

}
