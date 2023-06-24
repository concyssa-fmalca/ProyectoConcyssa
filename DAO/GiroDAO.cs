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
using System.Data.Common;

namespace DAO
{
    public class GiroDAO
    {
        public List<GiroDTO> ObtenerGiro( ref string mensaje_error,int IdSociedad, int IdObra = 0, int IdTipoRegistro = 0, int IdSemana = 0, int IdEstadoGiro = 0,  int Estado = 3, int IdUsuario=0)
        {
            List<GiroDTO> lstGiroDTO = new List<GiroDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGIro", cn);
                   // da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    if(IdObra != 0)da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    if(IdTipoRegistro != 0)da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", IdTipoRegistro);
                    if(IdSemana != 0)da.SelectCommand.Parameters.AddWithValue("@IdSemana", IdSemana);
                    if(IdEstadoGiro != 0)da.SelectCommand.Parameters.AddWithValue("@IdEstadoGiro", IdEstadoGiro);
                    if (IdUsuario != 0) da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GiroDTO olstGiroDTO = new GiroDTO();
                        olstGiroDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                        olstGiroDTO.IdResponsable = Convert.ToInt32(drd["IdResponsable"].ToString());
                        olstGiroDTO.IdSolicitante = Convert.ToInt32(drd["IdSolicitante"].ToString());
                        olstGiroDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        olstGiroDTO.Tipo = Convert.ToInt32(drd["Tipo"].ToString());
                    
                        olstGiroDTO.Obra = HelperDao.toString(drd, "Obra");
                        olstGiroDTO.Semana = HelperDao.toString(drd, "Semana"); 

                        olstGiroDTO.Responsable = HelperDao.toString(drd, "Responsable");
                   
                        olstGiroDTO.Solicitante = HelperDao.toString(drd, "Solicitante");
                     
                        olstGiroDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        olstGiroDTO.Fecha = Convert.ToDateTime(drd["Fecha"].ToString());
                        olstGiroDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        olstGiroDTO.MontoSoles = Convert.ToDecimal(drd["MontoSoles"].ToString());
                        olstGiroDTO.MontoDolares = Convert.ToDecimal(drd["MontoDolares"].ToString());
                        olstGiroDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        olstGiroDTO.Contabilizado = Convert.ToBoolean(drd["Contabilizado"].ToString());
                        olstGiroDTO.EstadoGiro = HelperDao.toString(drd, "EstadoGiro");
                        olstGiroDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        olstGiroDTO.NombSerie = (drd["NombSerie"].ToString());
                        olstGiroDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        olstGiroDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        
                        lstGiroDTO.Add(olstGiroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }

      

        public List<GiroDTO> ObtenerGiroxId(int IdGiro, ref string mensaje_error)
        {
            List<GiroDTO> lstGiroDTO = new List<GiroDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GiroDTO oGiroDTO = new GiroDTO();
                        oGiroDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                        oGiroDTO.IdResponsable = Convert.ToInt32(drd["IdResponsable"].ToString());
                        oGiroDTO.IdSolicitante = Convert.ToInt32(drd["IdSolicitante"].ToString());
                        oGiroDTO.Tipo = Convert.ToInt32(drd["Tipo"].ToString());
                        oGiroDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oGiroDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        oGiroDTO.IdTipoRegistro = Convert.ToInt32(drd["IdTipoRegistro"].ToString());
                        oGiroDTO.IdEstadoGiro = Convert.ToInt32(drd["IdEstadoGiro"].ToString());
                        oGiroDTO.IdCreador = Convert.ToInt32((drd["IdCreador"].ToString()== null ? 0 : drd["IdCreador"].ToString()));

                        lstGiroDTO.Add(oGiroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }


        public List<GiroDetalleDTO> ObtenerGiroDetalle(int IdGiro, ref string mensaje_error, int Estado = 3)
        {
            List<GiroDetalleDTO> lstGiroDTO = new List<GiroDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroDetallexIdGiro", cn);
                    // da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GiroDetalleDTO oGiroDetalleDTO = new GiroDetalleDTO();
                        oGiroDetalleDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                        oGiroDetalleDTO.IdGiroDetalle = Convert.ToInt32(drd["IdGiroDetalle"].ToString());
                        oGiroDetalleDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oGiroDetalleDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oGiroDetalleDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oGiroDetalleDTO.TipoDocumento = drd["TipoDocumento"].ToString();
                        oGiroDetalleDTO.Documento = drd["Anexo"].ToString();
                        oGiroDetalleDTO.Moneda = drd["Moneda"].ToString();
                        oGiroDetalleDTO.Monto = Convert.ToDecimal(drd["Monto"].ToString());
                        oGiroDetalleDTO.Rendicion = Convert.ToDecimal(drd["Rendicion"].ToString());
                        oGiroDetalleDTO.Serie = drd["Serie"].ToString();
                        oGiroDetalleDTO.NroDocumento = drd["NroDocumento"].ToString();
                        oGiroDetalleDTO.Comentario = drd["Comentario"].ToString();
                        oGiroDetalleDTO.Proveedor = drd["Proveedor"].ToString();
                      
                        lstGiroDTO.Add(oGiroDetalleDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }


        public List<GiroDetalleDTO> ObtenerGiroDetallexId(int IdGiroDetalle, ref string mensaje_error)
        {
            List<GiroDetalleDTO> lstGiroDetalleDTO = new List<GiroDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroDetallexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiroDetalle", IdGiroDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GiroDetalleDTO oGiroDetalleDTO = new GiroDetalleDTO();
                        oGiroDetalleDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                        oGiroDetalleDTO.IdGiroDetalle = Convert.ToInt32(drd["IdGiroDetalle"].ToString());
                        oGiroDetalleDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oGiroDetalleDTO.IdProveedor = Convert.ToInt32(drd["IdProveedor"].ToString());
                        oGiroDetalleDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());                       
                        oGiroDetalleDTO.Monto = Convert.ToDecimal(drd["Monto"].ToString());                       
                        oGiroDetalleDTO.NroDocumento = drd["NroDocumento"].ToString();
                        oGiroDetalleDTO.Comentario = drd["Comentario"].ToString();
                        oGiroDetalleDTO.Anexo = drd["Anexo"].ToString();
                        lstGiroDetalleDTO.Add(oGiroDetalleDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDetalleDTO;
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

        public int UpdateInsertGiroDetalle(GiroDetalleDTO oGiroDetalleDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateGiroDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGiro", oGiroDetalleDTO.IdGiro);
                        da.SelectCommand.Parameters.AddWithValue("@IdGiroDetalle", oGiroDetalleDTO.IdGiroDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oGiroDetalleDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oGiroDetalleDTO.IdTipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NroDocumento", oGiroDetalleDTO.NroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@IdMoneda", oGiroDetalleDTO.IdMoneda);
                        da.SelectCommand.Parameters.AddWithValue("@Monto", oGiroDetalleDTO.Monto);
                        da.SelectCommand.Parameters.AddWithValue("@Comentario", oGiroDetalleDTO.Comentario);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGiroDetalleDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Anexo", oGiroDetalleDTO.@Anexo);
                      
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

        public List<string> UpdateInsertGiro(GiroDTO oGiroDTO, int IdUsuario,int IdSociedad,ref string mensaje_error)
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
                        string Result = "";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertUpdateGiro", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGiro", oGiroDTO.IdGiro);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oGiroDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoRegistro", oGiroDTO.IdTipoRegistro);
                        da.SelectCommand.Parameters.AddWithValue("@IdSemana", oGiroDTO.IdSemana);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", oGiroDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@IdResponsable", oGiroDTO.IdResponsable);
                        da.SelectCommand.Parameters.AddWithValue("@IdSolicitante", oGiroDTO.IdSolicitante);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oGiroDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdEstadoGiro", oGiroDTO.IdEstadoGiro);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);


                        string? rpta = "";
                        bool EsNuevoIngreso = false;

                        if (oGiroDTO.IdGiro > 0)
                        {
                            SqlDataReader drd = da.SelectCommand.ExecuteReader();
                            while (drd.Read())
                            {

                                rpta = drd["IdGiro"].ToString();

                            }
                            drd.Close();
                        }
                        else
                        {
                            rpta = (da.SelectCommand.ExecuteScalar()).ToString();
                            EsNuevoIngreso = true;
                        }


                     

                        var IdGiroSplit = rpta.Split("-");

                        for (int i = 0; i < oGiroDTO.DetalleGiro.Count; i++)
                        {
                            SqlDataAdapter das = new SqlDataAdapter("SMC_InsertUpdateGiroDetalle", cn);
                            das.SelectCommand.CommandType = CommandType.StoredProcedure;
                            das.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiroSplit[0]);
                            das.SelectCommand.Parameters.AddWithValue("@IdGiroDetalle", oGiroDTO.DetalleGiro[i].IdGiroDetalle);
                            das.SelectCommand.Parameters.AddWithValue("@IdProveedor", oGiroDTO.DetalleGiro[i].IdProveedor);
                            das.SelectCommand.Parameters.AddWithValue("@IdTipoDocumento", oGiroDTO.DetalleGiro[i].IdTipoDocumento);
                            das.SelectCommand.Parameters.AddWithValue("@NroDocumento", oGiroDTO.DetalleGiro[i].NroDocumento);
                            das.SelectCommand.Parameters.AddWithValue("@IdMoneda", oGiroDTO.DetalleGiro[i].IdMoneda);
                            das.SelectCommand.Parameters.AddWithValue("@Monto", oGiroDTO.DetalleGiro[i].Monto);
                            das.SelectCommand.Parameters.AddWithValue("@Comentario", oGiroDTO.DetalleGiro[i].Comentario);
                            das.SelectCommand.Parameters.AddWithValue("@Estado", oGiroDTO.DetalleGiro[i].Estado);
                            das.SelectCommand.Parameters.AddWithValue("@Anexo", oGiroDTO.DetalleGiro[i].Anexo);
                            das.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                            Result = das.SelectCommand.ExecuteNonQuery().ToString();
                        }


                        List<String> resultados = new List<String>();
                        if (EsNuevoIngreso)
                        {
                            resultados.Add("5");
                            resultados.Add(rpta);
                        }
                        else
                        {
                            resultados.Add("1");
                            resultados.Add(rpta);
                        }


                        transactionScope.Complete();
                        return resultados;



                    }
                    catch (Exception ex)
                    {
                        List<string> resultadoerror = new List<string>();
                        resultadoerror.Add(ex.Message.ToString());
                        resultadoerror.Add("0");
                        return resultadoerror;
                        //mensaje_error = ex.Message.ToString();
                        //return "";
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

        public int DeleteGiro(int IdGiro, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarGiro", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
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

        public int DeleteGiroDetalle(int IdGiroDetalle, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarGiroDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdGiroDetalle", IdGiroDetalle);
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








        public GiroDTO DatosSolicitudRq(int IdGiro)
        {
            GiroDTO oGiroDTO = new GiroDTO();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oGiroDTO.IdGiro = int.Parse(drd["IdGiro"].ToString());
                        //oGiroDTO.IdSerie = int.Parse(drd["IdSerie"].ToString());
                        //oGiroDTO.Serie = drd["Serie"].ToString();
                        //oGiroDTO.Numero = int.Parse(drd["Numero"].ToString());
                        oGiroDTO.IdSolicitante = int.Parse(drd["IdSolicitante"].ToString());
                        oGiroDTO.IdResponsable = int.Parse(drd["IdResponsable"].ToString());
                        oGiroDTO.Total = decimal.Parse(drd["Total"].ToString());
                        oGiroDTO.MontoSoles = decimal.Parse(drd["MontoSoles"].ToString());
                        oGiroDTO.MontoDolares = decimal.Parse(drd["MontoDolares"].ToString());
                        //oGiroDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        //oGiroDTO.FechaCreacion = Convert.ToDateTime(drd["FechaCreacion"].ToString());
                        //oGiroDTO.Comentarios = drd["Comentarios"].ToString();
                        //oGiroDTO.Estado = int.Parse(drd["Estado"].ToString());
                        //oGiroDTO.DetalleEstado = drd["DetalleEstado"].ToString();
                        //oGiroDTO.Prioridad = int.Parse(drd["Prioridad"].ToString());
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                }
            }

            Int32 filasGiroModelo = 0;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroModeloxIdGiro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasGiroModelo++;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            oGiroDTO.ListGiroModelo = new GiroModeloDTO[filasGiroModelo];
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroModeloxIdGiro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdGiro", IdGiro);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (drd.Read())
                    {
                        GiroModeloDTO oGiroModeloDTO = new GiroModeloDTO();
                        oGiroModeloDTO.IdGiroModelo = Convert.ToInt32(drd["Id"].ToString());
                        oGiroModeloDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                        oGiroModeloDTO.IdModelo = Convert.ToInt32(drd["IdModelo"].ToString());
                        oGiroModeloDTO.IdEtapa = Convert.ToInt32(drd["IdEtapa"].ToString());
                        oGiroModeloDTO.Aprobaciones = Convert.ToInt32(drd["Aprobaciones"].ToString());
                        oGiroModeloDTO.Rechazos = Convert.ToInt32(drd["Rechazos"].ToString());
                        oGiroModeloDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oGiroDTO.ListGiroModelo[posicion] = oGiroModeloDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            if (oGiroDTO.ListGiroModelo.Count() > 0)
            {
                for (int i = 0; i < oGiroDTO.ListGiroModelo.Count(); i++)
                {
                    Int32 filasGiroModeloAprobaciones = 0;
                    using (SqlConnection cn = new Conexion().conectar())
                    {
                        try
                        {
                            cn.Open();
                            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroModeloAprobacionesxId", cn);
                            da.SelectCommand.Parameters.AddWithValue("@IdGiroModelo", oGiroDTO.ListGiroModelo[i].IdGiroModelo);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                            while (dr1.Read())
                            {
                                filasGiroModeloAprobaciones++;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    oGiroDTO.ListGiroModelo[i].ListModeloAprobacionesDTO = new GiroModeloAprobacionesDTO[filasGiroModeloAprobaciones];
                    using (SqlConnection cn = new Conexion().conectar())
                    {
                        try
                        {
                            cn.Open();
                            SqlDataAdapter da = new SqlDataAdapter("SMC_ListarGiroModeloAprobacionesxId", cn);
                            da.SelectCommand.Parameters.AddWithValue("@IdGiroModelo", oGiroDTO.ListGiroModelo[i].IdGiroModelo);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            SqlDataReader drd = da.SelectCommand.ExecuteReader();
                            Int32 posicion = 0;
                            while (drd.Read())
                            {
                                GiroModeloAprobacionesDTO oGiroModeloAprobacionesDTO = new GiroModeloAprobacionesDTO();
                                oGiroModeloAprobacionesDTO.IdGiroModeloAprobaciones = Convert.ToInt32(drd["Id"].ToString());
                                oGiroModeloAprobacionesDTO.IdGiroModelo = Convert.ToInt32(drd["IdGiroModelo"].ToString());
                                oGiroModeloAprobacionesDTO.IdAutorizador = Convert.ToInt32(drd["IdAutorizador"].ToString());
                                //oGiroModeloAprobacionesDTO.IdArticulo = (drd["IdArticulo"].ToString());
                                oGiroModeloAprobacionesDTO.FechaAprobacion = Convert.ToDateTime(drd["FechaAprobacion"].ToString());
                                oGiroModeloAprobacionesDTO.Accion = Convert.ToInt32(drd["Accion"].ToString());
                                oGiroModeloAprobacionesDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                                oGiroModeloAprobacionesDTO.Estado = Convert.ToInt32(drd["Estado"].ToString());
                                oGiroModeloAprobacionesDTO.IdDetalle = Convert.ToInt32(drd["IdDetalle"].ToString());
                                oGiroModeloAprobacionesDTO.Autorizador = drd["Autorizador"].ToString();
                                oGiroModeloAprobacionesDTO.NroDocumento = drd["NroDocumento"].ToString();
                                oGiroModeloAprobacionesDTO.Proveedor = drd["Proveedor"].ToString();
                                //oGiroModeloAprobacionesDTO.NombArticulo = (drd["NombArticulo"].ToString());
                                oGiroModeloAprobacionesDTO.NombEstado = (drd["NombEstado"].ToString());


                                oGiroDTO.ListGiroModelo[i].ListModeloAprobacionesDTO[posicion] = oGiroModeloAprobacionesDTO;
                                posicion = posicion + 1;
                            }

                        }
                        catch (Exception ex)
                        {
                        }
                    }




                }
            }
            return oGiroDTO;
        }

        public List<GiroDTO> ObtenerGiroxAprobado(int IdObra, ref string mensaje_error)
        {
            List<GiroDTO> lstGiroDTO = new List<GiroDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGiroxAprobado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GiroDTO oGiroDTO = new GiroDTO();
                        oGiroDTO.IdGiro = Convert.ToInt32(drd["IdGiro"].ToString());
                        oGiroDTO.IdResponsable = Convert.ToInt32(drd["IdResponsable"].ToString());
                        oGiroDTO.IdSolicitante = Convert.ToInt32(drd["IdSolicitante"].ToString());
                        oGiroDTO.Tipo = Convert.ToInt32(drd["Tipo"].ToString());
                        oGiroDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oGiroDTO.IdSemana = Convert.ToInt32(drd["IdSemana"].ToString());
                        oGiroDTO.IdTipoRegistro = Convert.ToInt32(drd["IdTipoRegistro"].ToString());
                        oGiroDTO.IdEstadoGiro = Convert.ToInt32(drd["IdEstadoGiro"].ToString());
                        oGiroDTO.IdCreador = Convert.ToInt32((drd["IdCreador"].ToString() == null ? 0 : drd["IdCreador"].ToString()));
                        oGiroDTO.Serie = drd["Serie"].ToString();
                        lstGiroDTO.Add(oGiroDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstGiroDTO;
        }








    }



    
}

