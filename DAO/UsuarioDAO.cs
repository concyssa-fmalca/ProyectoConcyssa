using DAO.helpers;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class UsuarioDAO
    {

        public List<UsuarioDTO> ValidarUsuario(string Usuario, string Password,int IdSociedad, ref string mensajeError)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ValidaUsuario", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Usuario", Usuario);
                    da.SelectCommand.Parameters.AddWithValue("@Password", Password);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.Nombre = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = Convert.ToInt32(drd["IdPerfil"].ToString());
                        oUsuarioDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oUsuarioDTO.SapUsuario = drd["SapUsuario"].ToString();
                        oUsuarioDTO.SapPassword = drd["SapPassword"].ToString();
                        oUsuarioDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oUsuarioDTO.MovimientoInventario = Convert.ToBoolean(drd["MovimientoInventario"].ToString());
                        oUsuarioDTO.NombBase = drd["NombBase"].ToString();



                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensajeError = ex.Message.ToString();
                }
            }
            return lstUsuarioDTO;
        }


        public List<UsuarioDTO> ObtenerUsuarios(ref string mensaje_error)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUsuarios", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.Nombre = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.NombrePerfil = drd["NombrePerfil"].ToString();
                        oUsuarioDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oUsuarioDTO.MovimientoInventario = Convert.ToBoolean(drd["MovimientoInventario"].ToString());
                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUsuarioDTO;
        }

        
        public List<UsuarioDTO> ObtenerBasesxIdUsuario(int Idusuario,ref string mensaje_error)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarBasesxIdUsuario", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Idusuario", Idusuario);
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.Nombre = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oUsuarioDTO.MovimientoInventario= Convert.ToBoolean(drd["MovimientoInventario"].ToString());
                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUsuarioDTO;
        }

        public int UpdateInsertUsuario(UsuarioDTO oUsuarioDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertUsuarios", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idUsuario", oUsuarioDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", oUsuarioDTO.Nombre);
                        da.SelectCommand.Parameters.AddWithValue("@Usuario", oUsuarioDTO.Usuario);
                        da.SelectCommand.Parameters.AddWithValue("@Contraseña", oUsuarioDTO.Password);
                        da.SelectCommand.Parameters.AddWithValue("@IdPerfil", oUsuarioDTO.IdPerfil);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oUsuarioDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@SapUsuario", oUsuarioDTO.SapUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@SapPassword", oUsuarioDTO.SapPassword);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oUsuarioDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@MovimientoInventario", oUsuarioDTO.MovimientoInventario);
                        da.SelectCommand.Parameters.AddWithValue("@Correo", oUsuarioDTO.Correo);
                        da.SelectCommand.Parameters.AddWithValue("@IdDepartamento", oUsuarioDTO.IdDepartamento);
                        da.SelectCommand.Parameters.AddWithValue("@AprobarGiro", oUsuarioDTO.AprobarGiro);
                        da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", oUsuarioDTO.IdEmpleado);

                        int rpta = da.SelectCommand.ExecuteNonQuery();
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


        public List<UsuarioDTO> ObtenerDatosxID(int IdUsuario,ref string mensaje_error)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUsuariosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.Password = drd["Password"].ToString();
                        oUsuarioDTO.Nombre = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.NombrePerfil = drd["NombrePerfil"].ToString();
                        oUsuarioDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oUsuarioDTO.MovimientoInventario = bool.Parse(drd["MovimientoInventario"].ToString());

                        oUsuarioDTO.SapUsuario = drd["SapUsuario"].ToString();
                        oUsuarioDTO.SapPassword = drd["SapPassword"].ToString();
                        oUsuarioDTO.Correo = drd["Correo"].ToString();
                        oUsuarioDTO.AprobarGiro = HelperDao.conversionInt(drd,"AprobarGiro");
                        oUsuarioDTO.IdDepartamento =Convert.ToInt32((String.IsNullOrEmpty(drd["IdDepartamento"].ToString())) ? "0" : drd["IdDepartamento"].ToString());

                        oUsuarioDTO.IdEmpleado = Convert.ToInt32((String.IsNullOrEmpty(drd["IdEmpleado"].ToString())) ? "0" : drd["IdEmpleado"].ToString());

                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUsuarioDTO;
        }

        public int Delete(int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarUsuarios", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }
        
        public int DeleteUsuarioBase(int IdUsuarioBase)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarUsuarioBase", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuarioBase", IdUsuarioBase);
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }


        public List<UsuarioDTO> ObtenerUsuariosAutorizadores(string IdSociedad)
        {
            List<UsuarioDTO> lstUsuarioDTO = new List<UsuarioDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarUsuariosAutorizadores", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioDTO oUsuarioDTO = new UsuarioDTO();
                        oUsuarioDTO.IdUsuario = int.Parse(drd["IdUsuario"].ToString());
                        oUsuarioDTO.Usuario = drd["Usuario"].ToString();
                        oUsuarioDTO.NombreUsuario = drd["Nombre"].ToString();
                        oUsuarioDTO.IdPerfil = int.Parse(drd["IdPerfil"].ToString());
                        oUsuarioDTO.NombrePerfil = drd["NombrePerfil"].ToString();
                        oUsuarioDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oUsuarioDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oUsuarioDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oUsuarioDTO.IdDepartamento = Convert.ToInt32((String.IsNullOrEmpty(drd["IdDepartamento"].ToString())) ? "0" : drd["IdDepartamento"].ToString());
                        oUsuarioDTO.Correo = drd["Correo"].ToString();
                        lstUsuarioDTO.Add(oUsuarioDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUsuarioDTO;
        }

        public List<UsuarioBaseAlmacenDTO> ObtenerBaseAlmacenxIdUsuario(int IdUsuario, ref string mensaje_error)
        {
            List<UsuarioBaseAlmacenDTO> lstUsuarioBaseAlmacenDTO = new List<UsuarioBaseAlmacenDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerBaseAlmacenxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UsuarioBaseAlmacenDTO oUsuarioBaseAlmacenDTO = new UsuarioBaseAlmacenDTO();
                        oUsuarioBaseAlmacenDTO.IdUsuarioBase = Convert.ToInt32(drd["IdUsuarioBase"].ToString());
                        oUsuarioBaseAlmacenDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oUsuarioBaseAlmacenDTO.IdUsuario = Convert.ToInt32(drd["IdUsuario"].ToString());
                        oUsuarioBaseAlmacenDTO.IdObra = Convert.ToInt32(drd["IdObra"].ToString());
                        oUsuarioBaseAlmacenDTO.IdAlmacen = Convert.ToInt32(String.IsNullOrEmpty(drd["IdAlmacen"].ToString()) ? "0" : drd["IdAlmacen"].ToString() );
                        oUsuarioBaseAlmacenDTO.CountObra = Convert.ToInt32(drd["CountObra"].ToString());
                        oUsuarioBaseAlmacenDTO.CountBase = Convert.ToInt32(drd["CountBase"].ToString());
                        oUsuarioBaseAlmacenDTO.CountAlmacen = Convert.ToInt32(drd["CountAlmacen"].ToString());
                        lstUsuarioBaseAlmacenDTO.Add(oUsuarioBaseAlmacenDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstUsuarioBaseAlmacenDTO;
        }


        public int UpdateInsertUsuarioBaseAlmacen(UsuarioBaseAlmacenDTO oUsuarioBaseAlmacenDTO, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertUsuarioBaseAlmacen", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuarioBase", oUsuarioBaseAlmacenDTO.IdUsuarioBase);
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", oUsuarioBaseAlmacenDTO.IdBase);
                        da.SelectCommand.Parameters.AddWithValue("@IdObra", oUsuarioBaseAlmacenDTO.IdObra);
                        da.SelectCommand.Parameters.AddWithValue("@IdUsuario", oUsuarioBaseAlmacenDTO.IdUsuario);
                        da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", oUsuarioBaseAlmacenDTO.IdAlmacen);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
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
