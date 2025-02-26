using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Transactions;

namespace DAO
{
    public class ClienteDAO
    {
        public List<ClienteDTO> ObtenerClientes(string IdSociedad, string BaseDatos)
        {
            List<ClienteDTO> lstClienteDTO = new List<ClienteDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarClientes", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ClienteDTO oClienteDTO = new ClienteDTO();
                        oClienteDTO.IdCliente = int.Parse(drd["Id"].ToString());
                        oClienteDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oClienteDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oClienteDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oClienteDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oClienteDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oClienteDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oClienteDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oClienteDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oClienteDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oClienteDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oClienteDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oClienteDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oClienteDTO.Telefono = drd["Telefono"].ToString();
                        oClienteDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oClienteDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oClienteDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oClienteDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oClienteDTO.Email = drd["Email"].ToString();
                        oClienteDTO.Web = drd["Web"].ToString();
                        oClienteDTO.Fax = drd["Fax"].ToString();
                        oClienteDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oClienteDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oClienteDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oClienteDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oClienteDTO.Observacion = drd["Observacion"].ToString();
                        oClienteDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstClienteDTO.Add(oClienteDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstClienteDTO;
        }

        public int UpdateInsertCliente(ClienteDTO clienteDTO, string IdSociedad, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertClientes", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", clienteDTO.IdCliente);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoCliente", clienteDTO.CodigoCliente);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPersona", clienteDTO.TipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@TipoDocumento", clienteDTO.TipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDocumento", clienteDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", clienteDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoContribuyente", clienteDTO.EstadoContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionContribuyente", clienteDTO.CondicionContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@DireccionFiscal", clienteDTO.DireccionFiscal);
                        da.SelectCommand.Parameters.AddWithValue("@Departamento", clienteDTO.Departamento);
                        da.SelectCommand.Parameters.AddWithValue("@Provincia", clienteDTO.Provincia);
                        da.SelectCommand.Parameters.AddWithValue("@Distrito", clienteDTO.Distrito);
                        da.SelectCommand.Parameters.AddWithValue("@Pais", clienteDTO.Pais);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", clienteDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@ComprobantesElectronicos", clienteDTO.ComprobantesElectronicos);
                        da.SelectCommand.Parameters.AddWithValue("@AfiliadoPLE", clienteDTO.AfiliadoPLE);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionPago", clienteDTO.CondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@LineaCredito", clienteDTO.LineaCredito);
                        da.SelectCommand.Parameters.AddWithValue("@Email", clienteDTO.Email);
                        da.SelectCommand.Parameters.AddWithValue("@Web", clienteDTO.Web);
                        da.SelectCommand.Parameters.AddWithValue("@Fax", clienteDTO.Fax);
                        da.SelectCommand.Parameters.AddWithValue("@NombreContacto", clienteDTO.NombreContacto);
                        da.SelectCommand.Parameters.AddWithValue("@TelefonoContacto", clienteDTO.TelefonoContacto);
                        da.SelectCommand.Parameters.AddWithValue("@EmailContacto", clienteDTO.EmailContacto);
                        da.SelectCommand.Parameters.AddWithValue("@FechaIngreso", clienteDTO.FechaIngreso);
                        da.SelectCommand.Parameters.AddWithValue("@Observacion", clienteDTO.Observacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", clienteDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", clienteDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
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


        public List<ClienteDTO> ObtenerDatosxID(int IdCliente, string BaseDatos)
        {
            List<ClienteDTO> lstClienteDTO = new List<ClienteDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarClientesxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ClienteDTO oClienteDTO = new ClienteDTO();
                        oClienteDTO.IdCliente = int.Parse(drd["Id"].ToString());
                        oClienteDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oClienteDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oClienteDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oClienteDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oClienteDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oClienteDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oClienteDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oClienteDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oClienteDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oClienteDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oClienteDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oClienteDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oClienteDTO.Telefono = drd["Telefono"].ToString();
                        oClienteDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oClienteDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oClienteDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oClienteDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oClienteDTO.Email = drd["Email"].ToString();
                        oClienteDTO.Web = drd["Web"].ToString();
                        oClienteDTO.Fax = drd["Fax"].ToString();
                        oClienteDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oClienteDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oClienteDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oClienteDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oClienteDTO.Observacion = drd["Observacion"].ToString();
                        oClienteDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstClienteDTO.Add(oClienteDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstClienteDTO;
        }


        public int Delete(int IdCliente, string BaseDatos)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarCliente", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdCliente", IdCliente);
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


        public string ConsultarDocumento(int tipo, string Documento)
        {
            string responseBody = "";
            var url = "";
            if (tipo == 1)
            {
                url = $"https://e-factura.tuscomprobantes.pe/wsconsulta/dni/" + Documento;
            }
            else
            {
                url = $"https://e-factura.tuscomprobantes.pe/wsconsulta/ruc/" + Documento;
            }
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) { }
                        else
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                responseBody = objReader.ReadToEnd();
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                return "Error";
            }
            return responseBody;
        }

        public string ConsultarDNI(string Documento)
        {
            string responseBody = "";
            var url = "";
                url = $"https://apiconsulta.smartcodeserver.pe/api/ConsultaSmartcode/ConsultaRucDni?ruc=" + Documento;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) { }
                        else
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                responseBody = objReader.ReadToEnd();
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                return "Error";
            }
            return responseBody;
        }
    }
}
