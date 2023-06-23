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
    public class GlosaContableDAO
    {
        public List<GlosaContableDTO> ObtenerGlosaContable(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<GlosaContableDTO> lstGlosaContableDTO = new List<GlosaContableDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGlosaContable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GlosaContableDTO oGlosaContableDTO = new GlosaContableDTO();
                        oGlosaContableDTO.IdGlosaContable = int.Parse(drd["IdGlosaContable"].ToString());
                        oGlosaContableDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oGlosaContableDTO.Division = drd["Division"].ToString();
                        oGlosaContableDTO.Codigo = drd["Codigo"].ToString();
                        oGlosaContableDTO.Descripcion = drd["Descripcion"].ToString();
                        oGlosaContableDTO.CuentaContable = drd["CuentaContable"].ToString();
                        oGlosaContableDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGlosaContableDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstGlosaContableDTO.Add(oGlosaContableDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGlosaContableDTO;
        }

        public List<GlosaContableDTO> ObtenerGlosaContableDivision(int IdSociedad,int Idbase, ref string mensaje_error, int Estado = 3)
        {
            List<GlosaContableDTO> lstGlosaContableDTO = new List<GlosaContableDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGlosaContableDivision", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Idbase", Idbase);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GlosaContableDTO oGlosaContableDTO = new GlosaContableDTO();
                        oGlosaContableDTO.IdGlosaContable = int.Parse(drd["IdGlosaContable"].ToString());
                        oGlosaContableDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oGlosaContableDTO.Division = drd["Division"].ToString();
                        oGlosaContableDTO.Codigo = drd["Codigo"].ToString();
                        oGlosaContableDTO.Descripcion = drd["Descripcion"].ToString();
                        oGlosaContableDTO.CuentaContable = drd["CuentaContable"].ToString();
                        oGlosaContableDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGlosaContableDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstGlosaContableDTO.Add(oGlosaContableDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGlosaContableDTO;
        }

        public int UpdateInsertGlosaContable(GlosaContableDTO oGlosaContableDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateGlosaContable", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGlosaContable", oGlosaContableDTO.IdGlosaContable);
                        da.SelectCommand.Parameters.AddWithValue("@IdDivision", oGlosaContableDTO.IdDivision);
                        da.SelectCommand.Parameters.AddWithValue("@CuentaContable", oGlosaContableDTO.CuentaContable);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oGlosaContableDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oGlosaContableDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oGlosaContableDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGlosaContableDTO.Estado);
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

        public List<GlosaContableDTO> ObtenerDatosxID(int IdGlosaContable, ref string mensaje_error)
        {
            List<GlosaContableDTO> lstGlosaContableDTO = new List<GlosaContableDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGlosaContablexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGlosaContable", IdGlosaContable);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GlosaContableDTO oGlosaContableDTO = new GlosaContableDTO();
                        oGlosaContableDTO.IdGlosaContable = int.Parse(drd["IdGlosaContable"].ToString());
                        oGlosaContableDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oGlosaContableDTO.Codigo = drd["Codigo"].ToString();
                        oGlosaContableDTO.Descripcion = drd["Descripcion"].ToString();
                        oGlosaContableDTO.CuentaContable = drd["CuentaContable"].ToString();
                        oGlosaContableDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oGlosaContableDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstGlosaContableDTO.Add(oGlosaContableDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGlosaContableDTO;
        }


        public int Delete(int IdArea, ref string mensaje_error)
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

