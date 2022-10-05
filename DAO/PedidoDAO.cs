using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class PedidoDAO
    {


        public List<PedidoDTO> ObtenerPedido(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedido", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PedidoDTO oPedidoDTO = new PedidoDTO();
                        oPedidoDTO.IdPedido = Convert.ToInt32(drd["IdPedido"].ToString());
                        oPedidoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oPedidoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oPedidoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oPedidoDTO.Direccion = (drd["Direccion"].ToString());
                        oPedidoDTO.Telefono = (drd["Telefono"].ToString());
                        oPedidoDTO.FechaEntrega = Convert.ToDateTime(drd["FechaEntrega"].ToString());
                        oPedidoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oPedidoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oPedidoDTO.IdTipoPedido = Convert.ToInt32(drd["IdTipoPedido"].ToString());
                        oPedidoDTO.LugarEntrega = (drd["LugarEntrega"].ToString());
                        oPedidoDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oPedidoDTO.ElaboradoPor = Convert.ToInt32(drd["ElaboradoPor"].ToString());
                        oPedidoDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oPedidoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oPedidoDTO.Observacion = (drd["Observacion"].ToString());
                        oPedidoDTO.Serie = Convert.ToInt32(drd["Serie"].ToString());
                        oPedidoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oPedidoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oPedidoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oPedidoDTO.NombBase =(drd["NombBase"].ToString());
                        oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                        oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                        oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                        oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal(drd["total_venta"].ToString());


                        lstPedidoDTO.Add(oPedidoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstPedidoDTO;
        }



        public List<PedidoDTO> ObtenerPedidosEntregaLDT(int IdSociedad, ref string mensaje_error, string EstadoPedido = "O")
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerPedidosEntregaLDT", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", EstadoPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PedidoDTO oPedidoDTO = new PedidoDTO();
                        oPedidoDTO.IdPedido = Convert.ToInt32(drd["IdPedido"].ToString());
                        oPedidoDTO.DT_RowId = Convert.ToInt32(drd["IdPedido"].ToString());

                        oPedidoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oPedidoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oPedidoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oPedidoDTO.Direccion = (drd["Direccion"].ToString());
                        oPedidoDTO.Telefono = (drd["Telefono"].ToString());
                        oPedidoDTO.FechaEntrega = Convert.ToDateTime(drd["FechaEntrega"].ToString());
                        oPedidoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oPedidoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oPedidoDTO.IdTipoPedido = Convert.ToInt32(drd["IdTipoPedido"].ToString());
                        oPedidoDTO.LugarEntrega = (drd["LugarEntrega"].ToString());
                        oPedidoDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oPedidoDTO.ElaboradoPor = Convert.ToInt32(drd["ElaboradoPor"].ToString());
                        oPedidoDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oPedidoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oPedidoDTO.Observacion = (drd["Observacion"].ToString());
                        oPedidoDTO.Serie = Convert.ToInt32(drd["Serie"].ToString());
                        oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oPedidoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oPedidoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oPedidoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oPedidoDTO.NombBase = (drd["NombBase"].ToString());
                        oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                        oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                        oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                        oPedidoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oPedidoDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal(drd["total_venta"].ToString());
                        oPedidoDTO.CantidadDisponible = Convert.ToDecimal(drd["CantidadDisponible"].ToString());

                        lstPedidoDTO.Add(oPedidoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }


           
            return lstPedidoDTO;
        }


        public List<PedidoDetalleDTO> ObtenerDetallePedido(int IdPedido, ref string mensaje_error)
        {
            List<PedidoDetalleDTO> lstPedidoDetalleDTO = new List<PedidoDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidoDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PedidoDetalleDTO oPedidoDetalleDTO = new PedidoDetalleDTO();
                        oPedidoDetalleDTO.DT_RowId = Convert.ToInt32(drd["IdPedidoDetalle"].ToString());
                        oPedidoDetalleDTO.IdPedidoDetalle = Convert.ToInt32(drd["IdPedidoDetalle"].ToString());
                        oPedidoDetalleDTO.IdPedido = Convert.ToInt32(drd["IdPedido"].ToString());
                        oPedidoDetalleDTO.DescripcionArticulo =(drd["DescripcionArticulo"].ToString());

                        oPedidoDetalleDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oPedidoDetalleDTO.IdGrupoArticulo = Convert.ToInt32(drd["IdGrupoArticulo"].ToString());
                        oPedidoDetalleDTO.IdDefinicion = Convert.ToInt32(drd["IdDefinicion"].ToString());
                        oPedidoDetalleDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oPedidoDetalleDTO.CantidadObtenida = Convert.ToDecimal(drd["CantidadObtenida"].ToString());
                        oPedidoDetalleDTO.valor_unitario = Convert.ToDecimal(drd["valor_unitario"].ToString());
                        oPedidoDetalleDTO.precio_unitario = Convert.ToDecimal(drd["precio_unitario"].ToString());
                        oPedidoDetalleDTO.total_base_igv = Convert.ToDecimal(drd["total_base_igv"].ToString());
                        oPedidoDetalleDTO.porcentaje_igv = Convert.ToDecimal(drd["porcentaje_igv"].ToString());
                        oPedidoDetalleDTO.total_igv = Convert.ToDecimal(drd["total_igv"].ToString());
                        oPedidoDetalleDTO.total_impuestos = Convert.ToDecimal(drd["total_impuestos"].ToString());
                        oPedidoDetalleDTO.total_valor_item = Convert.ToDecimal(drd["total_valor_item"].ToString());
                        oPedidoDetalleDTO.total_item = Convert.ToDecimal(drd["total_item"].ToString());
                        oPedidoDetalleDTO.IdIndicadorImpuesto = Convert.ToInt32(drd["IdIndicadorImpuesto"].ToString());
                        oPedidoDetalleDTO.CodImpuesto = (drd["CodImpuesto"].ToString());
                        oPedidoDetalleDTO.NombImpuesto = (drd["NombImpuesto"].ToString());
                        oPedidoDetalleDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        lstPedidoDetalleDTO.Add(oPedidoDetalleDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstPedidoDetalleDTO;
            }
        
        }

        
        public List<ArticuloStockDTO> ObtenerStockxIdDetalleSolicitudRQ(int IdDetalleRQ, ref string mensaje_error)
        {
            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerStockxIdDetalleSolicitudRQ", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdDetalleRQ", IdDetalleRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloStockDTO oArticuloStockDTO = new ArticuloStockDTO();
                        oArticuloStockDTO.IdArticuloStock = Convert.ToInt32(drd["IdArticuloStock"].ToString());
                        oArticuloStockDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oArticuloStockDTO.Stock = Convert.ToDecimal(drd["Stock"].ToString());
                        oArticuloStockDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oArticuloStockDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oArticuloStockDTO.NombArticulo = (drd["NombArticulo"].ToString());
                        lstArticuloStockDTO.Add(oArticuloStockDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstArticuloStockDTO;
            }

        }
        public List<ItemAprobadosDTO> ListarItemAprobadosxSociedad(int IdSociedad, ref string mensaje_error)
        {
            List<ItemAprobadosDTO> lstItemAprobadosDTO = new List<ItemAprobadosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProductosAprobados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ItemAprobadosDTO oItemAprobadosDTO = new ItemAprobadosDTO();
                        oItemAprobadosDTO.DT_RowId = Convert.ToInt32(drd["IdDetalle"].ToString());
                        oItemAprobadosDTO.IdDetalle = Convert.ToInt32(drd["IdDetalle"].ToString());
                        oItemAprobadosDTO.NombArticulo = (drd["NombArticulo"].ToString());
                        oItemAprobadosDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oItemAprobadosDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oItemAprobadosDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oItemAprobadosDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oItemAprobadosDTO.NombObra = (drd["NombObra"].ToString());
                        oItemAprobadosDTO.NumeroSolicitud = (drd["NumeroSolicitud"].ToString());
                        oItemAprobadosDTO.NombProveedor = (drd["NombProveedor"].ToString());
                        oItemAprobadosDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());


                        lstItemAprobadosDTO.Add(oItemAprobadosDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstItemAprobadosDTO;
            }

        }

        
        public List<ProveedoresPrecioProductoDTO> ObtenerProveedoresPrecioxProducto(int IdArticulo, ref string mensaje_error)
        {
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerProveedoresPrecioxProducto", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedoresPrecioProductoDTO oProveedoresPrecioProductoDTO = new ProveedoresPrecioProductoDTO();
                        oProveedoresPrecioProductoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oProveedoresPrecioProductoDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oProveedoresPrecioProductoDTO.PrecioNacional = Convert.ToDecimal(drd["PrecioNacional"].ToString());
                        oProveedoresPrecioProductoDTO.PrecioExtranjero = Convert.ToDecimal(drd["PrecioExtranjero"].ToString());
                        oProveedoresPrecioProductoDTO.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        lstProveedoresPrecioProductoDTO.Add(oProveedoresPrecioProductoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstProveedoresPrecioProductoDTO;
            }
        }


        
        public List<AsignadoPedidoRequeridoDTO> ListarProductosAsignadosxProveedorxIdUsuario(int IdUsuario, ref string mensaje_error)
        {
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProductosAsignadosxProveedorxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AsignadoPedidoRequeridoDTO oAsignadoPedidoRequeridoDTO = new AsignadoPedidoRequeridoDTO();
                        oAsignadoPedidoRequeridoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oAsignadoPedidoRequeridoDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdBase= Convert.ToInt32(drd["IdBase"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombObra = (drd["NombObra"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombBase = (drd["NombBase"].ToString());
                        lstAsignadoPedidoRequeridoDTO.Add(oAsignadoPedidoRequeridoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstAsignadoPedidoRequeridoDTO;
            }
        }


        
        public List<AsignadoPedidoRequeridoDTO> ListarProductosAsignadosxProveedorDetalle(int IdProveedor, ref string mensaje_error)
        {
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProductosAsignadosxProveedorDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AsignadoPedidoRequeridoDTO oAsignadoPedidoRequeridoDTO = new AsignadoPedidoRequeridoDTO();
                        oAsignadoPedidoRequeridoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oAsignadoPedidoRequeridoDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombObra = (drd["NombObra"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombBase = (drd["NombBase"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdUnidadMedida = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombArticulo = (drd["NombArticulo"].ToString());
                        oAsignadoPedidoRequeridoDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdAsignadoPedidoRequerimiento = Convert.ToInt32(drd["IdAsignadoPedidoRequerimiento"].ToString());
                        oAsignadoPedidoRequeridoDTO.Precio = Convert.ToDecimal(drd["Precio"].ToString());
                        lstAsignadoPedidoRequeridoDTO.Add(oAsignadoPedidoRequeridoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstAsignadoPedidoRequeridoDTO;
            }
        }


        public int UpdateInsertPedidoAsignadoPedidoRQ(int IdProveedor, decimal precionacional, decimal precioextranjero, int idproducto, int IdDetalleRq,ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertAsignadoPedidoRequerido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@precionacional", precionacional);
                        da.SelectCommand.Parameters.AddWithValue("@precioextranjero", precioextranjero);
                        da.SelectCommand.Parameters.AddWithValue("@idproducto", idproducto);
                        da.SelectCommand.Parameters.AddWithValue("@IdDetalleRq", IdDetalleRq);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return 0;
                    }

                }
            }
        }
        public int UpdateInsertPedido(PedidoDTO oPedidoDTO, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oPedidoDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oPedidoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oPedidoDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oPedidoDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", oPedidoDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@FechaEntrega", oPedidoDTO.FechaEntrega);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oPedidoDTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oPedidoDTO.FechaContabilizacion);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoPedido", oPedidoDTO.IdTipoPedido);
                        da.SelectCommand.Parameters.AddWithValue("@LugarEntrega", oPedidoDTO.LugarEntrega);
                        da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", oPedidoDTO.IdCondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@ElaboradoPor", oPedidoDTO.ElaboradoPor);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oPedidoDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oPedidoDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Observacion", oPedidoDTO.Observacion);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oPedidoDTO.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@Correlativo", oPedidoDTO.Correlativo);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oPedidoDTO.TipoCambio);

                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return 0;
                    }

                }
            }
        }


        #region InsertUpdatePedidoDetalle
        public int InsertUpdatePedidoDetalle(PedidoDetalleDTO oPedidoDetalleDTO, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPedidoDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedidoDetalle", oPedidoDetalleDTO.IdPedidoDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoDetalleDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionArticulo", oPedidoDetalleDTO.DescripcionArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oPedidoDetalleDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoArticulo", oPedidoDetalleDTO.IdGrupoArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdDefinicion", oPedidoDetalleDTO.IdDefinicion);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", oPedidoDetalleDTO.Cantidad);
                        da.SelectCommand.Parameters.AddWithValue("@valor_unitario", oPedidoDetalleDTO.valor_unitario);
                        da.SelectCommand.Parameters.AddWithValue("@precio_unitario", oPedidoDetalleDTO.precio_unitario);
                        //da.SelectCommand.Parameters.AddWithValue("@codigo_tipo_afectacion_igv", oPedidoDetalleDTO.codigo_tipo_afectacion_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_base_igv", oPedidoDetalleDTO.total_base_igv);
                        da.SelectCommand.Parameters.AddWithValue("@porcentaje_igv", oPedidoDetalleDTO.porcentaje_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_igv", oPedidoDetalleDTO.total_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_impuestos", oPedidoDetalleDTO.total_impuestos);
                        da.SelectCommand.Parameters.AddWithValue("@total_valor_item", oPedidoDetalleDTO.total_valor_item);
                        da.SelectCommand.Parameters.AddWithValue("@total_item", oPedidoDetalleDTO.total_item);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oPedidoDetalleDTO.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oPedidoDetalleDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@DescOrigen", oPedidoDetalleDTO.DescOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@IdOrigen", oPedidoDetalleDTO.IdOrigen);






                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }
        #endregion



        #region UpdateTotalesPedido
        public int UpdateTotalesPedido(int  IdPedido, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar())
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateTotalesPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }
        #endregion


        public PedidoDTO ObtenerPedidoxId(int IdPedido, ref string mensaje_error)
        {
            PedidoDTO oPedidoDTO = new PedidoDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_PedidoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oPedidoDTO.IdPedido = Convert.ToInt32(drd["IdPedido"].ToString());
                        oPedidoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oPedidoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oPedidoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oPedidoDTO.Direccion = (drd["Direccion"].ToString());
                        oPedidoDTO.Telefono = (drd["Telefono"].ToString());
                        oPedidoDTO.FechaEntrega = Convert.ToDateTime(drd["FechaEntrega"].ToString());
                        oPedidoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oPedidoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oPedidoDTO.IdTipoPedido = Convert.ToInt32(drd["IdTipoPedido"].ToString());
                        oPedidoDTO.LugarEntrega = (drd["LugarEntrega"].ToString());
                        oPedidoDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oPedidoDTO.ElaboradoPor = Convert.ToInt32(drd["ElaboradoPor"].ToString());
                        oPedidoDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oPedidoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oPedidoDTO.Observacion = (drd["Observacion"].ToString());
                        oPedidoDTO.Serie = Convert.ToInt32(drd["Serie"].ToString());
                        oPedidoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oPedidoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oPedidoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oPedidoDTO.NombBase = (drd["NombBase"].ToString());
                        oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                        oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                        oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                        oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal(drd["total_venta"].ToString());
                        oPedidoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oPedidoDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oPedidoDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oPedidoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());





                    }
                    drd.Close();

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }

            #region Contar Detalle 
            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidoDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalle++;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error += ex.Message.ToString();
                }
            }

           oPedidoDTO.detalles = new PedidoDetalleDTO[filasdetalle];

            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidoDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr2 = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (dr2.Read())
                    {
                        PedidoDetalleDTO oPedidoDetalleDTO = new PedidoDetalleDTO();
                        oPedidoDetalleDTO.DT_RowId = Convert.ToInt32(dr2["IdPedidoDetalle"].ToString());
                        oPedidoDetalleDTO.IdPedidoDetalle = Convert.ToInt32(dr2["IdPedidoDetalle"].ToString());
                        oPedidoDetalleDTO.IdPedido = Convert.ToInt32(dr2["IdPedido"].ToString());
                        oPedidoDetalleDTO.DescripcionArticulo = (dr2["DescripcionArticulo"].ToString());

                        oPedidoDetalleDTO.IdArticulo = Convert.ToInt32(dr2["IdArticulo"].ToString());
                        oPedidoDetalleDTO.IdGrupoArticulo = Convert.ToInt32(dr2["IdGrupoArticulo"].ToString());
                        oPedidoDetalleDTO.IdDefinicion = Convert.ToInt32(dr2["IdDefinicion"].ToString());
                        oPedidoDetalleDTO.Cantidad = Convert.ToDecimal(dr2["Cantidad"].ToString());
                        oPedidoDetalleDTO.CantidadObtenida = Convert.ToDecimal(dr2["CantidadObtenida"].ToString());
                        oPedidoDetalleDTO.valor_unitario = Convert.ToDecimal(dr2["valor_unitario"].ToString());
                        oPedidoDetalleDTO.precio_unitario = Convert.ToDecimal(dr2["precio_unitario"].ToString());
                        oPedidoDetalleDTO.total_base_igv = Convert.ToDecimal(dr2["total_base_igv"].ToString());
                        oPedidoDetalleDTO.porcentaje_igv = Convert.ToDecimal(dr2["porcentaje_igv"].ToString());
                        oPedidoDetalleDTO.total_igv = Convert.ToDecimal(dr2["total_igv"].ToString());
                        oPedidoDetalleDTO.total_impuestos = Convert.ToDecimal(dr2["total_impuestos"].ToString());
                        oPedidoDetalleDTO.total_valor_item = Convert.ToDecimal(dr2["total_valor_item"].ToString());
                        oPedidoDetalleDTO.total_item = Convert.ToDecimal(dr2["total_item"].ToString());
                        oPedidoDetalleDTO.IdIndicadorImpuesto = Convert.ToInt32(dr2["IdIndicadorImpuesto"].ToString());
                        oPedidoDetalleDTO.CodImpuesto = (dr2["CodImpuesto"].ToString());
                        oPedidoDetalleDTO.NombImpuesto = (dr2["NombImpuesto"].ToString());
                        oPedidoDetalleDTO.IdGrupoUnidadMedida = Convert.ToInt32(dr2["IdGrupoUnidadMedida"].ToString());
              
                        oPedidoDTO.detalles[posicion] = oPedidoDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
                #endregion

                return oPedidoDTO;
            }
            
        }

    }
}
