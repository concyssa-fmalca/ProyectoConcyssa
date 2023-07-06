using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class OpchDAO
    {


        public OpchDTO ObtenerDatosxIdOpch(int IdOpch, ref string mensaje_error)
        {
            OpchDTO oOpchDTO = new OpchDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerOpchxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOpch", IdOpch);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oOpchDTO.DT_RowId = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOpchDTO.IdOPCH = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOpchDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpchDTO.ObjType = (drd["ObjType"].ToString());
                        oOpchDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpchDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpchDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpchDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpchDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpchDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpchDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpchDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpchDTO.Referencia = (drd["Referencia"].ToString());
                        oOpchDTO.Comentario = (drd["Comentario"].ToString());
                        oOpchDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpchDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpchDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpchDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpchDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpchDTO.Redondeo = Convert.ToDecimal(drd["Redondeo"].ToString());
                        oOpchDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpchDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpchDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpchDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpchDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpchDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpchDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpchDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpchDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpchDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpchDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOpchDTO.NombObra = (drd["NombObra"].ToString());
                        oOpchDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpchDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOpchDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOpchDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oOpchDTO.IdTipoDocumentoRef = Convert.ToInt32(drd["IdTipoDocumentoRef"].ToString());
                        oOpchDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());
                        oOpchDTO.IdCuadrilla = Convert.ToInt32(String.IsNullOrEmpty(drd["IdCuadrilla"].ToString()) ? "0" : drd["IdCuadrilla"].ToString());
                        oOpchDTO.IdResponsable = Convert.ToInt32(String.IsNullOrEmpty(drd["IdResponsable"].ToString()) ? "0" : drd["IdResponsable"].ToString());
                        oOpchDTO.idCondicionPago = Convert.ToInt32(drd["idCondicionPago"].ToString());
                        oOpchDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        oOpchDTO.IdTipoRegistro = Convert.ToInt32(drd["IdTipoRegistro"].ToString());
                        oOpchDTO.IdGlosaContable = Convert.ToInt32(drd["IdGlosaContable"].ToString());
                        oOpchDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oOpchDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());


                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oOpchDTO;
        }
        public List<OpchDTO> ObtenerOPCHxEstado(int IdBase,int IdSociedad, ref string mensaje_error, string EstadoOPCH,int IdUsuario=0)
        {
            List<OpchDTO> lstOPCHDTO = new List<OpchDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPCHxEstado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPCH", EstadoOPCH);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OpchDTO oOpchDTO = new OpchDTO();
                        oOpchDTO.DT_RowId = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOpchDTO.IdOPCH = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOpchDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpchDTO.ObjType = (drd["ObjType"].ToString());
                        oOpchDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpchDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpchDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpchDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpchDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpchDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpchDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpchDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpchDTO.Referencia = (drd["Referencia"].ToString());
                        oOpchDTO.Comentario = (drd["Comentario"].ToString());
                        oOpchDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpchDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpchDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpchDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpchDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpchDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpchDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpchDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpchDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpchDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpchDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpchDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpchDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpchDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpchDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpchDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOpchDTO.NombObra = (drd["NombObra"].ToString());
                        oOpchDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpchDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOpchDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOpchDTO.Moneda = (drd["Moneda"].ToString());
                        oOpchDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oOpchDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oOpchDTO.Proveedor = drd["Proveedor"].ToString();
                        oOpchDTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();

                        lstOPCHDTO.Add(oOpchDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOPCHDTO;
        }

        public List<OpchDTO> ObtenerOPCHxEstadoModal(int IdSociedad, ref string mensaje_error, string EstadoOPCH,int IdUsuario=0)
        {
            List<OpchDTO> lstOPCHDTO = new List<OpchDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPCHxEstadoModal", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPCH", EstadoOPCH);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OpchDTO oOpchDTO = new OpchDTO();
                        oOpchDTO.DT_RowId = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOpchDTO.IdOPCH = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOpchDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpchDTO.ObjType = (drd["ObjType"].ToString());
                        oOpchDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpchDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpchDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpchDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpchDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpchDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpchDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpchDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpchDTO.Referencia = (drd["Referencia"].ToString());
                        oOpchDTO.Comentario = (drd["Comentario"].ToString());
                        oOpchDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpchDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpchDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpchDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpchDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpchDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpchDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpchDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpchDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpchDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpchDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpchDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpchDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpchDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpchDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpchDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oOpchDTO.NombObra = (drd["NombObra"].ToString());
                        oOpchDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpchDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oOpchDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oOpchDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oOpchDTO.IdCuadrilla = Convert.ToInt32((String.IsNullOrEmpty(drd["IdCuadrilla"].ToString())) ? 0 : drd["IdCuadrilla"].ToString());
                        oOpchDTO.IdTipoDocumentoRef = Convert.ToInt32(drd["IdTipoDocumentoRef"].ToString());
                        oOpchDTO.IdResponsable = Convert.ToInt32((String.IsNullOrEmpty(drd["IdResponsable"].ToString())) ? 0 : drd["IdResponsable"].ToString()); 
                        oOpchDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());


                        











                        lstOPCHDTO.Add(oOpchDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOPCHDTO;
        }

        public List<OPCHDetalle> ObtenerDetalleOpch(int IdOPCH, ref string mensaje_error)
        {
            List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPCHDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOPCH", IdOPCH);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OPCHDetalle oOPCHDetalle = new OPCHDetalle();
                        oOPCHDetalle.DT_RowId = Convert.ToInt32(drd["IdOPCHDetalle"].ToString());
                        oOPCHDetalle.IdOPCH = Convert.ToInt32(drd["IdOPCH"].ToString());
                        oOPCHDetalle.IdOPCHDetalle = Convert.ToInt32(drd["IdOPCHDetalle"].ToString());
                        oOPCHDetalle.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        oOPCHDetalle.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oOPCHDetalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oOPCHDetalle.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oOPCHDetalle.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oOPCHDetalle.valor_unitario = Convert.ToDecimal(drd["valor_unitario"].ToString());
                        oOPCHDetalle.precio_unitario = Convert.ToDecimal(drd["precio_unitario"].ToString());
                        oOPCHDetalle.total_base_igv = Convert.ToDecimal(drd["total_base_igv"].ToString());
                        oOPCHDetalle.porcentaje_igv = Convert.ToDecimal(drd["porcentaje_igv"].ToString());
                        oOPCHDetalle.total_igv = Convert.ToDecimal(drd["total_igv"].ToString());
                        oOPCHDetalle.total_impuestos = Convert.ToDecimal(drd["total_impuestos"].ToString());
                        oOPCHDetalle.total_valor_item = Convert.ToDecimal(drd["total_valor_item"].ToString());
                        oOPCHDetalle.total_item = Convert.ToDecimal(drd["total_item"].ToString());
                        oOPCHDetalle.IdIndicadorImpuesto = Convert.ToInt32(drd["IdIndicadorImpuesto"].ToString());
                        oOPCHDetalle.CodImpuesto = (drd["CodImpuesto"].ToString());
                        oOPCHDetalle.NombImpuesto = (drd["NombImpuesto"].ToString());
                        oOPCHDetalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oOPCHDetalle.PrecioUnidadBase = Convert.ToDecimal(drd["PrecioUnidadBase"].ToString());
                        oOPCHDetalle.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oOPCHDetalle.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oOPCHDetalle.NombResponsable = drd["NombResponsable"].ToString();
                        oOPCHDetalle.TipoServicio = drd["TipoServicio"].ToString();



                        oOPCHDetalle.CantidadNotaCredito = Convert.ToDecimal((String.IsNullOrEmpty(drd["CantidadNotaCredito"].ToString())) ? "0" : drd["CantidadNotaCredito"].ToString());

                        lstOPCHDetalle.Add(oOPCHDetalle);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstOPCHDetalle;
            }

        }



        public int UpdateTotalesOPCH(int IdOPCH, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateTotalesOPCH", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCH", IdOPCH);
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


        public List<AnexoDTO> ObtenerAnexoOpch(int IdOpch, ref string mensaje_error)
        {
            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosOpchxIdOpch", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOpch", IdOpch);
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
