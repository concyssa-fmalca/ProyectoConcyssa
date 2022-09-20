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
    public class SolicitudRQModeloDAO
    {
        public int UpdateInsertSolicitudRQModelo(SolicitudRQModeloDTO oSolicitudRQModeloDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudRQModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQModelo", oSolicitudRQModeloDTO.IdSolicitudRQModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oSolicitudRQModeloDTO.IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oSolicitudRQModeloDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapa", oSolicitudRQModeloDTO.IdEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@Aprobaciones", oSolicitudRQModeloDTO.Aprobaciones);
                        da.SelectCommand.Parameters.AddWithValue("@Rechazos", oSolicitudRQModeloDTO.Rechazos);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
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


        public List<SolicitudRQModeloDTO> ObtenerDatosxID(int IdSolicitudRQModelo, string IdSociedad)
        {
            List<SolicitudRQModeloDTO> lstSolicitudRQModeloDTO = new List<SolicitudRQModeloDTO>();

            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerSolicitudRQModeloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudRQModelo", IdSolicitudRQModelo);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudRQModeloDTO oSolicitudRQModeloDTO = new SolicitudRQModeloDTO();
                        oSolicitudRQModeloDTO.IdSolicitudRQModelo = int.Parse(drd["Id"].ToString());
                        oSolicitudRQModeloDTO.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudRQModeloDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());
                        oSolicitudRQModeloDTO.IdEtapa = int.Parse(drd["IdEtapa"].ToString());
                        lstSolicitudRQModeloDTO.Add(oSolicitudRQModeloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudRQModeloDTO;
        }


    }
}
