

using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class CentroCostoDAO
    {
        public List<CentroCostoDTO> ObtenerCentroCosto(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<CentroCostoDTO> lstCentroCostoDTO = new List<CentroCostoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCentroCosto", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CentroCostoDTO oCentroCostoDTO = new CentroCostoDTO();
                        oCentroCostoDTO.IdCentroCosto = int.Parse(drd["IdCentroCosto"].ToString());
                        oCentroCostoDTO.Codigo = drd["Codigo"].ToString();
                        oCentroCostoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCentroCostoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oCentroCostoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstCentroCostoDTO.Add(oCentroCostoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCentroCostoDTO;
        }

        public int UpdateInsertCentroCosto(CentroCostoDTO oCentroCostoDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCentroCosto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", oCentroCostoDTO.IdCentroCosto);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCentroCostoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCentroCostoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oCentroCostoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCentroCostoDTO.Estado);
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

        public List<CentroCostoDTO> ObtenerDatosxID(int IdCentroCosto, ref string mensaje_error)
        {
            List<CentroCostoDTO> lstCentroCostoDTO = new List<CentroCostoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCentroCostoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CentroCostoDTO oCentroCostoDTO = new CentroCostoDTO();
                        oCentroCostoDTO.IdCentroCosto = int.Parse(drd["IdCentroCosto"].ToString());
                        oCentroCostoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oCentroCostoDTO.Codigo = drd["Codigo"].ToString();
                        oCentroCostoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCentroCostoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCentroCostoDTO.Add(oCentroCostoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCentroCostoDTO;
        }


        public int Delete(int IdCentroCosto, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaCentroCosto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCentroCosto", IdCentroCosto);
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
