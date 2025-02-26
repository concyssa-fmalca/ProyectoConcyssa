using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQDTO
    {
        public int IdSolicitudRQ { get; set; }
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public int IdSolicitante { get; set; }
        public string Solicitante { get; set; }
        public int IdSucursal { get; set; }
        public int IdDepartamento { get; set; }

        public string NombreDepartamento { get; set; }
        public int IdClaseArticulo { get; set; }
        public string IdMoneda { get; set; }
        public decimal TipoCambio { get; set; }
        public int IdTitular { get; set; }
        public decimal TotalAntesDescuento { get; set; }
        public int IdIndicadorImpuesto { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaValidoHasta { get; set; }
        public DateTime FechaDocumento { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string Comentarios { get; set; }
        public int Estado { get; set; }
        public int Prioridad { get; set; }
        public string DetalleEstado { get; set; }
        public string NombObra { get; set; } = "";
        public string NombBase { get; set; } = "";
        public int IdBase { get; set; }
        public string NombAlmacen { get; set; } = "";

        public IList<SolicitudDetalleDTO> Detalle;
        public IList<AnexoDTO> AnexoDetalle { get; set; }
        public List<SolicitudRQAnexos> DetalleAnexo { get; set; }
        public IList<SolicitudRQAnexos> DetallesAnexo;

        public IList<SolicitudRQModeloDTO> ListSolicitudRqModelo;
        public int EstadoSolicitud { get; set; } = 1;

        public string NombMoneda { get; set; } = "";
        public string OCRelacionadas { get; set; } = "";
        public decimal TotalCantidad { get; set; } = 0;
        public int Usuario { get; set; } = 0;
        public bool EsSubContrato { get; set; }

    }


    public class SolicitudRQAnexos
    {
        public int IdSolicitudRQAnexos { get; set; }
        public int IdSolicitud { get; set; }
        public string Nombre { get; set; }
        public int IdSociedad { get; set; }
    }


    public class DetalleSolicitudRqAprobacionDTO
    {
        public int IdArticulo { get; set; }
        public decimal PrecioInfo { get; set; }
        public int IdSolicitud { get; set; }

        public string ObraDescripcion { get; set; }
        public string NumeroPedido { get; set; }

        public string TipoProducto { get; set; }

        public int IdClaseArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public int Numero { get; set; }
        public decimal CantidadNecesaria { get; set; }
        public int Accion { get; set; }
        public int IdSolicitudRQModelo { get; set; }
        public int IdSolicitudRQDetalle { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoProducto { get; set; } = 1;
        public string NombUsuario { get; set; } = "";
        public string Serie { get; set; } = "";
        public string Referencia { get; set; } = "";
    }
}
