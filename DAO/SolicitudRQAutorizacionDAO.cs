
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class SolicitudRQAutorizacionDAO
    {

        public int ValidarSipuedeAprobar(int IdSolicitudRQ, int IdEtapa, string BaseDatos)
        {
            int puedeentrar = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarSipuedeAprobar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitudRQ);
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



        public int ValidarSipuedeAprobarDetalle(int IdSolicitudRQ, int IdEtapa,int IdDetalle, string BaseDatos)
        {
            int puedeentrar = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarSipuedeAprobar1", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitudRQ);
                    da.SelectCommand.Parameters.AddWithValue("@IdEtapa", IdEtapa);
                    da.SelectCommand.Parameters.AddWithValue("@IdDetalle", IdDetalle);
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


        public List<SolicitudRQAutorizacionDTO> ObtenerSolicitudesxAutorizar(string IdUsuario, string IdSociedad, string FechaInicio, string FechaFinal, int Estado, string BaseDatos)
        {
            List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = new List<SolicitudRQAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizar", cn);
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizarNEW", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPR", Estado);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudRQAutorizacionDTO oSolicitudRQAutorizacionDTO = new SolicitudRQAutorizacionDTO();
                        oSolicitudRQAutorizacionDTO.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQAutorizacionDTO.UsuarioAprobador = int.Parse(drd["UsuarioAprobador"].ToString());
                        oSolicitudRQAutorizacionDTO.NumeroSolicitud = drd["NumeroSolicitud"].ToString();
                        oSolicitudRQAutorizacionDTO.Solicitante = drd["Solicitante"].ToString();
                        oSolicitudRQAutorizacionDTO.Area = drd["Area"].ToString();
                        oSolicitudRQAutorizacionDTO.TipoArticulo = drd["TipoArticulo"].ToString();
                        oSolicitudRQAutorizacionDTO.IdClaseArticulo = int.Parse(drd["IdClaseArticulo"].ToString());
                        oSolicitudRQAutorizacionDTO.Moneda = drd["Moneda"].ToString();
                        oSolicitudRQAutorizacionDTO.Impuesto = drd["Impuesto"].ToString();
                        oSolicitudRQAutorizacionDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oSolicitudRQAutorizacionDTO.Neto = decimal.Parse(drd["Neto"].ToString());
                        oSolicitudRQAutorizacionDTO.TotalIgv = decimal.Parse(drd["TotalIgv"].ToString());
                        oSolicitudRQAutorizacionDTO.Prioridad = drd["Prioridad"].ToString();
                        oSolicitudRQAutorizacionDTO.Estado = drd["Estado"].ToString();
                        oSolicitudRQAutorizacionDTO.FechaCreacion = DateTime.Parse(drd["FechaCreacion"].ToString());
                        oSolicitudRQAutorizacionDTO.FechaValidoHasta = DateTime.Parse(drd["FechaValidoHasta"].ToString());
                        //oSolicitudRQAutorizacionDTO.FechaAprobacion = drd["FechaAprobacion"].ToString();
                        oSolicitudRQAutorizacionDTO.IdSolicitudModelo = int.Parse(drd["IdSolicitudModelo"].ToString());
                        oSolicitudRQAutorizacionDTO.IdEtapa = int.Parse(drd["IdEtapaAutorizacion"].ToString());
                        oSolicitudRQAutorizacionDTO.NombEtapa = (drd["NombEtapa"].ToString());


                        lstSolicitudRQAutorizacionDTO.Add(oSolicitudRQAutorizacionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudRQAutorizacionDTO;
        }


        
        public List<DetalleSolicitudRqAprobacionDTO> ObtenerSolicitudesxAutorizarDetalle(string IdUsuario, string IdSociedad, string FechaInicio, string FechaFinal, int Estado,int IdObra, string BaseDatos)
        {
            List<DetalleSolicitudRqAprobacionDTO> lstDetalleSolicitudRqAprobacionDTO = new List<DetalleSolicitudRqAprobacionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    int aprobar = 0;
                    cn.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizar", cn);
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPedidosDetalleAutorizar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoPR", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        aprobar = 0;
                        aprobar = ValidarSipuedeAprobarDetalle(Convert.ToInt32(drd["IdSolicitud"].ToString()), Convert.ToInt32(drd["IdEtapa"].ToString()), Convert.ToInt32(drd["IdSolicitudRQDetalle"].ToString()),BaseDatos);
                        if (aprobar==1)
                        {
                            DetalleSolicitudRqAprobacionDTO oDetalleSolicitudRqAprobacionDTO = new DetalleSolicitudRqAprobacionDTO();
                            oDetalleSolicitudRqAprobacionDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.PrecioInfo = Convert.ToDecimal(drd["PrecioInfo"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.IdSolicitud = Convert.ToInt32(drd["IdSolicitud"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.NumeroPedido = (drd["Numero"].ToString());

                            oDetalleSolicitudRqAprobacionDTO.NumeroPedido = (drd["Numero"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.ObraDescripcion = (drd["ObraDescripcion"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.TipoProducto = (drd["TipoProducto"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.IdClaseArticulo = Convert.ToInt32(drd["IdClaseArticulo"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.CodigoArticulo = (drd["CodigoArticulo"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.NombreArticulo = (drd["Descripcion1"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.Numero = Convert.ToInt32(drd["Numero"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.CantidadNecesaria = Convert.ToDecimal(drd["CantidadNecesaria"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.Accion = Convert.ToInt32(drd["Accion"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.IdSolicitudRQModelo = Convert.ToInt32(drd["IdSolicitudRQModelo"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.IdSolicitudRQDetalle = Convert.ToInt32((drd["IdSolicitudRQDetalle"].ToString()));
                            oDetalleSolicitudRqAprobacionDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.IdUsuario = Convert.ToInt32(IdUsuario);
                            oDetalleSolicitudRqAprobacionDTO.IdTipoProducto = Convert.ToInt32(drd["IdTipoProducto"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.NombUsuario = (drd["NombUsuario"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.Serie = (drd["Serie"].ToString());
                            oDetalleSolicitudRqAprobacionDTO.Referencia = drd["Referencia"].ToString();


                            lstDetalleSolicitudRqAprobacionDTO.Add(oDetalleSolicitudRqAprobacionDTO);
                        }
                       
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstDetalleSolicitudRqAprobacionDTO;
        }


        public int PasarPendiente(int IdSolicitud, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_PasarRQPendienteAprobacion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }
        }


        //public bool ValidarGuardoAprobacionesDetalle(int IdSolicitud)
        //{
        //    int datos = 0;
        //    using (SqlConnection cn = new Conexion().conectar(BaseDatos))
        //    {
        //        try
        //        {
        //            cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarGuardoAprobacionesDetalle", cn);
        //            da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", IdSolicitud);
        //            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //            SqlDataReader drd = da.SelectCommand.ExecuteReader();
        //            while (drd.Read())
        //            {
        //                datos++;
        //            }
        //            drd.Close();


        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //    if (datos != 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


    }
}
