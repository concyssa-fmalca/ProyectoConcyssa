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
        public List<ObraCatalogoDTO> ListarArticulosxIdSociedadObra(int IdSociedad,int IdObra, ref string mensaje_error)
        {
            List<ObraCatalogoDTO> lstObraCatalogoDTO = new List<ObraCatalogoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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

        public List<ObraDTO> ObtenerObra(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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
                        oObraDTO.Descripcion =(drd["Descripcion"].ToString());
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
        public List<ObraDTO> ObtenerObraFiltroBase(int IdBase,int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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

        public List<ObraDTO> ObtenerObraxIdBase(int IdBase, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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


        public int UpdateInsertObra(ObraDTO oObraDTO, ref string mensaje_error,int IdUsuario)
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

        
        public int UpdateInsertObraCatalogoProducto(ObraCatalogoDTO oObraCatalogoDTO, ref string mensaje_error)
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

        public List<ObraDTO> ObtenerDatosxID(int IdObra, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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
                        oObraDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());;
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


        public int Delete(int IdObra, ref string mensaje_error)
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


        public List<ObraDTO> ObtenerObraxIdUsuario(int IdPerfil,int IdBase,int IdUsuario, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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
        public List<ObraDTO> ObtenerObraxIdUsuarioSinBase(int IdPerfil, int IdUsuario, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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
        public List<ObraDTO> ObtenerObraxIdUsuarioFiltro(int IdPerfil,int IdUsuario, ref string mensaje_error)
        {
            List<ObraDTO> lstObraDTO = new List<ObraDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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
    }

}
