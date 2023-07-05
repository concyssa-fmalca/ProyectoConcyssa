using DTO;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace DAO
{
    public class OrpcDAO
    {
        public OrpcDTO ObtenerDatosxIdOrpc(int IdOrpc, ref string mensaje_error)
        {
            OrpcDTO oOrpcDTO = new OrpcDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerOrpcxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOrpc", IdOrpc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oOrpcDTO.DT_RowId = Convert.ToInt32(drd["IdORPC"].ToString());
                        oOrpcDTO.IdORPC = Convert.ToInt32(drd["IdORPC"].ToString());
                        oOrpcDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOrpcDTO.ObjType = (drd["ObjType"].ToString());
                        oOrpcDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOrpcDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOrpcDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOrpcDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOrpcDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOrpcDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOrpcDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOrpcDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOrpcDTO.Referencia = (drd["Referencia"].ToString());
                        oOrpcDTO.Comentario = (drd["Comentario"].ToString());
                        oOrpcDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOrpcDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOrpcDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOrpcDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOrpcDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOrpcDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOrpcDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOrpcDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOrpcDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOrpcDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOrpcDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOrpcDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOrpcDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOrpcDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOrpcDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOrpcDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOrpcDTO.NombObra = (drd["NombObra"].ToString());
                        oOrpcDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOrpcDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOrpcDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOrpcDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oOrpcDTO.IdTipoDocumentoRef = Convert.ToInt32(drd["IdTipoDocumentoRef"].ToString());
                        oOrpcDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());
                        oOrpcDTO.IdCuadrilla = Convert.ToInt32(String.IsNullOrEmpty(drd["IdCuadrilla"].ToString()) ? "0" : drd["IdCuadrilla"].ToString()); 
                        oOrpcDTO.IdResponsable = Convert.ToInt32(String.IsNullOrEmpty(drd["IdResponsable"].ToString()) ? "0" : drd["IdResponsable"].ToString());
                        oOrpcDTO.idCondicionPago = Convert.ToInt32(drd["IdCondicionPago"].ToString());
                        oOrpcDTO.IdGlosaContable = Convert.ToInt32(drd["IdGlosaContable"].ToString());
                        oOrpcDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oOrpcDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        oOrpcDTO.IdTipoRegistro = Convert.ToInt32(drd["IdTipoRegistro"].ToString());
                        oOrpcDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());

                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oOrpcDTO;
        }
        public List<OrpcDTO> ObtenerORPCxEstado(int IdBase,int IdSociedad, ref string mensaje_error, string EstadoORPC,int IdUsuario=0)
        {
            List<OrpcDTO> lstOrpcDTO = new List<OrpcDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarORPCxEstado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoORPC", EstadoORPC);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OrpcDTO oOrpcDTO = new OrpcDTO();
                        oOrpcDTO.DT_RowId = Convert.ToInt32(drd["IdORPC"].ToString());
                        oOrpcDTO.IdORPC = Convert.ToInt32(drd["IdORPC"].ToString());
                        oOrpcDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOrpcDTO.ObjType = (drd["ObjType"].ToString());
                        oOrpcDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOrpcDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOrpcDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOrpcDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOrpcDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOrpcDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOrpcDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOrpcDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOrpcDTO.Referencia = (drd["Referencia"].ToString());
                        oOrpcDTO.Comentario = (drd["Comentario"].ToString());
                        oOrpcDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOrpcDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOrpcDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOrpcDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOrpcDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOrpcDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOrpcDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOrpcDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOrpcDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOrpcDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOrpcDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOrpcDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOrpcDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOrpcDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOrpcDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOrpcDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOrpcDTO.NombObra = (drd["NombObra"].ToString());
                        oOrpcDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOrpcDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOrpcDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOrpcDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oOrpcDTO.NombProveedor = (drd["NombProveedor"].ToString());
                        oOrpcDTO.NumProveedor = (drd["NumProveedor"].ToString());
                        oOrpcDTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oOrpcDTO.Moneda = drd["Moneda"].ToString();
                        oOrpcDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();

                        lstOrpcDTO.Add(oOrpcDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOrpcDTO;
        }


        public List<ORPCDetalle> ObtenerDetalleOrpc(int IdORPC, ref string mensaje_error)
        {
            List<ORPCDetalle> lstORPCDetalle = new List<ORPCDetalle>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarORPCDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdORPC", IdORPC);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ORPCDetalle oORPCDetalle = new ORPCDetalle();
                        oORPCDetalle.DT_RowId = Convert.ToInt32(drd["IdORPCDetalle"].ToString());
                        oORPCDetalle.IdORPC = Convert.ToInt32(drd["IdORPC"].ToString());
                        oORPCDetalle.IdORPCDetalle = Convert.ToInt32(drd["IdORPCDetalle"].ToString());
                        oORPCDetalle.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        oORPCDetalle.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oORPCDetalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oORPCDetalle.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oORPCDetalle.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oORPCDetalle.valor_unitario = Convert.ToDecimal(drd["valor_unitario"].ToString());
                        oORPCDetalle.precio_unitario = Convert.ToDecimal(drd["precio_unitario"].ToString());
                        oORPCDetalle.total_base_igv = Convert.ToDecimal(drd["total_base_igv"].ToString());
                        oORPCDetalle.porcentaje_igv = Convert.ToDecimal(drd["porcentaje_igv"].ToString());
                        oORPCDetalle.total_igv = Convert.ToDecimal(drd["total_igv"].ToString());
                        oORPCDetalle.total_impuestos = Convert.ToDecimal(drd["total_impuestos"].ToString());
                        oORPCDetalle.total_valor_item = Convert.ToDecimal(drd["total_valor_item"].ToString());
                        oORPCDetalle.total_item = Convert.ToDecimal(drd["total_item"].ToString());
                        oORPCDetalle.IdIndicadorImpuesto = Convert.ToInt32(drd["IdIndicadorImpuesto"].ToString());
                        oORPCDetalle.CodImpuesto = (drd["CodImpuesto"].ToString());
                        oORPCDetalle.NombImpuesto = (drd["NombImpuesto"].ToString());
                        oORPCDetalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oORPCDetalle.CodigoArticulo = drd["CodigoArticulo"].ToString();
                        oORPCDetalle.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oORPCDetalle.NombResponsable = drd["NombResponsable"].ToString();
                        lstORPCDetalle.Add(oORPCDetalle);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstORPCDetalle;
            }

        }



        public int UpdateTotalesORPC(int IdORPC, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateTotalesORPC", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdORPC", IdORPC);
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

        public List<AnexoDTO> ObtenerAnexoOrpc(int IdOrpc, ref string mensaje_error)
        {
            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosOrpcxIdOrpc", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOrpc", IdOrpc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AnexoDTO oAnexoDTO = new AnexoDTO();
                        oAnexoDTO.IdAnexo = Convert.ToInt32(drd["IdAnexo"].ToString());
                        oAnexoDTO.ruta = (drd["ruta"].ToString());
                        oAnexoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oAnexoDTO.Tabla = (drd["Tabla"].ToString());
                        oAnexoDTO.IdTabla = Convert.ToInt32(drd["IdTabla"].ToString());
                        oAnexoDTO.NombreArchivo = (drd["NombreArchivo"].ToString());
                        lstAnexoDTO.Add(oAnexoDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstAnexoDTO;
            }


        }



    }
}
