using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KardexDTO
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
        public string Cuadrilla { get; set; }
        public string NumRef { get; set; }
        public int DocEntrySap { get; set; }

    }
    public class KardexTributario
    {
        public string CAMPO1 { get; set; }
        public string CAMPO2 { get; set; }
        public string CAMPO3 { get; set; }
        public string CAMPO4 { get; set; }
        public string CAMPO5 { get; set; }
        public string CAMPO6 { get; set; }
        public string CAMPO7 { get; set; }
        public string CAMPO8 { get; set; }
        public string CAMPO9 { get; set; }
        public string CAMPO10 { get; set; }
        public string CAMPO11 { get; set; }
        public string CAMPO12 { get; set; }
        public string CAMPO13 { get; set; }
        public string CAMPO14 { get; set; }
        public string CAMPO15 { get; set; }
        public string CAMPO16 { get; set; }
        public string CAMPO17 { get; set; }
        public decimal CAMPO18 { get; set; }
        public decimal CAMPO19 { get; set; }
        public decimal CAMPO20 { get; set; }
        public decimal CAMPO21 { get; set; }
        public decimal CAMPO22 { get; set; }
        public decimal CAMPO23 { get; set; }
        public decimal CAMPO24 { get; set; }
        public decimal CAMPO25 { get; set; }
        public decimal CAMPO26 { get; set; }
        public int CAMPO27 { get; set; }
        public string CAMPO28 { get; set; }
        public int IdAlmacen { get; set; }
        public int IdArticulo { get; set; }
        public int IdKardex { get; set; }
        public string FechaContabilizacion { get; set; }
        public string FechaRegistro { get; set; }
        public string TipoArticulo { get; set; }
        public string NombreContrato { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string DescripcionMovimiento { get; set; }

    }
    public class GrupoKardexTributarioDTO
    {
        public int IDAlmacen { get; set; }
        public int IDArticulo { get; set; }
        public List<KardexTributario> Kardex { get; set; }
    }
}
