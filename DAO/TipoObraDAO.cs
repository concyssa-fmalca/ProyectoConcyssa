
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class TipoObraDAO
    {
        public List<TipoObraDTO> ObtenerTipoObra(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<TipoObraDTO> lstTipoObraDTO = new List<TipoObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoObraDTO oTipoObraDTO = new TipoObraDTO();
                        oTipoObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oTipoObraDTO.Codigo = drd["Codigo"].ToString();
                        oTipoObraDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oTipoObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstTipoObraDTO.Add(oTipoObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoObraDTO;
        }

        public int UpdateInsertTipoObra(TipoObraDTO oTipoObraDTO, string BaseDatos, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertTipoObra", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoObra", oTipoObraDTO.IdTipoObra);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oTipoObraDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oTipoObraDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oTipoObraDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTipoObraDTO.Estado);
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

        public List<TipoObraDTO> ObtenerDatosxID(int IdTipoObra, string BaseDatos, ref string mensaje_error)
        {
            List<TipoObraDTO> lstTipoObraDTO = new List<TipoObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoObraxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoObra", IdTipoObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoObraDTO oTipoObraDTO = new TipoObraDTO();
                        oTipoObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oTipoObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oTipoObraDTO.Codigo = drd["Codigo"].ToString();
                        oTipoObraDTO.Descripcion = drd["Descripcion"].ToString();
                        oTipoObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTipoObraDTO.Add(oTipoObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoObraDTO;
        }


        public int Delete(int IdTipoObra, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaTipoObra", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoObra", IdTipoObra);
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
