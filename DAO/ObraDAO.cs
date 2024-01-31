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
    public class ObraDAO
    {
        public List<ObraCatalogoDTO> ListarArticulosxIdSociedadObra(int IdSociedad, int IdObra, string BaseDatos, ref string mensaje_error)
        {
            List<ObraCatalogoDTO> lstObraCatalogoDTO = new List<ObraCatalogoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxIdSociedadObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraCatalogoDTO oObraCatalogoDTO = new ObraCatalogoDTO();
                        oObraCatalogoDTO.IdObraCatalogo = int.Parse(drd["IdObraCatalogo"].ToString());
                        oObraCatalogoDTO.Codigo = (drd["Codigo"].ToString());

                        oObraCatalogoDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oObraCatalogoDTO.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        oObraCatalogoDTO.IdTipoProducto = int.Parse(drd["IdTipoProducto"].ToString());
                        oObraCatalogoDTO.IdObra = int.Parse(drd["IdObra"].ToString());

                        lstObraCatalogoDTO.Add(oObraCatalogoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraCatalogoDTO;
        }

        public List<ObraCatalogoServicioDTO> ListarServiciosxIdSociedadObra(int IdSociedad, int IdObra, string BaseDatos, ref string mensaje_error)
        {
            List<ObraCatalogoServicioDTO> lstObraCatalogoDTO = new List<ObraCatalogoServicioDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarServiciosxIdSociedadObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraCatalogoServicioDTO oObraCatalogoDTO = new ObraCatalogoServicioDTO();
                        oObraCatalogoDTO.IdObraCatalogoServicios = int.Parse(drd["IdObraCatalogoServicios"].ToString());
                        oObraCatalogoDTO.Codigo = (drd["Codigo"].ToString());

                        oObraCatalogoDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oObraCatalogoDTO.DescripcionArticulo = (drd["DescripcionArticulo"].ToString());
                        oObraCatalogoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraCatalogoDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraCatalogoDTO.CuentaContable = (drd["CuentaContable"].ToString());

                        lstObraCatalogoDTO.Add(oObraCatalogoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraCatalogoDTO;
        }

        public List<ObraDTO> ObtenerObra(int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObra", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());
                        oObraDTO.DescripcionBase = (drd["DescripcionBase"].ToString());
                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }
        public List<ObraDTO> ObtenerObraFiltroBase(int IdBase, int IdSociedad, string BaseDatos, ref string mensaje_error, int Estado = 3)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObraFiltroBase", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());
                        oObraDTO.DescripcionBase = (drd["DescripcionBase"].ToString());
                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }

        public List<ObraDTO> ObtenerObraxIdBase(int IdBase, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObraxIdBase", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());

                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }


        public int UpdateInsertObra(ObraDTO oObraDTO, string BaseDatos, ref string mensaje_error, int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertObra", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oObraDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", oObraDTO.IdBase);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoObra", oObraDTO.IdTipoObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdDivision", oObraDTO.IdDivision);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oObraDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oObraDTO.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@Descripcion", oObraDTO.Descripcion);
                        da.SelectCommand.Parameters.AddWithValue("@DescripcionCorta", oObraDTO.DescripcionCorta);
                        da.SelectCommand.Parameters.AddWithValue("@ContratoMantenimiento", oObraDTO.ContratoMantenimiento);
                        da.SelectCommand.Parameters.AddWithValue("@VisibleInternet", oObraDTO.VisibleInternet);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oObraDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Direccion", oObraDTO.Direccion);
                        da.SelectCommand.Parameters.AddWithValue("@Eliminado", oObraDTO.Eliminado);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioActualizacion", IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoUbigeo", oObraDTO.CodigoUbigeo);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoAnexo", oObraDTO.CodigoAnexo);
                        da.SelectCommand.Parameters.AddWithValue("@CorreoObra", oObraDTO.CorreoObra);
                        da.SelectCommand.Parameters.AddWithValue("@CodProyecto", oObraDTO.CodProyecto);
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


        public int UpdateInsertObraCatalogoProducto(ObraCatalogoDTO oObraCatalogoDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCatalogoProducto", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdObraCatalogo", oObraCatalogoDTO.IdObraCatalogo);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oObraCatalogoDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oObraCatalogoDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@IdTipoProducto", oObraCatalogoDTO.IdTipoProducto);
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

        public int UpdateInsertObraCatalogoServicio(ObraCatalogoServicioDTO oObraCatalogoServicioDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertCatalogoServicio", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdObraCatalogoServicios", oObraCatalogoServicioDTO.IdObraCatalogoServicios);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oObraCatalogoServicioDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oObraCatalogoServicioDTO.IdArticulo);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oObraCatalogoServicioDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@CuentaContable", oObraCatalogoServicioDTO.CuentaContable);
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

        public List<ObraDTO> ObtenerDatosxID(int IdObra, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObraxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString()); ;
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());
                        oObraDTO.Direccion = (drd["Direccion"].ToString());
                        oObraDTO.CodProyecto = (drd["CodProyecto"].ToString());
                        oObraDTO.CodigoUbigeo = (String.IsNullOrEmpty(drd["CodigoUbigeo"].ToString())) ? "0" : drd["CodigoUbigeo"].ToString();
                        oObraDTO.CodigoAnexo = (String.IsNullOrEmpty(drd["CodigoAnexo"].ToString())) ? "0" : drd["CodigoAnexo"].ToString();
                        oObraDTO.CorreoObra = (drd["correo"].ToString());
                        //oObraDTO.DescripcionBase = (drd["DescripcionBase"].ToString());


                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }


        public int Delete(int IdObra, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaObra", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
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


        public List<ObraDTO> ObtenerObraxIdUsuario(int IdPerfil, int IdBase, int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObrasxIdUsuariov2", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdBase", IdBase);
                    da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());

                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }
        public List<ObraDTO> ObtenerObraxIdUsuarioSinBase(int IdPerfil, int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObrasxIdUsuarioSinBase", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());
                        oObraDTO.DescripcionBase = (drd["DescripcionBase"].ToString());

                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }
        public List<ObraDTO> ObtenerObraxIdUsuarioFiltro(int IdPerfil, int IdUsuario, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarObrasxIdUsuarioFiltro", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                        oObraDTO.IdTipoObra = int.Parse(drd["IdTipoObra"].ToString());
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        oObraDTO.DescripcionCorta = (drd["DescripcionCorta"].ToString());
                        oObraDTO.ContratoMantenimiento = bool.Parse(drd["ContratoMantenimiento"].ToString());
                        oObraDTO.VisibleInternet = bool.Parse(drd["VisibleInternet"].ToString());
                        oObraDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraDTO.Eliminado = bool.Parse(drd["Eliminado"].ToString());

                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }

        public ObraCatalogoServicioDTO ObtenerDatosCatalogoServicioxId(int IdArticulo, int IdObra, string BaseDatos, ref string mensaje_error)
        {
            ObraCatalogoServicioDTO oObraCatalogoServicioDTO = new ObraCatalogoServicioDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDatosCatalogoServicioxId", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@IdObra", IdObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {

                        oObraCatalogoServicioDTO.IdObraCatalogoServicios = int.Parse(drd["IdObraCatalogoServicios"].ToString());
                        oObraCatalogoServicioDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraCatalogoServicioDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oObraCatalogoServicioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oObraCatalogoServicioDTO.CuentaContable = (drd["CuentaContable"].ToString());

                        drd.Close();
                    }

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oObraCatalogoServicioDTO;
        }

        public ObraDTO ObtenerObraxIdAlmacen(int IdAlmacen, string BaseDatos, ref string mensaje_error)
        {       
                ObraDTO oObraDTO = new ObraDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {


                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerObraxIdAlmacenProc", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.IdBase = int.Parse(drd["IdBase"].ToString());
                      
                        oObraDTO.IdDivision = int.Parse(drd["IdDivision"].ToString());
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.CodProyecto = (drd["CodProyecto"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                       

                        
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return oObraDTO;
        }



        public List<ObraDTO> ObtenerObraxDivision(int IdDivision, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerObraxDivision", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Division", IdDivision);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        lstObraDTO.Add(oObraDTO);

                    }
                    drd.Close();

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }

        public List<ObraDTO> ObtenerObraxCodigo(string CodigoObra, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerObraxCodigo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Codigo", CodigoObra);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();
                        oObraDTO.IdObra = int.Parse(drd["IdObra"].ToString());
                        oObraDTO.Codigo = (drd["Codigo"].ToString());
                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                        lstObraDTO.Add(oObraDTO);

                    }
                    drd.Close();

                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstObraDTO;
        }

        public List<ObraDTO> ObtenerObrasTransfPendientes(DateTime FechaEvaluacion, string BaseDatos, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerTransferenciasPendientes", cn);
                    da.SelectCommand.Parameters.AddWithValue("@FechaEvaluacion", FechaEvaluacion);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ObraDTO oObraDTO = new ObraDTO();

                        oObraDTO.Descripcion = (drd["Descripcion"].ToString());
                    
                        oObraDTO.Cantidad = int.Parse(drd["Cantidad"].ToString());
                        lstObraDTO.Add(oObraDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    lstObraDTO = new List<ObraDTO>();
                }
            }
            return lstObraDTO;
        }


    }

}
