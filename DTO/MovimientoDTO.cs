using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MovimientoDTO
    {
        public int existeObraDestinoUsuario { get; set; }
        public int DT_RowId { get; set; }
        public int IdMovimiento { get; set; }
        public int IdTipoDocumento { get; set; }

        public string ObjType { get; set; }

        public int IdMoneda { get; set; }

        public string CodMoneda { get; set; }

        public decimal TipoCambio { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdListaPrecios { get; set; }

        public string Referencia { get; set; }

        public string Comentario { get; set; }

        public int DocEntrySap { get; set; }

        public string DocNumSap { get; set; }

        public int IdCentroCosto { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public int IdTipoAfectacionIgv { get; set; }
        public decimal Total { get; set; }
        public int IdAlmacen { get; set; }
        public int IdSerie { get; set; }
        public int Correlativo { get; set; }
        public int IdSociedad { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public int IdDocExtorno { get; set; }
        public string TDocumento { get; set; }


        public IList<MovimientoDetalleDTO> detalles  { get; set; }

        public IList<AnexoDTO> AnexoDetalle { get; set; }



        //Campos Adicionales

        public string NombTipoDocumentoOperacion { get; set; }


        public string NombSerie { get; set; }
        public int IdCuadrilla { get; set; }
        public int IdAlmacenDestino { get; set; }
        public int IdResponsable { get; set; }
        public int IdTipoDocumentoRef { get; set; }
        public string NumSerieTipoDocumentoRef { get; set; }
        public int EntregadoA { get; set; }

        public int IdBase { get; set; }
        public int IdObra { get; set; }
        public int IdObraDestino { get; set; }
        public int IdUsuario { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NombUsuario { get; set; }

        public string NombAlmacen { get; set; }
        public string DescCuadrilla { get; set; }
        public string NombObra { get; set; }

        public int IdDestinatario { get; set; }=0;
        public int IdMotivoTraslado { get; set; } = 0;
        public int IdTransportista { get; set; } = 0;

        public string PlacaVehiculo { get; set; } = "";
        public string MarcaVehiculo { get; set; } = "";
        public string NumIdentidadConductor { get; set; } = "";
        public string NombreConductor { get; set; } = "";
        public string ApellidoConductor { get; set; } = "";
        public string LicenciaConductor { get; set; } = "";
        public string TipoTransporte { get; set; } = "";

        public string DireccionPartida { get; set; } = "";
        public string CodigoUbigeoPartida { get; set; } = "";
        public string CodigoAnexoPartida { get; set; } = "";

        public string DireccionLlegada { get; set; } = "";
        public string CodigoUbigeoLlegada { get; set; } = "";
        public string CodigoAnexoLlegada { get; set; } = "";

        public string SerieGuiaElectronica { get; set; } = "";
        public int NumeroGuiaElectronica { get; set; } = 0;

        public int EstadoFE { get; set; } = 0;

        public decimal Peso { get; set; } = 0;
        public decimal Bulto { get; set; } = 0;

        public string NumDocumentoDestinatario { get; set; } = "";
        public string NombDestinatario { get; set; } = "";
        public string NumDocumentoTransportista { get; set; } = "";
        public string NombTransportista { get; set; } = "";

        public string CodigoMotivoTrasladoSunat { get; set; } = "";
        public string DescripcionMotivoTrasladoSunat { get; set; } = "";


        public int ValidarIngresoSalidaOAmbos { get; set; }

        public int TranferenciaDirecta { get; set; }
        public string NombMoneda { get; set; }
        public string NombUsuarioEdicion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public string SGI { get; set; }
        public string DistritoLlegada { get; set; }

    }


    public class MovimientoTranferenciaFinal
    {
        public int IdMovimiento { get; set; }
        public string AlmacenInicial { get; set; }
        public string AlmacenDestino { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string Moneda { get; set; }
        public string Cuadrilla { get; set; }
        public string UsuarioCreador { get; set; }
        public string SerieCorrelativo { get; set; }
        public string Guia { get; set; }
        public string NomObraOrigen { get; set; }
        public string NomObraDestino { get; set; }
    }



}
