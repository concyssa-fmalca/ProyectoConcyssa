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
    public class SociedadDAO
    {
        public List<SociedadDTO> ObtenerSociedades(ref string mensajeError)
        {
            List<SociedadDTO> lstSociedadDTO = new List<SociedadDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarSociedades", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SociedadDTO oSociedadDTO = new SociedadDTO();
                        oSociedadDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oSociedadDTO.NombreSociedad = drd["NombreSociedad"].ToString();
                        oSociedadDTO.Descripcion = drd["Descripcion"].ToString();
                        oSociedadDTO.NumeroDocumento = drd["NumeroDocumento"].ToString();
                        oSociedadDTO.Estado = bool.Parse(drd["Estado"].ToString());
                        lstSociedadDTO.Add(oSociedadDTO);
                    }
                    drd.Close();
                }
                catch (Exception ex)
                {
                    mensajeError=ex.Message.ToString();
                }
            }
            return lstSociedadDTO;
        }
    }
}
