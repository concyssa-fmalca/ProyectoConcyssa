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
                        oPedidoDetalleDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oPedidoDetalleDTO.IdGrupoArticulo = Convert.ToInt32(drd["IdGrupoArticulo"].ToString());
                        oPedidoDetalleDTO.IdDefinicion = Convert.ToInt32(drd["IdDefinicion"].ToString());
                        oPedidoDetalleDTO.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
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
    }
}
