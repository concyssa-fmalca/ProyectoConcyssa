using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string CodigoCliente { get; set; }
        public int TipoPersona { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionContribuyente { get; set; }
        public string DireccionFiscal { get; set; }
        public int Departamento { get; set; }
        public int Provincia { get; set; }
        public int Distrito { get; set; }
        public int Pais { get; set; }
        public string Telefono { get; set; }
        public string ComprobantesElectronicos { get; set; }
        public string AfiliadoPLE { get; set; }
        public int CondicionPago { get; set; }
        public string LineaCredito { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Fax { get; set; }
        public string NombreContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string EmailContacto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public int Tipo { get; set; }
    }
}
