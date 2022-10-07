using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class BaseDAO
    {

        public List<BaseDTO> ObtenerBase(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<BaseDTO> lstBaseDTO = new List<BaseDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarBase", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        BaseDTO oBaseDTO = new BaseDTO();
                        oBaseDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oBaseDTO.Codigo = drd["Codigo"].ToString();
                        oBaseDTO.Descripcion = drd["Descripcion"].ToString();
                        oBaseDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oBaseDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstBaseDTO.Add(oBaseDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstBaseDTO;
        }

        public int UpdateInsertBase(BaseDTO oBaseDTO, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertBase", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", oBaseDTO.IdBase);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oBaseDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oBaseDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oBaseDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oBaseDTO.Estado);
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

        public List<BaseDTO> ObtenerDatosxID(int IdBase, ref string mensaje_error)
        {
            List<BaseDTO> lstBaseDTO = new List<BaseDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarBasexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        BaseDTO oBaseDTO = new BaseDTO();
                        oBaseDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oBaseDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oBaseDTO.Codigo = drd["Codigo"].ToString();
                        oBaseDTO.Descripcion = drd["Descripcion"].ToString();
                        oBaseDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstBaseDTO.Add(oBaseDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstBaseDTO;
        }


        public int Delete(int IdBase, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaBase", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
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
