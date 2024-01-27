using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class SolicitudDespachoModeloDAO
    {
        public int UpdateInsertSolicitudDespachoModelo(SolicitudDespachoModeloDTO oSolicitudDespachoModeloDTO, string IdSociedad, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSolicitudDespachoModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespachoModelo", oSolicitudDespachoModeloDTO.IdSolicitudDespachoModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitud", oSolicitudDespachoModeloDTO.IdSolicitud);
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oSolicitudDespachoModeloDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapa", oSolicitudDespachoModeloDTO.IdEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@Aprobaciones", oSolicitudDespachoModeloDTO.Aprobaciones);
                        da.SelectCommand.Parameters.AddWithValue("@Rechazos", oSolicitudDespachoModeloDTO.Rechazos);
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


        public List<SolicitudDespachoModeloDTO> ObtenerDatosxID(int IdSolicitudDespachoModelo, string IdSociedad, string BaseDatos)
        {
            List<SolicitudDespachoModeloDTO> lstSolicitudDespachoModeloDTO = new List<SolicitudDespachoModeloDTO>();

            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerSolicitudDespachoModeloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSolicitudDespachoModelo", IdSolicitudDespachoModelo);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SolicitudDespachoModeloDTO oSolicitudDespachoModeloDTO = new SolicitudDespachoModeloDTO();
                        oSolicitudDespachoModeloDTO.IdSolicitudDespachoModelo = int.Parse(drd["Id"].ToString());
                        oSolicitudDespachoModeloDTO.IdSolicitud = int.Parse(drd["IdSolicitud"].ToString());
                        oSolicitudDespachoModeloDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());
                        oSolicitudDespachoModeloDTO.IdEtapa = int.Parse(drd["IdEtapa"].ToString());
                        lstSolicitudDespachoModeloDTO.Add(oSolicitudDespachoModeloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSolicitudDespachoModeloDTO;
        }

    }
}
