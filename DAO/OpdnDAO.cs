using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class OpdnDAO
    {

        public OpdnDTO ObtenerDatosxIDOPDN(int IdOpdn, string BaseDatos, ref string mensaje_error)
        {
            OpdnDTO oOpdnDTO = new OpdnDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oOpdnDTO.FechaEntrega = Convert.ToDateTime(drd["FechaEntrega1"].ToString());
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
                        oOpdnDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oOpdnDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        oOpdnDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oOpdnDTO.FechaEdicion = Convert.ToDateTime(String.IsNullOrEmpty(drd["FechaEdicion"].ToString()) ? "1990/01/01" : drd["FechaEdicion"].ToString());
                        oOpdnDTO.NombUsuarioEdicion = (String.IsNullOrEmpty(drd["NombUsuarioEdicion"].ToString()) ? "" : drd["NombUsuarioEdicion"].ToString());
                        oOpdnDTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());



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


        public List<OpdnDTO> ObtenerOPDNxEstado(int IdBase,int IdSociedad, string BaseDatos, ref string mensaje_error, string EstadoOPDN, int IdUsuario=0)
        {
            List<OpdnDTO> lstOPDNDTO = new List<OpdnDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPDNxEstado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPDN", EstadoOPDN);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

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
                        oOpdnDTO.NombProveedor = (drd["NombProveedor"].ToString());
                        oOpdnDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
                        oOpdnDTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());
                        oOpdnDTO.NOC = drd["NOC"].ToString();
                        oOpdnDTO.IdPedido = Convert.ToInt32(drd["IdPedido"].ToString());
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

        public List<OpdnDTO> ListarOPDNDTModalOPCH(int IdSociedad, string BaseDatos, ref string mensaje_error, string EstadoOPDN,int IdUsuario=0)
        {
            List<OpdnDTO> lstOPDNDTO = new List<OpdnDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPDNDTModalOPCH", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPDN", EstadoOPDN);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

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
                        oOpdnDTO.NumSerieTipoDocumentoRef = drd["NumSerieTipoDocumentoRef"].ToString();
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

        public List<OPDNDetalle> ObtenerDetalleOpdn(int IdOpdn, string BaseDatos, ref string mensaje_error)
        {
            List<OPDNDetalle> lstOPDNDetalle = new List<OPDNDetalle>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
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
                        oOPDNDetalle.CantidadDePedido = Convert.ToDecimal(drd["CantidadDePedido"].ToString());
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
                        oOPDNDetalle.CantidadDevolucion = Convert.ToDecimal((String.IsNullOrEmpty(drd["CantidadDevolucion"].ToString())) ? "0" : drd["CantidadDevolucion"].ToString());
                        oOPDNDetalle.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                        oOPDNDetalle.IdCuadrilla = Convert.ToInt32(drd["IdCuadrilla"].ToString());
                        oOPDNDetalle.IdResponsable = Convert.ToInt32(drd["IdResponsable"].ToString());
                        oOPDNDetalle.NombCuadrilla = (drd["NombCuadrilla"].ToString());
                        oOPDNDetalle.NombResponsable = (drd["NombResponsable"].ToString());
                        oOPDNDetalle.TipoServicio = drd["TipoServicio"].ToString();


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
        public List<OPDNDetalle> ObtenerDetalleOpdnModal(int IdOpdn, string BaseDatos, ref string mensaje_error)
        {
            List<OPDNDetalle> lstOPDNDetalle = new List<OPDNDetalle>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOpdnDetalleModal", cn);
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

        


        public int UpdateTotalesOPDN(int IdOPDN, string BaseDatos, ref string mensaje_error)
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





        public List<AnexoDTO> ObtenerAnexoOpdn(int IdOpdn, string BaseDatos, ref string mensaje_error)
        {
            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosOpdnxIdOpdn", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOpdn", IdOpdn);
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

        public int UpdateOPDN(int IdUsuario, OpdnDTO oOpdnDTO, string BaseDatos, ref string mensaje_error)
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
                        string comentario = oOpdnDTO.Comentario == null ? " " : oOpdnDTO.Comentario;
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateOPDN", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDN", oOpdnDTO.IdOPDN);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumentoRef", oOpdnDTO.IdTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@NumSerieTipoDocumentoRef", oOpdnDTO.NumSerieTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", comentario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioEdicion", IdUsuario);

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
        public string ValidaExtorno(int IdOPDN, string BaseDatos, ref string mensaje_error)
        {
            string Valida = "0";
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarExtornoOPDN", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOPDN", IdOPDN);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        Valida = (drd["IdDocExtorno"].ToString());

                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return Valida;
        }
        public int ValidarOPDNTieneFactura(int IdOPDN, string BaseDatos, ref string mensaje_error)
        {
           
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarOPDNTieneFactura", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOPDN", IdOPDN);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    int rpta = int.Parse(da.SelectCommand.ExecuteScalar().ToString());
               
                    return rpta;


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                    return 0;
                }
            }
     
        }
        public List<OPDNDetalle> ObtenerStockParaExtornoOPDN(int IdOPDN, string BaseDatos, ref string mensaje_error)
        {
            List<OPDNDetalle> lstOPDNDetalle = new List<OPDNDetalle>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerStockParaExtornoOPDN", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdOPDN", IdOPDN);
                  

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OPDNDetalle oOPDNDetalle = new OPDNDetalle();
                        oOPDNDetalle.Resta = Convert.ToInt32(drd["Resta"].ToString());
                        oOPDNDetalle.DescripcionArticulo = drd["DescripcionArticulo"].ToString();
                        lstOPDNDetalle.Add(oOPDNDetalle);
                       
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOPDNDetalle;
        }
        public int ExtornoConfirmado(int IdOPDn,string EsServicio, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ExtornoConfirmadoOPDN", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDN", IdOPDn);
                        da.SelectCommand.Parameters.AddWithValue("@EsServicio", EsServicio);


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
        public List<OpdnDTO> ValidaTipoProductoOPDN(int ArticuloMuestra, string BaseDatos, ref string mensaje_error)
        {
            List<OpdnDTO> lstOPDNDTO = new List<OpdnDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidaTipoProductoOPDN", cn);
                    da.SelectCommand.Parameters.AddWithValue("@ArticuloMuestra", ArticuloMuestra);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OpdnDTO oOpdnDTO = new OpdnDTO();                   
                        oOpdnDTO.TipoArticulos = drd["TipoArticulos"].ToString();
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
        public int UpdateCuadrillas(OPDNDetalle oOPDNDetalle, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateOPDNCuadrillas", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDNDetalle", oOPDNDetalle.IdOPDNDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oOPDNDetalle.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oOPDNDetalle.IdResponsable);


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