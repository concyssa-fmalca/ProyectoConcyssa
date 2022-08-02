using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class MovimientoDAO
    {
        #region ListarMovimientos
        public List<MovimientoDTO> ObtenerClientes(string IdSociedad,ref string mensaje_error)
        {
            List<MovimientoDTO> lstMovimientoDTO = new List<MovimientoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
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
                        MovimientoDTO oMovimientoDTO = new MovimientoDTO();
                        //oClienteDTO.IdCliente = int.Parse(drd["Id"].ToString());
                        //oClienteDTO.CodigoCliente = drd["CodigoCliente"].ToString();
                        //oClienteDTO.TipoPersona = int.Parse(drd["TipoPersona"].ToString());
                        //oClienteDTO.TipoDocumento = int.Parse(drd["TipoDocumento"].ToString());
                        //oClienteDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        //oClienteDTO.RazonSocial = drd["RazonSocial"].ToString();
                        //oClienteDTO.EstadoContribuyente = drd["EstadoContribuyente"].ToString();
                        //oClienteDTO.CondicionContribuyente = drd["CondicionContribuyente"].ToString();
                        //oClienteDTO.DireccionFiscal = drd["DireccionFiscal"].ToString();
                        //oClienteDTO.Departamento = int.Parse(drd["Departamento"].ToString());
                        //oClienteDTO.Provincia = int.Parse(drd["Provincia"].ToString());
                        //oClienteDTO.Distrito = int.Parse(drd["Distrito"].ToString());
                        //oClienteDTO.Pais = int.Parse(drd["Pais"].ToString());
                        //oClienteDTO.Telefono = drd["Telefono"].ToString();
                        //oClienteDTO.ComprobantesElectronicos = drd["ComprobantesElectronicos"].ToString();
                        //oClienteDTO.AfiliadoPLE = drd["AfiliadoPLE"].ToString();
                        //oClienteDTO.CondicionPago = int.Parse(drd["CondicionPago"].ToString());
                        //oClienteDTO.LineaCredito = drd["LineaCredito"].ToString();
                        //oClienteDTO.Email = drd["Email"].ToString();
                        //oClienteDTO.Web = drd["Web"].ToString();
                        //oClienteDTO.Fax = drd["Fax"].ToString();
                        //oClienteDTO.NombreContacto = drd["NombreContacto"].ToString();
                        //oClienteDTO.TelefonoContacto = drd["TelefonoContacto"].ToString();
                        //oClienteDTO.EmailContacto = drd["EmailContacto"].ToString();
                        //oClienteDTO.FechaIngreso = Convert.ToDateTime(drd["FechaIngreso"].ToString());
                        //oClienteDTO.Observacion = drd["Observacion"].ToString();
                        //oClienteDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstMovimientoDTO.Add(oMovimientoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstMovimientoDTO;
        }
        #endregion


    }
}
