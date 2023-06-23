using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DAO.helpers;

namespace DAO
{
    public class SemanaDAO
    {

        public List<SemanaDTO> ObtenerSemanas(int IdTipoRegistro,int Anio, int IdObra,int IdSociedad, int Estado, ref string mensaje_error)
        {
            List<SemanaDTO> lstSemanaDTO = new List<SemanaDTO>();
            mensaje_error = "";
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSemana", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@Anio", Anio);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", IdTipoRegistro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SemanaDTO oSemanaDTO = new SemanaDTO();
                        oSemanaDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        oSemanaDTO.Anio = Convert.ToInt32(drd["Anio"].ToString());
                        oSemanaDTO.FechaI = Convert.ToDateTime(drd["FechaI"].ToString());
                        oSemanaDTO.FechaF = Convert.ToDateTime(drd["FechaF"].ToString());
                        oSemanaDTO.NumSemana = Convert.ToInt32(drd["NumSemana"].ToString());
                        oSemanaDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oSemanaDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oSemanaDTO.TipoRegistro = drd["TipoRegistro"].ToString();
                        oSemanaDTO.Obra =drd["Obra"].ToString();
                        oSemanaDTO.Fondo = Convert.ToDecimal(drd["Fondo"].ToString());
                        oSemanaDTO.Descripcion ="Semana"+ drd["NumSemana"].ToString() +" del "+ oSemanaDTO.FechaI.ToString("dd/MM") + " al " + oSemanaDTO.FechaF.ToString("dd/MM");

                       

                        lstSemanaDTO.Add(oSemanaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSemanaDTO;
        }

        public List<SemanaDTO> ObtenerSemanasxIdTipoRegistro(int IdSociedad, int Estado,int IdTipoRegistro,int IdObra, ref string mensaje_error)
        {
            List<SemanaDTO> lstSemanaDTO = new List<SemanaDTO>();
            mensaje_error = "";
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSemanaActivas", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", IdTipoRegistro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        int dIdTipoRegistro = Convert.ToInt32(drd["IdTipoRegistro"].ToString());
                        int dIdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        if (dIdTipoRegistro == IdTipoRegistro && dIdObra == IdObra)
                        {
                            SemanaDTO oSemanaDTO = new SemanaDTO();
                            oSemanaDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());

                            var FechaI = Convert.ToDateTime(drd["FechaI"].ToString());
                            var FechaF = Convert.ToDateTime(drd["FechaF"].ToString());
                            oSemanaDTO.Descripcion = "Semana " + drd["NumSemana"].ToString() + " del " + FechaI.ToString("dd/MM") + " al " + FechaF.ToString("dd/MM");
                            lstSemanaDTO.Add(oSemanaDTO);
                        }
                                
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSemanaDTO;
        }

        public int UpdateInsertSemana(SemanaDTO oSemanaDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertSemana", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSemana", oSemanaDTO.IdSemana);
                        da.SelectCommand.Parameters.AddWithValue("@Anio", oSemanaDTO.Anio);
                        da.SelectCommand.Parameters.AddWithValue("@FechaI", oSemanaDTO.FechaI);
                        da.SelectCommand.Parameters.AddWithValue("@FechaF", oSemanaDTO.FechaF);
                        da.SelectCommand.Parameters.AddWithValue("@NumSemana", oSemanaDTO.NumSemana);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oSemanaDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oSemanaDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", oSemanaDTO.IdTipoRegistro);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oSemanaDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@Fondo", oSemanaDTO.Fondo);

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


        public List<SemanaDTO> ObtenerDatosxID(int IdTIpoRegistro, ref string mensaje_error)
        {
            List<SemanaDTO> lstSemanaDTO = new List<SemanaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSemanaxId", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSemana", IdTIpoRegistro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SemanaDTO oSemanaDTO = new SemanaDTO();
                        oSemanaDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        oSemanaDTO.Anio = Convert.ToInt32(drd["Anio"].ToString());
                        oSemanaDTO.FechaI = Convert.ToDateTime(drd["FechaI"].ToString());
                        oSemanaDTO.FechaF = Convert.ToDateTime(drd["FechaF"].ToString());
                        oSemanaDTO.NumSemana = Convert.ToInt32(drd["NumSemana"].ToString());
                        oSemanaDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oSemanaDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());

                      
                        oSemanaDTO.IdTipoRegistro = HelperDao.conversionInt(drd,"IdTipoRegistro");
                        oSemanaDTO.IdObra = HelperDao.conversionInt(drd,"IdObra");
                       
                        oSemanaDTO.Fondo = Convert.ToDecimal(drd["Fondo"].ToString());
                        lstSemanaDTO.Add(oSemanaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstSemanaDTO;
        }

        public int Delete(int IdSemana, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_DeleteSemana", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdSemana", IdSemana);
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
