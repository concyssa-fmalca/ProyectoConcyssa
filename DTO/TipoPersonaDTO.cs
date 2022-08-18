using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TipoPersonaDTO
    {
        public int IdTipoPersona { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "TipoPersona" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string TipoPersona { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "TipoPersona" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public bool Estado { get; set; }
    }
}
