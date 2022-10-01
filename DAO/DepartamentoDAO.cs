using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class DepartamentoDAO
    {
        public List<DepartamentoDTO> ObtenerDepartamentos(string IdSociedad)
        {
            List<DepartamentoDTO> lstDepartamentoDTO = new List<DepartamentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarMaestroDepartamentos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DepartamentoDTO oDepartamentoDTO = new DepartamentoDTO();
                        oDepartamentoDTO.IdDepartamento = int.Parse(drd["Id"].ToString());
                        oDepartamentoDTO.Codigo = drd["Codigo"].ToString();
                        oDepartamentoDTO.Descripcion = drd["Descripcion"].ToString();
                        oDepartamentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstDepartamentoDTO.Add(oDepartamentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstDepartamentoDTO;
        }

        public int UpdateInsertDepartamento(DepartamentoDTO oDepartamentoDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertDepartamentos", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", oDepartamentoDTO.IdDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oDepartamentoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oDepartamentoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oDepartamentoDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
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

        public List<DepartamentoDTO> ObtenerDatosxID(int IdDepartamento)
        {
            List<DepartamentoDTO> lstDepartamentoDTO = new List<DepartamentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDepartamentosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DepartamentoDTO oDepartamentoDTO = new DepartamentoDTO();
                        oDepartamentoDTO.IdDepartamento = int.Parse(drd["Id"].ToString());
                        oDepartamentoDTO.Codigo = drd["Codigo"].ToString();
                        oDepartamentoDTO.Descripcion = drd["Descripcion"].ToString();
                        oDepartamentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstDepartamentoDTO.Add(oDepartamentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstDepartamentoDTO;
        }


        public int Delete(int IdDepartamento)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarDepartamento", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", IdDepartamento);
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


        public List<DepartamentoDTO> ObtenerDepartamentosxUsuario(int IdUsuario)
        {
            List<DepartamentoDTO> lstDepartamentoDTO = new List<DepartamentoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDepartamentosxUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        DepartamentoDTO oDepartamentoDTO = new DepartamentoDTO();
                        oDepartamentoDTO.IdDepartamento = int.Parse(drd["Id"].ToString());
                        oDepartamentoDTO.Codigo = drd["Codigo"].ToString();
                        oDepartamentoDTO.Descripcion = drd["Descripcion"].ToString();
                        oDepartamentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstDepartamentoDTO.Add(oDepartamentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstDepartamentoDTO;
        }

    }
}
