using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GrupoDTO
    {
        public int IdGrupo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdSociedad { get; set; }
        public int IdObra { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public IList<SubGrupoDTO> SubGrupos { get; set; }
    }

    public class SubGrupoDTO
    {
        public int IdSubGrupo { get; set; }
        public int IdGrupo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public int IdSociedad { get; set; }
        public string DescGrupo { get; set; } //Campo Opcional
    }
}
