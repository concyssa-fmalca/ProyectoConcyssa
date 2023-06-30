using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
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
                        da.SelectCommand.Parameters.AddWithValue("@FechaEntrega", oOpdnDTO.FechaEntrega.ToString("yyyyMMdd"));
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

                        int rpta = int.Parse(da.SelectCommand.ExecuteScalar().ToString());
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

                        da.SelectCommand.Parameters.AddWithValue("@IdDestinatario", oMovimientoDTO.IdDestinatario);
                        da.SelectCommand.Parameters.AddWithValue("@IdMotivoTraslado", oMovimientoDTO.IdMotivoTraslado);
                        da.SelectCommand.Parameters.AddWithValue("@IdTransportista", oMovimientoDTO.IdTransportista);
                        da.SelectCommand.Parameters.AddWithValue("@PlacaVehiculo", oMovimientoDTO.PlacaVehiculo);
                        da.SelectCommand.Parameters.AddWithValue("@MarcaVehiculo", oMovimientoDTO.MarcaVehiculo);
                        da.SelectCommand.Parameters.AddWithValue("@NumIdentidadConductor", oMovimientoDTO.NumIdentidadConductor);
                        da.SelectCommand.Parameters.AddWithValue("@NombreConductor", oMovimientoDTO.NombreConductor);
                        da.SelectCommand.Parameters.AddWithValue("@ApellidoConductor", oMovimientoDTO.ApellidoConductor);
                        da.SelectCommand.Parameters.AddWithValue("@LicenciaConductor", oMovimientoDTO.LicenciaConductor);
                        da.SelectCommand.Parameters.AddWithValue("@TipoTransporte", oMovimientoDTO.TipoTransporte);
                        //da.SelectCommand.Parameters.AddWithValue("@UbigeoPartida", oMovimientoDTO.UbigeoPartida);
                        //da.SelectCommand.Parameters.AddWithValue("@UbigeoLlegada", oMovimientoDTO.UbigeoLlegada);
                        da.SelectCommand.Parameters.AddWithValue("@Peso", oMovimientoDTO.Peso);
                        da.SelectCommand.Parameters.AddWithValue("@Bulto", oMovimientoDTO.Bulto);


                        da.SelectCommand.Parameters.AddWithValue("@IdObraDestino", oMovimientoDTO.IdObraDestino);

                        da.SelectCommand.Parameters.AddWithValue("@IdDocExtorno", oMovimientoDTO.detalles[0].IdMovimiento);
                        
                        


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
        public int InsertUpdateMovimientoDetalle(MovimientoDetalleDTO oMovimientoDetalleDTO,int ValidarIngresoSalidaOAmbos, ref string mensaje_error)
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
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oMovimientoDetalleDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@TablaOrigen", oMovimientoDetalleDTO.TablaOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@IdOrigen", oMovimientoDetalleDTO.IdOrigen);

                        da.SelectCommand.Parameters.AddWithValue("@ValidarIngresoSalidaOAmbos", ValidarIngresoSalidaOAmbos);
                        

                        int rpta = int.Parse(da.SelectCommand.ExecuteScalar().ToString());
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

        #region InsertUpdateMovimientoDetalleCuadrilla
        public int InsertUpdateMovimientoDetalleCuadrilla(int MovDetalle,MovimientoDetalleDTO oMovimientoDetalleDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimientoDetalleCuadrilla", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimientoDetalle", MovDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oMovimientoDetalleDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oMovimientoDetalleDTO.IdResponsable);
                        
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





        public int InsertUpdateMovimientoDetalleIngreso(MovimientoDetalleDTO oMovimientoDetalleDTO, int ValidarIngresoSalidaOAmbos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimientoDetalleIngreso", cn);
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
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oMovimientoDetalleDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@TablaOrigen", oMovimientoDetalleDTO.TablaOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@IdOrigen", oMovimientoDetalleDTO.IdOrigen);

                        da.SelectCommand.Parameters.AddWithValue("@ValidarIngresoSalidaOAmbos", 1);


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




        #region InsertUpdateTranferenciaPrevia
        public int InsertUpdateTranferenciaPrevia(MovimientoDTO oMovimientoDTO,int IdMovimiento, ref string mensaje_error)
        {
            var JsonMovimiento = JsonSerializer.Serialize(oMovimientoDTO);

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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateTranferenciaPrevia", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Id", oMovimientoDTO.IdMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oMovimientoDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Numero", oMovimientoDTO.Correlativo);
                        da.SelectCommand.Parameters.AddWithValue("@AlmacenInicial", oMovimientoDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@AlmacenDestino", oMovimientoDTO.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oMovimientoDTO.FechaDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oMovimientoDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oMovimientoDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oMovimientoDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@Json", JsonMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oMovimientoDTO.IdSociedad);
                        //da.SelectCommand.Parameters.AddWithValue("@JsonEditado", oMovimientoDTO.TotalBase);



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




        public int ActualziarJsonTranferencia(MovimientoDTO oMovimientoDTO, int IdMovimiento, ref string mensaje_error)
        {
            var JsonMovimiento = JsonSerializer.Serialize(oMovimientoDTO);

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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ActualziarJsonTranferencia", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@Json", JsonMovimiento);
                        //da.SelectCommand.Parameters.AddWithValue("@JsonEditado", oMovimientoDTO.TotalBase);



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








        public List<MovimientoDTO> ObtenerMovimientosTransferencias(int IdBase, int IdSociedad,int IdUsuario, ref string mensaje_error, int Estado = 3)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosTransferencias", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
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
                        oMovimientoDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());
                        oMovimientoDTO.NombUsuario = (drd["NombUsuario"].ToString());

                        oMovimientoDTO.SerieGuiaElectronica =(String.IsNullOrEmpty(drd["SerieGuiaElectronica"].ToString()) ? "" : drd["SerieGuiaElectronica"].ToString());
                        oMovimientoDTO.NumeroGuiaElectronica = Convert.ToInt32(String.IsNullOrEmpty(drd["NumeroGuiaElectronica"].ToString()) ? "0" : drd["NumeroGuiaElectronica"].ToString());

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








        public List<MovimientoTranferenciaFinal> ObtenerMovimientosTransferenciasFinal(int IdObraDestino,int IdSociedad,int IdUsuario, ref string mensaje_error)
        {
            List<MovimientoTranferenciaFinal> lstMovimientoDTO = new List<MovimientoTranferenciaFinal>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosTransferenciasFinal", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdObraDestino", IdObraDestino);
                    //da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MovimientoTranferenciaFinal oMovimientoDTO = new MovimientoTranferenciaFinal();
                        oMovimientoDTO.IdMovimiento = Convert.ToInt32(drd["IdMovimiento"].ToString());
                        oMovimientoDTO.AlmacenInicial = (drd["AlmacenInicial"].ToString());
                        oMovimientoDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oMovimientoDTO.Moneda = drd["Moneda"].ToString();
                        oMovimientoDTO.UsuarioCreador = drd["UsuarioCreador"].ToString();
                        oMovimientoDTO.SerieCorrelativo = drd["SerieCorrelativo"].ToString();
                        oMovimientoDTO.Guia = drd["Guia"].ToString();
                        oMovimientoDTO.NomObraOrigen = drd["NomObraOrigen"].ToString();
                        oMovimientoDTO.NomObraDestino = drd["NomObraDestino"].ToString();
                        //oMovimientoDTO.Cuadrilla = Convert.ToDecimal(drd["TipoCambio"].ToString());

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
        public List<MovimientoDTO> ObtenerMovimientosIngresos(int IdBase,int IdSociedad, ref string mensaje_error, int Estado = 3, int IdUsuario=0)
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
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
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
                        oMovimientoDTO.NombMoneda = (drd["NombMoneda"].ToString());

                        oMovimientoDTO.NombUsuario = (drd["NombUsuario"].ToString());

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

        
        public List<MovimientoDTO> ObtenerMovimientosSalidaModal(int IdSociedad,int IdAlmacen, ref string mensaje_error, int Estado = 3)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosSalidasModal", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MovimientoDTO oMovimientoDTO = new MovimientoDTO();
                        oMovimientoDTO.DT_RowId = Convert.ToInt32(drd["IdMovimiento"].ToString());

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
                        oMovimientoDTO.NumSerieTipoDocumentoRef = (drd["NumSerieTipoDocumentoRef"].ToString());
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


        public List<MovimientoDTO> ObtenerMovimientosSalida(int IdBase,int IdSociedad, ref string mensaje_error, int Estado = 3,int IdUsuario=0)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerMovimientosSalidas", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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
                        oMovimientoDTO.NombMoneda = (drd["NombMoneda"].ToString());
                        oMovimientoDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oMovimientoDTO.IdDocExtorno = Convert.ToInt32(drd["IdDocExtorno"].ToString());
                        oMovimientoDTO.TDocumento = (drd["TDocumento"].ToString());
                        oMovimientoDTO.SerieGuiaElectronica = (String.IsNullOrEmpty(drd["SerieGuiaElectronica"].ToString()) ? "" : drd["SerieGuiaElectronica"].ToString());
                        oMovimientoDTO.NumeroGuiaElectronica = Convert.ToInt32(String.IsNullOrEmpty(drd["NumeroGuiaElectronica"].ToString()) ? "0" : drd["NumeroGuiaElectronica"].ToString());
                        oMovimientoDTO.EstadoFE = Convert.ToInt32(String.IsNullOrEmpty(drd["EstadoFE"].ToString()) ? "0" : drd["EstadoFE"].ToString());
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
                        oMovimientoDTO.IdObraDestino = Convert.ToInt32(drd["IdObraDestino"].ToString());
                        oMovimientoDTO.IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(drd["IdUsuario"].ToString())) ? "0" : drd["IdUsuario"].ToString());
                        oMovimientoDTO.CreatedAt = Convert.ToDateTime((String.IsNullOrEmpty(drd["CreatedAt"].ToString())) ? "2022-08-01" : drd["CreatedAt"].ToString());
                        oMovimientoDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oMovimientoDTO.Comentario = (drd["Comentario"].ToString());

                        //(String.IsNullOrEmpty(drd["IdUsuario"].ToString())) ? "0" : drd["IdUsuario"].ToString())

                        oMovimientoDTO.IdDestinatario = Convert.ToInt32(String.IsNullOrEmpty(drd["IdDestinatario"].ToString()) ? "0": drd["IdDestinatario"].ToString());
                        oMovimientoDTO.IdMotivoTraslado = Convert.ToInt32(String.IsNullOrEmpty(drd["IdMotivoTraslado"].ToString()) ? "0" : drd["IdMotivoTraslado"].ToString());
                        oMovimientoDTO.IdTransportista = Convert.ToInt32(String.IsNullOrEmpty(drd["IdTransportista"].ToString()) ? "0" : drd["IdTransportista"].ToString());
                        oMovimientoDTO.PlacaVehiculo = (String.IsNullOrEmpty(drd["PlacaVehiculo"].ToString()) ? "" : drd["PlacaVehiculo"].ToString());
                        oMovimientoDTO.MarcaVehiculo = (String.IsNullOrEmpty(drd["MarcaVehiculo"].ToString()) ? "" : drd["MarcaVehiculo"].ToString());
                        oMovimientoDTO.NumIdentidadConductor = (String.IsNullOrEmpty(drd["NumIdentidadConductor"].ToString()) ? "" : drd["NumIdentidadConductor"].ToString());

                        oMovimientoDTO.NombreConductor = (String.IsNullOrEmpty(drd["NombreConductor"].ToString()) ? "" : drd["NombreConductor"].ToString());
                        oMovimientoDTO.ApellidoConductor = (String.IsNullOrEmpty(drd["ApellidoConductor"].ToString()) ? "" : drd["ApellidoConductor"].ToString());
                        oMovimientoDTO.LicenciaConductor = (String.IsNullOrEmpty(drd["LicenciaConductor"].ToString()) ? "" : drd["LicenciaConductor"].ToString());
                        oMovimientoDTO.TipoTransporte = (String.IsNullOrEmpty(drd["TipoTransporte"].ToString()) ? "" : drd["TipoTransporte"].ToString());

                        oMovimientoDTO.DireccionPartida = (String.IsNullOrEmpty(drd["DireccionPartida"].ToString()) ? "" : drd["DireccionPartida"].ToString());
                        oMovimientoDTO.CodigoUbigeoPartida = (String.IsNullOrEmpty(drd["CodigoUbigeoPartida"].ToString()) ? "" : drd["CodigoUbigeoPartida"].ToString());
                        oMovimientoDTO.CodigoAnexoPartida = (String.IsNullOrEmpty(drd["CodigoAnexoPartida"].ToString()) ? "" : drd["CodigoAnexoPartida"].ToString());

                        oMovimientoDTO.DireccionLlegada = (String.IsNullOrEmpty(drd["DireccionLlegada"].ToString()) ? "" : drd["DireccionLlegada"].ToString());
                        oMovimientoDTO.CodigoUbigeoLlegada = (String.IsNullOrEmpty(drd["CodigoUbigeoLlegada"].ToString()) ? "" : drd["CodigoUbigeoLlegada"].ToString());
                        oMovimientoDTO.CodigoAnexoLlegada = (String.IsNullOrEmpty(drd["CodigoAnexoLlegada"].ToString()) ? "" : drd["CodigoAnexoLlegada"].ToString());

                        oMovimientoDTO.SerieGuiaElectronica = (String.IsNullOrEmpty(drd["SerieGuiaElectronica"].ToString()) ? "" : drd["SerieGuiaElectronica"].ToString());
                        oMovimientoDTO.NumeroGuiaElectronica = Convert.ToInt32(String.IsNullOrEmpty(drd["NumeroGuiaElectronica"].ToString()) ? "0" : drd["NumeroGuiaElectronica"].ToString());

                        oMovimientoDTO.Peso = Convert.ToDecimal(String.IsNullOrEmpty(drd["Peso"].ToString()) ? "0" : drd["Peso"].ToString());
                        oMovimientoDTO.Bulto = Convert.ToDecimal(String.IsNullOrEmpty(drd["Bulto"].ToString()) ? "0" : drd["Bulto"].ToString());

                        oMovimientoDTO.NumDocumentoDestinatario = (String.IsNullOrEmpty(drd["NumDocumentoDestinatario"].ToString()) ? "" : drd["NumDocumentoDestinatario"].ToString());
                        oMovimientoDTO.NombDestinatario = (String.IsNullOrEmpty(drd["NombDestinatario"].ToString()) ? "" : drd["NombDestinatario"].ToString());
                        oMovimientoDTO.NumDocumentoTransportista = (String.IsNullOrEmpty(drd["NumDocumentoTransportista"].ToString()) ? "" : drd["NumDocumentoTransportista"].ToString());
                        oMovimientoDTO.NombTransportista = (String.IsNullOrEmpty(drd["NombTransportista"].ToString()) ? "" : drd["NombTransportista"].ToString());

                        oMovimientoDTO.CodigoMotivoTrasladoSunat = (String.IsNullOrEmpty(drd["CodigoMotivoTrasladoSunat"].ToString()) ? "" : drd["CodigoMotivoTrasladoSunat"].ToString());
                        oMovimientoDTO.DescripcionMotivoTrasladoSunat = (String.IsNullOrEmpty(drd["DescripcionMotivoTrasladoSunat"].ToString()) ? "" : drd["DescripcionMotivoTrasladoSunat"].ToString());

                      
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
                        oMovimientoDetalleDTO.IdMovimientoDetalle = Convert.ToInt32(dr2["IdMovimientoDetalle"].ToString());

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
                        oMovimientoDetalleDTO.Referencia = (dr2["Referencia"].ToString());
                        oMovimientoDetalleDTO.CodigoArticulo = (dr2["CodigoArticulo"].ToString());
                        oMovimientoDetalleDTO.TipoUnidadMedida = (dr2["TipoUnidadMedida"].ToString());
                        oMovimientoDetalleDTO.IdGrupoUnidadMedida = Convert.ToInt32(dr2["IdGrupoUnidadMedida"].ToString());
                        oMovimientoDetalleDTO.NombCuadrilla = dr2["NombCuadrilla"].ToString();
                        oMovimientoDetalleDTO.NombResponsable = dr2["NombResponsable"].ToString();

                        oMovimientoDetalleDTO.IdAlmacenDestino = Convert.ToInt32(dr2["IdAlmacenDestino"].ToString());

                        oMovimientoDetalleDTO.CantidadNotaCredito = Convert.ToDecimal(String.IsNullOrEmpty(dr2["CantidadNotaCredito"].ToString()) ? "0" : dr2["CantidadNotaCredito"].ToString());


                        oMovimientoDTO.detalles[posicion] = oMovimientoDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
               #endregion
                
            }
            #endregion


            #region AnexoDetalle
            Int32 filasdetalleAnexo = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosMovimientosxIdMovimiento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
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
            oMovimientoDTO.AnexoDetalle = new AnexoDTO[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosMovimientosxIdMovimiento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
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
                        oMovimientoDTO.AnexoDetalle[posicion] = oAnexoDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            #endregion





            return oMovimientoDTO;
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
                        da.SelectCommand.Parameters.AddWithValue("Redondeo", oOpchDTO.Redondeo);
                        
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
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", oOpchDTO.IdTipoRegistro);
                        da.SelectCommand.Parameters.AddWithValue("@IdSemana", oOpchDTO.IdSemana);
                        da.SelectCommand.Parameters.AddWithValue("@IdGlosaContable", oOpchDTO.IdGlosaContable);

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

        public int InsertUpdateOPCHDetalleCuadrilla(int IdOPCHDet,OPCHDetalle oOPCHDetalle, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateOPCHDetalleCuadrilla", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdOPCHDetalle", IdOPCHDet);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oOPCHDetalle.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oOPCHDetalle.IdResponsable);
                       

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


        public int InsertAnexoMovimiento(AnexoDTO oAnexoDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateAnexo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@ruta", oAnexoDTO.ruta);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oAnexoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Tabla", oAnexoDTO.Tabla);
                        da.SelectCommand.Parameters.AddWithValue("@IdTabla", oAnexoDTO.IdTabla);
                        da.SelectCommand.Parameters.AddWithValue("@NombreArchivo", oAnexoDTO.NombreArchivo);
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



        public int GuardarTicketUpdateEstadoGuia(int IdMovimiento,string Ticket, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_GuardarTicketUpdateEstadoGuia", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
                        da.SelectCommand.Parameters.AddWithValue("@Ticket", Ticket);
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



        #region ORPC
        public int InsertUpdateMovimientoORPC(OrpcDTO oOrpcDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateMovimientoORPC", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdORPC", oOrpcDTO.IdORPC);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oOrpcDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oOrpcDTO.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oOrpcDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oOrpcDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@ObjType", oOrpcDTO.ObjType);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oOrpcDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@CodMoneda", oOrpcDTO.CodMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambio", oOrpcDTO.TipoCambio);
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", oOrpcDTO.IdCliente);
                        da.SelectCommand.Parameters.AddWithValue("@FechaContabilizacion", oOrpcDTO.FechaContabilizacion.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaDocumento", oOrpcDTO.FechaDocumento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@FechaVencimiento", oOrpcDTO.FechaVencimiento.ToString("yyyyMMdd"));
                        da.SelectCommand.Parameters.AddWithValue("@IdListaPrecios", oOrpcDTO.IdListaPrecios);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oOrpcDTO.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oOrpcDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@SubTotal", oOrpcDTO.SubTotal);
                        da.SelectCommand.Parameters.AddWithValue("@Impuesto", oOrpcDTO.Impuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oOrpcDTO.Total);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oOrpcDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oOrpcDTO.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oOrpcDTO.IdResponsable);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumentoRef", oOrpcDTO.IdTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@NumSerieTipoDocumentoRef", oOrpcDTO.NumSerieTipoDocumentoRef);
                        da.SelectCommand.Parameters.AddWithValue("@EntregadoA", oOrpcDTO.EntregadoA);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oOrpcDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oOrpcDTO.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oOrpcDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", oOrpcDTO.idCondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@IdGlosaContable", oOrpcDTO.IdGlosaContable);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", oOrpcDTO.IdTipoRegistro);
                        da.SelectCommand.Parameters.AddWithValue("@IdSemana", oOrpcDTO.IdSemana);

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


        public int InsertUpdateORPCDetalle(ORPCDetalle oORPCDetalle, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateORPCDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdORPCDetalle", oORPCDetalle.IdORPCDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdORPC", oORPCDetalle.IdORPC);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oORPCDetalle.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionArticulo", oORPCDetalle.DescripcionArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdDefinicionGrupoUnidad", oORPCDetalle.IdDefinicionGrupoUnidad);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oORPCDetalle.IdAlmacen);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", oORPCDetalle.Cantidad);
                        da.SelectCommand.Parameters.AddWithValue("@Igv", oORPCDetalle.Igv);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadBase", oORPCDetalle.PrecioUnidadBase);
                        da.SelectCommand.Parameters.AddWithValue("@PrecioUnidadTotal", oORPCDetalle.PrecioUnidadTotal);
                        da.SelectCommand.Parameters.AddWithValue("@TotalBase", oORPCDetalle.TotalBase);
                        da.SelectCommand.Parameters.AddWithValue("@Total", oORPCDetalle.Total);
                        da.SelectCommand.Parameters.AddWithValue("@CuentaContable", oORPCDetalle.CuentaContable);
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oORPCDetalle.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@IdAfectacionIgv", oORPCDetalle.IdAfectacionIgv);
                        da.SelectCommand.Parameters.AddWithValue("@Descuento", oORPCDetalle.Descuento);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacenDestino", oORPCDetalle.IdAlmacenDestino);
                        da.SelectCommand.Parameters.AddWithValue("@valor_unitario", oORPCDetalle.valor_unitario);
                        da.SelectCommand.Parameters.AddWithValue("@precio_unitario", oORPCDetalle.precio_unitario);
                        //da.SelectCommand.Parameters.AddWithValue("@codigo_tipo_afectacion_igv", oOPDNDetalle.codigo_tipo_afectacion_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_base_igv", oORPCDetalle.total_base_igv);
                        da.SelectCommand.Parameters.AddWithValue("@porcentaje_igv", oORPCDetalle.porcentaje_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_igv", oORPCDetalle.total_igv);
                        da.SelectCommand.Parameters.AddWithValue("@total_impuestos", oORPCDetalle.total_impuestos);
                        da.SelectCommand.Parameters.AddWithValue("@total_valor_item", oORPCDetalle.total_valor_item);
                        da.SelectCommand.Parameters.AddWithValue("@total_item", oORPCDetalle.total_item);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorImpuesto", oORPCDetalle.IdIndicadorImpuesto);
                        da.SelectCommand.Parameters.AddWithValue("@Referencia", oORPCDetalle.Referencia);
                        da.SelectCommand.Parameters.AddWithValue("@NombTablaOrigen", oORPCDetalle.NombTablaOrigen);
                        da.SelectCommand.Parameters.AddWithValue("@IdOrigen", oORPCDetalle.IdOrigen);

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
        public int InsertUpdateORPCDetalleCuadrilla(int OPRCDDetId,ORPCDetalle oORPCDetalle, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateORPCDetalleCuadrilla", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdORPCDetalle", OPRCDDetId);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oORPCDetalle.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oORPCDetalle.IdResponsable);


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



        public string ValidaExtorno(int IdMovimiento, ref string mensaje_error)
        {
            string Valida = "0";
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarExtorno", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);

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

        public int ValidarExisteObraxIdUsuario(int IdUsuario,int IdObraDestino, ref string mensaje_error)
        {
            int Valida =0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarExisteObraxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdObraDestino", IdObraDestino);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        Valida = Convert.ToInt32(drd["existe"].ToString());
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

        public MovimientoDTO ObtenerMovimientosDetallexIdMovimientoOLD(int IdMovimiento, ref string mensaje_error)
        {
            MovimientoDTO oMovimientoDTO = new MovimientoDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_MovimientoxID_OLD", cn);
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
                        oMovimientoDTO.IdObraDestino = Convert.ToInt32(drd["IdObraDestino"].ToString());
                        oMovimientoDTO.IdUsuario = Convert.ToInt32((String.IsNullOrEmpty(drd["IdUsuario"].ToString())) ? "0" : drd["IdUsuario"].ToString());
                        oMovimientoDTO.CreatedAt = Convert.ToDateTime((String.IsNullOrEmpty(drd["CreatedAt"].ToString())) ? "2022-08-01" : drd["CreatedAt"].ToString());
                        oMovimientoDTO.NombUsuario = (drd["NombUsuario"].ToString());
                        oMovimientoDTO.Comentario = (drd["Comentario"].ToString());

                        //(String.IsNullOrEmpty(drd["IdUsuario"].ToString())) ? "0" : drd["IdUsuario"].ToString())

                        oMovimientoDTO.IdDestinatario = Convert.ToInt32(String.IsNullOrEmpty(drd["IdDestinatario"].ToString()) ? "0" : drd["IdDestinatario"].ToString());
                        oMovimientoDTO.IdMotivoTraslado = Convert.ToInt32(String.IsNullOrEmpty(drd["IdMotivoTraslado"].ToString()) ? "0" : drd["IdMotivoTraslado"].ToString());
                        oMovimientoDTO.IdTransportista = Convert.ToInt32(String.IsNullOrEmpty(drd["IdTransportista"].ToString()) ? "0" : drd["IdTransportista"].ToString());
                        oMovimientoDTO.PlacaVehiculo = (String.IsNullOrEmpty(drd["PlacaVehiculo"].ToString()) ? "" : drd["PlacaVehiculo"].ToString());
                        oMovimientoDTO.MarcaVehiculo = (String.IsNullOrEmpty(drd["MarcaVehiculo"].ToString()) ? "" : drd["MarcaVehiculo"].ToString());
                        oMovimientoDTO.NumIdentidadConductor = (String.IsNullOrEmpty(drd["NumIdentidadConductor"].ToString()) ? "" : drd["NumIdentidadConductor"].ToString());

                        oMovimientoDTO.NombreConductor = (String.IsNullOrEmpty(drd["NombreConductor"].ToString()) ? "" : drd["NombreConductor"].ToString());
                        oMovimientoDTO.ApellidoConductor = (String.IsNullOrEmpty(drd["ApellidoConductor"].ToString()) ? "" : drd["ApellidoConductor"].ToString());
                        oMovimientoDTO.LicenciaConductor = (String.IsNullOrEmpty(drd["LicenciaConductor"].ToString()) ? "" : drd["LicenciaConductor"].ToString());
                        oMovimientoDTO.TipoTransporte = (String.IsNullOrEmpty(drd["TipoTransporte"].ToString()) ? "" : drd["TipoTransporte"].ToString());

                        oMovimientoDTO.EstadoFE = Convert.ToInt32(String.IsNullOrEmpty(drd["EstadoFE"].ToString()) ? "0" : drd["EstadoFE"].ToString());

                        oMovimientoDTO.SerieGuiaElectronica = (String.IsNullOrEmpty(drd["SerieGuiaElectronica"].ToString()) ? "" : drd["SerieGuiaElectronica"].ToString());
                        oMovimientoDTO.NumeroGuiaElectronica = Convert.ToInt32(String.IsNullOrEmpty(drd["NumeroGuiaElectronica"].ToString()) ? "0" : drd["NumeroGuiaElectronica"].ToString());

                        oMovimientoDTO.Peso = Convert.ToDecimal(String.IsNullOrEmpty(drd["Peso"].ToString()) ? "0" : drd["Peso"].ToString());
                        oMovimientoDTO.Bulto = Convert.ToDecimal(String.IsNullOrEmpty(drd["Bulto"].ToString()) ? "0" : drd["Bulto"].ToString());

                        oMovimientoDTO.NumDocumentoDestinatario = (String.IsNullOrEmpty(drd["NumDocumentoDestinatario"].ToString()) ? "" : drd["NumDocumentoDestinatario"].ToString());
                        oMovimientoDTO.NombDestinatario = (String.IsNullOrEmpty(drd["NombDestinatario"].ToString()) ? "" : drd["NombDestinatario"].ToString());
                        oMovimientoDTO.NumDocumentoTransportista = (String.IsNullOrEmpty(drd["NumDocumentoTransportista"].ToString()) ? "" : drd["NumDocumentoTransportista"].ToString());
                        oMovimientoDTO.NombTransportista = (String.IsNullOrEmpty(drd["NombTransportista"].ToString()) ? "" : drd["NombTransportista"].ToString());

                        oMovimientoDTO.CodigoMotivoTrasladoSunat = (String.IsNullOrEmpty(drd["CodigoMotivoTrasladoSunat"].ToString()) ? "" : drd["CodigoMotivoTrasladoSunat"].ToString());
                        oMovimientoDTO.DescripcionMotivoTrasladoSunat = (String.IsNullOrEmpty(drd["DescripcionMotivoTrasladoSunat"].ToString()) ? "" : drd["DescripcionMotivoTrasladoSunat"].ToString());


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
                        oMovimientoDetalleDTO.IdMovimientoDetalle = Convert.ToInt32(dr2["IdMovimientoDetalle"].ToString());

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
                        oMovimientoDetalleDTO.Referencia = (dr2["Referencia"].ToString());
                        oMovimientoDetalleDTO.CodigoArticulo = (dr2["CodigoArticulo"].ToString());
                        oMovimientoDetalleDTO.TipoUnidadMedida = (dr2["TipoUnidadMedida"].ToString());
                        oMovimientoDetalleDTO.IdGrupoUnidadMedida = Convert.ToInt32(dr2["IdGrupoUnidadMedida"].ToString());

                        oMovimientoDetalleDTO.IdAlmacenDestino = Convert.ToInt32(dr2["IdAlmacenDestino"].ToString());

                        oMovimientoDetalleDTO.CantidadNotaCredito = Convert.ToDecimal(String.IsNullOrEmpty(dr2["CantidadNotaCredito"].ToString()) ? "0" : dr2["CantidadNotaCredito"].ToString());
                        oMovimientoDetalleDTO.NombCuadrilla = (dr2["NombCuadrilla"].ToString());
                        oMovimientoDetalleDTO.NombResponsable = (dr2["NombResponsable"].ToString());

                        oMovimientoDTO.detalles[posicion] = oMovimientoDetalleDTO;
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
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosMovimientosxIdMovimiento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
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
            oMovimientoDTO.AnexoDetalle = new AnexoDTO[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosMovimientosxIdMovimiento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMovimiento", IdMovimiento);
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
                        oMovimientoDTO.AnexoDetalle[posicion] = oAnexoDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            #endregion





            return oMovimientoDTO;
        }








    }
}
