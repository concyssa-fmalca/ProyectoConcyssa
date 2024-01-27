using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class UbigeoDAO
    {
        public List<UbigeoDTO> ObtenerTodosUbigeo( string BaseDatos)
        {
            List<UbigeoDTO> lstUbigeoDTO = new List<UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTodosUbigeo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UbigeoDTO oUbigeoDTO = new UbigeoDTO();
                        oUbigeoDTO.CodUbigeo = drd["COD_UBIGEO"].ToString();
                        oUbigeoDTO.Descripcion = drd["DES_UBIGEO"].ToString();
                        ;
                        lstUbigeoDTO.Add(oUbigeoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUbigeoDTO;
        }


        public List<UbigeoDTO> ObtenerDepartamentos(string BaseDatos)
        {
            List<UbigeoDTO> lstUbigeoDTO = new List<UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDepartamentos", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UbigeoDTO oUbigeoDTO = new UbigeoDTO();
                        oUbigeoDTO.CodUbigeo = drd["COD_UBIGEO"].ToString();
                        oUbigeoDTO.Descripcion = drd["DES_UBIGEO"].ToString();
                        ;
                        lstUbigeoDTO.Add(oUbigeoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUbigeoDTO;
        }


        public List<UbigeoDTO> ObtenerProvincias(string Departamento, string BaseDatos)
        {
            List<UbigeoDTO> lstUbigeoDTO = new List<UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarProvincias", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Departamento", Departamento);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UbigeoDTO oUbigeoDTO = new UbigeoDTO();
                        oUbigeoDTO.CodUbigeo = drd["COD_UBIGEO"].ToString();
                        oUbigeoDTO.Descripcion = drd["DES_UBIGEO"].ToString();

                        lstUbigeoDTO.Add(oUbigeoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUbigeoDTO;
        }

        public List<UbigeoDTO> ObtenerDistritos(string Provincia, string BaseDatos)
        {
            List<UbigeoDTO> lstUbigeoDTO = new List<UbigeoDTO>();
            using (SqlConnection cn = new Conexion().conectar(BaseDatos))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarDistritos", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Provincia", Provincia);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        UbigeoDTO oUbigeoDTO = new UbigeoDTO();
                        oUbigeoDTO.CodUbigeo = drd["COD_UBIGEO"].ToString();
                        oUbigeoDTO.Descripcion = drd["DES_UBIGEO"].ToString();

                        lstUbigeoDTO.Add(oUbigeoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return lstUbigeoDTO;
        }
    }
}
