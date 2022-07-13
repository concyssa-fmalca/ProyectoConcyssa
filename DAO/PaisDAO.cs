using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class PaisDAO
    {
        public List<PaisDTO> ObtenerPaises()
        {
            List<PaisDTO> lstPaisDTO = new List<PaisDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarPaises", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        PaisDTO oPaisDTO = new PaisDTO();
                        oPaisDTO.IdPais = int.Parse(drd["idPais"].ToString());
                        oPaisDTO.CodigoSunat = drd["CodigoSunat"].ToString();
                        oPaisDTO.Descripcion = drd["Descripcion"].ToString();

                        lstPaisDTO.Add(oPaisDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstPaisDTO;
        }
    }
}
