
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class TiposDocumentosDAO
    {
        public List<TiposDocumentosDTO> ObtenerTiposDocumentos(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<TiposDocumentosDTO> lstTiposDocumentosDTO = new List<TiposDocumentosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTiposDocumentos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TiposDocumentosDTO oTiposDocumentosDTO = new TiposDocumentosDTO();
                        oTiposDocumentosDTO.IdTipoDocumento = int.Parse(drd["IdTipoDocumento"].ToString());
                        oTiposDocumentosDTO.CodSunat = (drd["CodSunat"].ToString());
                        oTiposDocumentosDTO.Codigo = drd["Codigo"].ToString();
                        oTiposDocumentosDTO.Descripcion = drd["Descripcion"].ToString();
                        oTiposDocumentosDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oTiposDocumentosDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstTiposDocumentosDTO.Add(oTiposDocumentosDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTiposDocumentosDTO;
        }

        public int UpdateInsertTiposDocumentos(TiposDocumentosDTO oTiposDocumentosDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertTiposDocumentos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oTiposDocumentosDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oTiposDocumentosDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@CodSunat", oTiposDocumentosDTO.CodSunat);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oTiposDocumentosDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oTiposDocumentosDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTiposDocumentosDTO.Estado);
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

        public List<TiposDocumentosDTO> ObtenerDatosxID(int IdTipoDocumento, ref string mensaje_error)
        {
            List<TiposDocumentosDTO> lstTiposDocumentosDTO = new List<TiposDocumentosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTiposDocumentosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", IdTipoDocumento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TiposDocumentosDTO oTiposDocumentosDTO = new TiposDocumentosDTO();
                        oTiposDocumentosDTO.IdTipoDocumento = int.Parse(drd["IdTipoDocumento"].ToString());
                        oTiposDocumentosDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oTiposDocumentosDTO.CodSunat = drd["CodSunat"].ToString();
                        oTiposDocumentosDTO.Codigo = drd["Codigo"].ToString();
                        oTiposDocumentosDTO.Descripcion = drd["Descripcion"].ToString();
                        oTiposDocumentosDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstTiposDocumentosDTO.Add(oTiposDocumentosDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTiposDocumentosDTO;
        }


        public int Delete(int IdTipoDocumento, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaTiposDocumentos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", IdTipoDocumento);
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
