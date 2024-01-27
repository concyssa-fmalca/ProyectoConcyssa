using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ExcelDTO
    {
        public string Codigo { get; set; }
        public string Articulo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class ExcelArticuloDTO
    {
        public string Codigo { get; set; }
        public string Descripcion1 { get; set; }
        public string Descripcion2 { get; set; }
        public bool ArticuloInventario { get; set; }
        public bool ArticuloCompra { get; set; }
        public bool ArticuloVenta { get; set; }
        public bool ActivoFijo { get; set; }
        public bool ActivoCatalogo { get; set; }
        public int GrupoUnidadMedidaBase { get; set; }
        public int UnidadMedida { get; set; }
        public int Construccion { get; set; }
        public int Servicio { get; set; }
        public int TipoProducto { get; set; }

    }

    public class ExcelPrecioProvDTO
    {
        public string CodigoProducto { get; set; }
        public string RUC { get; set; }
        public decimal PrecioExtranjero { get; set; }
        public decimal PrecioNacional { get; set; }
        public string CodigoObra { get; set; }
        public int DiasEntrega { get; set; }

    }

    public class ProveedorExcelDTO
    {
        public string Codigo { get; set; }
        public string NroDoc { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public int CondPagoSAp { get; set; }
        public int CondicionPago { get; set; }
    }
}
