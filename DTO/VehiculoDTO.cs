using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VehiculoDTO
    {
        public int IdVehiculo { get; set; }
        public int IdMarca { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string Licencia { get; set; }
        public int IdBase { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "Condicion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Condicion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Condicion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "CertificadoInscripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string CertificadoInscripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "CertificadoInscripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "Placa" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string Placa { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "Placa" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public int IdChofer { get; set; }
        public int IdSociedad { get; set; }

        public bool Estado { get; set; }
        public bool Eliminado { get; set; }

        //Parametros que llenan datos
#pragma warning disable CS8618 // El elemento propiedad "MarcaDescripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string MarcaDescripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "MarcaDescripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
#pragma warning disable CS8618 // El elemento propiedad "BaseDescripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string BaseDescripcion { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "BaseDescripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.

#pragma warning disable CS8618 // El elemento propiedad "ChoferDescripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string ChoferDescripcion { get; set; }
        public int IdCuadrilla { get; set; }
        public string Brevete { get; set; }
        public int IdPropietario { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "ChoferDescripcion" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
}
}
