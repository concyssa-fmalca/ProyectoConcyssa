using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class SerieDAO
    {
        public List<SerieDTO> ObtenerSerie(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSerie", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.Serie = drd["Serie"].ToString();
                        oSerieDTO.NumeroInicial = Convert.ToInt32(drd["NumeroInicial"].ToString());
                        oSerieDTO.NumeroFinal = Convert.ToInt32(drd["NumeroFinal"].ToString());
                        oSerieDTO.Documento = Convert.ToInt32(drd["Documento"].ToString());
                        oSerieDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oSerieDTO.Eliminado= Convert.ToBoolean(drd["Eliminado"].ToString());
                        oSerieDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSerieDTO;
        }
        /*
        public int UpdateInsertSerie(SerieDTO oSerieDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSerie", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", oSerieDTO.IdSerie);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oSerieDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oSerieDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oSerieDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSerieDTO.Estado);
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

        public List<SerieDTO> ObtenerDatosxID(int IdSerie, ref string mensaje_error)
        {
            List<SerieDTO> lstSerieDTO = new List<SerieDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSeriexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSerie", IdSerie);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SerieDTO oSerieDTO = new SerieDTO();
                        oSerieDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        oSerieDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oSerieDTO.Codigo = drd["Codigo"].ToString();
                        oSerieDTO.Descripcion = drd["Descripcion"].ToString();
                        oSerieDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSerieDTO.Add(oSerieDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSerieDTO;
        }


        public int Delete(int IdSerie, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaSerie", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSerie", IdSerie);
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
        }*/
    }
}
