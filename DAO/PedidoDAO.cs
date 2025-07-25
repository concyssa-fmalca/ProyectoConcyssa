using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Globalization;

namespace DAO
{
    public class PedidoDAO
    {
        public List<PedidoDTO> ObtenerPedidoxEstadoConformidad(int IdSociedad, string BaseDatos, ref string mensaje_error, int Conformidad = 0)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidoxEstadoCoformidad", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Conformidad", Conformidad);
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
                        oPedidoDTO.NombBase = (drd["NombBase"].ToString());
                        oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                        oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                        oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                        oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oPedidoDTO.Conformidad = Convert.ToInt32((String.IsNullOrEmpty(drd["Conformidad"].ToString())) ? 0 : drd["Conformidad"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal((String.IsNullOrEmpty(drd["total_venta"].ToString())) ? "0" : drd["total_venta"].ToString());
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

        public List<PedidoDTO> ObtenerPedidoxEstadoConformidadAutorizacion(int IdUsuario, string BaseDatos, ref string mensaje_error, int Accion = 0)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    int aprobar = 0;
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidoAutorizar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@Accion", Accion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        aprobar = 0;
                        aprobar = ValidarSipuedeAprobar(Convert.ToInt32(drd["IdPedido"].ToString()), Convert.ToInt32(drd["IdEtapa"].ToString()), BaseDatos);
                        if (aprobar > 0)
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
                            oPedidoDTO.NombBase = (drd["NombBase"].ToString());
                            oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                            oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                            oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                            oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                            oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                            oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                            oPedidoDTO.Conformidad = Convert.ToInt32((String.IsNullOrEmpty(drd["Conformidad"].ToString())) ? 0 : drd["Conformidad"].ToString());
                            oPedidoDTO.total_venta = Convert.ToDecimal((String.IsNullOrEmpty(drd["total_venta"].ToString())) ? "0" : drd["total_venta"].ToString());
                            oPedidoDTO.Accion = Convert.ToInt32(drd["Accion"].ToString());
                            oPedidoDTO.IdModelo = Convert.ToInt32(drd["IdModelo"].ToString());
                            lstPedidoDTO.Add(oPedidoDTO);
                        }
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


        public List<PedidoDTO> ObtenerPedidoDTCorreoProveedor(int IdSociedad, int EnvioCorreo, int Proveedor, string BaseDatos, ref string mensaje_error)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidoCorreoProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EnvioCorreo", EnvioCorreo);
                    da.SelectCommand.Parameters.AddWithValue("@Proveedor", Proveedor);
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
                        oPedidoDTO.NombBase = (drd["NombBase"].ToString());
                        oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                        oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                        oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                        oPedidoDTO.EmailProveedor = (drd["EmailProveedor"].ToString());
                        oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oPedidoDTO.Conformidad = Convert.ToInt32((String.IsNullOrEmpty(drd["Conformidad"].ToString())) ? 0 : drd["Conformidad"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal((String.IsNullOrEmpty(drd["total_venta"].ToString())) ? "0" : drd["total_venta"].ToString());


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
        public List<PedidoDTO> ObtenerPedido(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oPedidoDTO.NombBase = (drd["NombBase"].ToString());
                        oPedidoDTO.NombObra = (drd["NombObra"].ToString());
                        oPedidoDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oPedidoDTO.NombreProveedor = (drd["NombreProveedor"].ToString());
                        oPedidoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oPedidoDTO.NombTipoPedido = (drd["NombTipoPedido"].ToString());
                        oPedidoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oPedidoDTO.Conformidad = Convert.ToInt32((String.IsNullOrEmpty(drd["Conformidad"].ToString())) ? 0 : drd["Conformidad"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal((String.IsNullOrEmpty(drd["total_venta"].ToString())) ? "0" : drd["total_venta"].ToString());
                        oPedidoDTO.EstadoOC = Convert.ToInt32(drd["EstadoOC"].ToString());


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



        public List<PedidoDTO> ObtenerPedidosEntregaLDT(int IdSociedad, string BaseDatos,int IdProveedor, ref string mensaje_error, string EstadoPedido = "O", int IdObra=0,int IdUsuario=0)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerPedidosEntregaLDT", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", EstadoPedido);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
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
                       
                        oPedidoDTO.total_venta = Convert.ToDecimal((String.IsNullOrEmpty(drd["total_venta"].ToString())) ? "0" : drd["total_venta"].ToString());
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


        public List<PedidoDetalleDTO> ObtenerDetallePedido(int IdPedido, string BaseDatos, ref string mensaje_error)
        {
            List<PedidoDetalleDTO> lstPedidoDetalleDTO = new List<PedidoDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oPedidoDetalleDTO.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());

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
                        oPedidoDetalleDTO.CodigoProducto = (drd["CodigoProducto"].ToString());
                        oPedidoDetalleDTO.TipoServicio = drd["TipoServicio"].ToString();
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


        public List<ArticuloStockDTO> ObtenerStockxIdDetalleSolicitudRQ(int IdDetalleRQ, string BaseDatos, ref string mensaje_error)
        {
            List<ArticuloStockDTO> lstArticuloStockDTO = new List<ArticuloStockDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oArticuloStockDTO.NombObra = (drd["NombObra"].ToString());
                        oArticuloStockDTO.Precio = decimal.Parse(drd["Precio"].ToString());
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
        public List<ItemAprobadosDTO> ListarItemAprobadosxSociedad(int IdSociedad, string BaseDatos, ref string mensaje_error)
        {
            List<ItemAprobadosDTO> lstItemAprobadosDTO = new List<ItemAprobadosDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oItemAprobadosDTO.IdSolicitud = Convert.ToInt32(drd["IdSolicitud"].ToString());
                        oItemAprobadosDTO.NombArticulo = (drd["NombArticulo"].ToString());
                        oItemAprobadosDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oItemAprobadosDTO.IdUnidadMedidaInv = Convert.ToInt32(drd["IdUnidadMedidaInv"].ToString());
                        oItemAprobadosDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oItemAprobadosDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oItemAprobadosDTO.NombObra = (drd["NombObra"].ToString());
                        oItemAprobadosDTO.NumeroSolicitud = (drd["NumeroSolicitud"].ToString());
                        oItemAprobadosDTO.NombProveedor = (drd["NombProveedor"].ToString());
                        oItemAprobadosDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oItemAprobadosDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());
                        
                        oItemAprobadosDTO.IdProveedor = convertInt(drd["IdProveedor"].ToString());

                        oItemAprobadosDTO.NombUnidadMedida = (drd["UnidadMedida"].ToString());
                        oItemAprobadosDTO.Referencia = drd["Referencia"].ToString();
                        oItemAprobadosDTO.TipoServicio = drd["TipoServicio"].ToString();
                        oItemAprobadosDTO.Pendientes = Convert.ToInt32(drd["Pendientes"].ToString());


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
        private int convertInt(string? numero)
        {
            bool success = Int32.TryParse(numero, out int conversion);
            return success? conversion : 0;
        }

        public List<ProveedoresPrecioProductoDTO> ObtenerProveedoresPrecioxProducto(int IdArticulo, string BaseDatos, ref string mensaje_error)
        {
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
        public List<ProveedoresPrecioProductoDTO> ObtenerProveedoresPrecioxProductoConObras(int IdObra,int IdArticulo, string BaseDatos, ref string mensaje_error)
        {
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerProveedoresPrecioxProductoConObras", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedoresPrecioProductoDTO oProveedoresPrecioProductoDTO = new ProveedoresPrecioProductoDTO();
                        oProveedoresPrecioProductoDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oProveedoresPrecioProductoDTO.IdArticuloProveedor = Convert.ToInt32(drd["IdArticuloProveedor"].ToString());
                        oProveedoresPrecioProductoDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oProveedoresPrecioProductoDTO.PrecioNacional = Convert.ToDecimal(drd["PrecioNacional"].ToString());
                        oProveedoresPrecioProductoDTO.PrecioExtranjero = Convert.ToDecimal(drd["PrecioExtranjero"].ToString());
                        oProveedoresPrecioProductoDTO.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        oProveedoresPrecioProductoDTO.NombObra = (drd["NombObra"].ToString());

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
        public List<ProveedoresPrecioProductoDTO> ObtenerPrecioxProductoUltimasVentas(int IdArticulo, string BaseDatos, ref string mensaje_error)
        {
            List<ProveedoresPrecioProductoDTO> lstProveedoresPrecioProductoDTO = new List<ProveedoresPrecioProductoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerPrecioxProductoUltimasVentas", cn);
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

                        oProveedoresPrecioProductoDTO.Cantidad =int.Parse(drd["Cantidad"].ToString(), NumberStyles.AllowDecimalPoint);
                        oProveedoresPrecioProductoDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());
                        oProveedoresPrecioProductoDTO.Obra = Convert.ToString(drd["Obra"].ToString());
                        oProveedoresPrecioProductoDTO.Serie = Convert.ToString(drd["Serie"].ToString()+"-"+ drd["Correlativo"].ToString());
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



        public List<AsignadoPedidoRequeridoDTO> ListarProductosAsignadosxProveedorxIdUsuario(int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oAsignadoPedidoRequeridoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombObra = (drd["NombObra"].ToString());
                        oAsignadoPedidoRequeridoDTO.NombBase = (drd["NombBase"].ToString());
                        oAsignadoPedidoRequeridoDTO.DireccionObra = (drd["DireccionObra"].ToString());
                        oAsignadoPedidoRequeridoDTO.TipoItem = (drd["TipoItem"].ToString());
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



        public List<AsignadoPedidoRequeridoDTO> ListarProductosAsignadosxProveedorDetalle(int IdProveedor,int TipoItem,int IdObra, bool EsSubContrato, string BaseDatos, ref string mensaje_error)
        {
            List<AsignadoPedidoRequeridoDTO> lstAsignadoPedidoRequeridoDTO = new List<AsignadoPedidoRequeridoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProductosAsignadosxProveedorDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.Parameters.AddWithValue("@TipoItem", TipoItem);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@EsSubContrato", EsSubContrato);
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
                        oAsignadoPedidoRequeridoDTO.PrecioExtrangero = Convert.ToDecimal(drd["PrecioExtrangero"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oAsignadoPedidoRequeridoDTO.CodigoArticulo = (drd["CodigoProducto"].ToString());
                        oAsignadoPedidoRequeridoDTO.SerieCorrelativo = (drd["SerieCorrelativo"].ToString());
                        oAsignadoPedidoRequeridoDTO.IdSolicitud = Convert.ToInt32(drd["IdSolicitud"].ToString());
                        oAsignadoPedidoRequeridoDTO.TipoServicio = drd["TipoServicio"].ToString();
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


        public int UpdateInsertPedidoAsignadoPedidoRQ(int IdProveedor, decimal precionacional, decimal precioextranjero, int idproducto, int IdDetalleRq,string Comentario, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", Comentario);


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
        public int UpdateInsertPedido(PedidoDTO oPedidoDTO, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        da.SelectCommand.Parameters.AddWithValue("@EntregasConAlmacen", oPedidoDTO.EntregasConAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@EsSubContrato", oPedidoDTO.EsSubContrato);

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
        public int InsertUpdatePedidoDetalle(PedidoDetalleDTO oPedidoDetalleDTO, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oPedidoDetalleDTO.IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@TipoServicio", oPedidoDetalleDTO.TipoServicio);
                        





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
        public int UpdateTotalesPedido(int IdPedido, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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


        public PedidoDTO ObtenerPedidoxId(int IdPedido, string BaseDatos, ref string mensaje_error)
        {
            PedidoDTO oPedidoDTO = new PedidoDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oPedidoDTO.total_igv = Convert.ToDecimal(drd["total_igv"].ToString());
                        oPedidoDTO.total_valor = Convert.ToDecimal(drd["total_valor"].ToString());
                        oPedidoDTO.total_venta = Convert.ToDecimal(drd["total_venta"].ToString());
                        oPedidoDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oPedidoDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        oPedidoDTO.NombCondicionPago = (drd["NombCondicionPago"].ToString());
                        oPedidoDTO.Conformidad = Convert.ToInt32(drd["Conformidad"].ToString());
                        oPedidoDTO.ComentarioConformidad = ((String.IsNullOrEmpty(drd["ComentarioConformidad"].ToString()) ? "" : drd["ComentarioConformidad"].ToString()));
                        oPedidoDTO.EmailProveedor = ((String.IsNullOrEmpty(drd["EmailProveedor"].ToString()) ? "" : drd["EmailProveedor"].ToString()));
                        oPedidoDTO.EnvioCorreo = Convert.ToBoolean(drd["EnvioCorreo"].ToString());
                        oPedidoDTO.EntregasConAlmacen = Convert.ToBoolean(drd["EntregasConAlmacen"].ToString());
                        oPedidoDTO.EsSubContrato = Convert.ToBoolean(drd["EsSubContrato"].ToString());


                        


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
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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

            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oPedidoDetalleDTO.Referencia = (dr2["Referencia"].ToString());
                        oPedidoDetalleDTO.CodigoProducto = (dr2["CodigoProducto"].ToString());
                        oPedidoDetalleDTO.ComentarioConformidad = ((String.IsNullOrEmpty(dr2["ComentarioConformidad"].ToString()) ? "" : dr2["ComentarioConformidad"].ToString())).Trim().ToString();
                        oPedidoDetalleDTO.SerieNumero = (dr2["SerieNumero"].ToString());



                        oPedidoDTO.detalles[posicion] = oPedidoDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
                #endregion
                
            }

            #region AnexoDetalle
            Int32 filasdetalleAnexo = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosPedidoxIdPedido", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleAnexo++;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error += ex.Message.ToString();
                }
            }
            oPedidoDTO.AnexoDetalle = new AnexoDTO[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosPedidoxIdPedido", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr2 = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (dr2.Read())
                    {
                        AnexoDTO oAnexoDTO = new AnexoDTO();
                        oAnexoDTO.IdAnexo = Convert.ToInt32(dr2["IdAnexo"].ToString());
                        oAnexoDTO.ruta = (dr2["ruta"].ToString());
                        oAnexoDTO.IdSociedad = Convert.ToInt32(dr2["IdSociedad"].ToString());
                        oAnexoDTO.Tabla = (dr2["Tabla"].ToString());
                        oAnexoDTO.IdTabla = Convert.ToInt32(dr2["IdTabla"].ToString());
                        oAnexoDTO.NombreArchivo = (dr2["NombreArchivo"].ToString());
                        oPedidoDTO.AnexoDetalle[posicion] = oAnexoDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            #endregion

            return oPedidoDTO;

        }


        public int UpdateInsertPedidoConformidadPedido(ConformidadPedidoDTO oConformidadPedidoDTO,int UsuarioConformidad, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPedidoConformidad", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oConformidadPedidoDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@Conformidad", oConformidadPedidoDTO.Conformidad);
                        da.SelectCommand.Parameters.AddWithValue("@ComentarioConformidad",(String.IsNullOrEmpty(oConformidadPedidoDTO.ComentarioConformidad) ? "" : oConformidadPedidoDTO.ComentarioConformidad.ToString()));
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioConformidad", UsuarioConformidad);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        
                        cn.Close();

                        if (oConformidadPedidoDTO.detalles.Count()>0)
                        {
                            int rpta1 = 0;
                            cn.Open();
                            for (int i = 0; i < oConformidadPedidoDTO.detalles.Count(); i++)
                            {
                                SqlDataAdapter de = new SqlDataAdapter("SMC_UpdateInsertPedidoConformidadDetalle", cn);
                                de.SelectCommand.CommandType = CommandType.StoredProcedure;
                                de.SelectCommand.Parameters.AddWithValue("@IdPedidoDetalle", oConformidadPedidoDTO.detalles[i].IdPedidoDetalle);
                                de.SelectCommand.Parameters.AddWithValue("@ComentarioConformidad",(String.IsNullOrEmpty(oConformidadPedidoDTO.detalles[i].ComentarioConformidadDetalle) ? "" : oConformidadPedidoDTO.detalles[i].ComentarioConformidadDetalle.ToString().Trim()));
                                rpta1 = Convert.ToInt32(de.SelectCommand.ExecuteScalar());
                            }
                            cn.Close();
                        }
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

        public int UpdateInsertPedidoConformidadPedidoAuth(ConformidadPedidoDTO oConformidadPedidoDTO, int UsuarioConformidad,int IdModelo, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPedidoModeloAprobaciones", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedidoModelo", IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdAutorizador", UsuarioConformidad);
                        da.SelectCommand.Parameters.AddWithValue("@Accion", oConformidadPedidoDTO.Conformidad);
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oConformidadPedidoDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", (String.IsNullOrEmpty(oConformidadPedidoDTO.ComentarioConformidad) ? "" : oConformidadPedidoDTO.ComentarioConformidad.ToString()));
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());

                        cn.Close();

                        if (oConformidadPedidoDTO.detalles.Count() > 0)
                        {
                            int rpta1 = 0;
                            cn.Open();
                            for (int i = 0; i < oConformidadPedidoDTO.detalles.Count(); i++)
                            {
                                SqlDataAdapter de = new SqlDataAdapter("SMC_UpdateInsertPedidoConformidadDetalle", cn);
                                de.SelectCommand.CommandType = CommandType.StoredProcedure;
                                de.SelectCommand.Parameters.AddWithValue("@IdPedidoDetalle", oConformidadPedidoDTO.detalles[i].IdPedidoDetalle);
                                de.SelectCommand.Parameters.AddWithValue("@ComentarioConformidad", (String.IsNullOrEmpty(oConformidadPedidoDTO.detalles[i].ComentarioConformidadDetalle) ? "" : oConformidadPedidoDTO.detalles[i].ComentarioConformidadDetalle.ToString().Trim()));
                                rpta1 = Convert.ToInt32(de.SelectCommand.ExecuteScalar());
                            }
                            cn.Close();
                        }
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



        public int UpdateEnvioCorreoPedido(int IdPedido,string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateEnvioPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
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
        public int CerrarPedido(PedidoDTO oPedidoDTO,int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_CerrarPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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
        public int LiberarPedido(PedidoDTO oPedidoDTO, int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_LiberarPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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
        public int AnularPedido(PedidoDTO oPedidoDTO, int IdUsuario,string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_AnularPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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


        public int UpdatePrecioProveedorArticulo(ProveedoresPrecioProductoDTO oProveedoresPrecioProductoDTO, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdatePrecioProveedorArticulo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdArticuloProveedor", oProveedoresPrecioProductoDTO.IdArticuloProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioNacional", oProveedoresPrecioProductoDTO.PrecioNacional);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioExtranjero", oProveedoresPrecioProductoDTO.PrecioExtranjero);
             


                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
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


        public List<PedidoDTO> ObtenerOcAbiertasxArtAlmacen(int IdArticulo,int IdAlmacen, string BaseDatos, ref string mensaje_error)
        {
            List<PedidoDTO> lstPedidoDTO = new List<PedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerOcAbiertasxArtAlmacen", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PedidoDTO oPedidoDTO = new PedidoDTO();
            
                        oPedidoDTO.NombSerie = (drd["Serie"].ToString());
                
                        oPedidoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());

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

        public int UpdateInsertPedidoModelo(PedidoModeloDTO oPedidoModeloDTO, string IdSociedad, string BaseDatos)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPedidoModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedidoModelo", oPedidoModeloDTO.IdPedidoModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoModeloDTO.IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oPedidoModeloDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapa", oPedidoModeloDTO.IdEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@Aprobaciones", oPedidoModeloDTO.Aprobaciones);
                        da.SelectCommand.Parameters.AddWithValue("@Rechazos", oPedidoModeloDTO.Rechazos);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }


        public int ValidarSipuedeAprobar(int IdPedido, int IdEtapa, string BaseDatos)
        {
            int puedeentrar = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarSipuedeAprobarPedido1", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        puedeentrar = int.Parse(drd["puedeentrar"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return puedeentrar;
        }

        public List<PedidoDTO> ObtenerPedidoSinModelo(string BaseDatos)
        {
            List<PedidoDTO> LSTPedidoDTO = new List<PedidoDTO>();


            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerPedidoSinModelo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PedidoDTO oPedidoDTO = new PedidoDTO();
                        oPedidoDTO.IdPedido = int.Parse(drd["IdPedido"].ToString());
                        oPedidoDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        LSTPedidoDTO.Add(oPedidoDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    return LSTPedidoDTO;
                }
            }
            return LSTPedidoDTO;

        }

        public int CorregirPedidoSinModelo(int IdPedido, int IdUsuario, string BaseDatos)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_CorregirPedidoSinModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdPedido);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }
        public List<PedidoPendienteDTO> ObtenerOCPendientes(int idSociedad, int idObra, int idArticulo, string BaseDatos)
        {
            List<PedidoPendienteDTO> lstPedidosDTO = new List<PedidoPendienteDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerPedidoPendienteObraArticulo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", idSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", idObra);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", idArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PedidoPendienteDTO oPedido = new PedidoPendienteDTO();
                        oPedido.Cantidad = decimal.Parse(drd["Cantidad"].ToString());
                        oPedido.Entregado = decimal.Parse(drd["Entregado"].ToString());
                        oPedido.Saldo = decimal.Parse(drd["Saldo"].ToString());
                        oPedido.FechaDocumento = DateTime.Parse(drd["FechaDocumento"].ToString());
                        oPedido.NumOC = drd["NumOC"].ToString();
                        oPedido.RazonSocial = drd["RazonSocial"].ToString();
                        lstPedidosDTO.Add(oPedido);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }
            return lstPedidosDTO;
        }
        public int Delete(int IdDetalle, string BaseDatos)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarItemRQAprobado", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDetalle", IdDetalle);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }

        public int ActivarPedido(PedidoDTO oPedidoDTO, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ActivarPedido", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPedido", oPedidoDTO.IdPedido);
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


        public int AprobarPedido(int IdPedido, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_AprobarPedido", cn);
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

       

    }
}
