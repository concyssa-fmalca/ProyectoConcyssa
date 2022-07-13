

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class CargoDAO
    {
        public List<CargoDTO> ObtenerCargo(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<CargoDTO> lstCargoDTO = new List<CargoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCargo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CargoDTO oCargoDTO = new CargoDTO();
                        oCargoDTO.IdCargo = int.Parse(drd["IdCargo"].ToString());
                        oCargoDTO.Codigo = drd["Codigo"].ToString();
                        oCargoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCargoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oCargoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstCargoDTO.Add(oCargoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCargoDTO;
        }

        public int UpdateInsertCargo(CargoDTO oCargoDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCargo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCargo", oCargoDTO.IdCargo);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCargoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCargoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oCargoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCargoDTO.Estado);
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

        public List<CargoDTO> ObtenerDatosxID(int IdCargo, ref string mensaje_error)
        {
            List<CargoDTO> lstCargoDTO = new List<CargoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCargoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCargo", IdCargo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CargoDTO oCargoDTO = new CargoDTO();
                        oCargoDTO.IdCargo = int.Parse(drd["IdCargo"].ToString());
                        oCargoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oCargoDTO.Codigo = drd["Codigo"].ToString();
                        oCargoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCargoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCargoDTO.Add(oCargoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCargoDTO;
        }


        public int Delete(int IdCargo, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaCargo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCargo", IdCargo);
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
