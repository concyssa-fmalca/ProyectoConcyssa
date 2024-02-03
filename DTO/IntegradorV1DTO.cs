using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IntegradorV1DTO
    {
        public int IdEnviarSap { get; set; }
        public int IdTablaOriginal { get; set; }
        public int IdTipoDocumento { get; set; }

        public string ObjType { get; set; }

        public int IdMoneda { get; set; }

        public string CodMoneda { get; set; }
        public string RUCProveedor { get; set; }
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
        public string NombTipoDocumentoOperacion { get; set; }

        public string NombSerie { get; set; }
        public int IdCuadrilla { get; set; }
        public int IdAlmacenDestino { get; set; }
        public int IdResponsable { get; set; }
        public int IdTipoDocumentoRef { get; set; }
        public string NumSerieTipoDocumentoRef { get; set; }
        public string CuentaContableDivision { get; set; }
        public int EntregadoA { get; set; }
        public string TablaOriginal { get; set; }

        public int IdBase { get; set; }
        public int IdObra { get; set; }

        public int IdUsuario { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NombUsuario { get; set; }

        public string NombAlmacen { get; set; }
        public string DescCuadrilla { get; set; }
        public string NombObra { get; set; }
        public int IdProveedor { get; set; }
        public int idCondicionPago { get; set; }
        public int IdTipoRegistro { get; set; }
        public int IdSemana { get; set; }
        public int IdGlosaContable { get; set; }
        public string Moneda { get; set; }
        public string NombUsuarioEdicion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public string TablaOrigen { get; set; }
        public string IdOrigen { get; set; }
        public int IdDocExtorno { get; set; }
        public bool Inventario { get; set; }
        public string NumOC { get; set; }
        public decimal ConsumoM3 { get; set; }
        public decimal ConsumoHW { get; set; }
        public int TasaDetraccion { get; set; }
        public int GrupoDetraccion { get; set; }
        public string SerieDocBaseORPC { get; set; }
        public int SapDocNum { get; set; }
        public int DocEntry { get; set; }
        public int EnviadoPor { get; set; }
        public int SerieSAP { get; set; }
        public int CondicionPagoDet { get; set; }
        public IList<IntegradorV1Detalle> detalles { get; set; }
        public IList<AnexoDTO> AnexoDetalle { get; set; }
    }
    public class ListaTrabajoDTO
    {
        public int GrupoCreacion { get; set; }
        public int UsuarioCreacionTabla { get; set; }
        public DateTime FechaCreacionTabla { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string EstadoEnviado { get; set; }
    }
    public class IntegradorV1Detalle
    {
        public int IdEnviarSapDetalle { get; set; }
        public int IdEnviarSap { get; set; }
        public int IdTablaOriginalDetalle { get; set; }
        public int IdTablaOriginal { get; set; }
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
        public int IdCuadrilla { get; set; }
        public int IdResponsable { get; set; }
        public string NombCuadrilla { get; set; }
        public string NombResponsable { get; set; }
        public string TipoServicio { get; set; }
        public int NCIdOPCHDetalle { get; set; }
        public int DocEntrySap { get; set; }
        public int Resta { get; set; }

    }

    public class IntegradorTasaDetDTO
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
    public class IntegradorGrupoDetDTO
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string U_Descripcion { get; set; }
    }
    public class IntegradorCondPagoDetDTO
    {
        public int GroupNum { get; set; }
        public string PymntGroup { get; set; }
        public int ExtraDays { get; set; }
        public int ExtraMonth { get; set; }
    }
    public class IntegradorSerieSapDTO
    {
        public int ObjectCode { get; set; }
        public int Series { get; set; }
        public string SeriesName { get; set; }
    }
    public class IntegradorClasif
    {
        public int Code { get; set; }
        public string Name { get; set; }


    }
    public class IntegradorTipoDocumento
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Prefijo { get; set; }
    }
    public class IntegradorProveedorDTO
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string CardType { get; set; }
        public int GroupCode { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string FederalTaxID { get; set; }
        public string Currency { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string SubjectToWithholdingTax { get; set; }
        public string BilltoDefault { get; set; }
        public string U_EXX_TIPOPERS { get; set; }
        public int U_EXX_TIPODOCU { get; set; }
        public string U_EXX_APELLPAT { get; set; }
        public string U_EXX_APELLMAT { get; set; }
        public string U_EXX_PRIMERNO { get; set; }
        public string U_EXX_SEGUNDNO { get; set; }
        public string Properties15 { get; set; }
        public int GroupNum { get; set; }
        public string WTLiable { get; set; }
    }


}
