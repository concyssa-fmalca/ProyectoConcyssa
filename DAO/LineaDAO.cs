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
    public class LineaDAO
    {
        public List<LineaDTO> ObtenerLinea(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<LineaDTO> lstLineaDTO = new List<LineaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarLinea", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        LineaDTO oLineaDTO = new LineaDTO();
                        oLineaDTO.IdLinea = int.Parse(drd["IdLinea"].ToString());
                        oLineaDTO.Codigo = drd["Codigo"].ToString();
                        oLineaDTO.Descripcion = drd["Descripcion"].ToString();
                        oLineaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oLineaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstLineaDTO.Add(oLineaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstLineaDTO;
        }

        public int UpdateInsertLinea(LineaDTO oLineaDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertLinea", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdLinea", oLineaDTO.IdLinea);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oLineaDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oLineaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oLineaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oLineaDTO.Estado);
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

        public List<LineaDTO> ObtenerDatosxID(int IdLinea, ref string mensaje_error)
        {
            List<LineaDTO> lstLineaDTO = new List<LineaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarLineaxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdLinea", IdLinea);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        LineaDTO oLineaDTO = new LineaDTO();
                        oLineaDTO.IdLinea = int.Parse(drd["IdLinea"].ToString());
                        oLineaDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oLineaDTO.Codigo = drd["Codigo"].ToString();
                        oLineaDTO.Descripcion = drd["Descripcion"].ToString();
                        oLineaDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstLineaDTO.Add(oLineaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstLineaDTO;
        }


        public int Delete(int IdLinea, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaLinea", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdLinea", IdLinea);
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
