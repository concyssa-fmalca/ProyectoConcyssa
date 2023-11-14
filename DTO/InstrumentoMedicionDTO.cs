using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InstrumentoMedicionDTO
    {
        public int IdInstrumentoMedicion { get; set; }
        public int IdObra { get; set; }
        public int IdSociedad { get; set; }
        public string Nombre { get; set; }
        public string NumeroSerie { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int PeriodoCalibracion { get; set; }
        public string ParametroMedicion { get; set; }
        public string PatronMedicion { get; set; }
        public int IdArea { get; set; }
        public string Responsable { get; set; }
        public int EstadoCalibracion { get; set; }
        public string Ubicacion { get; set; }
        public int TipoCalibracion { get; set; }
        public int EstadoInstrumento { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public IList<AnexoDTO> AnexoDetalle { get; set; }

        /////////////////////////

        public string NombObra { get; set; }
        public string NombMarca { get; set; }
        public string NombModelo { get; set; }
        public string NombArea { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime ProxCalib { get; set; }
        public int EstadoCalib { get; set; }


    }
    public class InstrumentoMedicionDetalleDTO
    {
        public int IdInstrumentoMedicionDetalle { get; set; }
        public int IdInstrumentoMedicion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime  ProximaFecha { get; set; }
        public string Observaciones { get; set; }
        public bool Eliminado { get; set; }

    }

    public class InstrumentoMedicionDetalleDocDTO
    {
        public int IdInstrumentoMedicionDetalleDocs { get; set; }
        public int IdInstrumentoMedicionDetalle { get; set; }
        public string NumeroDoc { get; set; }
        public int IdProveedor { get; set; }
        public string Observaciones { get; set; }
        public IList<AnexoDTO> AnexoDetalle { get; set; }

        /// ////////////
   
        public string NombreAnexo { get; set; }
    }
}
