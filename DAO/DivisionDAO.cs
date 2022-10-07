
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class DivisionDAO
    {
        public List<DivisionDTO> ObtenerDivision(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<DivisionDTO> lstDivisionDTO = new List<DivisionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDivision", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DivisionDTO oDivisionDTO = new DivisionDTO();
                        oDivisionDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oDivisionDTO.Codigo = drd["Codigo"].ToString();
                        oDivisionDTO.Descripcion = drd["Descripcion"].ToString();
                        oDivisionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oDivisionDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstDivisionDTO.Add(oDivisionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstDivisionDTO;
        }

        public int UpdateInsertDivision(DivisionDTO oDivisionDTO, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertDivision", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDivision", oDivisionDTO.IdDivision);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oDivisionDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oDivisionDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oDivisionDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oDivisionDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioActualizacion", IdUsuario);
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

        public List<DivisionDTO> ObtenerDatosxID(int IdDivision, ref string mensaje_error)
        {
            List<DivisionDTO> lstDivisionDTO = new List<DivisionDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDivisionxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdDivision", IdDivision);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DivisionDTO oDivisionDTO = new DivisionDTO();
                        oDivisionDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oDivisionDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oDivisionDTO.Codigo = drd["Codigo"].ToString();
                        oDivisionDTO.Descripcion = drd["Descripcion"].ToString();
                        oDivisionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstDivisionDTO.Add(oDivisionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstDivisionDTO;
        }


        public int Delete(int IdDivision, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaDivision", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDivision", IdDivision);
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
