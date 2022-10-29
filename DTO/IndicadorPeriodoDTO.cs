using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IndicadorPeriodoDTO
    {
        public int IdIndicadorPeriodo { get; set; } = 0;
        public string Indicador { set; get; } = "";
        public int IdSociedad { get; set; } = 0;    
        public int IdUsuario { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Boolean  Eliminado { get; set; }

        public string NombUsuario { get; set; }
    }
}
