using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class CategoriaDAO
    {

        public List<CategoriaDTO> ObtenerCategoria(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<CategoriaDTO> lstCategoriaDTO = new List<CategoriaDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCategoria", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CategoriaDTO oCategoriaDTO = new CategoriaDTO();
                        oCategoriaDTO.IdCategoria = int.Parse(drd["IdCategoria"].ToString());
                        oCategoriaDTO.Codigo = drd["Codigo"].ToString();
                        oCategoriaDTO.Descripcion = drd["Descripcion"].ToString();
                        oCategoriaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oCategoriaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstCategoriaDTO.Add(oCategoriaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCategoriaDTO;
        }

        public int UpdateInsertCategoria(CategoriaDTO oCategoriaDTO, string BaseDatos, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCategoria", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCategoria", oCategoriaDTO.IdCategoria);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCategoriaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCategoriaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oCategoriaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCategoriaDTO.Estado);
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

        public List<CategoriaDTO> ObtenerDatosxID(int IdCategoria, string BaseDatos, ref string mensaje_error)
        {
            List<CategoriaDTO> lstCategoriaDTO = new List<CategoriaDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCategoriaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CategoriaDTO oCategoriaDTO = new CategoriaDTO();
                        oCategoriaDTO.IdCategoria = int.Parse(drd["IdCategoria"].ToString());
                        oCategoriaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oCategoriaDTO.Codigo = drd["Codigo"].ToString();
                        oCategoriaDTO.Descripcion = drd["Descripcion"].ToString();
                        oCategoriaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCategoriaDTO.Add(oCategoriaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCategoriaDTO;
        }


        public int Delete(int IdCategoria, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaCategoria", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCategoria", IdCategoria);
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
