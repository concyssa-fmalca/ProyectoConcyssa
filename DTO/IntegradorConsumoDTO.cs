using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IntegradorConsumoDTO
    {
        public int IdKardex { get; set; }
        public int IdDetalleMovimiento { get; set; }
        public int IdDetalleOPCH { get; set; }
        public int IdDetalleOPDN { get; set; }
        public int IdDefinicionGrupoUnidad { get; set; }
        public decimal CantidadBase { get; set; }
        public int IdUnidadMedidaBase { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal CantidadRegistro { get; set; }
        public int IdUnidadMedidaRegistro { get; set; }
        public decimal PrecioRegistro { get; set; }
        public decimal PrecioPromedio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int IdAlmacen { get; set; }
        public int IdArticulo { get; set; }
        public decimal Saldo { get; set; }

        //Adicionales
        public string DescArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string DescSerie { get; set; }
        public int Correlativo { get; set; }
        public string TipoTransaccion { get; set; }
        public string DescUnidadMedidaBase { get; set; }

        public string NombUsuario { get; set; }

        public string Comentario { get; set; }

        public string Modulo { get; set; } = "";
        public string TipoDocumentoRef { get; set; }
        public string NumSerieTipoDocumentoRef { get; set; }
    }
}
