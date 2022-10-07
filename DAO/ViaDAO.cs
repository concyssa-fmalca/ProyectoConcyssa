using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class ViaDAO
    {
        public List<ViaDTO> ObtenerVia(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<ViaDTO> lstViaDTO = new List<ViaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarVia", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ViaDTO oViaDTO = new ViaDTO();
                        oViaDTO.IdVia = int.Parse(drd["IdVia"].ToString());
                        oViaDTO.Codigo = drd["Codigo"].ToString();
                        oViaDTO.Descripcion = drd["Descripcion"].ToString();
                        oViaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oViaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstViaDTO.Add(oViaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstViaDTO;
        }

        public int UpdateInsertVia(ViaDTO oViaDTO, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertVia", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdVia", oViaDTO.IdVia);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oViaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oViaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oViaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oViaDTO.Estado);
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

        public List<ViaDTO> ObtenerDatosxID(int IdVia, ref string mensaje_error)
        {
            List<ViaDTO> lstViaDTO = new List<ViaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarViaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdVia", IdVia);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ViaDTO oViaDTO = new ViaDTO();
                        oViaDTO.IdVia = int.Parse(drd["IdVia"].ToString());
                        oViaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oViaDTO.Codigo = drd["Codigo"].ToString();
                        oViaDTO.Descripcion = drd["Descripcion"].ToString();
                        oViaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstViaDTO.Add(oViaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstViaDTO;
        }


        public int Delete(int IdVia, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaVia", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdVia", IdVia);
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
