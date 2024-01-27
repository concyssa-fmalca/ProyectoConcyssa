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
    public class ConfiguracionDecimalesDAO
    {
        public List<ConfiguracionDecimalesDTO> ObtenerConfiguracionDecimales(int IdSociedad, string BaseDatos)
        {
            List<ConfiguracionDecimalesDTO> lstConfiguracionDecimalesDTO = new List<ConfiguracionDecimalesDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarConfiguracionDecimales", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ConfiguracionDecimalesDTO oConfiguracionDecimalesDTO = new ConfiguracionDecimalesDTO();
                        oConfiguracionDecimalesDTO.IdConfiguracionDecimales = int.Parse(drd["Id"].ToString());
                        oConfiguracionDecimalesDTO.Importes = int.Parse(drd["Importes"].ToString());
                        oConfiguracionDecimalesDTO.Precios = int.Parse(drd["Precios"].ToString());
                        oConfiguracionDecimalesDTO.Cantidades = int.Parse(drd["Cantidades"].ToString());
                        oConfiguracionDecimalesDTO.Porcentajes = int.Parse(drd["Porcentajes"].ToString());
                        oConfiguracionDecimalesDTO.Unidades = int.Parse(drd["Unidades"].ToString());
                        oConfiguracionDecimalesDTO.Decimales = int.Parse(drd["Decimales"].ToString());
                        lstConfiguracionDecimalesDTO.Add(oConfiguracionDecimalesDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstConfiguracionDecimalesDTO;
        }


        public int UpdateInsertConfiguracionDecimales(ConfiguracionDecimalesDTO oConfiguracionDecimalesDTO, int IdSociedad, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertConfiguracionDecimales", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdConfiguracionDecimales", oConfiguracionDecimalesDTO.IdConfiguracionDecimales);
                        da.SelectCommand.Parameters.AddWithValue("@Importes", oConfiguracionDecimalesDTO.Importes);
                        da.SelectCommand.Parameters.AddWithValue("@Precios", oConfiguracionDecimalesDTO.Precios);
                        da.SelectCommand.Parameters.AddWithValue("@Cantidades", oConfiguracionDecimalesDTO.Cantidades);
                        da.SelectCommand.Parameters.AddWithValue("@Porcentajes", oConfiguracionDecimalesDTO.Porcentajes);
                        da.SelectCommand.Parameters.AddWithValue("@Unidades", oConfiguracionDecimalesDTO.Unidades);
                        da.SelectCommand.Parameters.AddWithValue("@Decimales", oConfiguracionDecimalesDTO.Decimales);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
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
    }
}
