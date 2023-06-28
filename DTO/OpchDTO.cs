using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class OpchDTO
    {
        public int DT_RowId { get; set; }
        public int IdOPCH { get; set; }
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
        public decimal Redondeo { get; set; }
        public int IdTipoAfectacionIgv { get; set; }
        public decimal Total { get; set; }
        public int IdAlmacen { get; set; }
        public int IdSerie { get; set; }
        public int Correlativo { get; set; }
        public int IdSociedad { get; set; }
        public bool Estado { get; set; }
        public bool Eliminado { get; set; }
        public string TipoDocumentoRef { get; set; }
        public string Proveedor { get; set; }


        public IList<OPCHDetalle> detalles { get; set; }



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

        public int IdUsuario { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NombUsuario { get; set; }

        public string NombAlmacen { get; set; }
        public string DescCuadrilla { get; set; }
        public string NombObra { get; set; }
        public int IdProveedor { get; set; }
        public int idCondicionPago {get;set;}
        public int IdTipoRegistro {get;set;}
        public int IdSemana {get;set;}
        public int IdGlosaContable {get;set;}
        public string Moneda {get;set;}

        public IList<AnexoDTO> AnexoDetalle { get; set; }

    }

    public class OPCHDetalle
    {
        public int DT_RowId { get; set; }
        public int IdOPCHDetalle { get; set; }
        public int IdOPCH { get; set; }
        public int IdArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public int IdAlmacen { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Igv { get; set; }
        public decimal PrecioUnidadBase { get; set; }
        public decimal PrecioUnidadTotal { get; set; }
        public decimal TotalBase { get; set; }
        public decimal Total { get; set; }
        public int CuentaContable { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdAfectacionIgv { get; set; }
        public decimal Descuento { get; set; }
        public int IdDefinicionGrupoUnidad { get; set; }



        public int IdUnidadMedidaBase { get; set; }
        public int IdUnidadMedida { get; set; }
        public decimal CantidadBase { get; set; }
        public int IdAlmacenDestino { get; set; }


        public decimal valor_unitario { get; set; }
        public decimal precio_unitario { get; set; }
        public string codigo_tipo_afectacion_igv { get; set; }
        public decimal total_base_igv { get; set; }
        public decimal porcentaje_igv { get; set; }
        public decimal total_igv { get; set; }
        public decimal total_impuestos { get; set; }
        public decimal total_valor_item { get; set; }
        public decimal total_item { get; set; }
        public int IdIndicadorImpuesto { get; set; }
        public string CodImpuesto { get; set; }
        public string NombImpuesto { get; set; }
        public string Referencia { get; set; }
        public int IdGrupoUnidadMedida { get; set; }

        public string NombTablaOrigen { get; set; }
        public int IdOrigen { get; set; }

        public decimal CantidadUsada { get; set; }
        public decimal CantidadNotaCredito { get; set; }
        public string CodigoArticulo { get; set; }


    }
}
