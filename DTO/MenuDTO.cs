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

#pragma warning disable CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
		public string Descripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Descripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.

#pragma warning disable CS8618 // El elemento propiedad "Controller" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
		public string Controller { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Controller" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.

#pragma warning disable CS8618 // El elemento propiedad "Action" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
		public string Action { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Action" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.

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
#pragma warning disable CS8618 // El elemento propiedad "listaRol" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
		public List<beCampoString> listaRol { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "listaRol" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.

#pragma warning disable CS8618 // El elemento propiedad "listaMenu" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
		public List<MenuDTO> listaMenu { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "listaMenu" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
	}

}
