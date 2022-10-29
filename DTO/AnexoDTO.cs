using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AnexoDTO
    {
        public int IdAnexo { get; set; } = 0;
        public string ruta { get; set; } = "";
        public string NombreArchivo { get; set; } = "";

        public int IdSociedad { get; set; } = 0;
        public string Tabla { get; set; } = "";
        public int IdTabla { get; set; } = 0;
    }
}
