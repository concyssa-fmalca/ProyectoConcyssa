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
    public class PeriodoDAO
    {

        #region IndicadorPeriodo
        public List<IndicadorPeriodoDTO> ObtenerIndicadorPeriodo(int IdSociedad,int Estado ,ref string mensaje_error)
        {
            List<IndicadorPeriodoDTO> lstIndicadorPeriodoDTO = new List<IndicadorPeriodoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarIndicadorPeriodo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", (IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        IndicadorPeriodoDTO oIndicadorPeriodoDTO = new IndicadorPeriodoDTO();
                        oIndicadorPeriodoDTO.IdIndicadorPeriodo = int.Parse(drd["IdIndicadorPeriodo"].ToString());
                        oIndicadorPeriodoDTO.Indicador = drd["Indicador"].ToString();
                        oIndicadorPeriodoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oIndicadorPeriodoDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oIndicadorPeriodoDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        var dd = drd["Eliminado"].ToString();

                        oIndicadorPeriodoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        lstIndicadorPeriodoDTO.Add(oIndicadorPeriodoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstIndicadorPeriodoDTO;
        }


        public IndicadorPeriodoDTO ObtenerIndicadorPeriodoxId(int IdIndicadorPeriodo,ref string mensaje_error)
        {
            IndicadorPeriodoDTO oIndicadorPeriodoDTO = new IndicadorPeriodoDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarIndicadorPeriodoxId", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdIndicadorPeriodo", (IdIndicadorPeriodo));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oIndicadorPeriodoDTO.IdIndicadorPeriodo = int.Parse(drd["IdIndicadorPeriodo"].ToString());
                        oIndicadorPeriodoDTO.Indicador = drd["Indicador"].ToString();
                        oIndicadorPeriodoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oIndicadorPeriodoDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oIndicadorPeriodoDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        oIndicadorPeriodoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }
            return oIndicadorPeriodoDTO;
        }

        public int UpdateInsertIndicadorPeriodo(IndicadorPeriodoDTO oIndicadorPeriodoDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertIndicadorPeriodo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorPeriodo", oIndicadorPeriodoDTO.IdIndicadorPeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@Indicador", oIndicadorPeriodoDTO.Indicador);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oIndicadorPeriodoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oIndicadorPeriodoDTO.IdUsuario);
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

        public int DeleteIndicadorPeriodo(int IdIndicadorPeriodo, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarIndicadorPeriodo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorPeriodo", IdIndicadorPeriodo);
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
        #endregion





        #region PeriodoContable
        public List<PeriodoContableDTO> ObtenerPeriodoContable(int IdSociedad,int Estado,ref string mensaje_error)
        {
            List<PeriodoContableDTO> lstPeriodoContableDTO = new List<PeriodoContableDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPeriodoContable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PeriodoContableDTO oPeriodoContableDTO = new PeriodoContableDTO();
                        oPeriodoContableDTO.IdPeriodoContable = Convert.ToInt32(drd["IdPeriodoContable"].ToString());
                        oPeriodoContableDTO.CodigoPeriodo = drd["CodigoPeriodo"].ToString();
                        oPeriodoContableDTO.NombrePeriodo = drd["NombrePeriodo"].ToString();
                        oPeriodoContableDTO.SubPeriodo = Convert.ToInt32(drd["SubPeriodo"].ToString());
                        oPeriodoContableDTO.IdIndicadorPeriodo = Convert.ToInt32(drd["IdIndicadorPeriodo"].ToString());
                        oPeriodoContableDTO.StatusPeriodo = Convert.ToInt32(drd["StatusPeriodo"].ToString());
                        oPeriodoContableDTO.InicioEjercicio = Convert.ToDateTime(drd["InicioEjercicio"].ToString());
                        oPeriodoContableDTO.Ejercicio = (drd["Ejercicio"].ToString());
                        oPeriodoContableDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oPeriodoContableDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oPeriodoContableDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        oPeriodoContableDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oPeriodoContableDTO.FechaContabilizacionI = Convert.ToDateTime(drd["FechaContabilizacionI"].ToString());
                        oPeriodoContableDTO.FechaContabilizacionF = Convert.ToDateTime(drd["FechaContabilizacionF"].ToString());

                        lstPeriodoContableDTO.Add(oPeriodoContableDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstPeriodoContableDTO;
        }

        public PeriodoContableDTO ObtenerPeriodoContablexId(int IdPeriodoContable, ref string mensaje_error)
        {
            PeriodoContableDTO oPeriodoContableDTO = new PeriodoContableDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPeriodoContablexId", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContable", IdPeriodoContable);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oPeriodoContableDTO.IdPeriodoContable = Convert.ToInt32(drd["IdPeriodoContable"].ToString());
                        oPeriodoContableDTO.CodigoPeriodo = drd["CodigoPeriodo"].ToString();
                        oPeriodoContableDTO.NombrePeriodo = drd["NombrePeriodo"].ToString();
                        oPeriodoContableDTO.SubPeriodo = Convert.ToInt32(drd["SubPeriodo"].ToString());
                        oPeriodoContableDTO.IdIndicadorPeriodo = Convert.ToInt32(drd["IdIndicadorPeriodo"].ToString());
                        oPeriodoContableDTO.StatusPeriodo = Convert.ToInt32(drd["StatusPeriodo"].ToString());
                        oPeriodoContableDTO.InicioEjercicio = Convert.ToDateTime(drd["InicioEjercicio"].ToString());
                        oPeriodoContableDTO.Ejercicio = (drd["Ejercicio"].ToString());
                        oPeriodoContableDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oPeriodoContableDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oPeriodoContableDTO.CreatedAt = Convert.ToDateTime(drd["CreatedAt"].ToString());
                        oPeriodoContableDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();

                }
            }

            #region Contar Detalle 
            Int32 filasdetalle = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_PeriodoContableFechaxIdPeriodoContable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContable", IdPeriodoContable);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalle++;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error += ex.Message.ToString();
                }
            }

            oPeriodoContableDTO.Detalles= new PeriodoContableFechaDTO[filasdetalle];
            #endregion

            #region Detalle
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_PeriodoContableFechaxIdPeriodoContable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContable", IdPeriodoContable);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr2 = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (dr2.Read())
                    {
                        PeriodoContableFechaDTO oPeriodoContableFechaDTO = new PeriodoContableFechaDTO();
                        oPeriodoContableFechaDTO.IdPeriodoContableFecha = Convert.ToInt32(dr2["IdPeriodoContableFecha"].ToString());
                        oPeriodoContableFechaDTO.FechaInicio = Convert.ToDateTime(dr2["FechaInicio"].ToString());
                        oPeriodoContableFechaDTO.FechaFinal = Convert.ToDateTime(dr2["FechaFinal"].ToString());
                        oPeriodoContableFechaDTO.IdPeriodoContable = Convert.ToInt32(dr2["IdPeriodoContable"].ToString());
                        oPeriodoContableFechaDTO.Descripcion = (dr2["Descripcion"].ToString());
                        oPeriodoContableFechaDTO.Orden = Convert.ToInt32(dr2["Orden"].ToString());


                        oPeriodoContableDTO.Detalles[posicion] = oPeriodoContableFechaDTO;
                        posicion = posicion + 1;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error += ex.Message.ToString();
                }
            }
            #endregion
            return oPeriodoContableDTO;
        }

        public int UpdateInsertPeriodoContable(PeriodoContableDTO oPeriodoContableDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPeriodoContable", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContable", oPeriodoContableDTO.IdPeriodoContable);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoPeriodo", oPeriodoContableDTO.CodigoPeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@NombrePeriodo", oPeriodoContableDTO.NombrePeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@SubPeriodo", oPeriodoContableDTO.SubPeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@IdIndicadorPeriodo", oPeriodoContableDTO.IdIndicadorPeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@StatusPeriodo", oPeriodoContableDTO.StatusPeriodo);
                        da.SelectCommand.Parameters.AddWithValue("@InicioEjercicio", oPeriodoContableDTO.InicioEjercicio);
                        da.SelectCommand.Parameters.AddWithValue("@Ejercicio", oPeriodoContableDTO.Ejercicio);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oPeriodoContableDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oPeriodoContableDTO.IdUsuario);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        //int rpta = da.SelectCommand.ExecuteNonQuery();
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

        public int UpdateInsertPeriodoContableFecha(PeriodoContableFechaDTO oPeriodoContableFechaDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertPeriodoContableFecha", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContableFecha", oPeriodoContableFechaDTO.IdPeriodoContableFecha);
                        da.SelectCommand.Parameters.AddWithValue("@FechaInicio", oPeriodoContableFechaDTO.FechaInicio);
                        da.SelectCommand.Parameters.AddWithValue("@FechaFinal", oPeriodoContableFechaDTO.FechaFinal);
                        da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContable", oPeriodoContableFechaDTO.IdPeriodoContable);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oPeriodoContableFechaDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@Orden", oPeriodoContableFechaDTO.Orden);
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

        public int DeletePeriodoContable(int IdPeriodoContable, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarPeriodoContable", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdPeriodoContable", IdPeriodoContable);
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
        #endregion


        #region Validacion
        public List<PeriodoContableFechaDTO> ObtenerPeriodoContableFechaValidacion(int IdIndicadorPeriodo,string Fecha,int Orden)
        {
            List <PeriodoContableFechaDTO> lstPeriodoContableFechaDTO = new List<PeriodoContableFechaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_FechaPeriodoContable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdIndicadorPeriodo", IdIndicadorPeriodo);
                    da.SelectCommand.Parameters.AddWithValue("@Fecha", Fecha);
                    da.SelectCommand.Parameters.AddWithValue("@Orden", Orden);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PeriodoContableFechaDTO oPeriodoContableFechaDTO = new PeriodoContableFechaDTO();
                        oPeriodoContableFechaDTO.IdPeriodoContableFecha = Convert.ToInt32(drd["IdPeriodoContableFecha"].ToString());
                        oPeriodoContableFechaDTO.FechaInicio = Convert.ToDateTime(drd["FechaInicio"].ToString());
                        oPeriodoContableFechaDTO.FechaFinal = Convert.ToDateTime(drd["FechaFinal"].ToString());
                        oPeriodoContableFechaDTO.IdPeriodoContable = Convert.ToInt32(drd["IdPeriodoContable"].ToString());
                        oPeriodoContableFechaDTO.Descripcion = (drd["Descripcion"].ToString());
                        oPeriodoContableFechaDTO.Orden = Convert.ToInt32(drd["Orden"].ToString());
                        oPeriodoContableFechaDTO.StatusPeriodo = Convert.ToInt32(drd["StatusPeriodo"].ToString());
                        lstPeriodoContableFechaDTO.Add(oPeriodoContableFechaDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                   ex.Message.ToString();
                }
            }
            return lstPeriodoContableFechaDTO;
        }
        #endregion

    }
}
