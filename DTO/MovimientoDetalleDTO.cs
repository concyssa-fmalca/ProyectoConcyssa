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
        public int DescripcionArticulo { get; set; }
        public int IdUnidadMedidad { get; set; }
        public int IdAlmacen { get; set; }
        public int Cantidad { get; set; }
        public int Igv { get; set; }
        public int PrecioUnidadBase { get; set; }
        public int PrecioUnidadTotal { get; set; }
        public int TotalBase { get; set; }
        public int Total { get; set; }
        public int CuentaContable { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdAfectacionIgv { get; set; }
        public int Descuento { get; set; }
    }
}
