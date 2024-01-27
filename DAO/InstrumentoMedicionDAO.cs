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
    public class InstrumentoMedicionDAO
    {
        public int UpdateInsertInstrumentoMedicion(InstrumentoMedicionDTO oInstrumentoMedicionDTO, string BaseDatos, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertInstrumentoMedicion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicion", oInstrumentoMedicionDTO.IdInstrumentoMedicion);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oInstrumentoMedicionDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oInstrumentoMedicionDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", oInstrumentoMedicionDTO.Nombre);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroSerie", oInstrumentoMedicionDTO.NumeroSerie);
                        da.SelectCommand.Parameters.AddWithValue("@IdMarca", oInstrumentoMedicionDTO.IdMarca);
                        da.SelectCommand.Parameters.AddWithValue("@IdModelo", oInstrumentoMedicionDTO.IdModelo);
                        da.SelectCommand.Parameters.AddWithValue("@PeriodoCalibracion", oInstrumentoMedicionDTO.PeriodoCalibracion);
                        da.SelectCommand.Parameters.AddWithValue("@ParametroMedicion", oInstrumentoMedicionDTO.ParametroMedicion);
                        da.SelectCommand.Parameters.AddWithValue("@PatronMedicion", oInstrumentoMedicionDTO.PatronMedicion);
                        da.SelectCommand.Parameters.AddWithValue("@IdArea", oInstrumentoMedicionDTO.IdArea);
                        da.SelectCommand.Parameters.AddWithValue("@Responsable", oInstrumentoMedicionDTO.Responsable);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoCalibracion", oInstrumentoMedicionDTO.EstadoCalibracion);
                        da.SelectCommand.Parameters.AddWithValue("@Ubicacion", oInstrumentoMedicionDTO.Ubicacion);
                        da.SelectCommand.Parameters.AddWithValue("@TipoCalibracion", oInstrumentoMedicionDTO.TipoCalibracion);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoInstrumento", oInstrumentoMedicionDTO.EstadoInstrumento);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oInstrumentoMedicionDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oInstrumentoMedicionDTO.Observaciones);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar().ToString());
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

        public List<InstrumentoMedicionDTO> ObtenerInstrumentoMedicion(int IdSociedad, string BaseDatos, ref string mensaje_error)
        {
            List<InstrumentoMedicionDTO> lstInstrumentoMedicionDTO = new List<InstrumentoMedicionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarInstrumentosMedicion", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        InstrumentoMedicionDTO oInstrumentoMedicionDTO = new InstrumentoMedicionDTO();

                        oInstrumentoMedicionDTO.IdInstrumentoMedicion = int.Parse(drd["IdInstrumentoMedicion"].ToString());        
                        oInstrumentoMedicionDTO.IdObra = int.Parse(drd["IdObra"].ToString());        
                        oInstrumentoMedicionDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());        
                        oInstrumentoMedicionDTO.Nombre = (drd["Nombre"].ToString());        
                        oInstrumentoMedicionDTO.NumeroSerie = (drd["NumeroSerie"].ToString());        
                        oInstrumentoMedicionDTO.IdMarca = int.Parse(drd["IdMarca"].ToString());        
                        oInstrumentoMedicionDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());        
                        oInstrumentoMedicionDTO.PeriodoCalibracion = int.Parse(drd["PeriodoCalibracion"].ToString());        
                        oInstrumentoMedicionDTO.ParametroMedicion = (drd["ParametroMedicion"].ToString());        
                        oInstrumentoMedicionDTO.PatronMedicion = (drd["PatronMedicion"].ToString());        
                        oInstrumentoMedicionDTO.IdArea = int.Parse(drd["IdArea"].ToString());        
                        oInstrumentoMedicionDTO.Responsable = (drd["Responsable"].ToString());        
                        oInstrumentoMedicionDTO.EstadoCalibracion = int.Parse(drd["EstadoCalibracion"].ToString());        
                        oInstrumentoMedicionDTO.Ubicacion = (drd["Ubicacion"].ToString());        
                        oInstrumentoMedicionDTO.TipoCalibracion = int.Parse(drd["TipoCalibracion"].ToString());        
                        oInstrumentoMedicionDTO.EstadoInstrumento = int.Parse(drd["EstadoInstrumento"].ToString());        
                        oInstrumentoMedicionDTO.Observaciones = (drd["Observaciones"].ToString());
                        oInstrumentoMedicionDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oInstrumentoMedicionDTO.NombObra = (drd["NombObra"].ToString());
                        oInstrumentoMedicionDTO.NombMarca = (drd["NombMarca"].ToString());
                        oInstrumentoMedicionDTO.NombModelo = (drd["NombModelo"].ToString());
                        oInstrumentoMedicionDTO.NombArea = (drd["NombArea"].ToString());            
                        oInstrumentoMedicionDTO.NombreArchivo = (drd["NombreArchivo"].ToString());            
                        oInstrumentoMedicionDTO.ProxCalib = DateTime.Parse(drd["ProxCalib"].ToString());            
                        oInstrumentoMedicionDTO.EstadoCalib = int.Parse(drd["EstadoCalib"].ToString());            
                        lstInstrumentoMedicionDTO.Add(oInstrumentoMedicionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstInstrumentoMedicionDTO;
        }

        public List<InstrumentoMedicionDTO> ObtenerDatosxID(int IdInstrumentoMedicion, string BaseDatos, ref string mensaje_error)
        {
            List<InstrumentoMedicionDTO> lstInstrumentoMedicionDTO = new List<InstrumentoMedicionDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarInstrumentoMedicionxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicion", IdInstrumentoMedicion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        InstrumentoMedicionDTO oInstrumentoMedicionDTO = new InstrumentoMedicionDTO();
                        oInstrumentoMedicionDTO.IdInstrumentoMedicion = int.Parse(drd["IdInstrumentoMedicion"].ToString());
                        oInstrumentoMedicionDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oInstrumentoMedicionDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oInstrumentoMedicionDTO.Nombre = (drd["Nombre"].ToString());
                        oInstrumentoMedicionDTO.NumeroSerie = (drd["NumeroSerie"].ToString());
                        oInstrumentoMedicionDTO.IdMarca = int.Parse(drd["IdMarca"].ToString());
                        oInstrumentoMedicionDTO.IdModelo = int.Parse(drd["IdModelo"].ToString());
                        oInstrumentoMedicionDTO.PeriodoCalibracion = int.Parse(drd["PeriodoCalibracion"].ToString());
                        oInstrumentoMedicionDTO.ParametroMedicion = (drd["ParametroMedicion"].ToString());
                        oInstrumentoMedicionDTO.PatronMedicion = (drd["PatronMedicion"].ToString());
                        oInstrumentoMedicionDTO.IdArea = int.Parse(drd["IdArea"].ToString());
                        oInstrumentoMedicionDTO.Responsable = (drd["Responsable"].ToString());
                        oInstrumentoMedicionDTO.EstadoCalibracion = int.Parse(drd["EstadoCalibracion"].ToString());
                        oInstrumentoMedicionDTO.Ubicacion = (drd["Ubicacion"].ToString());
                        oInstrumentoMedicionDTO.TipoCalibracion = int.Parse(drd["TipoCalibracion"].ToString());
                        oInstrumentoMedicionDTO.EstadoInstrumento = int.Parse(drd["EstadoInstrumento"].ToString());
                        oInstrumentoMedicionDTO.Observaciones = (drd["Observaciones"].ToString());
                        oInstrumentoMedicionDTO.Estado = bool.Parse(drd["Estado"].ToString());                 
                        lstInstrumentoMedicionDTO.Add(oInstrumentoMedicionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstInstrumentoMedicionDTO;
        }
        public int Delete(int IdInstrumentoMedicion,int IdUsuario, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaInstrumentoMedicion", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicion", IdInstrumentoMedicion);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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

        public List<AnexoDTO> ObtenerAnexoInstMedicion(int IdInstrumentoMedicion, string BaseDatos, ref string mensaje_error)
        {
            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosInstrumentoMedicionXID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicion", IdInstrumentoMedicion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AnexoDTO oAnexoDTO = new AnexoDTO();
                        oAnexoDTO.IdAnexo = Convert.ToInt32(drd["IdAnexo"].ToString());
                        oAnexoDTO.ruta = (drd["ruta"].ToString());
                        oAnexoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oAnexoDTO.Tabla = (drd["Tabla"].ToString());
                        oAnexoDTO.IdTabla = Convert.ToInt32(drd["IdTabla"].ToString());
                        oAnexoDTO.NombreArchivo = (drd["NombreArchivo"].ToString());
                        lstAnexoDTO.Add(oAnexoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstAnexoDTO;
            }


        }

        public int UpdateInsertInstrumentoMedicionDetalle(InstrumentoMedicionDetalleDTO oInstrumentoMedicionDetalleDTO, string BaseDatos, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertInstrumentoMedicionDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalle", oInstrumentoMedicionDetalleDTO.IdInstrumentoMedicionDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicion", oInstrumentoMedicionDetalleDTO.IdInstrumentoMedicion);
                        da.SelectCommand.Parameters.AddWithValue("@Fecha", oInstrumentoMedicionDetalleDTO.Fecha);
                        da.SelectCommand.Parameters.AddWithValue("@ProximaFecha", oInstrumentoMedicionDetalleDTO.ProximaFecha);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oInstrumentoMedicionDetalleDTO.Observaciones);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar().ToString());
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

        public List<InstrumentoMedicionDetalleDTO> ObtenerInstrumentoMedicionDetalle(int IdInstrumentoMedicion, string BaseDatos, ref string mensaje_error)
        {
            List<InstrumentoMedicionDetalleDTO> lstInstrumentoMedicionDetalleDTO = new List<InstrumentoMedicionDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarInstrumentosMedicionDetalle", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicion", IdInstrumentoMedicion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        InstrumentoMedicionDetalleDTO oInstrumentoMedicionDetalleDTO = new InstrumentoMedicionDetalleDTO();

                        oInstrumentoMedicionDetalleDTO.IdInstrumentoMedicionDetalle = int.Parse(drd["IdInstrumentoMedicionDetalle"].ToString());
                        oInstrumentoMedicionDetalleDTO.IdInstrumentoMedicion = int.Parse(drd["IdInstrumentoMedicion"].ToString());
                        oInstrumentoMedicionDetalleDTO.Fecha = DateTime.Parse(drd["Fecha"].ToString());
                        oInstrumentoMedicionDetalleDTO.ProximaFecha = DateTime.Parse(drd["ProximaFecha"].ToString());
                        oInstrumentoMedicionDetalleDTO.Observaciones = (drd["Observaciones"].ToString());
                       
                        lstInstrumentoMedicionDetalleDTO.Add(oInstrumentoMedicionDetalleDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstInstrumentoMedicionDetalleDTO;
        }

        public List<InstrumentoMedicionDetalleDTO> ObtenerDatosDetallexID(int IdInstrumentoMedicionDetalle, string BaseDatos, ref string mensaje_error)
        {
            List<InstrumentoMedicionDetalleDTO> lstInstrumentoMedicionDetalleDTO = new List<InstrumentoMedicionDetalleDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarInstrumentoMedicionDetallexID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalle", IdInstrumentoMedicionDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        InstrumentoMedicionDetalleDTO oInstrumentoMedicionDetalleDTO = new InstrumentoMedicionDetalleDTO();
                        oInstrumentoMedicionDetalleDTO.IdInstrumentoMedicionDetalle = int.Parse(drd["IdInstrumentoMedicionDetalle"].ToString());
                        oInstrumentoMedicionDetalleDTO.IdInstrumentoMedicion = int.Parse(drd["IdInstrumentoMedicion"].ToString());
                        oInstrumentoMedicionDetalleDTO.Fecha = DateTime.Parse(drd["Fecha"].ToString());
                        oInstrumentoMedicionDetalleDTO.ProximaFecha = DateTime.Parse(drd["ProximaFecha"].ToString());
                        oInstrumentoMedicionDetalleDTO.Observaciones = (drd["Observaciones"].ToString());

                 
                        lstInstrumentoMedicionDetalleDTO.Add(oInstrumentoMedicionDetalleDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstInstrumentoMedicionDetalleDTO;
        }

        public int DeleteDetalle(int IdInstrumentoMedicionDetalle, int IdUsuario, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaInstrumentoMedicionDetalle", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalle", IdInstrumentoMedicionDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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


        public int UpdateInsertInstrumentoMedicionDetalleDoc(InstrumentoMedicionDetalleDocDTO oInstrumentoMedicionDetalleDocDTO, string BaseDatos, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertInstrumentoMedicionDetalleDoc", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalleDocs", oInstrumentoMedicionDetalleDocDTO.IdInstrumentoMedicionDetalleDocs);
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalle", oInstrumentoMedicionDetalleDocDTO.IdInstrumentoMedicionDetalle);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDoc", oInstrumentoMedicionDetalleDocDTO.NumeroDoc);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oInstrumentoMedicionDetalleDocDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@Observaciones", oInstrumentoMedicionDetalleDocDTO.Observaciones);
         
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar().ToString());
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

        public List<InstrumentoMedicionDetalleDocDTO> ObtenerInstrumentoMedicionDetalleDoc(int IdInstrumentoMedicionDetalle, string BaseDatos, ref string mensaje_error)
        {
            List<InstrumentoMedicionDetalleDocDTO> lstInstrumentoMedicionDetalleDocDTO = new List<InstrumentoMedicionDetalleDocDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarInstrumentosMedicionDetalleDocs", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalle", IdInstrumentoMedicionDetalle);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        InstrumentoMedicionDetalleDocDTO oInstrumentoMedicionDetalleDocDTO = new InstrumentoMedicionDetalleDocDTO();

                        oInstrumentoMedicionDetalleDocDTO.IdInstrumentoMedicionDetalleDocs = int.Parse(drd["IdInstrumentoMedicionDetalleDocs"].ToString());
                        oInstrumentoMedicionDetalleDocDTO.IdInstrumentoMedicionDetalle = int.Parse(drd["IdInstrumentoMedicionDetalle"].ToString());
                        oInstrumentoMedicionDetalleDocDTO.NumeroDoc = (drd["NumeroDoc"].ToString());
                        oInstrumentoMedicionDetalleDocDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oInstrumentoMedicionDetalleDocDTO.Observaciones = drd["Observaciones"].ToString();
                        oInstrumentoMedicionDetalleDocDTO.NombreAnexo = (drd["NombreAnexo"].ToString());
                        lstInstrumentoMedicionDetalleDocDTO.Add(oInstrumentoMedicionDetalleDocDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstInstrumentoMedicionDetalleDocDTO;
        }

        public List<InstrumentoMedicionDetalleDocDTO> ObtenerDatosDetalleDocxID(int IdInstrumentoMedicionDetalleDoc, string BaseDatos, ref string mensaje_error)
        {
            List<InstrumentoMedicionDetalleDocDTO> lstInstrumentoMedicionDetalleDocDTO = new List<InstrumentoMedicionDetalleDocDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarInstrumentoMedicionDetalleDocxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalleDoc", IdInstrumentoMedicionDetalleDoc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        InstrumentoMedicionDetalleDocDTO oInstrumentoMedicionDTO = new InstrumentoMedicionDetalleDocDTO();
                        oInstrumentoMedicionDTO.IdInstrumentoMedicionDetalleDocs = int.Parse(drd["IdInstrumentoMedicionDetalleDocs"].ToString());
                        oInstrumentoMedicionDTO.IdInstrumentoMedicionDetalle = int.Parse(drd["IdInstrumentoMedicionDetalle"].ToString());
                        oInstrumentoMedicionDTO.NumeroDoc = (drd["NumeroDoc"].ToString());
                        oInstrumentoMedicionDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        oInstrumentoMedicionDTO.Observaciones = (drd["Observaciones"].ToString());                     
                        oInstrumentoMedicionDTO.NombreAnexo = (drd["NombreAnexo"].ToString());                     
                        lstInstrumentoMedicionDetalleDocDTO.Add(oInstrumentoMedicionDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstInstrumentoMedicionDetalleDocDTO;
        }

        public List<AnexoDTO> ObtenerAnexoInstMedicionDetalleDoc(int IdInstrumentoMedicionDetalleDoc, string BaseDatos, ref string mensaje_error)
        {
            List<AnexoDTO> lstAnexoDTO = new List<AnexoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosInstrumentoMedicionDetalleDocXID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalleDoc", IdInstrumentoMedicionDetalleDoc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        AnexoDTO oAnexoDTO = new AnexoDTO();
                        oAnexoDTO.IdAnexo = Convert.ToInt32(drd["IdAnexo"].ToString());
                        oAnexoDTO.ruta = (drd["ruta"].ToString());
                        oAnexoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oAnexoDTO.Tabla = (drd["Tabla"].ToString());
                        oAnexoDTO.IdTabla = Convert.ToInt32(drd["IdTabla"].ToString());
                        oAnexoDTO.NombreArchivo = (drd["NombreArchivo"].ToString());
                        lstAnexoDTO.Add(oAnexoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstAnexoDTO;
            }


        }

        public int DeleteDetalleDoc(int IdInstrumentoMedicionDetalleDoc, int IdUsuario, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaInstrumentoMedicionDetalleDoc", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdInstrumentoMedicionDetalleDoc", IdInstrumentoMedicionDetalleDoc);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
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
