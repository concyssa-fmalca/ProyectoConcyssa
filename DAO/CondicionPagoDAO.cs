using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class CondicionPagoDAO
    {
        public List<CondicionPagoDTO> ObtenerCondicionPagos(string IdSociedad)
        {
            List<CondicionPagoDTO> lstCondicionPagoDTO = new List<CondicionPagoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCondicionPagos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CondicionPagoDTO oCondicionPagoDTO = new CondicionPagoDTO();
                        oCondicionPagoDTO.IdCondicionPago = int.Parse(drd["Id"].ToString());
                        oCondicionPagoDTO.Codigo = drd["Codigo"].ToString();
                        oCondicionPagoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCondicionPagoDTO.Dias = int.Parse(drd["Dias"].ToString());
                        oCondicionPagoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCondicionPagoDTO.Add(oCondicionPagoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstCondicionPagoDTO;
        }

        public int UpdateInsertCondicionPago(CondicionPagoDTO oCondicionPagoDTO, string IdSociedad,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCondicionPagos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", oCondicionPagoDTO.IdCondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCondicionPagoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCondicionPagoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Dias", oCondicionPagoDTO.Dias);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCondicionPagoDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioActualizacion", IdUsuario);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }
        }

        public List<CondicionPagoDTO> ObtenerDatosxID(int IdCondicionPago)
        {
            List<CondicionPagoDTO> lstCondicionPagoDTO = new List<CondicionPagoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCondicionPagosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", IdCondicionPago);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CondicionPagoDTO oCondicionPagoDTO = new CondicionPagoDTO();
                        oCondicionPagoDTO.IdCondicionPago = int.Parse(drd["Id"].ToString());
                        oCondicionPagoDTO.Codigo = drd["Codigo"].ToString();
                        oCondicionPagoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCondicionPagoDTO.Dias = int.Parse(drd["Dias"].ToString());
                        oCondicionPagoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCondicionPagoDTO.Add(oCondicionPagoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstCondicionPagoDTO;
        }


        public int Delete(int IdCondicionPago)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarCondicionPago", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", IdCondicionPago);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
