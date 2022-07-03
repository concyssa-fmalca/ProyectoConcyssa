using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class UnidadMedidaDTO
    {

        public int IdUnidadMedida { get; set; }
        public string Codigo { get; set; }
        public string CodigoSunat { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int IdSociedad { get; set; }
        
    }
}
