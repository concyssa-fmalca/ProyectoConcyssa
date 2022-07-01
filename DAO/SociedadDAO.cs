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
    public class SociedadDAO
    {
        public List<SociedadDTO> ObtenerSociedades(ref string mensajeError)
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedades", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.Descripcion = drd["Descripcion"].ToString();
                        oSociedadDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensajeError=ex.Message.ToString();
                }
            }
            return lstSociedadDTO;
        }

        public int UpdateInsertSociedad(SociedadDTO oSociedadDTO, ref string error_mensaje)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSociedades", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oSociedadDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@NombreSociedad", oSociedadDTO.NombreSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSociedadDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDocumento", oSociedadDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSociedadDTO.Estado);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        error_mensaje= ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }

        public List<SociedadDTO> ObtenerDatosxID(int IdSociedad)
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedadesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oSociedadDTO.Descripcion = drd["Descripcion"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstSociedadDTO;
        }

    }
}
