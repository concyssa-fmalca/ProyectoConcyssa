
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class DefinicionGrupoUnidadDAO
    {
        
          public List<DefinicionGrupoUnidadDTO> ObtenerDefinicionesxIdGrupoUnidadMedida(int IdGrupoUnidadMedida, ref string mensaje_error)
          {
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = new List<DefinicionGrupoUnidadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDefinicionesxIdGrupoUnidadMedida", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGrupoUnidadMedida", IdGrupoUnidadMedida);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DefinicionGrupoUnidadDTO oDefinicionGrupoUnidadDTO = new DefinicionGrupoUnidadDTO();
                        oDefinicionGrupoUnidadDTO.IdDefinicionGrupo = Convert.ToInt32(drd["IdDefinicionGrupo"].ToString());
                        oDefinicionGrupoUnidadDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oDefinicionGrupoUnidadDTO.IdUnidadMedidaBase =Convert.ToInt32( drd["IdUnidadMedidaBase"].ToString());
                        oDefinicionGrupoUnidadDTO.CantidadBase = Convert.ToDecimal(drd["CantidadBase"].ToString());
                        oDefinicionGrupoUnidadDTO.IdUnidadMedidaAlt = Convert.ToInt32(drd["IdUnidadMedidaAlt"].ToString());
                        oDefinicionGrupoUnidadDTO.CantidadAlt = Convert.ToDecimal(drd["CantidadAlt"].ToString());
                        oDefinicionGrupoUnidadDTO.DescUnidadMedidaAlt = (drd["DescUnidadMedidaAlt"].ToString());
                        oDefinicionGrupoUnidadDTO.DescUnidadMedidaBase = (drd["DescUnidadMedidaBase"].ToString());
                        lstDefinicionGrupoUnidadDTO.Add(oDefinicionGrupoUnidadDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstDefinicionGrupoUnidadDTO;
          }

        public List<DefinicionGrupoUnidadDTO> ListarDefinicionGrupoxIdDefinicionSelect(int IdDefinicionGrupo, ref string mensaje_error)
        {
            List<DefinicionGrupoUnidadDTO> lstDefinicionGrupoUnidadDTO = new List<DefinicionGrupoUnidadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDefinicionGrupoxIdDefinicionSelect", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdDefinicionGrupo", IdDefinicionGrupo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DefinicionGrupoUnidadDTO oDefinicionGrupoUnidadDTO = new DefinicionGrupoUnidadDTO();
                        oDefinicionGrupoUnidadDTO.IdDefinicionGrupo = Convert.ToInt32(drd["IdDefinicionGrupo"].ToString());
                        oDefinicionGrupoUnidadDTO.IdGrupoUnidadMedida = Convert.ToInt32(drd["IdGrupoUnidadMedida"].ToString());
                        oDefinicionGrupoUnidadDTO.IdUnidadMedidaBase = Convert.ToInt32(drd["IdUnidadMedidaBase"].ToString());
                        oDefinicionGrupoUnidadDTO.CantidadBase = Convert.ToDecimal(drd["CantidadBase"].ToString());
                        oDefinicionGrupoUnidadDTO.IdUnidadMedidaAlt = Convert.ToInt32(drd["IdUnidadMedidaAlt"].ToString());
                        oDefinicionGrupoUnidadDTO.CantidadAlt = Convert.ToDecimal(drd["CantidadAlt"].ToString());
                        oDefinicionGrupoUnidadDTO.DescUnidadMedidaAlt = (drd["DescUnidadMedidaAlt"].ToString());
                        oDefinicionGrupoUnidadDTO.DescUnidadMedidaBase = (drd["DescUnidadMedidaBase"].ToString());
                        lstDefinicionGrupoUnidadDTO.Add(oDefinicionGrupoUnidadDTO);
                    }
                    drd.Close();

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstDefinicionGrupoUnidadDTO;
        }

        



        public int UpdateInsertDefinicionGrupoUnidad(DefinicionGrupoUnidadDTO oDefinicionGrupoUnidadDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertDefinicionGrupoUnidad", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDefinicionGrupo", oDefinicionGrupoUnidadDTO.IdDefinicionGrupo);
                        da.SelectCommand.Parameters.AddWithValue("@IdGrupoUnidadMedida", oDefinicionGrupoUnidadDTO.IdGrupoUnidadMedida);
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedidaBase", oDefinicionGrupoUnidadDTO.IdUnidadMedidaBase);
                        da.SelectCommand.Parameters.AddWithValue("@CantidadBase", oDefinicionGrupoUnidadDTO.CantidadBase);
                        da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedidaAlt", oDefinicionGrupoUnidadDTO.IdUnidadMedidaAlt);
                        da.SelectCommand.Parameters.AddWithValue("@CantidadAlt", oDefinicionGrupoUnidadDTO.CantidadAlt);
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
