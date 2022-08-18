using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TipoDocumentoDTO
    {
        public int IdTipoDocumento { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Codigo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Codigo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "TipoDocumento" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string TipoDocumento { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "TipoDocumento" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool Estado { get; set; }
    }
}
