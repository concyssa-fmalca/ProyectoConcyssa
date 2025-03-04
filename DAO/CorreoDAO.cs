using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CorreoDAO
    {
        public CorreoDTO ObtenerDatosCorreo(string Nombre, string BaseDatos, ref string mensaje_error)
        {
            CorreoDTO oCorreoDTO = new CorreoDTO();
            oCorreoDTO.Servidor = "smtp.gmail.com";
            oCorreoDTO.Puerto = 587;
            oCorreoDTO.Email = "concyssa.smc@gmail.com";
            oCorreoDTO.Clave = "tlbvngkvjcetzunr";
            oCorreoDTO.SSL = true;

            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDatosCorreo", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Nombre", Nombre);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {                    
                        oCorreoDTO.Servidor = drd["Servidor"].ToString();
                        oCorreoDTO.Puerto = int.Parse(drd["Puerto"].ToString());
                        oCorreoDTO.Email = (drd["Email"].ToString());
                        oCorreoDTO.Clave = (drd["Clave"].ToString());
                        oCorreoDTO.SSL = bool.Parse(drd["SSL"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                    oCorreoDTO.Servidor = "smtp.gmail.com";
                    oCorreoDTO.Puerto = 587;
                    oCorreoDTO.Email = "concyssa.smc@gmail.com";
                    oCorreoDTO.Clave = "tlbvngkvjcetzunr";
                    oCorreoDTO.SSL = true;
                }
            }
            return oCorreoDTO;
        }
    }
}
