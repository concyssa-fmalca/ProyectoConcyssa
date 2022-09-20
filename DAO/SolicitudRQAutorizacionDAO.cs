
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class SolicitudRQAutorizacionDAO
    {
        public List<SolicitudRQAutorizacionDTO> ObtenerSolicitudesxAutorizar(string IdUsuario, string IdSociedad, string FechaInicio, string FechaFinal, int Estado)
        {
            List<SolicitudRQAutorizacionDTO> lstSolicitudRQAutorizacionDTO = new List<SolicitudRQAutorizacionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSolicitudesxAutorizar", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", int.Parse(IdUsuario));
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                    da.SelectCommand.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
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



        public int PasarPendiente(int IdSolicitud)
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
        //    using (SqlConnection cn = new Conexion().conectar())
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
