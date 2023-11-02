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
    public class ConfiguracionSociedadDAO
    {
        public List<ConfiguracionSociedadDTO> ObtenerConfiguracionSociedad(int IdSociedad, ref string mensaje_error)
        {
            List<ConfiguracionSociedadDTO> lstConfiguracionSociedadDTO = new List<ConfiguracionSociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarConfiguracionSociedad", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ConfiguracionSociedadDTO oConfiguracionSociedadDTO = new ConfiguracionSociedadDTO();
                        oConfiguracionSociedadDTO.Id = int.Parse(drd["Id"].ToString());
                        oConfiguracionSociedadDTO.Ruc = (drd["Ruc"].ToString());
                        oConfiguracionSociedadDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        oConfiguracionSociedadDTO.Direccion = (drd["Direccion"].ToString());
                        oConfiguracionSociedadDTO.NombreBDSAP = (drd["NombreBDSAP"].ToString());
                        lstConfiguracionSociedadDTO.Add(oConfiguracionSociedadDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstConfiguracionSociedadDTO;
        }


        public int UpdateInsertConfiguracionSociedad(ConfiguracionSociedadDTO oConfiguracionSociedadDTO,int IdSociedad, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertConfiguracionSociedad", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Id", oConfiguracionSociedadDTO.Id);
                        da.SelectCommand.Parameters.AddWithValue("@Ruc", oConfiguracionSociedadDTO.Ruc);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", (oConfiguracionSociedadDTO.RazonSocial) == null ? "": oConfiguracionSociedadDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oConfiguracionSociedadDTO.Direccion == null ? "": oConfiguracionSociedadDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@NombreBDSAP", oConfiguracionSociedadDTO.NombreBDSAP == null ? "": oConfiguracionSociedadDTO.NombreBDSAP);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
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
