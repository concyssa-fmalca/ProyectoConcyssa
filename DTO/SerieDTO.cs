using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SerieDTO
    {
        public int IdSerie { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Serie" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Serie { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Serie" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public int NumeroInicial { get; set; }
        public int NumeroFinal { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado{ get; set; }
        public int IdSociedad { get; set; }
        public int Documento { get; set; }
    }
}
