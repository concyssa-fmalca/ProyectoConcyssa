using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public  class UnidadMedidaDAO
    {

        
        public List<UnidadMedidaDTO> ObtenerUnidadMedidasxEstado(int IdSociedad,int estado, ref string mensaje_error)
        {
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = new List<UnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUnidadMedidasxEstado", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@estado", estado);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UnidadMedidaDTO oUnidadMedidaDTO = new UnidadMedidaDTO();
                        oUnidadMedidaDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oUnidadMedidaDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUnidadMedidaDTO.Add(oUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUnidadMedidaDTO;
        }

        public List<UnidadMedidaDTO> ObtenerUnidadMedidas(int IdSociedad, ref string mensaje_error)
        {
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = new List<UnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUnidadMedidas", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UnidadMedidaDTO oUnidadMedidaDTO = new UnidadMedidaDTO();
                        oUnidadMedidaDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oUnidadMedidaDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUnidadMedidaDTO.Add(oUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUnidadMedidaDTO;
        }

        public string UpdateInsertUnidadMedida(UnidadMedidaDTO oUnidadMedidaDTO, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertUnidadMedidas", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oUnidadMedidaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oUnidadMedidaDTO.IdUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oUnidadMedidaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoSunat", oUnidadMedidaDTO.CodigoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oUnidadMedidaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oUnidadMedidaDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioActualizacion", IdUsuario);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta.ToString();
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return "1";
                    }
                }
            }
        }

        public List<UnidadMedidaDTO> ObtenerDatosxID(int IdUnidadMedida, ref string mensaje_error)
        {
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = new List<UnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUnidadMedidasxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUnidadMedida);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UnidadMedidaDTO oUnidadMedidaDTO = new UnidadMedidaDTO();
                        oUnidadMedidaDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oUnidadMedidaDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstUnidadMedidaDTO.Add(oUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUnidadMedidaDTO;
        }


        public string  Delete(int IdUnidadMedida, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarUnidadMedida", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", IdUnidadMedida);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta.ToString();
                    }
                    catch (Exception ex)
                    {
                        mensaje_error = ex.Message.ToString();
                        return "0";
                    }
                }
            }
        }
    }
}
