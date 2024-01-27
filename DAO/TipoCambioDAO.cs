using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAO
{
    public class TipoCambioDAO
    {
        public List<TipoCambioDTO> ObtenerTipoCambio(string BaseDatos, ref string mensaje_error)
        {
            List<TipoCambioDTO> lstTipoCambioDTO = new List<TipoCambioDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoCambio", cn);              
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoCambioDTO oTipoCambioDTO = new TipoCambioDTO();
                        oTipoCambioDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oTipoCambioDTO.Moneda = drd["Moneda"].ToString();
                        oTipoCambioDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());
                        oTipoCambioDTO.Origen = drd["Origen"].ToString();
                        oTipoCambioDTO.TipoCambioCompra = Convert.ToDecimal(drd["TipoCambioCompra"].ToString());
                        oTipoCambioDTO.TipoCambioVenta = Convert.ToDecimal(drd["TipoCambioVenta"].ToString());

                        lstTipoCambioDTO.Add(oTipoCambioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoCambioDTO;
        }

        public int UpdateInsertTipoCambio(TipoCambioDTO oTipoCambioDTO, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateTipoCambio", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oTipoCambioDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Fecha", oTipoCambioDTO.Fecha);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambioCompra", oTipoCambioDTO.TipoCambioCompra);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCambioVenta", oTipoCambioDTO.TipoCambioVenta);
                        da.SelectCommand.Parameters.AddWithValue("@Origen", oTipoCambioDTO.Origen);
                 
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

        public List<TipoCambioDTO> ObtenerDatosxID(int IdMoneda,DateTime Fecha, string BaseDatos, ref string mensaje_error)
        {
            List<TipoCambioDTO> lstTipoCambioDTO = new List<TipoCambioDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoCambioxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdMoneda", IdMoneda);
                    da.SelectCommand.Parameters.AddWithValue("@Fecha", Fecha);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoCambioDTO oTipoCambioDTO = new TipoCambioDTO();
                        oTipoCambioDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oTipoCambioDTO.Moneda = drd["Moneda"].ToString();
                        oTipoCambioDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());
                        oTipoCambioDTO.Origen = drd["Origen"].ToString();
                        oTipoCambioDTO.TipoCambioCompra = Convert.ToDecimal(drd["TipoCambioCompra"].ToString());
                        oTipoCambioDTO.TipoCambioVenta = Convert.ToDecimal(drd["TipoCambioVenta"].ToString());

                        lstTipoCambioDTO.Add(oTipoCambioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoCambioDTO;
        }


        public int Delete(int IdArea, string BaseDatos, ref string mensaje_error)
        {
            TransactionOptions transactionOptions = default(TransactionOptions);
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60.0);
            TransactionOptions option = transactionOptions;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarGlosaContable", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGlosaContable", IdArea);
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

