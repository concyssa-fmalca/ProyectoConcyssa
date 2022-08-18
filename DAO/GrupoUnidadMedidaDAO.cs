using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class GrupoUnidadMedidaDAO
    {

        
        public List<GrupoUnidadMedidaDTO> ObtenerDatosxID(int IdGrupoUnidadMedida, ref string mensaje_error)
        {
            List<GrupoUnidadMedidaDTO> lstGrupoUnidadMedidaDTO = new List<GrupoUnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGrupoUnidadMedidaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupoUnidadMedida", IdGrupoUnidadMedida);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoUnidadMedidaDTO oGrupoUnidadMedidaDTO = new GrupoUnidadMedidaDTO();
                        oGrupoUnidadMedidaDTO.IdGrupoUnidadMedida = int.Parse(drd["IdGrupoUnidadMedida"].ToString());
                        oGrupoUnidadMedidaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oGrupoUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstGrupoUnidadMedidaDTO.Add(oGrupoUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoUnidadMedidaDTO;
        }

        public List<GrupoUnidadMedidaDTO> ObtenerGrupoUnidadMedida(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<GrupoUnidadMedidaDTO> lstGrupoUnidadMedidaDTO = new List<GrupoUnidadMedidaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_GrupoUnidadMedida", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoUnidadMedidaDTO oGrupoUnidadMedidaDTO = new GrupoUnidadMedidaDTO();
                        oGrupoUnidadMedidaDTO.IdGrupoUnidadMedida = int.Parse(drd["IdGrupoUnidadMedida"].ToString());
                        oGrupoUnidadMedidaDTO.Codigo = drd["Codigo"].ToString();
                        oGrupoUnidadMedidaDTO.Descripcion = drd["Descripcion"].ToString();
                        oGrupoUnidadMedidaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGrupoUnidadMedidaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstGrupoUnidadMedidaDTO.Add(oGrupoUnidadMedidaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGrupoUnidadMedidaDTO;
        }

        public int UpdateInsertGrupoUnidadMedida(GrupoUnidadMedidaDTO oGrupoUnidadMedidDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertGrupoUnidadMedida", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoUnidadMedida", oGrupoUnidadMedidDTO.IdGrupoUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oGrupoUnidadMedidDTO.Codigo);
                        //da.SelectCommand.Parameters.AddWithValue("@Codigo","ddddd");
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oGrupoUnidadMedidDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oGrupoUnidadMedidDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGrupoUnidadMedidDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Eliminado", oGrupoUnidadMedidDTO.Eliminado);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
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


    }
}
