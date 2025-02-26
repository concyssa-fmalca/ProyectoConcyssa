
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class ProveedorDAO
    {
        public List<ProveedorDTO> ObtenerProveedores(string IdSociedad,bool Logistica, bool AgregarConcyssa, string BaseDatos)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedores", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@Logistica", Logistica);
                    da.SelectCommand.Parameters.AddWithValue("@AgregarConcyssa", AgregarConcyssa);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oProveedorDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oProveedorDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oProveedorDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oProveedorDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oProveedorDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oProveedorDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oProveedorDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oProveedorDTO.Email = drd["Email"].ToString();
                        oProveedorDTO.Web = drd["Web"].ToString();
                        oProveedorDTO.Fax = drd["Fax"].ToString();
                        oProveedorDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oProveedorDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oProveedorDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oProveedorDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oProveedorDTO.Observacion = drd["Observacion"].ToString();
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public List<ProveedorDTO> ObtenerProveedoresSelect2(string Texto,string BaseDatos)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresSelect2", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Texto", Texto);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());                   
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();                    
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public List<ProveedorDTO> ObtenerProveedoresDataTable(int IgnorarPrimeros, int CantidadFilas, string SearchTerm,string IdSociedad, bool Logistica, string BaseDatos)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresDataTable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IgnorarPrimeros", IgnorarPrimeros);
                    da.SelectCommand.Parameters.AddWithValue("@CantidadFilas", CantidadFilas);
                    da.SelectCommand.Parameters.AddWithValue("@SearchTerm", SearchTerm);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@Logistica", Logistica);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oProveedorDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oProveedorDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oProveedorDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oProveedorDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oProveedorDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oProveedorDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oProveedorDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oProveedorDTO.Email = drd["Email"].ToString();
                        oProveedorDTO.Web = drd["Web"].ToString();
                        oProveedorDTO.Fax = drd["Fax"].ToString();
                        oProveedorDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oProveedorDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oProveedorDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oProveedorDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oProveedorDTO.Observacion = drd["Observacion"].ToString();
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }

        public int ObtenerProveedoresTotal(string SearchTerm, string IdSociedad, bool Logistica, string BaseDatos)
        {
            int resultado = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresTOTAL", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.Parameters.AddWithValue("@Logistica", Logistica);
                    da.SelectCommand.Parameters.AddWithValue("@SearchTerm", SearchTerm);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        resultado = int.Parse(drd["Cantidad"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return resultado;
        }



        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO, string IdSociedad, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertProveedores", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", proveedorDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoCliente", proveedorDTO.CodigoCliente);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPersona", proveedorDTO.TipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@TipoDocumento", proveedorDTO.TipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDocumento", proveedorDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", proveedorDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoContribuyente", proveedorDTO.EstadoContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionContribuyente", proveedorDTO.CondicionContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@DireccionFiscal", proveedorDTO.DireccionFiscal);
                        da.SelectCommand.Parameters.AddWithValue("@Departamento", proveedorDTO.Departamento);
                        da.SelectCommand.Parameters.AddWithValue("@Provincia", proveedorDTO.Provincia);
                        da.SelectCommand.Parameters.AddWithValue("@Distrito", proveedorDTO.Distrito);
                        da.SelectCommand.Parameters.AddWithValue("@Pais", proveedorDTO.Pais);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", proveedorDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@ComprobantesElectronicos", proveedorDTO.ComprobantesElectronicos);
                        da.SelectCommand.Parameters.AddWithValue("@AfiliadoPLE", proveedorDTO.AfiliadoPLE);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionPago", proveedorDTO.CondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@LineaCredito", proveedorDTO.LineaCredito);
                        da.SelectCommand.Parameters.AddWithValue("@Email", proveedorDTO.Email);
                        da.SelectCommand.Parameters.AddWithValue("@Web", proveedorDTO.Web);
                        da.SelectCommand.Parameters.AddWithValue("@Fax", proveedorDTO.Fax);
                        da.SelectCommand.Parameters.AddWithValue("@NombreContacto", proveedorDTO.NombreContacto);
                        da.SelectCommand.Parameters.AddWithValue("@TelefonoContacto", proveedorDTO.TelefonoContacto);
                        da.SelectCommand.Parameters.AddWithValue("@EmailContacto", proveedorDTO.EmailContacto);
                        da.SelectCommand.Parameters.AddWithValue("@FechaIngreso", proveedorDTO.FechaIngreso);
                        da.SelectCommand.Parameters.AddWithValue("@Observacion", proveedorDTO.Observacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", proveedorDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", proveedorDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        da.SelectCommand.Parameters.AddWithValue("@DiasEntrega", proveedorDTO.DiasEntrega);
                        da.SelectCommand.Parameters.AddWithValue("@Afecto4ta", proveedorDTO.Afecto4ta);
                        da.SelectCommand.Parameters.AddWithValue("@ConOrden", proveedorDTO.ConOrden);
                        da.SelectCommand.Parameters.AddWithValue("@SolicitarFTenPortalProv", proveedorDTO.SolicitarFTenPortalProv);
                        da.SelectCommand.Parameters.AddWithValue("@EmailPortalProv", proveedorDTO.EmailPortalProv);
                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                        transactionScope.Complete();

                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }


        public ProveedorDTO ObtenerDatosxIDNuevo(int IdProveedor, string BaseDatos)
        {
            string mensaje_error = "";
            ProveedorDTO oProveedorDTO = new ProveedorDTO();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oProveedorDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oProveedorDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oProveedorDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oProveedorDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oProveedorDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oProveedorDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oProveedorDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oProveedorDTO.Email = drd["Email"].ToString();
                        oProveedorDTO.Web = drd["Web"].ToString();
                        oProveedorDTO.Fax = drd["Fax"].ToString();
                        oProveedorDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oProveedorDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oProveedorDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oProveedorDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oProveedorDTO.Observacion = drd["Observacion"].ToString();
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oProveedorDTO.DiasEntrega = int.Parse(drd["DiasEntrega"].ToString());
                        oProveedorDTO.Afecto4ta = bool.Parse(drd["Afecto4ta"].ToString());
                        oProveedorDTO.ConOrden = bool.Parse(drd["ConOrden"].ToString());
                        oProveedorDTO.SolicitarFTenPortalProv = bool.Parse(drd["SolicitarFTenPortalProv"].ToString());
                        oProveedorDTO.EmailPortalProv = (drd["EmailPortalProv"].ToString());
                        
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            #region AnexoDetalle
            Int32 filasdetalleAnexo = 0;
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosPedidoxIdProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr1 = da.SelectCommand.ExecuteReader();
                    while (dr1.Read())
                    {
                        filasdetalleAnexo++;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error += ex.Message.ToString();
                }
            }
            oProveedorDTO.AnexoDetalle = new AnexoDTO[filasdetalleAnexo];
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerAnexosPedidoxIdProveedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdPedido", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr2 = da.SelectCommand.ExecuteReader();
                    Int32 posicion = 0;
                    while (dr2.Read())
                    {
                        AnexoDTO oAnexoDTO = new AnexoDTO();
                        oAnexoDTO.IdAnexo = Convert.ToInt32(dr2["IdAnexo"].ToString());
                        oAnexoDTO.ruta = (dr2["ruta"].ToString());
                        oAnexoDTO.IdSociedad = Convert.ToInt32(dr2["IdSociedad"].ToString());
                        oAnexoDTO.Tabla = (dr2["Tabla"].ToString());
                        oAnexoDTO.IdTabla = Convert.ToInt32(dr2["IdTabla"].ToString());
                        oAnexoDTO.NombreArchivo = (dr2["NombreArchivo"].ToString());
                        oProveedorDTO.AnexoDetalle[posicion] = oAnexoDTO;
                        posicion = posicion + 1;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            #endregion

            return oProveedorDTO;
        }
        public List<ProveedorDTO> ObtenerDatosxID(int IdProveedor, string BaseDatos)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProveedoresxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oProveedorDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oProveedorDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oProveedorDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oProveedorDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oProveedorDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oProveedorDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oProveedorDTO.Telefono = drd["Telefono"].ToString();
                        oProveedorDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oProveedorDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oProveedorDTO.Email = drd["Email"].ToString();
                        oProveedorDTO.Web = drd["Web"].ToString();
                        oProveedorDTO.Fax = drd["Fax"].ToString();
                        oProveedorDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oProveedorDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oProveedorDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oProveedorDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oProveedorDTO.Observacion = drd["Observacion"].ToString();
                        oProveedorDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        oProveedorDTO.DiasEntrega = int.Parse(drd["DiasEntrega"].ToString());
                        oProveedorDTO.Afecto4ta = bool.Parse(drd["Afecto4ta"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }


        public int Delete(int IdProveedor, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarProveedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
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

        public int DeleteAnexo(int IdAnexo, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarAnexo", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdAnexo", IdAnexo);
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
        public int InsertRubroProveedor_X_Provedor(RubroXProveedorDTO oRubroXProveedorDTO, int IdUsuario, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_InsertRubroProveedor_X_Provedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdRubroProveedor", oRubroXProveedorDTO.IdRubroProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", oRubroXProveedorDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", IdUsuario);       
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();

                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }
        public List<RubroXProveedorDTO>ListarRubroProveedor_X_Provedor(int IdProveedor, string BaseDatos, ref string mensaje_error)
        {
            List<RubroXProveedorDTO> lstRubroXProveedorDTO = new List<RubroXProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarRubroProveedor_X_Provedor", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        RubroXProveedorDTO RubroXProveedorDTO = new RubroXProveedorDTO();
                        RubroXProveedorDTO.Id = int.Parse(drd["Id"].ToString());
                        RubroXProveedorDTO.IdRubroProveedor = int.Parse(drd["IdRubroProveedor"].ToString());
                        RubroXProveedorDTO.IdProveedor = int.Parse(drd["IdProveedor"].ToString());
                        RubroXProveedorDTO.Descripcion = drd["Descripcion"].ToString();
                        RubroXProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        lstRubroXProveedorDTO.Add(RubroXProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstRubroXProveedorDTO;
        }
        public int EliminarRubroProveedor_X_Provedor(int Id,int IdUsuario, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarRubroProveedor_X_Provedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Id", Id);
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
        public int UpdateCondicionPagoProveedor(ProveedorDTO proveedorDTO, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateCondicionPagoProveedor", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", proveedorDTO.IdProveedor);
                        da.SelectCommand.Parameters.AddWithValue("@IdCondicionPago", proveedorDTO.CondicionPago);                      
                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
                        transactionScope.Complete();

                        return rpta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
        }


        public List<ProveedorDTO> ObtenerProveedorxNroDoc(string NroDoc, string BaseDatos)
        {
            List<ProveedorDTO> lstProveedorDTO = new List<ProveedorDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerProveedorxNumDoc", cn);
                    da.SelectCommand.Parameters.AddWithValue("@NroDoc", NroDoc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ProveedorDTO oProveedorDTO = new ProveedorDTO();
                        oProveedorDTO.IdProveedor = int.Parse(drd["Id"].ToString());
                        oProveedorDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oProveedorDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oProveedorDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oProveedorDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oProveedorDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oProveedorDTO.DiasEntrega = int.Parse(drd["DiasEntrega"].ToString());
                        lstProveedorDTO.Add(oProveedorDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstProveedorDTO;
        }


        public int CrearUsuarioPortalProv(ProveedorDTO proveedorDTO,string Password, string BaseDatos, ref string mensaje_error)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_SGC_CrearUsuarioPortal", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Usuario", proveedorDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@Password", Password);
                        da.SelectCommand.Parameters.AddWithValue("@IdProveedor", proveedorDTO.IdProveedor.ToString());
                        da.SelectCommand.Parameters.AddWithValue("@DescProveedor", proveedorDTO.NumeroDocumento+"-"+ proveedorDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@Correo", proveedorDTO.EmailPortalProv);
                        //int rpta = da.SelectCommand.ExecuteNonQuery();
                        int rpta = Convert.ToInt32(da.SelectCommand.ExecuteNonQuery());
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

    }
}
