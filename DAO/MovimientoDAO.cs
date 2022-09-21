using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class MovimientoDAO
    {


        #region InsertUpdateMovimientoOPDN
        public int InsertUpdateMovimientoOPDN(OpdnDTO oOpdnDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimientoOPDN", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDN", oOpdnDTO.IdOPDN);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oOpdnDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oOpdnDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oOpdnDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oOpdnDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@ObjType", oOpdnDTO.ObjType);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oOpdnDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@CodMoneda", oOpdnDTO.CodMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oOpdnDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", oOpdnDTO.IdCliente);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oOpdnDTO.FechaContabilizacion.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oOpdnDTO.FechaDocumento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaVencimiento", oOpdnDTO.FechaVencimiento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@IdListaPrecios", oOpdnDTO.IdListaPrecios);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oOpdnDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oOpdnDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotal", oOpdnDTO.SubTotal);
                        da.SelectCommand.Parameters.AddWithValue("@Impuesto", oOpdnDTO.Impuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oOpdnDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oOpdnDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oOpdnDTO.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oOpdnDTO.IdResponsable);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumentoRef", oOpdnDTO.IdTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@NumSerieTipoDocumentoRef", oOpdnDTO.NumSerieTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@EntregadoA", oOpdnDTO.EntregadoA);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oOpdnDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oOpdnDTO.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oOpdnDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", oOpdnDTO.IdCondicionPago);




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
        #endregion

        #region InsertUpdateOPDNDetalle
        public int InsertUpdateOPDNDetalle(OPDNDetalle oOPDNDetalle, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateOPDNDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDNDetalle", oOPDNDetalle.IdOPDNDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdOPDN", oOPDNDetalle.IdOPDN);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oOPDNDetalle.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionArticulo", oOPDNDetalle.DescripcionArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdDefinicionGrupoUnidad", oOPDNDetalle.IdDefinicionGrupoUnidad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oOPDNDetalle.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", oOPDNDetalle.Cantidad);
                        da.SelectCommand.Parameters.AddWithValue("@Igv", oOPDNDetalle.Igv);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadBase", oOPDNDetalle.PrecioUnidadBase);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadTotal", oOPDNDetalle.PrecioUnidadTotal);
                        da.SelectCommand.Parameters.AddWithValue("@TotalBase", oOPDNDetalle.TotalBase);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oOPDNDetalle.Total);
                        da.SelectCommand.Parameters.AddWithValue("@CuentaContable", oOPDNDetalle.CuentaContable);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oOPDNDetalle.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdAfectacionIgv", oOPDNDetalle.IdAfectacionIgv);
                        da.SelectCommand.Parameters.AddWithValue("@Descuento", oOPDNDetalle.Descuento);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oOPDNDetalle.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@valor_unitario", oOPDNDetalle.valor_unitario);
                        da.SelectCommand.Parameters.AddWithValue("@precio_unitario", oOPDNDetalle.precio_unitario);
                        //da.SelectCommand.Parameters.AddWithValue("@codigo_tipo_afectacion_igv", oOPDNDetalle.codigo_tipo_afectacion_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_base_igv", oOPDNDetalle.total_base_igv);
                        da.SelectCommand.Parameters.AddWithValue("@porcentaje_igv", oOPDNDetalle.porcentaje_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_igv", oOPDNDetalle.total_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_impuestos", oOPDNDetalle.total_impuestos);
                        da.SelectCommand.Parameters.AddWithValue("@total_valor_item", oOPDNDetalle.total_valor_item);
                        da.SelectCommand.Parameters.AddWithValue("@total_item", oOPDNDetalle.total_item);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oOPDNDetalle.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oOPDNDetalle.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@NombTablaOrigen", oOPDNDetalle.NombTablaOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@IdOrigen", oOPDNDetalle.IdOrigen);

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


        #region InsertUpdateMovimiento
        public int InsertUpdateMovimiento(MovimientoDTO oMovimientoDTO,ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimiento", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", oMovimientoDTO.IdMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oMovimientoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oMovimientoDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oMovimientoDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oMovimientoDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@ObjType", oMovimientoDTO.ObjType);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oMovimientoDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@CodMoneda", oMovimientoDTO.CodMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oMovimientoDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", oMovimientoDTO.IdCliente);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oMovimientoDTO.FechaContabilizacion.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oMovimientoDTO.FechaDocumento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaVencimiento", oMovimientoDTO.FechaVencimiento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@IdListaPrecios", oMovimientoDTO.IdListaPrecios);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oMovimientoDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oMovimientoDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotal", oMovimientoDTO.SubTotal);
                        da.SelectCommand.Parameters.AddWithValue("@Impuesto", oMovimientoDTO.Impuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oMovimientoDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oMovimientoDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oMovimientoDTO.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oMovimientoDTO.IdResponsable);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumentoRef", oMovimientoDTO.IdTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@NumSerieTipoDocumentoRef", oMovimientoDTO.NumSerieTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@EntregadoA", oMovimientoDTO.EntregadoA);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oMovimientoDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oMovimientoDTO.IdCentroCosto);



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
        #endregion

        #region InsertUpdateMovimientoDetalle
        public int InsertUpdateMovimientoDetalle(MovimientoDetalleDTO oMovimientoDetalleDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimientoDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimientoDetalle", oMovimientoDetalleDTO.IdMovimientoDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", oMovimientoDetalleDTO.IdMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oMovimientoDetalleDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionArticulo", oMovimientoDetalleDTO.DescripcionArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdDefinicionGrupoUnidad", oMovimientoDetalleDTO.IdDefinicionGrupoUnidad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oMovimientoDetalleDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", oMovimientoDetalleDTO.Cantidad);
                        da.SelectCommand.Parameters.AddWithValue("@Igv", oMovimientoDetalleDTO.Igv);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadBase", oMovimientoDetalleDTO.PrecioUnidadBase);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadTotal", oMovimientoDetalleDTO.PrecioUnidadTotal);
                        da.SelectCommand.Parameters.AddWithValue("@TotalBase", oMovimientoDetalleDTO.TotalBase);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oMovimientoDetalleDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@CuentaContable", oMovimientoDetalleDTO.CuentaContable);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oMovimientoDetalleDTO.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdAfectacionIgv", oMovimientoDetalleDTO.IdAfectacionIgv);
                        da.SelectCommand.Parameters.AddWithValue("@Descuento", oMovimientoDetalleDTO.Descuento);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oMovimientoDetalleDTO.IdAlmacenDestino);
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

        
        public List<MovimientoDTO> ObtenerMovimientosTransferencias(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosTransferencias", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MovimientoDTO oMovimientoDTO = new MovimientoDTO();
                        oMovimientoDTO.IdMovimiento = Convert.ToInt32(drd["IdMovimiento"].ToString());
                        oMovimientoDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oMovimientoDTO.ObjType = (drd["ObjType"].ToString());
                        oMovimientoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oMovimientoDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oMovimientoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oMovimientoDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oMovimientoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oMovimientoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oMovimientoDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oMovimientoDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oMovimientoDTO.Referencia = (drd["Referencia"].ToString());
                        oMovimientoDTO.Comentario = (drd["Comentario"].ToString());
                        oMovimientoDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oMovimientoDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oMovimientoDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oMovimientoDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oMovimientoDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oMovimientoDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oMovimientoDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oMovimientoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oMovimientoDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oMovimientoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oMovimientoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oMovimientoDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oMovimientoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oMovimientoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oMovimientoDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oMovimientoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oMovimientoDTO.NombObra = (drd["NombObra"].ToString());


                        lstMovimientoDTO.Add(oMovimientoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMovimientoDTO;
        }

        #region ObtenerMovimientosIngresos
        public List<MovimientoDTO> ObtenerMovimientosIngresos(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosIngresos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MovimientoDTO oMovimientoDTO = new MovimientoDTO();
                        oMovimientoDTO.IdMovimiento = Convert.ToInt32(drd["IdMovimiento"].ToString());
                        oMovimientoDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oMovimientoDTO.ObjType = (drd["ObjType"].ToString());
                        oMovimientoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oMovimientoDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oMovimientoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oMovimientoDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oMovimientoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oMovimientoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oMovimientoDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oMovimientoDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oMovimientoDTO.Referencia = (drd["Referencia"].ToString());
                        oMovimientoDTO.Comentario = (drd["Comentario"].ToString());
                        oMovimientoDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oMovimientoDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oMovimientoDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oMovimientoDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oMovimientoDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oMovimientoDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oMovimientoDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oMovimientoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oMovimientoDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oMovimientoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oMovimientoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oMovimientoDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oMovimientoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oMovimientoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oMovimientoDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oMovimientoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        oMovimientoDTO.NombObra = (drd["NombObra"].ToString());

                        
                        lstMovimientoDTO.Add(oMovimientoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMovimientoDTO;
        }
        #endregion


        
        public List<MovimientoDTO> ObtenerMovimientosSalida(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosSalidas", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MovimientoDTO oMovimientoDTO = new MovimientoDTO();
                        oMovimientoDTO.IdMovimiento = Convert.ToInt32(drd["IdMovimiento"].ToString());
                        oMovimientoDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oMovimientoDTO.ObjType = (drd["ObjType"].ToString());
                        oMovimientoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oMovimientoDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oMovimientoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oMovimientoDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oMovimientoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oMovimientoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oMovimientoDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oMovimientoDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oMovimientoDTO.Referencia = (drd["Referencia"].ToString());
                        oMovimientoDTO.Comentario = (drd["Comentario"].ToString());
                        oMovimientoDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oMovimientoDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oMovimientoDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oMovimientoDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oMovimientoDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oMovimientoDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oMovimientoDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oMovimientoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oMovimientoDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oMovimientoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oMovimientoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oMovimientoDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oMovimientoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oMovimientoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oMovimientoDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oMovimientoDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        lstMovimientoDTO.Add(oMovimientoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMovimientoDTO;
        }


        #region ObtenerDAtosxId
        public MovimientoDTO ObtenerMovimientosDetallexIdMovimiento(int IdMovimiento, ref string mensaje_error)
        {
            MovimientoDTO oMovimientoDTO = new MovimientoDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_MovimientoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oMovimientoDTO.IdMovimiento = Convert.ToInt32(drd["IdMovimiento"].ToString());
                        oMovimientoDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oMovimientoDTO.ObjType = (drd["ObjType"].ToString());
                        oMovimientoDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oMovimientoDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oMovimientoDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oMovimientoDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oMovimientoDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oMovimientoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oMovimientoDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oMovimientoDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oMovimientoDTO.Referencia = (drd["Referencia"].ToString());
                        oMovimientoDTO.Comentario = (drd["Comentario"].ToString());
                        oMovimientoDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oMovimientoDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oMovimientoDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oMovimientoDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oMovimientoDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oMovimientoDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oMovimientoDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oMovimientoDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oMovimientoDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oMovimientoDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oMovimientoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oMovimientoDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oMovimientoDTO.NombSerie = (drd["NombSerie"].ToString());
                        oMovimientoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oMovimientoDTO.IdCuadrilla = Convert.ToInt32(drd["IdCuadrilla"].ToString());
                        oMovimientoDTO.IdAlmacenDestino = Convert.ToInt32(drd["IdAlmacenDestino"].ToString());
                        oMovimientoDTO.IdResponsable = Convert.ToInt32(drd["IdResponsable"].ToString());
                        oMovimientoDTO.IdTipoDocumentoRef = Convert.ToInt32(drd["IdTipoDocumentoRef"].ToString());
                        oMovimientoDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());
                        oMovimientoDTO.EntregadoA = Convert.ToInt32(drd["EntregadoA"].ToString());
                        oMovimientoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oMovimientoDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oMovimientoDTO.IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(drd["IdUsuario"].ToString())) ? "0" : drd["IdUsuario"].ToString());
                        oMovimientoDTO.CreatedAt = Convert.ToDateTime((String.IsNullOrEmpty(drd["CreatedAt"].ToString())) ? "2022-08-01" : drd["CreatedAt"].ToString());
                        oMovimientoDTO.NombUsuario = (drd["NombUsuario"].ToString());
            

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
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDetallesMovimientosxIdMovimiento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
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

            oMovimientoDTO.detalles = new MovimientoDetalleDTO[filasdetalle];

            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDetallesMovimientosxIdMovimiento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr2 = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (dr2.Read())
                    {
                        MovimientoDetalleDTO oMovimientoDetalleDTO = new MovimientoDetalleDTO();
                        oMovimientoDetalleDTO.IdMovimiento = Convert.ToInt32(dr2["IdMovimiento"].ToString());
                        oMovimientoDetalleDTO.IdArticulo = Convert.ToInt32(dr2["IdArticulo"].ToString());
                        oMovimientoDetalleDTO.DescripcionArticulo = (dr2["DescripcionArticulo"].ToString());
                        oMovimientoDetalleDTO.IdDefinicionGrupoUnidad = Convert.ToInt32(dr2["IdDefinicionGrupoUnidad"].ToString());
                        oMovimientoDetalleDTO.IdUnidadMedidaBase = Convert.ToInt32(dr2["IdUnidadMedidaBase"].ToString());
                        oMovimientoDetalleDTO.IdUnidadMedida = Convert.ToInt32(dr2["IdUnidadMedida"].ToString());
                        oMovimientoDetalleDTO.IdAlmacen = Convert.ToInt32(dr2["IdAlmacen"].ToString());
                        oMovimientoDetalleDTO.CantidadBase = Convert.ToDecimal(dr2["CantidadBase"].ToString());
                        oMovimientoDetalleDTO.Cantidad = Convert.ToDecimal(dr2["Cantidad"].ToString());
                        oMovimientoDetalleDTO.Igv = Convert.ToDecimal(dr2["Igv"].ToString());
                        oMovimientoDetalleDTO.PrecioUnidadBase = Convert.ToDecimal(dr2["PrecioUnidadBase"].ToString());
                        oMovimientoDetalleDTO.PrecioUnidadTotal = Convert.ToDecimal(dr2["PrecioUnidadTotal"].ToString());
                        oMovimientoDetalleDTO.TotalBase = Convert.ToDecimal(dr2["TotalBase"].ToString());
                        oMovimientoDetalleDTO.Total = Convert.ToDecimal(dr2["Total"].ToString());
                        oMovimientoDetalleDTO.CuentaContable = Convert.ToInt32(dr2["CuentaContable"].ToString());
                        oMovimientoDetalleDTO.IdCentroCosto = Convert.ToInt32(dr2["IdCentroCosto"].ToString());
                        oMovimientoDetalleDTO.IdAfectacionIgv = Convert.ToInt32(dr2["IdAfectacionIgv"].ToString());
                        oMovimientoDetalleDTO.Descuento = Convert.ToDecimal(dr2["Descuento"].ToString());
                        oMovimientoDTO.detalles[posicion] = oMovimientoDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
                #endregion

                return oMovimientoDTO;
            }
            #endregion
        }


        #region InsertUpdateMovimientoOPCH
        public int InsertUpdateMovimientoOPCH(OpchDTO oOpchDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimientoOPCH", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCH", oOpchDTO.IdOPCH);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oOpchDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oOpchDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oOpchDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oOpchDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@ObjType", oOpchDTO.ObjType);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oOpchDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@CodMoneda", oOpchDTO.CodMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oOpchDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", oOpchDTO.IdCliente);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oOpchDTO.FechaContabilizacion.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oOpchDTO.FechaDocumento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaVencimiento", oOpchDTO.FechaVencimiento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@IdListaPrecios", oOpchDTO.IdListaPrecios);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oOpchDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oOpchDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotal", oOpchDTO.SubTotal);
                        da.SelectCommand.Parameters.AddWithValue("@Impuesto", oOpchDTO.Impuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oOpchDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oOpchDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oOpchDTO.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oOpchDTO.IdResponsable);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumentoRef", oOpchDTO.IdTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@NumSerieTipoDocumentoRef", oOpchDTO.NumSerieTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@EntregadoA", oOpchDTO.EntregadoA);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oOpchDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oOpchDTO.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oOpchDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@idCondicionPago", oOpchDTO.idCondicionPago);

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
        #endregion


        #region InsertUpdateOPCHDetalle
        public int InsertUpdateOPCHDetalle(OPCHDetalle oOPCHDetalle, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateOPCHDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCHDetalle", oOPCHDetalle.IdOPCHDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCH", oOPCHDetalle.IdOPCH);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oOPCHDetalle.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionArticulo", oOPCHDetalle.DescripcionArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdDefinicionGrupoUnidad", oOPCHDetalle.IdDefinicionGrupoUnidad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oOPCHDetalle.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", oOPCHDetalle.Cantidad);
                        da.SelectCommand.Parameters.AddWithValue("@Igv", oOPCHDetalle.Igv);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadBase", oOPCHDetalle.PrecioUnidadBase);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadTotal", oOPCHDetalle.PrecioUnidadTotal);
                        da.SelectCommand.Parameters.AddWithValue("@TotalBase", oOPCHDetalle.TotalBase);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oOPCHDetalle.Total);
                        da.SelectCommand.Parameters.AddWithValue("@CuentaContable", oOPCHDetalle.CuentaContable);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oOPCHDetalle.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdAfectacionIgv", oOPCHDetalle.IdAfectacionIgv);
                        da.SelectCommand.Parameters.AddWithValue("@Descuento", oOPCHDetalle.Descuento);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oOPCHDetalle.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@valor_unitario", oOPCHDetalle.valor_unitario);
                        da.SelectCommand.Parameters.AddWithValue("@precio_unitario", oOPCHDetalle.precio_unitario);
                        //da.SelectCommand.Parameters.AddWithValue("@codigo_tipo_afectacion_igv", oOPDNDetalle.codigo_tipo_afectacion_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_base_igv", oOPCHDetalle.total_base_igv);
                        da.SelectCommand.Parameters.AddWithValue("@porcentaje_igv", oOPCHDetalle.porcentaje_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_igv", oOPCHDetalle.total_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_impuestos", oOPCHDetalle.total_impuestos);
                        da.SelectCommand.Parameters.AddWithValue("@total_valor_item", oOPCHDetalle.total_valor_item);
                        da.SelectCommand.Parameters.AddWithValue("@total_item", oOPCHDetalle.total_item);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oOPCHDetalle.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oOPCHDetalle.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@NombTablaOrigen", oOPCHDetalle.NombTablaOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@IdOrigen", oOPCHDetalle.IdOrigen);

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
