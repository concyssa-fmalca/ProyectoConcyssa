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
    public class NumeracionDocumentoDAO
    {
        public List<NumeracionDocumentoDTO> Numeracion(int IdBase, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<NumeracionDocumentoDTO> lstNumeracionDocumentoDTO = new List<NumeracionDocumentoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarNumeracionDocumento", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        NumeracionDocumentoDTO oNumeracionDocumentoDTO = new NumeracionDocumentoDTO();
                        oNumeracionDocumentoDTO.IdNumeracionDocumento = int.Parse(drd["IdNumeracionDocumento"].ToString());
                        oNumeracionDocumentoDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oNumeracionDocumentoDTO.IdTipoDocumento = int.Parse(drd["IdTipoDocumento"].ToString());
                        oNumeracionDocumentoDTO.Serie = drd["Serie"].ToString();
                        oNumeracionDocumentoDTO.Base = drd["Base"].ToString();
                        oNumeracionDocumentoDTO.TipoDocumento = drd["TipoDocumento"].ToString();
                        oNumeracionDocumentoDTO.NumeracionInicial = int.Parse(drd["NumeracionInicial"].ToString());
                        oNumeracionDocumentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstNumeracionDocumentoDTO.Add(oNumeracionDocumentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstNumeracionDocumentoDTO;
        }

       
        public int UpdateInsertNumeracion(NumeracionDocumentoDTO oNumeracionDocumentoDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateNumeracionDocumento", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdNumeracionDocumento", oNumeracionDocumentoDTO.IdNumeracionDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", oNumeracionDocumentoDTO.IdBase);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oNumeracionDocumentoDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Serie", oNumeracionDocumentoDTO.Serie);
                        da.SelectCommand.Parameters.AddWithValue("@NumeracionInicial", oNumeracionDocumentoDTO.NumeracionInicial);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oNumeracionDocumentoDTO.Estado);
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

        public List<NumeracionDocumentoDTO> ObtenerDatosxID(int IdNumeracionDocumento, string BaseDatos, ref string mensaje_error)
        {
            List<NumeracionDocumentoDTO> lstNumeracionDocumentoDTO = new List<NumeracionDocumentoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarNumeracionDocumentoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdNumeracionDocumento", IdNumeracionDocumento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        NumeracionDocumentoDTO oNumeracionDocumentoDTO = new NumeracionDocumentoDTO();
                        oNumeracionDocumentoDTO.IdNumeracionDocumento = int.Parse(drd["IdNumeracionDocumento"].ToString());
                        oNumeracionDocumentoDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oNumeracionDocumentoDTO.IdTipoDocumento = int.Parse(drd["IdTipoDocumento"].ToString());
                        oNumeracionDocumentoDTO.Serie = drd["Serie"].ToString();
                       
                        oNumeracionDocumentoDTO.NumeracionInicial = int.Parse(drd["NumeracionInicial"].ToString());
                        oNumeracionDocumentoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstNumeracionDocumentoDTO.Add(oNumeracionDocumentoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstNumeracionDocumentoDTO;
        }


        public int Delete(int IdNumeracionDocumento, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaNumeracionDocumento", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdNumeracionDocumento", IdNumeracionDocumento);
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

