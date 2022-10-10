using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MovimientoDetalleDTO
    {
        public int IdMovimientoDetalle { get; set; }
        public int IdMovimiento { get; set; }
        public int IdArticulo { get; set; }
#pragma warning disable CS8618 // El elemento propiedad "DescripcionArticulo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public string DescripcionArticulo { get; set; }
#pragma warning restore CS8618 // El elemento propiedad "DescripcionArticulo" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
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
        public string Referencia { get; set; }
    }
}
