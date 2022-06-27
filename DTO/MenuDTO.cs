using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MenuDTO
    {
		public int idMenu { get; set; }

		public string Descripcion { get; set; }

		public string Controller { get; set; }

		public string Action { get; set; }

		public int Menu { get; set; }

		public DateTime FechaCreacion { get; set; }

		public DateTime FechaUltimaModificacion { get; set; }

		public int UsuarioCreacion { get; set; }

		public int UsuarioUltimaModificacion { get; set; }

		public bool Estado { get; set; }
		public int Orden { get; set; }
	}

	public class Seg_RolMenuVistaDTO
	{
		public List<beCampoString> listaRol { get; set; }

		public List<MenuDTO> listaMenu { get; set; }
	}

}
