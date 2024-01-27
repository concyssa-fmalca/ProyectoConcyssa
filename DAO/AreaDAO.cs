
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class AreaDAO
    {
        public List<AreaDTO> ObtenerArea(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<AreaDTO> lstAreaDTO = new List<AreaDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArea", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AreaDTO oAreaDTO = new AreaDTO();
                        oAreaDTO.IdArea = int.Parse(drd["IdArea"].ToString());
                        oAreaDTO.Codigo = drd["Codigo"].ToString();
                        oAreaDTO.Descripcion = drd["Descripcion"].ToString();
                        oAreaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oAreaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstAreaDTO.Add(oAreaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAreaDTO;
        }

        public int UpdateInsertArea(AreaDTO oAreaDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertArea", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdArea", oAreaDTO.IdArea);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oAreaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oAreaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oAreaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oAreaDTO.Estado);
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

        public List<AreaDTO> ObtenerDatosxID(int IdArea, string BaseDatos, ref string mensaje_error)
        {
            List<AreaDTO> lstAreaDTO = new List<AreaDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarAreaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArea", IdArea);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AreaDTO oAreaDTO = new AreaDTO();
                        oAreaDTO.IdArea = int.Parse(drd["IdArea"].ToString());
                        oAreaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oAreaDTO.Codigo = drd["Codigo"].ToString();
                        oAreaDTO.Descripcion = drd["Descripcion"].ToString();
                        oAreaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstAreaDTO.Add(oAreaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstAreaDTO;
        }


        public int Delete(int IdArea, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaArea", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdArea", IdArea);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return -1;
                    }
                }
            }
        }

    }
}
