using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MotivoTrasladoDTO
    {
        public int IdMotivoTraslado { get; set; } = 1;
        public string CodigoSunat { get; set; } = "";
        public string CodigoInterno { get; set; } = "";
        public string Descripcion { get; set; } = "";

    }
}
