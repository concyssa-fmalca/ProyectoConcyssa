using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DAO
{
    public class EmpleadoDAO
    {
        public List<EmpleadoDTO> ObtenerEmpleados(string IdSociedad)
        {
            List<EmpleadoDTO> lstEmpleadoDTO = new List<EmpleadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEmpleados", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EmpleadoDTO oEmpleadoDTO = new EmpleadoDTO();
                        oEmpleadoDTO.IdEmpleado = int.Parse(drd["Id"].ToString());
                        oEmpleadoDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oEmpleadoDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oEmpleadoDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oEmpleadoDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oEmpleadoDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oEmpleadoDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oEmpleadoDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oEmpleadoDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oEmpleadoDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oEmpleadoDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oEmpleadoDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oEmpleadoDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oEmpleadoDTO.Telefono = drd["Telefono"].ToString();
                        oEmpleadoDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oEmpleadoDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oEmpleadoDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oEmpleadoDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oEmpleadoDTO.Email = drd["Email"].ToString();
                        oEmpleadoDTO.Web = drd["Web"].ToString();
                        oEmpleadoDTO.Fax = drd["Fax"].ToString();
                        oEmpleadoDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oEmpleadoDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oEmpleadoDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oEmpleadoDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oEmpleadoDTO.Observacion = drd["Observacion"].ToString();
                        oEmpleadoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstEmpleadoDTO.Add(oEmpleadoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstEmpleadoDTO;
        }

        public int UpdateInsertEmpleado(EmpleadoDTO EmpleadoDTO, string IdSociedad)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertEmpleados", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", EmpleadoDTO.IdEmpleado);
                        da.SelectCommand.Parameters.AddWithValue("@CodigoCliente", EmpleadoDTO.CodigoCliente);
                        da.SelectCommand.Parameters.AddWithValue("@TipoPersona", EmpleadoDTO.TipoPersona);
                        da.SelectCommand.Parameters.AddWithValue("@TipoDocumento", EmpleadoDTO.TipoDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@NumeroDocumento", EmpleadoDTO.NumeroDocumento);
                        da.SelectCommand.Parameters.AddWithValue("@RazonSocial", EmpleadoDTO.RazonSocial);
                        da.SelectCommand.Parameters.AddWithValue("@EstadoContribuyente", EmpleadoDTO.EstadoContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionContribuyente", EmpleadoDTO.CondicionContribuyente);
                        da.SelectCommand.Parameters.AddWithValue("@DireccionFiscal", EmpleadoDTO.DireccionFiscal);
                        da.SelectCommand.Parameters.AddWithValue("@Departamento", EmpleadoDTO.Departamento);
                        da.SelectCommand.Parameters.AddWithValue("@Provincia", EmpleadoDTO.Provincia);
                        da.SelectCommand.Parameters.AddWithValue("@Distrito", EmpleadoDTO.Distrito);
                        da.SelectCommand.Parameters.AddWithValue("@Pais", EmpleadoDTO.Pais);
                        da.SelectCommand.Parameters.AddWithValue("@Telefono", EmpleadoDTO.Telefono);
                        da.SelectCommand.Parameters.AddWithValue("@ComprobantesElectronicos", EmpleadoDTO.ComprobantesElectronicos);
                        da.SelectCommand.Parameters.AddWithValue("@AfiliadoPLE", EmpleadoDTO.AfiliadoPLE);
                        da.SelectCommand.Parameters.AddWithValue("@CondicionPago", EmpleadoDTO.CondicionPago);
                        da.SelectCommand.Parameters.AddWithValue("@LineaCredito", EmpleadoDTO.LineaCredito);
                        da.SelectCommand.Parameters.AddWithValue("@Email", EmpleadoDTO.Email);
                        da.SelectCommand.Parameters.AddWithValue("@Web", EmpleadoDTO.Web);
                        da.SelectCommand.Parameters.AddWithValue("@Fax", EmpleadoDTO.Fax);
                        da.SelectCommand.Parameters.AddWithValue("@NombreContacto", EmpleadoDTO.NombreContacto);
                        da.SelectCommand.Parameters.AddWithValue("@TelefonoContacto", EmpleadoDTO.TelefonoContacto);
                        da.SelectCommand.Parameters.AddWithValue("@EmailContacto", EmpleadoDTO.EmailContacto);
                        da.SelectCommand.Parameters.AddWithValue("@FechaIngreso", EmpleadoDTO.FechaIngreso);
                        da.SelectCommand.Parameters.AddWithValue("@Observacion", EmpleadoDTO.Observacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", EmpleadoDTO.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@Tipo", EmpleadoDTO.Tipo);
                        da.SelectCommand.Parameters.AddWithValue("@IdSociedad", int.Parse(IdSociedad));
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        transactionScope.Complete();
                        return rpta;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
            }
        }


        public List<EmpleadoDTO> ObtenerDatosxID(int IdEmpleado)
        {
            List<EmpleadoDTO> lstEmpleadoDTO = new List<EmpleadoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarEmpleadosxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        EmpleadoDTO oEmpleadoDTO = new EmpleadoDTO();
                        oEmpleadoDTO.IdEmpleado = int.Parse(drd["Id"].ToString());
                        oEmpleadoDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        oEmpleadoDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        oEmpleadoDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        oEmpleadoDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oEmpleadoDTO.RazonSocial = drd["RazonSocial"].ToString();
                        oEmpleadoDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        oEmpleadoDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        oEmpleadoDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        oEmpleadoDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        oEmpleadoDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        oEmpleadoDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        oEmpleadoDTO.Pais = int.Parse(drd["Pais"].ToString());
                        oEmpleadoDTO.Telefono = drd["Telefono"].ToString();
                        oEmpleadoDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        oEmpleadoDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        oEmpleadoDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        oEmpleadoDTO.LineaCredito = drd["LineaCredito"].ToString();
                        oEmpleadoDTO.Email = drd["Email"].ToString();
                        oEmpleadoDTO.Web = drd["Web"].ToString();
                        oEmpleadoDTO.Fax = drd["Fax"].ToString();
                        oEmpleadoDTO.NombreContacto = drd["NombreContacto"].ToString();
                        oEmpleadoDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        oEmpleadoDTO.EmailContacto = drd["EmailContacto"].ToString();
                        oEmpleadoDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        oEmpleadoDTO.Observacion = drd["Observacion"].ToString();
                        oEmpleadoDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstEmpleadoDTO.Add(oEmpleadoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstEmpleadoDTO;
        }


        public int Delete(int IdEmpleado)
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
                        SqlDataAdapter da = new SqlDataAdapter("SMC_EliminarEmpleado", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
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

    }
}
