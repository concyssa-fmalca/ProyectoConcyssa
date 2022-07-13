
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class MonedaDAO
    {
        public List<MonedaDTO> ObtenerMonedas(string IdSociedad)
        {
            List<MonedaDTO> lstMonedaDTO = new List<MonedaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMonedas", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MonedaDTO oMonedaDTO = new MonedaDTO();
                        oMonedaDTO.IdMoneda = int.Parse(drd["IdMoneda"].ToString());
                        oMonedaDTO.Codigo = drd["Codigo"].ToString();
                        oMonedaDTO.Descripcion = drd["Descripcion"].ToString();
                        oMonedaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oMonedaDTO.Base = bool.Parse(drd["Base"].ToString());
                        lstMonedaDTO.Add(oMonedaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstMonedaDTO;
        }

        public int UpdateInsertMoneda(MonedaDTO oMonedaDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertMonedas", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oMonedaDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oMonedaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMonedaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Base", oMonedaDTO.Base);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMonedaDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
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

        public List<MonedaDTO> ObtenerDatosxID(int IdMoneda)
        {
            List<MonedaDTO> lstMonedaDTO = new List<MonedaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMonedasxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMoneda", IdMoneda);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MonedaDTO oMonedaDTO = new MonedaDTO();
                        oMonedaDTO.IdMoneda = int.Parse(drd["IdMoneda"].ToString());
                        oMonedaDTO.Codigo = drd["Codigo"].ToString();
                        oMonedaDTO.Descripcion = drd["Descripcion"].ToString();
                        oMonedaDTO.Base = bool.Parse(drd["Base"].ToString());
                        oMonedaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstMonedaDTO.Add(oMonedaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstMonedaDTO;
        }


        public int Delete(int IdMoneda)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarMoneda", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", IdMoneda);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }


        public List<MonedaDTO> ValidarMonedaBase(int IdMoneda)
        {
            List<MonedaDTO> lstMonedaDTO = new List<MonedaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidarMonedaBase", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMoneda", IdMoneda);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MonedaDTO oMonedaDTO = new MonedaDTO();
                        oMonedaDTO.IdMoneda = int.Parse(drd["IdMoneda"].ToString());
                        oMonedaDTO.Codigo = drd["Codigo"].ToString();
                        oMonedaDTO.Descripcion = drd["Descripcion"].ToString();
                        oMonedaDTO.Base = bool.Parse(drd["Base"].ToString());
                        oMonedaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstMonedaDTO.Add(oMonedaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstMonedaDTO;
        }
    }
}
