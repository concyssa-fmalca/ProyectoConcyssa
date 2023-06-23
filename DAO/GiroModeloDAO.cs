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
    public class GiroModeloDAO
    {

        public int UpdateInsertGiroModelo(GiroModeloDTO oGiroModeloDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertGiroModelo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGiroModelo", oGiroModeloDTO.IdGiroModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdGiro", oGiroModeloDTO.IdGiro);
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oGiroModeloDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@IdEtapa", oGiroModeloDTO.IdEtapa);
                        da.SelectCommand.Parameters.AddWithValue("@Aprobaciones", oGiroModeloDTO.Aprobaciones);
                        da.SelectCommand.Parameters.AddWithValue("@Rechazos", oGiroModeloDTO.Rechazos);
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


        public List<GiroModeloDTO> ObtenerDatosxID(int IdGiroModelo, string IdSociedad)
        {
            List<GiroModeloDTO> lstGiroModeloDTO = new List<GiroModeloDTO>();

            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGiroModeloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiroModelo", IdGiroModelo);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GiroModeloDTO oGiroModeloDTO = new GiroModeloDTO();
                        oGiroModeloDTO.IdGiroModelo = int.Parse(drd["Id"].ToString());
                        oGiroModeloDTO.IdGiro = int.Parse(drd["IdGiro"].ToString());
                        oGiroModeloDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());
                        oGiroModeloDTO.IdEtapa = int.Parse(drd["IdEtapa"].ToString());
                        lstGiroModeloDTO.Add(oGiroModeloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstGiroModeloDTO;
        }


    }
}
