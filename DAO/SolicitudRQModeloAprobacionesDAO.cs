using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class SolicitudRQModeloAprobacionesDAO
    {
        public int UpdateInsertModeloAprobaciones(List<SolicitudRQModeloAprobacionesDTO> oSolicitudRQModeloAprobacionesDTO, string IdSociedad)
        {
            if (oSolicitudRQModeloAprobacionesDTO.Count() > 0)
            {
                int rpta = 0;
                for (int i = 0; i < oSolicitudRQModeloAprobacionesDTO.Count; i++)
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
                                SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQModeloAprobaciones", cn);
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQModeloAprobaciones", oSolicitudRQModeloAprobacionesDTO[i].IdSolicitudRQModeloAprobaciones);
                                da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", oSolicitudRQModeloAprobacionesDTO[i].IdSolicitudModelo);
                                da.SelectCommand.Parameters.AddWithValue("@IdAutorizador", oSolicitudRQModeloAprobacionesDTO[i].IdAutorizador);
                                da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oSolicitudRQModeloAprobacionesDTO[i].IdArticulo);
                                da.SelectCommand.Parameters.AddWithValue("@Accion", oSolicitudRQModeloAprobacionesDTO[i].Accion);
                                da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                                da.SelectCommand.Parameters.AddWithValue("@IdDetalle", oSolicitudRQModeloAprobacionesDTO[i].IdDetalle);
                                rpta = da.SelectCommand.ExecuteNonQuery();
                                transactionScope.Complete();

                            }
                            catch (Exception ex)
                            {
                                return 0;
                            }
                        }
                    }
                }
                return rpta;
            }
            else
            {
                return 0;
            }

        }



        public int AprobarSolicitudRQ(int IdSolicitud, int IdSolicitudModelo)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_AprobarSolicitudRQ", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", IdSolicitudModelo);
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


        public int AprobarSolicitudRQxIdSolicitud(int IdSolicitud)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_AprobarSolicitudRQxIdSolicitud", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
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


        public int RechazarSolicitudRQ(int IdSolicitud, int IdSolicitudModelo)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_RechazarSolicitudRQ", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", IdSolicitudModelo);
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


        public int ActualizarCantidadPrecioDetalle(SolicitudRQModeloAprobacionesDTO oSolicitudRQModeloAprobacionesDTO, string IdSociedad)
        {

            int rpta = 0;

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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ActualizarCantidadPrecioDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oSolicitudRQModeloAprobacionesDTO.IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oSolicitudRQModeloAprobacionesDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidad", oSolicitudRQModeloAprobacionesDTO.CantidadItem);
                        da.SelectCommand.Parameters.AddWithValue("@Precio", oSolicitudRQModeloAprobacionesDTO.PrecioItem);
                        da.SelectCommand.Parameters.AddWithValue("@Total", ((oSolicitudRQModeloAprobacionesDTO.CantidadItem) * (oSolicitudRQModeloAprobacionesDTO.PrecioItem)));
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }

            return rpta;

        }


        public List<SolicitudRQDTO> istarDetalleItemAprobados(int IdSolicitudRQ, int IdAprobador, int IdEtapa)
        {
            List<SolicitudRQDTO> lstSolicitudRQDTO = new List<SolicitudRQDTO>();

            SolicitudRQDTO oSolicitudRQDTO = new SolicitudRQDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oSolicitudRQDTO.IdSolicitudRQ = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSolicitudRQDTO.Serie = drd["Serie"].ToString();
                        oSolicitudRQDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oSolicitudRQDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oSolicitudRQDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQDTO.IdSucursal = int.Parse(drd["IdSucursal"].ToString());
                        oSolicitudRQDTO.IdDepartamento = int.Parse(drd["IdDepartamento"].ToString());
                        oSolicitudRQDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQDTO.IdTitular = int.Parse(drd["IdTitular"].ToString());
                        oSolicitudRQDTO.IdMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDTO.TipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDTO.TotalAntesDescuento = decimal.Parse(drd["TotalAntesDescuento"].ToString());
                        oSolicitudRQDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDTO.Impuesto = decimal.Parse(drd["Impuesto"].ToString());
                        oSolicitudRQDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oSolicitudRQDTO.FechaValidoHasta = Convert.ToDateTime(drd["FechaValidoHasta"].ToString());
                        oSolicitudRQDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oSolicitudRQDTO.FechaCreacion = Convert.ToDateTime(drd["FechaCreacion"].ToString());
                        oSolicitudRQDTO.Comentarios = drd["Comentarios"].ToString();
                        oSolicitudRQDTO.Estado = int.Parse(drd["Estado"].ToString());
                        oSolicitudRQDTO.DetalleEstado = drd["DetalleEstado"].ToString();
                        oSolicitudRQDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }


            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDetalleItemAprobados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdAutorizador", IdAprobador);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalle++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oSolicitudRQDTO.Detalle = new SolicitudDetalleDTO[filasdetalle];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDetalleItemAprobados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdAutorizador", IdAprobador);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudDetalleDTO oSolicitudRQDetalleDTO = new SolicitudDetalleDTO();
                        oSolicitudRQDetalleDTO.IdSolicitudRQDetalle = int.Parse(drd["Id"].ToString());
                        oSolicitudRQDetalleDTO.IdSolicitudCabecera = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQDetalleDTO.IdArticulo = drd["IdArticulo"].ToString();
                        oSolicitudRQDetalleDTO.Descripcion = drd["Descripcion"].ToString();
                        oSolicitudRQDetalleDTO.IdUnidadMedida = drd["IdUnidadMedida"].ToString();
                        oSolicitudRQDetalleDTO.FechaNecesaria = Convert.ToDateTime(drd["FechaNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.CantidadNecesaria = decimal.Parse(drd["CantidadNecesaria"].ToString());
                        oSolicitudRQDetalleDTO.PrecioInfo = decimal.Parse(drd["PrecioInfo"].ToString());
                        oSolicitudRQDetalleDTO.IdIndicadorImpuesto = int.Parse(drd["IdIndicadorImpuesto"].ToString());
                        oSolicitudRQDetalleDTO.ItemTotal = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQDetalleDTO.IdAlmacen = drd["IdAlmacen"].ToString();
                        oSolicitudRQDetalleDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oSolicitudRQDetalleDTO.NumeroFabricacion = drd["NumeroFabricacion"].ToString();
                        oSolicitudRQDetalleDTO.NumeroSerie = drd["NumeroSerie"].ToString();
                        oSolicitudRQDetalleDTO.IdLineaNegocio = int.Parse(drd["IdLineaNegocio"].ToString());
                        oSolicitudRQDetalleDTO.IdCentroCostos = drd["IdCentroCostos"].ToString();
                        oSolicitudRQDetalleDTO.IdProyecto = drd["IdProyecto"].ToString();
                        oSolicitudRQDetalleDTO.IdItemMoneda = drd["IdMoneda"].ToString();
                        oSolicitudRQDetalleDTO.ItemTipoCambio = decimal.Parse(drd["TipoCambio"].ToString());
                        oSolicitudRQDetalleDTO.Referencia = drd["Referencia"].ToString();
                        oSolicitudRQDetalleDTO.EstadoItemAutorizado = int.Parse(drd["Estado"].ToString());
                        oSolicitudRQDetalleDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                        oSolicitudRQDetalleDTO.EstadoDisabled = int.Parse(drd["EstadoDisabled"].ToString());
                        oSolicitudRQDetalleDTO.IdDetalle = int.Parse(drd["IdDetalle"].ToString());
                        oSolicitudRQDetalleDTO.AprobadoAnterior = int.Parse(drd["AprobadoAnterior"].ToString());

                        //oSolicitudRQDetalleDTO.DescripcionItem = drd["DescripcionItem"].ToString();
                        //lstSolicitudRQDTO.Add(oSolicitudRQDTO.Detalle.Add(oSolicitudRQDetalleDTO));
                        oSolicitudRQDTO.Detalle[posicion] = oSolicitudRQDetalleDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

            }



            Int32 filasdetalleAnexo = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudAnexosRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleAnexo++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oSolicitudRQDTO.DetallesAnexo = new SolicitudRQAnexos[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudAnexosRQxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQ", IdSolicitudRQ);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        SolicitudRQAnexos oSolicitudRQAnexos = new SolicitudRQAnexos();
                        oSolicitudRQAnexos.IdSolicitudRQAnexos = int.Parse(drd["Id"].ToString());
                        oSolicitudRQAnexos.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQAnexos.Nombre = drd["Nombre"].ToString();
                        oSolicitudRQAnexos.IdSociedad = int.Parse(drd["IdSociedad"].ToString());

                        oSolicitudRQDTO.DetallesAnexo[posicion] = oSolicitudRQAnexos;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }

                lstSolicitudRQDTO.Add(oSolicitudRQDTO);

            }



            return lstSolicitudRQDTO;
        }

        public int ValidarItemsAutorizados(int IdSolicitudModelo, string IdArticulo, int IdDetalle)
        {
            int datos = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarItemsAutorizados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", IdSolicitudModelo);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdDetalle", IdDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        datos++;
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return datos;
        }


        public int ValidarItemsDesAutorizados(int IdSolicitudModelo, string IdArticulo, int IdDetalle)
        {
            int datos = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarItemsDesAutorizados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", IdSolicitudModelo);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdDetalle", IdDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        datos++;
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return datos;
        }

        public int ActualizarEstadoDisabledItem(SolicitudRQModeloAprobacionesDTO oSolicitudRQModeloAprobacionesDTO, string IdSociedad)
        {

            int rpta = 0;

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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_ActualizarEstadoDisabledItem", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudModelo", oSolicitudRQModeloAprobacionesDTO.IdSolicitudModelo);
                        da.SelectCommand.Parameters.AddWithValue("@Accion", oSolicitudRQModeloAprobacionesDTO.Accion);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oSolicitudRQModeloAprobacionesDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oSolicitudRQModeloAprobacionesDTO.IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdDetalle", oSolicitudRQModeloAprobacionesDTO.IdDetalle);
                        rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();

                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }

            return rpta;

        }

    }
}
