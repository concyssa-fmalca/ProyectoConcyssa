using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class MarcaDAO
    {
        public List<MarcaDTO> ObtenerMarca(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<MarcaDTO> lstMarcaDTO = new List<MarcaDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMarca", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MarcaDTO oMarcaDTO = new MarcaDTO();
                        oMarcaDTO.IdMarca = int.Parse(drd["IdMarca"].ToString());
                        oMarcaDTO.Codigo = drd["Codigo"].ToString();
                        oMarcaDTO.Descripcion = drd["Descripcion"].ToString();
                        oMarcaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oMarcaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstMarcaDTO.Add(oMarcaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMarcaDTO;
        }

        public int UpdateInsertMarca(MarcaDTO oMarcaDTO, string BaseDatos, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertMarca", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMarca", oMarcaDTO.IdMarca);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oMarcaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMarcaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oMarcaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oMarcaDTO.Estado);
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

        public List<MarcaDTO> ObtenerDatosxID(int IdMarca, string BaseDatos, ref string mensaje_error)
        {
            List<MarcaDTO> lstMarcaDTO = new List<MarcaDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMarcaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMarca", IdMarca);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MarcaDTO oMarcaDTO = new MarcaDTO();
                        oMarcaDTO.IdMarca = int.Parse(drd["IdMarca"].ToString());
                        oMarcaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oMarcaDTO.Codigo = drd["Codigo"].ToString();
                        oMarcaDTO.Descripcion = drd["Descripcion"].ToString();
                        oMarcaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstMarcaDTO.Add(oMarcaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMarcaDTO;
        }


        public int Delete(int IdMarca, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaMarca", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMarca", IdMarca);
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
