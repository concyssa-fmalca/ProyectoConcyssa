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
    public  class CodigoUbsoDAO
    {
        public List<CodigoUbsoDTO> ObtenerCodigoUbso(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<CodigoUbsoDTO> lstCodigoUbsoDTO = new List<CodigoUbsoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCodigoUbso", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CodigoUbsoDTO oCodigoUbsoDTO = new CodigoUbsoDTO();
                        oCodigoUbsoDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oCodigoUbsoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oCodigoUbsoDTO.Codigo = drd["Codigo"].ToString();
                        oCodigoUbsoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCodigoUbsoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCodigoUbsoDTO.Add(oCodigoUbsoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCodigoUbsoDTO;
        }
        
        public int UpdateInsertCodigoUbso(CodigoUbsoDTO oCodigoUbsoDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCodigoUbso", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCodigoUbso", oCodigoUbsoDTO.IdCodigoUbso);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oCodigoUbsoDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oCodigoUbsoDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oCodigoUbsoDTO.Estado);
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

        public List<CodigoUbsoDTO> ObtenerDatosxID(int IdCodigoUbso,ref string mensaje_error)
        {
            List<CodigoUbsoDTO> lstCodigoUbsoDTO = new List<CodigoUbsoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarCodigoUbsoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCodigoUbso", IdCodigoUbso);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        CodigoUbsoDTO oCodigoUbsoDTO = new CodigoUbsoDTO();
                        oCodigoUbsoDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oCodigoUbsoDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oCodigoUbsoDTO.Codigo = drd["Codigo"].ToString();
                        oCodigoUbsoDTO.Descripcion = drd["Descripcion"].ToString();
                        oCodigoUbsoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstCodigoUbsoDTO.Add(oCodigoUbsoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstCodigoUbsoDTO;
        }


        public int Delete(int IdCodigoUbso, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarLineaNegocio", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCodigoUbso", IdCodigoUbso);
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
