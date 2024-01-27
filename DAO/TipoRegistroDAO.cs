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
    public class TipoRegistroDAO
    {

        public List<TipoRegistroDTO> ObtenerTipoRegistro(int IdSociedad, int Estado, string BaseDatos, ref string mensaje_error )
        {
            List<TipoRegistroDTO> lstTipoRegistroDTO = new List<TipoRegistroDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoRegistro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoRegistroDTO oTipoRegistroDTO = new TipoRegistroDTO();
                        oTipoRegistroDTO.IdTipoRegistro = int.Parse(drd["IdTipoRegistro"].ToString());
                        oTipoRegistroDTO.NombTipoRegistro = drd["NombTipoRegistro"].ToString(); 
                        oTipoRegistroDTO.Estado =Convert.ToBoolean( drd["Estado"].ToString());
                        lstTipoRegistroDTO.Add(oTipoRegistroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoRegistroDTO;
        }
        public int UpdateInsertTipoRegistro(TipoRegistroDTO oTipoRegistroDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertTipoRegistro", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", oTipoRegistroDTO.IdTipoRegistro);
                        da.SelectCommand.Parameters.AddWithValue("@NombTipoRegistro", oTipoRegistroDTO.NombTipoRegistro);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oTipoRegistroDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oTipoRegistroDTO.IdSociedad);
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


        public List<TipoRegistroDTO> ObtenerDatosxID(int IdTIpoRegistro, string BaseDatos, ref string mensaje_error)
        {
            List<TipoRegistroDTO> lstTipoRegistroDTO = new List<TipoRegistroDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoRegistroxId", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdTIpoRegistro", IdTIpoRegistro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoRegistroDTO oTipoRegistroDTO = new TipoRegistroDTO();
                        oTipoRegistroDTO.IdTipoRegistro = int.Parse(drd["IdTipoRegistro"].ToString());
                        oTipoRegistroDTO.NombTipoRegistro = (drd["NombTipoRegistro"].ToString());
                        oTipoRegistroDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oTipoRegistroDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        lstTipoRegistroDTO.Add(oTipoRegistroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoRegistroDTO;
        }

        public int Delete(int IdTipoRegistro, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarTipoRegistro", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", IdTipoRegistro);
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
