using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class VehiculoDAO
    {
        public List<VehiculoDTO> ObtenerVehiculo(int IdSociedad, string BaseDatos,int IdUsuario, ref string mensaje_error, int Estado = 3)
        {
            List<VehiculoDTO> lstVehiculoDTO = new List<VehiculoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarVehiculo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        VehiculoDTO oVehiculoDTO = new VehiculoDTO();
                        oVehiculoDTO.IdVehiculo = Convert.ToInt32(drd["IdVehiculo"].ToString());
                        oVehiculoDTO.IdMarca = Convert.ToInt32(drd["IdMarca"].ToString());
                        oVehiculoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oVehiculoDTO.Condicion = (drd["Condicion"].ToString());
                        oVehiculoDTO.IdChofer = Convert.ToInt32(drd["IdChofer"].ToString());
                        oVehiculoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oVehiculoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oVehiculoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oVehiculoDTO.Placa = (drd["Placa"].ToString());
                        oVehiculoDTO.MarcaDescripcion = (drd["MarcaDescripcion"].ToString());
                        oVehiculoDTO.BaseDescripcion = (drd["BaseDescripcion"].ToString());
                        oVehiculoDTO.ChoferDescripcion = (drd["ChoferDescripcion"].ToString());
                        lstVehiculoDTO.Add(oVehiculoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstVehiculoDTO;
        }

        public List<VehiculoDTO> ObtenerVehiculoxIdUsuario(int IdSociedad, string BaseDatos, int IdUsuario, ref string mensaje_error)
        {
            List<VehiculoDTO> lstVehiculoDTO = new List<VehiculoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarVehiculoxIdUsuario", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        VehiculoDTO oVehiculoDTO = new VehiculoDTO();
                        oVehiculoDTO.IdVehiculo = Convert.ToInt32(drd["IdVehiculo"].ToString());
                        oVehiculoDTO.IdMarca = Convert.ToInt32(drd["IdMarca"].ToString());
                        oVehiculoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oVehiculoDTO.Condicion = (drd["Condicion"].ToString());
                        oVehiculoDTO.IdChofer = Convert.ToInt32(drd["IdChofer"].ToString());
                        oVehiculoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oVehiculoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oVehiculoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oVehiculoDTO.Placa = (drd["Placa"].ToString());
                        oVehiculoDTO.MarcaDescripcion = (drd["MarcaDescripcion"].ToString());
                        oVehiculoDTO.BaseDescripcion = (drd["BaseDescripcion"].ToString());
                        oVehiculoDTO.ChoferDescripcion = (drd["ChoferDescripcion"].ToString());
                        lstVehiculoDTO.Add(oVehiculoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstVehiculoDTO;
        }

        public List<VehiculoDTO> ObtenerVehiculosxIdCuadrilla(int IdCuadrilla, string BaseDatos, int IdUsuario, ref string mensaje_error)
        {
            List<VehiculoDTO> lstVehiculoDTO = new List<VehiculoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarVehiculoxIdCuadrilla", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", IdCuadrilla);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        VehiculoDTO oVehiculoDTO = new VehiculoDTO();
                        oVehiculoDTO.IdVehiculo = Convert.ToInt32(drd["IdVehiculo"].ToString());
                        oVehiculoDTO.IdMarca = Convert.ToInt32(drd["IdMarca"].ToString());
                        oVehiculoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oVehiculoDTO.Condicion = (drd["Condicion"].ToString());
                        oVehiculoDTO.IdChofer = Convert.ToInt32(drd["IdChofer"].ToString());
                        oVehiculoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oVehiculoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oVehiculoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oVehiculoDTO.Placa = (drd["Placa"].ToString());
                        lstVehiculoDTO.Add(oVehiculoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstVehiculoDTO;
        }

        public int UpdateInsertVehiculo1(VehiculoDTO oVehiculoDTO, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertVehiculo", cn);
                        da.SelectCommand.Parameters.AddWithValue("@IdVehiculo", 0);
                        da.SelectCommand.Parameters.AddWithValue("@IdMarca", 1);
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", 1);
                        da.SelectCommand.Parameters.AddWithValue("@Condicion", "ALQUILADO");
                        da.SelectCommand.Parameters.AddWithValue("@CertificadoInscripcion", "123");
                        da.SelectCommand.Parameters.AddWithValue("@IdChofer", 6);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad",1);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", 1);

                        //da.SelectCommand.Parameters.AddWithValue("@IdVehiculo", oVehiculoDTO.IdVehiculo);
                        //da.SelectCommand.Parameters.AddWithValue("@IdMarca", oVehiculoDTO.IdMarca);
                        //da.SelectCommand.Parameters.AddWithValue("@IdBase", oVehiculoDTO.IdBase);
                        //da.SelectCommand.Parameters.AddWithValue("@Condicion", oVehiculoDTO.Condicion); 
                        //da.SelectCommand.Parameters.AddWithValue("@CertificadoInscripcion", oVehiculoDTO.CertificadoInscripcion);
                        //da.SelectCommand.Parameters.AddWithValue("@IdChofer", oVehiculoDTO.IdChofer);
                        //da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oVehiculoDTO.IdSociedad);
                        //da.SelectCommand.Parameters.AddWithValue("@Estado", oVehiculoDTO.Estado);
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


        public int UpdateInsertVehiculo(VehiculoDTO oVehiculoDTO, string BaseDatos, ref string mensaje_error,int IdUsuario)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertVehiculo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdVehiculo", oVehiculoDTO.IdVehiculo);
                        da.SelectCommand.Parameters.AddWithValue("@Placa", oVehiculoDTO.Placa);
                        da.SelectCommand.Parameters.AddWithValue("@IdMarca", oVehiculoDTO.IdMarca);
                        da.SelectCommand.Parameters.AddWithValue("@IdBase", oVehiculoDTO.IdBase);
                        da.SelectCommand.Parameters.AddWithValue("@Condicion", oVehiculoDTO.Condicion);
                        da.SelectCommand.Parameters.AddWithValue("@CertificadoInscripcion", oVehiculoDTO.CertificadoInscripcion);
                        da.SelectCommand.Parameters.AddWithValue("@IdChofer", oVehiculoDTO.IdChofer);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oVehiculoDTO.IdSociedad);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oVehiculoDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@IdCuadrilla", oVehiculoDTO.IdCuadrilla);
                        da.SelectCommand.Parameters.AddWithValue("@Brevete", oVehiculoDTO.Brevete);
                        da.SelectCommand.Parameters.AddWithValue("@IdPropietario", oVehiculoDTO.IdPropietario);
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




        public List<VehiculoDTO> ObtenerDatosxID(int IdVehiculo, string BaseDatos, ref string mensaje_error)
        {
            List<VehiculoDTO> lstVehiculoDTO = new List<VehiculoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarVehiculoxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdVehiculo", IdVehiculo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        VehiculoDTO oVehiculoDTO = new VehiculoDTO();
                        oVehiculoDTO.IdVehiculo = Convert.ToInt32(drd["IdVehiculo"].ToString());
                        oVehiculoDTO.IdMarca = Convert.ToInt32(drd["IdMarca"].ToString());
                        oVehiculoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oVehiculoDTO.Placa = (drd["Placa"].ToString());
                        oVehiculoDTO.CertificadoInscripcion = (drd["CertificadoInscripcion"].ToString());
                        oVehiculoDTO.Condicion = (drd["Condicion"].ToString());
                        oVehiculoDTO.IdChofer = Convert.ToInt32(drd["IdChofer"].ToString());
                        oVehiculoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oVehiculoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oVehiculoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oVehiculoDTO.Brevete = drd["Brevete"].ToString();
                        oVehiculoDTO.IdCuadrilla = Convert.ToInt32(String.IsNullOrEmpty(drd["IdCuadrilla"].ToString()) ? "0" : drd["IdCuadrilla"].ToString());
                        oVehiculoDTO.IdPropietario = Convert.ToInt32(String.IsNullOrEmpty(drd["IdPropietario"].ToString()) ? "0" : drd["IdPropietario"].ToString());
                        lstVehiculoDTO.Add(oVehiculoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstVehiculoDTO;
        }


        public int Delete(int IdVehiculo, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminaVehiculo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdVehiculo", IdVehiculo);
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




        public List<VehiculoDTO> ObtenerDatosConductorxPlaca(string Placa,string BaseDatos)
        {
            List<VehiculoDTO> lstVehiculoDTO = new List<VehiculoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDatosConductorxPlaca", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Placa", Placa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        VehiculoDTO oVehiculoDTO = new VehiculoDTO();
                        oVehiculoDTO.MarcaDescripcion = (drd["Marca"].ToString());
                        oVehiculoDTO.Placa = (drd["Placa"].ToString());
                        oVehiculoDTO.Licencia = (drd["Licencia"].ToString());
                        oVehiculoDTO.NumeroDocumento = (drd["NumeroDocumento"].ToString());
                        oVehiculoDTO.RazonSocial = (drd["RazonSocial"].ToString());
                        lstVehiculoDTO.Add(oVehiculoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    
                }
            }
            return lstVehiculoDTO;
        }

        public List<VehiculoDTO> ObtenerVehiculoTransferencia(int IdObraOrigen, int IdObraDestino, string BaseDatos, ref string mensaje_error)
        {
            List<VehiculoDTO> lstVehiculoDTO = new List<VehiculoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarVehiculoTransferencia", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdObraOrigen", IdObraOrigen);
                    da.SelectCommand.Parameters.AddWithValue("@IdObraDestino", IdObraDestino);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        VehiculoDTO oVehiculoDTO = new VehiculoDTO();
                        oVehiculoDTO.IdVehiculo = Convert.ToInt32(drd["IdVehiculo"].ToString());
                        oVehiculoDTO.IdMarca = Convert.ToInt32(drd["IdMarca"].ToString());
                        oVehiculoDTO.IdBase = Convert.ToInt32(drd["IdBase"].ToString());
                        oVehiculoDTO.Condicion = (drd["Condicion"].ToString());
                        oVehiculoDTO.IdChofer = Convert.ToInt32(drd["IdChofer"].ToString());
                        oVehiculoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oVehiculoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oVehiculoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        oVehiculoDTO.Placa = (drd["Placa"].ToString());
                        oVehiculoDTO.MarcaDescripcion = (drd["MarcaDescripcion"].ToString());
                        oVehiculoDTO.BaseDescripcion = (drd["BaseDescripcion"].ToString());
                        oVehiculoDTO.ChoferDescripcion = (drd["ChoferDescripcion"].ToString());
                        lstVehiculoDTO.Add(oVehiculoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstVehiculoDTO;
        }

    }
}
