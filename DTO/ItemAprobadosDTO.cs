namespace DTO
{
    public class ItemAprobadosDTO
    {
        public int DT_RowId { get; set; }
        public int IdDetalle { get; set; }
        public int IdArticulo { get; set; }

        public string NombArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public int IdUnidadMedidaInv { get; set; }
        public string NombUnidadMedida { get; set; }
        public int IdAlmacen { get; set; }
        public string NombAlmacen { get; set; }
        public int IdObra { get; set; }
        public string NombObra {get;set;}
        public string NombProveedor { get; set; }
        public int IdProveedor { get; set; }
        public string NumeroSolicitud { get; set; }
        public DateTime Fecha { get; set; }
        public string Referencia { get; set; }

        public string TipoServicio { get; set; }

    }
}
