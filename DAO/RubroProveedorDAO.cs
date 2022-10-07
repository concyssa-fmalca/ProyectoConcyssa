using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class RubroProveedorDAO
    {
        public List<RubroProveedorDTO> ObtenerRubroProveedor(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<RubroProveedorDTO> lstRubroProveedorDTO = new List<RubroProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarRubroProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        RubroProveedorDTO oRubroProveedorDTO = new RubroProveedorDTO();
                        oRubroProveedorDTO.IdRubroProveedor = int.Parse(drd["IdRubroProveedor"].ToString());
                        oRubroProveedorDTO.Codigo = drd["Codigo"].ToString();
                        oRubroProveedorDTO.Descripcion = drd["Descripcion"].ToString();
                        oRubroProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oRubroProveedorDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstRubroProveedorDTO.Add(oRubroProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstRubroProveedorDTO;
        }

        public int UpdateInsertRubroProveedor(RubroProveedorDTO oRubroProveedorDTO, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertRubroProveedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdRubroProveedor", oRubroProveedorDTO.IdRubroProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oRubroProveedorDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oRubroProveedorDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oRubroProveedorDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oRubroProveedorDTO.Estado);
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

        public List<RubroProveedorDTO> ObtenerDatosxID(int IdRubroProveedor, ref string mensaje_error)
        {
            List<RubroProveedorDTO> lstRubroProveedorDTO = new List<RubroProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarRubroProveedorxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdRubroProveedor", IdRubroProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        RubroProveedorDTO oRubroProveedorDTO = new RubroProveedorDTO();
                        oRubroProveedorDTO.IdRubroProveedor = int.Parse(drd["IdRubroProveedor"].ToString());
                        oRubroProveedorDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oRubroProveedorDTO.Codigo = drd["Codigo"].ToString();
                        oRubroProveedorDTO.Descripcion = drd["Descripcion"].ToString();
                        oRubroProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstRubroProveedorDTO.Add(oRubroProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstRubroProveedorDTO;
        }


        public int Delete(int IdRubroProveedor, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaRubroProveedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdRubroProveedor", IdRubroProveedor);
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
