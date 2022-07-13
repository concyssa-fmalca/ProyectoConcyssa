using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CondicionPagoDTO
    {
        public int IdCondicionPago { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Dias { get; set; }
        public bool Estado { get; set; }
    }
}
