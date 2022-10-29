using DTO;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace DAO
{
    public class MotivoTrasladoDAO
    {
        public List<MotivoTrasladoDTO> ObtenerMotivoTraslado(int IdSociedad, ref string mensaje_error)
        {
            List<MotivoTrasladoDTO> lstMotivoTrasladoDTO = new List<MotivoTrasladoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMotivoTraslado", cn);
                    //da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    //da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MotivoTrasladoDTO oMotivoTrasladoDTO = new MotivoTrasladoDTO();
                        oMotivoTrasladoDTO.IdMotivoTraslado = Convert.ToInt32(drd["IdMotivoTraslado"].ToString());
                        oMotivoTrasladoDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oMotivoTrasladoDTO.CodigoInterno = drd["CodigoInterno"].ToString();
                        oMotivoTrasladoDTO.Descripcion = (drd["Descripcion"].ToString());
                        lstMotivoTrasladoDTO.Add(oMotivoTrasladoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMotivoTrasladoDTO;
        }

        public int UpdateInsertMotivoTraslado(MotivoTrasladoDTO oMotivoTrasladoDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertMotivoTraslado", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMotivoTraslado", oMotivoTrasladoDTO.IdMotivoTraslado);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oMotivoTrasladoDTO.CodigoSunat);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoInterno", oMotivoTrasladoDTO.CodigoInterno);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oMotivoTrasladoDTO.Descripcion);
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

        public List<MotivoTrasladoDTO> ObtenerDatosxID(int IdMotivoTraslado, ref string mensaje_error)
        {
            List<MotivoTrasladoDTO> lstMotivoTrasladoDTO = new List<MotivoTrasladoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMotivoTrasladoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMotivoTraslado", IdMotivoTraslado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        MotivoTrasladoDTO oMotivoTrasladoDTO = new MotivoTrasladoDTO();
                        oMotivoTrasladoDTO.IdMotivoTraslado = int.Parse(drd["IdMotivoTraslado"].ToString());
                        oMotivoTrasladoDTO.CodigoSunat = (drd["CodigoSunat"].ToString());
                        oMotivoTrasladoDTO.CodigoInterno = drd["CodigoInterno"].ToString();
                        oMotivoTrasladoDTO.Descripcion = drd["Descripcion"].ToString();
                        lstMotivoTrasladoDTO.Add(oMotivoTrasladoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMotivoTrasladoDTO;
        }


        public int Delete(int IdMotivoTraslado, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaMotivoTraslado", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMotivoTraslado", IdMotivoTraslado);
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
