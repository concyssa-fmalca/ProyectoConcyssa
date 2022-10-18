using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class OpdnDAO
    {

        public OpdnDTO ObtenerDatosxIDOPDN(int IdOpdn, ref string mensaje_error)
        {
            OpdnDTO oOpdnDTO = new OpdnDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDatosxIDOPDN", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOPDN", IdOpdn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oOpdnDTO.DT_RowId = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdOPDN = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpdnDTO.ObjType = (drd["ObjType"].ToString());
                        oOpdnDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpdnDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpdnDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpdnDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpdnDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpdnDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpdnDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpdnDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpdnDTO.Referencia = (drd["Referencia"].ToString());
                        oOpdnDTO.Comentario = (drd["Comentario"].ToString());
                        oOpdnDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpdnDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpdnDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpdnDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpdnDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpdnDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpdnDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpdnDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpdnDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpdnDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpdnDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpdnDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpdnDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpdnDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOpdnDTO.NombObra = (drd["NombObra"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOpdnDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOpdnDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oOpdnDTO.IdTipoDocumentoRef = Convert.ToInt32(drd["IdTipoDocumentoRef"].ToString());
                        oOpdnDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());



                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }


            return oOpdnDTO;
        }


        public List<OpdnDTO> ObtenerOPDNxEstado(int IdSociedad, ref string mensaje_error, string EstadoOPDN)
        {
            List<OpdnDTO> lstOPDNDTO = new List<OpdnDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPDNxEstado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPDN", EstadoOPDN);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OpdnDTO oOpdnDTO = new OpdnDTO();
                        oOpdnDTO.DT_RowId = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdOPDN = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpdnDTO.ObjType = (drd["ObjType"].ToString());
                        oOpdnDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpdnDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpdnDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpdnDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpdnDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpdnDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpdnDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpdnDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpdnDTO.Referencia = (drd["Referencia"].ToString());
                        oOpdnDTO.Comentario = (drd["Comentario"].ToString());
                        oOpdnDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpdnDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpdnDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpdnDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpdnDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpdnDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpdnDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpdnDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpdnDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpdnDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpdnDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpdnDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpdnDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpdnDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOpdnDTO.NombObra = (drd["NombObra"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOpdnDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());


                        lstOPDNDTO.Add(oOpdnDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOPDNDTO;
        }

        public List<OpdnDTO> ListarOPDNDTModalOPCH(int IdSociedad, ref string mensaje_error, string EstadoOPDN)
        {
            List<OpdnDTO> lstOPDNDTO = new List<OpdnDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPDNDTModalOPCH", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPDN", EstadoOPDN);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OpdnDTO oOpdnDTO = new OpdnDTO();
                        oOpdnDTO.DT_RowId = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdOPDN = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpdnDTO.ObjType = (drd["ObjType"].ToString());
                        oOpdnDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpdnDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpdnDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpdnDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpdnDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpdnDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpdnDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpdnDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpdnDTO.Referencia = (drd["Referencia"].ToString());
                        oOpdnDTO.Comentario = (drd["Comentario"].ToString());
                        oOpdnDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpdnDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpdnDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpdnDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpdnDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpdnDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpdnDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpdnDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpdnDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpdnDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpdnDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpdnDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpdnDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpdnDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOpdnDTO.NombObra = (drd["NombObra"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOpdnDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOpdnDTO.CantidadUsada = Convert.ToDecimal(drd["CantidadUsada"].ToString());
                        oOpdnDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oOpdnDTO.IdCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oOpdnDTO.NombProveedor = (drd["NombProveedor"].ToString());
                        lstOPDNDTO.Add(oOpdnDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOPDNDTO;
        }

        public List<OPDNDetalle> ObtenerDetalleOpdn(int IdOpdn, ref string mensaje_error)
        {
            List<OPDNDetalle> lstOPDNDetalle = new List<OPDNDetalle>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOpdnDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOpdn", IdOpdn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OPDNDetalle oOPDNDetalle = new OPDNDetalle();
                        oOPDNDetalle.DT_RowId = Convert.ToInt32(drd["IdOPDNDetalle"].ToString());
                        oOPDNDetalle.IdOPDN = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOPDNDetalle.IdOPDNDetalle = Convert.ToInt32(drd["IdOPDNDetalle"].ToString());
                        oOPDNDetalle.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());

                        oOPDNDetalle.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oOPDNDetalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oOPDNDetalle.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oOPDNDetalle.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oOPDNDetalle.valor_unitario = Convert.ToDecimal(drd["valor_unitario"].ToString());
                        oOPDNDetalle.precio_unitario = Convert.ToDecimal(drd["precio_unitario"].ToString());
                        oOPDNDetalle.total_base_igv = Convert.ToDecimal(drd["total_base_igv"].ToString());
                        oOPDNDetalle.porcentaje_igv = Convert.ToDecimal(drd["porcentaje_igv"].ToString());
                        oOPDNDetalle.total_igv = Convert.ToDecimal(drd["total_igv"].ToString());
                        oOPDNDetalle.total_impuestos = Convert.ToDecimal(drd["total_impuestos"].ToString());
                        oOPDNDetalle.total_valor_item = Convert.ToDecimal(drd["total_valor_item"].ToString());
                        oOPDNDetalle.total_item = Convert.ToDecimal(drd["total_item"].ToString());
                        oOPDNDetalle.IdIndicadorImpuesto = Convert.ToInt32(drd["IdIndicadorImpuesto"].ToString());
                        oOPDNDetalle.CodImpuesto = (drd["CodImpuesto"].ToString());
                        oOPDNDetalle.NombImpuesto = (drd["NombImpuesto"].ToString());
                        oOPDNDetalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oOPDNDetalle.CantidadUsada = Convert.ToDecimal(drd["CantidadUsada"].ToString());
                        oOPDNDetalle.Referencia = (drd["Referencia"].ToString());



                        lstOPDNDetalle.Add(oOPDNDetalle);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstOPDNDetalle;
            }

        }



        public int UpdateTotalesOPDN(int IdOPDN, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateTotalesOPDN", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDN", IdOPDN);
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