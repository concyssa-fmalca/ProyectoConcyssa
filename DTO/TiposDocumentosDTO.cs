using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TiposDocumentosDTO
    {
        public int IdTipoDocumento { get; set; }
        public string CodSunat { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public int IdSociedad { get; set; }
        public string CodeSAP { get; set; }
        public string PrefijoSAP { get; set; }
        public string NombreSAP { get; set; }
    }
}
