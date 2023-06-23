using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GiroDTO
    {      
        public int IdGiro { get; set; }

        public int IdTipoRegistro { get; set; }
        public int IdSemana { get; set; }
        public string? Semana { get; set; }
        public int IdResponsable { get; set; }
        public int IdSolicitante { get; set; }
        public int IdObra { get; set; }
        public string? Obra { get; set; }
        public string? Responsable { get; set; }
        public string? Solicitante { get; set; }
        public int  Tipo { get; set; }      
        public bool Estado { get; set; }
        public bool Contabilizado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal MontoSoles { get; set; }
        public decimal MontoDolares{ get; set; }
        public int IdEstadoGiro { get; set; }
        public string? EstadoGiro { get; set; }

        public int IdCreador { get; set; }

        public List<GiroDetalleDTO> DetalleGiro { get; set; }
        public IList<GiroDetalleDTO> DetallesGiro;

        public IList<GiroModeloDTO> ListGiroModelo;

        public int IdSerie { get; set; }
        public string NombSerie { get; set; }
        public int Correlativo { get; set; }

    }
}
