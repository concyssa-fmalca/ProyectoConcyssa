using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO;

namespace DAO
{
    public class IntregadorV1DAO
    {
        public int MoverFacturaEnviarSap(int IdOPCH, int IdUsuario, int GrupoCreacion)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_PasarFacturaTablaEnviarSap", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCH", IdOPCH);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
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
        public int MoverNotaCreditoEnviarSap(int IdORPC, int IdUsuario, int GrupoCreacion)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_PasarNotaCreditoTablaEnviarSap", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdORPC", IdORPC);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
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
        public int ObtenerGrupoCreacionEnviarSap()
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGrupoCreacionEnviarSap", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
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

        public List<ListaTrabajoDTO> ObtenerListaDatosTrabajo(int IdUsuario)
        {
            List<ListaTrabajoDTO> lstListaTrabajoDTO = new List<ListaTrabajoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerListasTrabajoxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ListaTrabajoDTO oListaTrabajoDTO = new ListaTrabajoDTO();
                        oListaTrabajoDTO.GrupoCreacion = int.Parse(drd["GrupoCreacion"].ToString());
                        oListaTrabajoDTO.UsuarioCreacionTabla = int.Parse(drd["UsuarioCreacionTabla"].ToString());
                        oListaTrabajoDTO.FechaCreacionTabla = DateTime.Parse(drd["FechaCreacionTabla"].ToString());
                        oListaTrabajoDTO.Usuario = drd["Usuario"].ToString();
                        oListaTrabajoDTO.Nombre = drd["Nombre"].ToString();
                        oListaTrabajoDTO.EstadoEnviado = drd["EstadoEnviado"].ToString();


                        lstListaTrabajoDTO.Add(oListaTrabajoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstListaTrabajoDTO;
        }

        public List<IntegradorV1DTO> ListarEnviarSap(int GrupoCreacion, ref string mensaje_error)
        {
            List<IntegradorV1DTO> lstIntegradorV1DTO = new List<IntegradorV1DTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEnviarSap", cn);
                    da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);


                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorV1DTO oIntegradorV1DTO = new IntegradorV1DTO();
                        oIntegradorV1DTO.IdEnviarSap = Convert.ToInt32(drd["IdEnviarSap"].ToString());
                        oIntegradorV1DTO.IdTablaOriginal = Convert.ToInt32(drd["IdTablaOriginal"].ToString());
                        oIntegradorV1DTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oIntegradorV1DTO.ObjType = (drd["ObjType"].ToString());
                        oIntegradorV1DTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oIntegradorV1DTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oIntegradorV1DTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oIntegradorV1DTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oIntegradorV1DTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oIntegradorV1DTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oIntegradorV1DTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oIntegradorV1DTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oIntegradorV1DTO.Referencia = (drd["Referencia"].ToString());
                        oIntegradorV1DTO.Comentario = (drd["Comentario"].ToString());
                        oIntegradorV1DTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oIntegradorV1DTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oIntegradorV1DTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oIntegradorV1DTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oIntegradorV1DTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oIntegradorV1DTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oIntegradorV1DTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oIntegradorV1DTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oIntegradorV1DTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oIntegradorV1DTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oIntegradorV1DTO.NombSerie = (drd["NombSerie"].ToString());
                        oIntegradorV1DTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oIntegradorV1DTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oIntegradorV1DTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oIntegradorV1DTO.NombObra = (drd["NombObra"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oIntegradorV1DTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oIntegradorV1DTO.Moneda = (drd["Moneda"].ToString());
                        oIntegradorV1DTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oIntegradorV1DTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oIntegradorV1DTO.Proveedor = drd["Proveedor"].ToString();
                        oIntegradorV1DTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oIntegradorV1DTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());
                        oIntegradorV1DTO.Inventario = bool.Parse(drd["Inventario"].ToString());
                        oIntegradorV1DTO.TablaOriginal = drd["TablaOriginal"].ToString();
                        oIntegradorV1DTO.NombGlosa = drd["NombGlosa"].ToString();
                        lstIntegradorV1DTO.Add(oIntegradorV1DTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstIntegradorV1DTO;
        }

        public IntegradorV1DTO ObtenerDatosxIdEnviarSap(int IdEnviarSap, ref string mensaje_error)
        {
            IntegradorV1DTO oIntegradorV1DTO = new IntegradorV1DTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerEnviarSAPxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oIntegradorV1DTO.IdEnviarSap = Convert.ToInt32(drd["IdEnviarSap"].ToString());
                        oIntegradorV1DTO.IdTablaOriginal = Convert.ToInt32(drd["IdTablaOriginal"].ToString());
                        oIntegradorV1DTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oIntegradorV1DTO.ObjType = (drd["ObjType"].ToString());
                        oIntegradorV1DTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oIntegradorV1DTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oIntegradorV1DTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oIntegradorV1DTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oIntegradorV1DTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oIntegradorV1DTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oIntegradorV1DTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oIntegradorV1DTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oIntegradorV1DTO.Referencia = (drd["Referencia"].ToString());
                        oIntegradorV1DTO.Comentario = (drd["Comentario"].ToString());
                        oIntegradorV1DTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oIntegradorV1DTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oIntegradorV1DTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oIntegradorV1DTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oIntegradorV1DTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oIntegradorV1DTO.Redondeo = Convert.ToDecimal(drd["Redondeo"].ToString());
                        oIntegradorV1DTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oIntegradorV1DTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oIntegradorV1DTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oIntegradorV1DTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oIntegradorV1DTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oIntegradorV1DTO.NombSerie = (drd["NombSerie"].ToString());
                        oIntegradorV1DTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oIntegradorV1DTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oIntegradorV1DTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oIntegradorV1DTO.NombObra = (drd["NombObra"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oIntegradorV1DTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oIntegradorV1DTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oIntegradorV1DTO.IdTipoDocumentoRef = Convert.ToInt32(drd["IdTipoDocumentoRef"].ToString());
                        oIntegradorV1DTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());
                        oIntegradorV1DTO.IdCuadrilla = Convert.ToInt32(String.IsNullOrEmpty(drd["IdCuadrilla"].ToString()) ? "0" : drd["IdCuadrilla"].ToString());
                        oIntegradorV1DTO.IdResponsable = Convert.ToInt32(String.IsNullOrEmpty(drd["IdResponsable"].ToString()) ? "0" : drd["IdResponsable"].ToString());
                        oIntegradorV1DTO.idCondicionPago = Convert.ToInt32(drd["idCondicionPago"].ToString());
                        oIntegradorV1DTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        oIntegradorV1DTO.IdTipoRegistro = Convert.ToInt32(drd["IdTipoRegistro"].ToString());
                        oIntegradorV1DTO.IdGlosaContable = Convert.ToInt32(drd["IdGlosaContable"].ToString());
                        oIntegradorV1DTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oIntegradorV1DTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        //oIntegradorV1DTO.FechaEdicion = Convert.ToDateTime(String.IsNullOrEmpty(drd["FechaEdicion"].ToString()) ? "1990/01/01" : drd["FechaEdicion"].ToString());
                        //oIntegradorV1DTO.NombUsuarioEdicion = (String.IsNullOrEmpty(drd["NombUsuarioEdicion"].ToString()) ? "" : drd["NombUsuarioEdicion"].ToString());
                        oIntegradorV1DTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());
                        oIntegradorV1DTO.TablaOrigen = drd["TablaOrigen"].ToString();
                        oIntegradorV1DTO.ConsumoM3 = decimal.Parse(drd["ConsumoM3"].ToString());
                        oIntegradorV1DTO.ConsumoHW = decimal.Parse(drd["ConsumoHW"].ToString());
                        oIntegradorV1DTO.TasaDetraccion = int.Parse(drd["TasaDetraccion"].ToString());
                        oIntegradorV1DTO.GrupoDetraccion = int.Parse(drd["GrupoDetraccion"].ToString());
                        oIntegradorV1DTO.SerieSAP = int.Parse(drd["SerieSAP"].ToString());
                        oIntegradorV1DTO.CondicionPagoDet = int.Parse(drd["CondicionPagoDet"].ToString());
                        oIntegradorV1DTO.Inventario = bool.Parse(drd["Inventario"].ToString());
                        oIntegradorV1DTO.TablaOriginal = drd["TablaOriginal"].ToString();
                        oIntegradorV1DTO.SerieDocBaseORPC = drd["SerieDocBaseORPC"].ToString();


                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oIntegradorV1DTO;
        }
        public List<IntegradorV1Detalle> ObtenerDatosxIdEnviarSapDetalle(int IdEnviarSap, ref string mensaje_error)
        {
            List<IntegradorV1Detalle> lstIntegradorV1Detalle = new List<IntegradorV1Detalle>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEnviarSapDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorV1Detalle oIntegradorV1Detalle = new IntegradorV1Detalle();
                        oIntegradorV1Detalle.IdEnviarSapDetalle = Convert.ToInt32(drd["IdEnviarSapDetalle"].ToString());
                        oIntegradorV1Detalle.IdEnviarSap = Convert.ToInt32(drd["IdEnviarSap"].ToString());
                        oIntegradorV1Detalle.IdTablaOriginal = Convert.ToInt32(drd["IdTablaOriginal"].ToString());
                        oIntegradorV1Detalle.IdTablaOriginalDetalle = Convert.ToInt32(drd["IdTablaOriginalDetalle"].ToString());
                        oIntegradorV1Detalle.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        oIntegradorV1Detalle.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oIntegradorV1Detalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oIntegradorV1Detalle.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oIntegradorV1Detalle.Cantidad = Convert.ToDecimal(drd["Cantidad"].ToString());
                        oIntegradorV1Detalle.valor_unitario = Convert.ToDecimal(drd["valor_unitario"].ToString());
                        oIntegradorV1Detalle.precio_unitario = Convert.ToDecimal(drd["precio_unitario"].ToString());
                        oIntegradorV1Detalle.total_base_igv = Convert.ToDecimal(drd["total_base_igv"].ToString());
                        oIntegradorV1Detalle.porcentaje_igv = Convert.ToDecimal(drd["porcentaje_igv"].ToString());
                        oIntegradorV1Detalle.total_igv = Convert.ToDecimal(drd["total_igv"].ToString());
                        oIntegradorV1Detalle.total_impuestos = Convert.ToDecimal(drd["total_impuestos"].ToString());
                        oIntegradorV1Detalle.total_valor_item = Convert.ToDecimal(drd["total_valor_item"].ToString());
                        oIntegradorV1Detalle.total_item = Convert.ToDecimal(drd["total_item"].ToString());
                        oIntegradorV1Detalle.IdIndicadorImpuesto = Convert.ToInt32(drd["IdIndicadorImpuesto"].ToString());
                        oIntegradorV1Detalle.CodImpuesto = (drd["CodImpuesto"].ToString());
                        oIntegradorV1Detalle.NombImpuesto = (drd["NombImpuesto"].ToString());
                        oIntegradorV1Detalle.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oIntegradorV1Detalle.PrecioUnidadBase = Convert.ToDecimal(drd["PrecioUnidadBase"].ToString());
                        oIntegradorV1Detalle.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oIntegradorV1Detalle.NombCuadrilla = drd["NombCuadrilla"].ToString();
                        oIntegradorV1Detalle.NombResponsable = drd["NombResponsable"].ToString();
                        oIntegradorV1Detalle.TipoServicio = drd["TipoServicio"].ToString();
                        oIntegradorV1Detalle.IdCuadrilla = Convert.ToInt32((String.IsNullOrEmpty(drd["IdCuadrilla"].ToString())) ? "0" : drd["IdCuadrilla"].ToString());
                        oIntegradorV1Detalle.IdResponsable = Convert.ToInt32((String.IsNullOrEmpty(drd["IdResponsable"].ToString())) ? "0" : drd["IdResponsable"].ToString());



                        oIntegradorV1Detalle.CantidadNotaCredito = Convert.ToDecimal((String.IsNullOrEmpty(drd["CantidadNotaCredito"].ToString())) ? "0" : drd["CantidadNotaCredito"].ToString());

                        lstIntegradorV1Detalle.Add(oIntegradorV1Detalle);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstIntegradorV1Detalle;
            }

        }

        public List<AnexoDTO> ObtenerAnexoEnviarSap(int IdEnviarSap, ref string mensaje_error)
        {
            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosOpchxIdEnviarSap", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
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
        public int DeleteAnexo(int IdEnviarSapAnexos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarAnexo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdAnexo", IdEnviarSapAnexos);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
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

        public int EliminarMarcoTrabajo(int GrupoCreacion)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarMarcoTrabajo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
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

        public int UpdateEnviarSap(IntegradorV1DTO oIntegradorV1DTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateEnviarSap", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", oIntegradorV1DTO.IdEnviarSap);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oIntegradorV1DTO.FechaContabilizacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oIntegradorV1DTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumentoRef", oIntegradorV1DTO.IdTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@NumSerieTipoDocumentoRef", oIntegradorV1DTO.NumSerieTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@idCondicionPago", oIntegradorV1DTO.idCondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@IdGlosaContable", oIntegradorV1DTO.IdGlosaContable);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oIntegradorV1DTO.Comentario ?? "");
                        da.SelectCommand.Parameters.AddWithValue("@ConsumoM3", oIntegradorV1DTO.ConsumoM3);
                        da.SelectCommand.Parameters.AddWithValue("@ConsumoHW", oIntegradorV1DTO.ConsumoHW);
                        da.SelectCommand.Parameters.AddWithValue("@TasaDetraccion", oIntegradorV1DTO.TasaDetraccion);
                        da.SelectCommand.Parameters.AddWithValue("@GrupoDetraccion", oIntegradorV1DTO.GrupoDetraccion);
                        da.SelectCommand.Parameters.AddWithValue("@SerieSAP", oIntegradorV1DTO.SerieSAP);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionPagoDet", oIntegradorV1DTO.CondicionPagoDet);
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

        public int UpdateCuadrillasEnviarSAP(IntegradorV1Detalle oIntegradorV1DTO)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateCuadrillaEnviarSap", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEnviarSapDetalle", oIntegradorV1DTO.IdEnviarSapDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oIntegradorV1DTO.IdResponsable);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oIntegradorV1DTO.IdCuadrilla);
                      
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
        public int UpdateDocEntryEnviarSAP(int IdEnviarSap, int DocEntrySap)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateDocEntryEnviarSAP", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
                        da.SelectCommand.Parameters.AddWithValue("@DocEntrySap ", DocEntrySap);

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

        public int UpdateDocEntryEnviarSAPDetalle(int IdEnviarSapDetalle, int DocEntrySap, int BorradorFirme)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateDocEntryEnviarSAPDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEnviarSapDetalle", IdEnviarSapDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@DocEntrySap", DocEntrySap);
                        da.SelectCommand.Parameters.AddWithValue("@BorradorFirme", BorradorFirme);

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
        public int UpdateDocEntryOPCHDetalle(int IdOPCHDetalle, int DocEntrySap, int BorradorFirme)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateDocEntryOPCHDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCHDetalle", IdOPCHDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@DocEntrySap", DocEntrySap);
                        da.SelectCommand.Parameters.AddWithValue("@BorradorFirme", BorradorFirme);

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
        public int UpdateDocEntryORPCDetalle(int IdORPCDetalle, int DocEntrySap, int BorradorFirme)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateDocEntryORPCDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdORPCDetalle", IdORPCDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@DocEntrySap", DocEntrySap);
                        da.SelectCommand.Parameters.AddWithValue("@BorradorFirme", BorradorFirme);

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
        public List<OPCHDetalle> ObtenerDocEntryFacturaxNotaCredito(int IdOPCHDetalle)
        {
            string mensaje_error = "";
            List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDocEntryOPCH", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOPCHDetalle", IdOPCHDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OPCHDetalle oOPCHDetalle = new OPCHDetalle();
                        oOPCHDetalle.DocEntry = Convert.ToInt32(drd["DocEntry"].ToString());
                        oOPCHDetalle.EnviadoPor = Convert.ToInt32(drd["EnviadoPor"].ToString());
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

        public List<IntegradorV1DTO> ListarCamposxIdObra(int IdObra, DateTime FechaInicio, DateTime FechaFin, ref string mensaje_error)
        {
            List<IntegradorV1DTO> lstIntegradorV1DTO = new List<IntegradorV1DTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCamposxObrayFecha", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFin", FechaFin);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorV1DTO oIntegradorV1DTO = new IntegradorV1DTO();
                        oIntegradorV1DTO.IdTablaOriginal = Convert.ToInt32(drd["IdTablaOriginal"].ToString());
                        oIntegradorV1DTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oIntegradorV1DTO.ObjType = (drd["ObjType"].ToString());
                        oIntegradorV1DTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oIntegradorV1DTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oIntegradorV1DTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oIntegradorV1DTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oIntegradorV1DTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oIntegradorV1DTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oIntegradorV1DTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oIntegradorV1DTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oIntegradorV1DTO.Referencia = (drd["Referencia"].ToString());
                        oIntegradorV1DTO.Comentario = (drd["Comentario"].ToString());
                        oIntegradorV1DTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oIntegradorV1DTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oIntegradorV1DTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oIntegradorV1DTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oIntegradorV1DTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oIntegradorV1DTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oIntegradorV1DTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oIntegradorV1DTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oIntegradorV1DTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oIntegradorV1DTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oIntegradorV1DTO.NombSerie = (drd["NombSerie"].ToString());
                        oIntegradorV1DTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oIntegradorV1DTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oIntegradorV1DTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oIntegradorV1DTO.NombObra = (drd["NombObra"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oIntegradorV1DTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oIntegradorV1DTO.Moneda = (drd["Moneda"].ToString());
                        oIntegradorV1DTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oIntegradorV1DTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oIntegradorV1DTO.Proveedor = drd["Proveedor"].ToString();
                        oIntegradorV1DTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oIntegradorV1DTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());
                        oIntegradorV1DTO.Inventario = Convert.ToBoolean(drd["Inventario"].ToString());
                        oIntegradorV1DTO.TablaOriginal = drd["TablaOriginal"].ToString();
                        oIntegradorV1DTO.SapDocNum = int.Parse(drd["SapDocNum"].ToString());
                        oIntegradorV1DTO.EnviadoPor = int.Parse(drd["EnviadoPor"].ToString());
                        oIntegradorV1DTO.DocEntry = int.Parse(drd["DocEntry"].ToString());
                        lstIntegradorV1DTO.Add(oIntegradorV1DTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstIntegradorV1DTO;
        }

        public List<IntegradorTasaDetDTO> ObtenerTasaDetSAP(ref string mensaje_error)
        {
            List<IntegradorTasaDetDTO> lstIntegradorTasaDetDTO = new List<IntegradorTasaDetDTO>();

            using (SqlConnection cn = new ConexionSQLSAP().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerTasaDet", cn);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorTasaDetDTO oIntegradorTasaDetDTO = new IntegradorTasaDetDTO();
                        oIntegradorTasaDetDTO.Code = Convert.ToInt32(drd["Code"].ToString());
                        oIntegradorTasaDetDTO.Name = drd["Name"].ToString();

                        lstIntegradorTasaDetDTO.Add(oIntegradorTasaDetDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstIntegradorTasaDetDTO;
            }
        }
        public List<IntegradorGrupoDetDTO> ObtenerGrupoDetSAP(ref string mensaje_error)
        {
            List<IntegradorGrupoDetDTO> lstIntegradorGrupoDetDTO = new List<IntegradorGrupoDetDTO>();

            using (SqlConnection cn = new ConexionSQLSAP().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGrupoDet", cn);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorGrupoDetDTO oIntegradorGrupoDetDTO = new IntegradorGrupoDetDTO();
                        oIntegradorGrupoDetDTO.Code = Convert.ToInt32(drd["Code"].ToString());
                        oIntegradorGrupoDetDTO.Name = drd["Name"].ToString();
                        oIntegradorGrupoDetDTO.U_Descripcion = drd["U_Descripcion"].ToString();

                        lstIntegradorGrupoDetDTO.Add(oIntegradorGrupoDetDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstIntegradorGrupoDetDTO;
            }
        }

        public ListaTrabajoDTO ValidarGrupoEnviado(int GrupoCreacion,ref string mensaje_error)
        {


            using (SqlConnection cn = new Conexion().conectar())
            {
                ListaTrabajoDTO oListaTrabajoDTO = new ListaTrabajoDTO();
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarEnviaGrupoCreacion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                       
                        oListaTrabajoDTO.EstadoEnviado = drd["EstadoEnviado"].ToString();

                        
                    }
                    drd.Close();

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                   
                }
                return oListaTrabajoDTO;


            }
        }

        public List<IntegradorSerieSapDTO> ObtenerSerieSAP(int ObjectCode, ref string mensaje_error)
        {
            List<IntegradorSerieSapDTO> lstIntegradorGrupoDetDTO = new List<IntegradorSerieSapDTO>();

            using (SqlConnection cn = new ConexionSQLSAP().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_GETSERIESAP", cn);
                    da.SelectCommand.Parameters.AddWithValue("@ObjectCode", ObjectCode);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorSerieSapDTO oIntegradorSerieSapDTO = new IntegradorSerieSapDTO();
                        oIntegradorSerieSapDTO.ObjectCode = Convert.ToInt32(drd["ObjectCode"].ToString());
                        oIntegradorSerieSapDTO.Series = Convert.ToInt32(drd["Series"].ToString());
                        oIntegradorSerieSapDTO.SeriesName = drd["SeriesName"].ToString();

                        lstIntegradorGrupoDetDTO.Add(oIntegradorSerieSapDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstIntegradorGrupoDetDTO;
            }
        }

        public List<IntegradorCondPagoDetDTO> ObtenerCondPagoDetSAP(string GrupoDet,ref string mensaje_error)
        {
            List<IntegradorCondPagoDetDTO> lstIntegradorCondPagoDetDTO = new List<IntegradorCondPagoDetDTO>();

            using (SqlConnection cn = new ConexionSQLSAP().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerCondicionPagoDet", cn);
                    da.SelectCommand.Parameters.AddWithValue("@GrupoDet", GrupoDet);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorCondPagoDetDTO oIntegradorCondPagoDetDTO = new IntegradorCondPagoDetDTO();
                        oIntegradorCondPagoDetDTO.GroupNum = Convert.ToInt32(drd["GroupNum"].ToString());
                        oIntegradorCondPagoDetDTO.PymntGroup = drd["PymntGroup"].ToString();
                        oIntegradorCondPagoDetDTO.ExtraDays = Convert.ToInt32(drd["ExtraDays"].ToString());
                        oIntegradorCondPagoDetDTO.ExtraMonth = Convert.ToInt32(drd["ExtraMonth"].ToString());

                        lstIntegradorCondPagoDetDTO.Add(oIntegradorCondPagoDetDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstIntegradorCondPagoDetDTO;
            }
        }


        public List<IntegradorV1DTO> ListarEnviarSapConDetallexGrupoCreacion(int GrupoCreacion)
        {
            List<IntegradorV1DTO> lstIntegradorV1DTO = new List<IntegradorV1DTO>();


            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {

                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEnviarSap", cn);
                    da.SelectCommand.Parameters.AddWithValue("@GrupoCreacion", GrupoCreacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IntegradorV1DTO oIntegradorV1DTO = new IntegradorV1DTO();
                        oIntegradorV1DTO.IdEnviarSap = Convert.ToInt32(drd["IdEnviarSap"].ToString());
                        oIntegradorV1DTO.IdTablaOriginal = Convert.ToInt32(drd["IdTablaOriginal"].ToString());
                        oIntegradorV1DTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oIntegradorV1DTO.ObjType = (drd["ObjType"].ToString());
                        oIntegradorV1DTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oIntegradorV1DTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oIntegradorV1DTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oIntegradorV1DTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oIntegradorV1DTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oIntegradorV1DTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oIntegradorV1DTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oIntegradorV1DTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oIntegradorV1DTO.Referencia = (drd["Referencia"].ToString());
                        oIntegradorV1DTO.Comentario = (drd["Comentario"].ToString());
                        oIntegradorV1DTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oIntegradorV1DTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oIntegradorV1DTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oIntegradorV1DTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oIntegradorV1DTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oIntegradorV1DTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oIntegradorV1DTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oIntegradorV1DTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oIntegradorV1DTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oIntegradorV1DTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oIntegradorV1DTO.NombSerie = (drd["NombSerie"].ToString());
                        oIntegradorV1DTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oIntegradorV1DTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oIntegradorV1DTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oIntegradorV1DTO.NombObra = (drd["NombObra"].ToString());
                        oIntegradorV1DTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oIntegradorV1DTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oIntegradorV1DTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oIntegradorV1DTO.Moneda = (drd["Moneda"].ToString());
                        oIntegradorV1DTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oIntegradorV1DTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oIntegradorV1DTO.Proveedor = drd["Proveedor"].ToString();
                        oIntegradorV1DTO.TipoDocumentoRef = drd["TipoDocumentoRef"].ToString();
                        oIntegradorV1DTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());
                        oIntegradorV1DTO.Inventario = bool.Parse(drd["Inventario"].ToString());
                        oIntegradorV1DTO.IdGlosaContable = int.Parse(drd["IdGlosaContable"].ToString());
                        oIntegradorV1DTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oIntegradorV1DTO.IdTipoRegistro = int.Parse(drd["IdTipoRegistro"].ToString());
                        oIntegradorV1DTO.IdSemana = int.Parse(drd["IdSemana"].ToString());
                        oIntegradorV1DTO.NumOC = drd["NumOC"].ToString();
                        oIntegradorV1DTO.Inventario = bool.Parse(drd["Inventario"].ToString());
                        oIntegradorV1DTO.ConsumoM3 = decimal.Parse(drd["ConsumoM3"].ToString());
                        oIntegradorV1DTO.ConsumoHW = decimal.Parse(drd["ConsumoHW"].ToString());
                        oIntegradorV1DTO.TasaDetraccion = int.Parse(drd["TasaDetraccion"].ToString());
                        oIntegradorV1DTO.GrupoDetraccion = int.Parse(drd["GrupoDetraccion"].ToString());
                        oIntegradorV1DTO.idCondicionPago = int.Parse(drd["IdCondicionPago"].ToString());
                        oIntegradorV1DTO.ConsumoM3 = decimal.Parse(drd["ConsumoM3"].ToString());
                        oIntegradorV1DTO.ConsumoHW = decimal.Parse(drd["ConsumoHW"].ToString());
                        oIntegradorV1DTO.TasaDetraccion = int.Parse(drd["TasaDetraccion"].ToString());
                        oIntegradorV1DTO.GrupoDetraccion = int.Parse(drd["GrupoDetraccion"].ToString());
                        oIntegradorV1DTO.IdTipoDocumentoRef = int.Parse(drd["IdTipoDocumentoRef"].ToString());
                        oIntegradorV1DTO.TablaOriginal = drd["TablaOriginal"].ToString();
                        oIntegradorV1DTO.SerieDocBaseORPC = drd["SerieDocBaseORPC"].ToString();
                        oIntegradorV1DTO.SerieSAP = int.Parse(drd["SerieSAP"].ToString());
                        oIntegradorV1DTO.CondicionPagoDet = int.Parse(drd["CondicionPagoDet"].ToString());



                        int IdEnviarSap = int.Parse(drd["IdEnviarSap"].ToString());
                        Int32 filasdetalle = 0;
                        Int32 filasAnexo = 0;
                        using (SqlConnection cn2 = new Conexion().conectar())
                        {
                            try
                            {
                                cn2.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter("SMC_ListarEnviarSapDetalle", cn2);
                                da2.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);                            
                                da2.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader dr2 = da2.SelectCommand.ExecuteReader();
                                while (dr2.Read())
                                {
                                    filasdetalle++;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }



                        oIntegradorV1DTO.detalles = new IntegradorV1Detalle[filasdetalle];
                        using (SqlConnection cn3 = new Conexion().conectar())
                        {
                            try
                            {
                                cn3.Open();
                                SqlDataAdapter da3 = new SqlDataAdapter("SMC_ListarEnviarSapDetalle", cn3);
                                da3.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
                                da3.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drd3 = da3.SelectCommand.ExecuteReader();
                                Int32 posicion = 0;
                                while (drd3.Read())
                                {
                                    IntegradorV1Detalle oIntegradorV1Detalle = new IntegradorV1Detalle();
                                    oIntegradorV1Detalle.IdEnviarSapDetalle = Convert.ToInt32(drd3["IdEnviarSapDetalle"].ToString());
                                    oIntegradorV1Detalle.IdEnviarSap = Convert.ToInt32(drd3["IdEnviarSap"].ToString());
                                    oIntegradorV1Detalle.IdTablaOriginal = Convert.ToInt32(drd3["IdTablaOriginal"].ToString());
                                    oIntegradorV1Detalle.IdTablaOriginalDetalle = Convert.ToInt32(drd3["IdTablaOriginalDetalle"].ToString());
                                    oIntegradorV1Detalle.NCIdOPCHDetalle = Convert.ToInt32(drd3["NCIdOPCHDetalle"].ToString());
                                    oIntegradorV1Detalle.DocEntrySap = Convert.ToInt32(drd3["DocEntrySap"].ToString());
                                    oIntegradorV1Detalle.DescripcionArticulo = (drd3["DescripcionArticulo"].ToString());
                                    oIntegradorV1Detalle.IdArticulo = Convert.ToInt32(drd3["IdArticulo"].ToString());
                                    oIntegradorV1Detalle.IdGrupoUnidadMedida = Convert.ToInt32(drd3["IdGrupoUnidadMedida"].ToString());
                                    oIntegradorV1Detalle.IdDefinicionGrupoUnidad = Convert.ToInt32(drd3["IdDefinicionGrupoUnidad"].ToString());
                                    oIntegradorV1Detalle.Cantidad = Convert.ToDecimal(drd3["Cantidad"].ToString());
                                    oIntegradorV1Detalle.valor_unitario = Convert.ToDecimal(drd3["valor_unitario"].ToString());
                                    oIntegradorV1Detalle.precio_unitario = Convert.ToDecimal(drd3["precio_unitario"].ToString());
                                    oIntegradorV1Detalle.total_base_igv = Convert.ToDecimal(drd3["total_base_igv"].ToString());
                                    oIntegradorV1Detalle.porcentaje_igv = Convert.ToDecimal(drd3["porcentaje_igv"].ToString());
                                    oIntegradorV1Detalle.total_igv = Convert.ToDecimal(drd3["total_igv"].ToString());
                                    oIntegradorV1Detalle.total_impuestos = Convert.ToDecimal(drd3["total_impuestos"].ToString());
                                    oIntegradorV1Detalle.total_valor_item = Convert.ToDecimal(drd3["total_valor_item"].ToString());
                                    oIntegradorV1Detalle.total_item = Convert.ToDecimal(drd3["total_item"].ToString());
                                    oIntegradorV1Detalle.IdIndicadorImpuesto = Convert.ToInt32(drd3["IdIndicadorImpuesto"].ToString());
                                    oIntegradorV1Detalle.CodImpuesto = (drd3["CodImpuesto"].ToString());
                                    oIntegradorV1Detalle.NombImpuesto = (drd3["NombImpuesto"].ToString());
                                    oIntegradorV1Detalle.IdGrupoUnidadMedida = Convert.ToInt32(drd3["IdGrupoUnidadMedida"].ToString());
                                    oIntegradorV1Detalle.PrecioUnidadBase = Convert.ToDecimal(drd3["PrecioUnidadBase"].ToString());
                                    oIntegradorV1Detalle.CodigoArticulo = (drd3["CodigoArticulo"].ToString());
                                    oIntegradorV1Detalle.NombCuadrilla = drd3["NombCuadrilla"].ToString();
                                    oIntegradorV1Detalle.NombResponsable = drd3["NombResponsable"].ToString();
                                    oIntegradorV1Detalle.TipoServicio = drd3["TipoServicio"].ToString();
                                    oIntegradorV1Detalle.IdCuadrilla = Convert.ToInt32((String.IsNullOrEmpty(drd3["IdCuadrilla"].ToString())) ? "0" : drd3["IdCuadrilla"].ToString());
                                    oIntegradorV1Detalle.IdResponsable = Convert.ToInt32((String.IsNullOrEmpty(drd3["IdResponsable"].ToString())) ? "0" : drd3["IdResponsable"].ToString());
                                    oIntegradorV1Detalle.CantidadNotaCredito = Convert.ToDecimal((String.IsNullOrEmpty(drd3["CantidadNotaCredito"].ToString())) ? "0" : drd3["CantidadNotaCredito"].ToString());



                                    oIntegradorV1DTO.detalles[posicion] = oIntegradorV1Detalle;
                                    posicion = posicion + 1;
                                }

                            }
                            catch (Exception ex)
                            {
                            }

                        }

                        using (SqlConnection cn4 = new Conexion().conectar())
                        {
                            try
                            {
                                cn4.Open();
                                SqlDataAdapter da4 = new SqlDataAdapter("SMC_ObtenerAnexosOpchxIdEnviarSap", cn4);
                                da4.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
                                da4.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader dr4 = da4.SelectCommand.ExecuteReader();
                                while (dr4.Read())
                                {
                                    filasAnexo++;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        oIntegradorV1DTO.AnexoDetalle = new AnexoDTO[filasAnexo];
                        using (SqlConnection cn5 = new Conexion().conectar())
                        {
                            try
                            {
                                cn5.Open();
                                SqlDataAdapter da5 = new SqlDataAdapter("SMC_ObtenerAnexosOpchxIdEnviarSap", cn5);
                                da5.SelectCommand.Parameters.AddWithValue("@IdEnviarSap", IdEnviarSap);
                                da5.SelectCommand.CommandType = CommandType.StoredProcedure;
                                SqlDataReader drd5 = da5.SelectCommand.ExecuteReader();
                                Int32 posicion = 0;
                                while (drd5.Read())
                                {
                                    AnexoDTO oAnexoDTO = new AnexoDTO();
                                    oAnexoDTO.IdAnexo = Convert.ToInt32(drd5["IdAnexo"].ToString());
                                    oAnexoDTO.ruta = (drd5["ruta"].ToString());
                                    oAnexoDTO.IdSociedad = Convert.ToInt32(drd5["IdSociedad"].ToString());
                                    oAnexoDTO.Tabla = (drd5["Tabla"].ToString());
                                    oAnexoDTO.IdTabla = Convert.ToInt32(drd5["IdTabla"].ToString());
                                    oAnexoDTO.NombreArchivo = (drd5["NombreArchivo"].ToString());



                                    oIntegradorV1DTO.AnexoDetalle[posicion] = oAnexoDTO;
                                    posicion = posicion + 1;
                                }

                            }
                            catch (Exception ex)
                            {
                            }

                            lstIntegradorV1DTO.Add(oIntegradorV1DTO);

                        }


                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }

            return lstIntegradorV1DTO;
        }

    }
}
