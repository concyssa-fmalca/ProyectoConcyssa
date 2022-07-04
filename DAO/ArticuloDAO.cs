
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class ArticuloDAO
    {
        public List<ArticuloDTO> ObtenerticulosxSociedad(int IdSociedad, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticulosxSociedad", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdArticulo = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = drd["Descripcion1"].ToString();
                        oArticuloDTO.Descripcion2 = drd["Descripcion2"].ToString();
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }


        public bool UpdateInsertArticulo(ArticuloDTO oArticuloDTO,ref string mensaje_error)
        {
            bool exito = false;
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_UpdateInsertArticulo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", oArticuloDTO.IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@Codigo", oArticuloDTO.Codigo);
                    da.SelectCommand.Parameters.AddWithValue("@Descripcion1", oArticuloDTO.Descripcion1);
                    da.SelectCommand.Parameters.AddWithValue("@Descripcion2", oArticuloDTO.Descripcion2);
                    da.SelectCommand.Parameters.AddWithValue("@IdUnidadMedida", oArticuloDTO.IdUnidadMedida);
                    da.SelectCommand.Parameters.AddWithValue("@ActivoFijo", oArticuloDTO.ActivoFijo);
                    da.SelectCommand.Parameters.AddWithValue("@ActivoCatalogo", oArticuloDTO.ActivoCatalogo);
                    da.SelectCommand.Parameters.AddWithValue("@IdCodigoUbso", oArticuloDTO.IdCodigoUbso);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", oArticuloDTO.IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", oArticuloDTO.Estado);
                    int i = da.SelectCommand.ExecuteNonQuery();
                    if (i > 0)
                    {
                        exito = true;
                    }
                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return exito;
        }

        public List<ArticuloDTO> ObtenerDatosxID(int IdArticulo, ref string mensaje_error)
        {
            List<ArticuloDTO> lstArticuloDTO = new List<ArticuloDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarArticuloxID", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        ArticuloDTO oArticuloDTO = new ArticuloDTO();
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdArticulo"].ToString());
                        oArticuloDTO.Codigo = drd["Codigo"].ToString();
                        oArticuloDTO.Descripcion1 = (drd["Descripcion1"].ToString());
                        oArticuloDTO.Descripcion2 = (drd["Descripcion2"].ToString());
                        oArticuloDTO.IdUnidadMedida = int.Parse(drd["IdUnidadMedida"].ToString());
                        oArticuloDTO.ActivoFijo = Convert.ToBoolean(drd["ActivoFijo"].ToString());
                        oArticuloDTO.ActivoCatalogo = Convert.ToBoolean(drd["ActivoCatalogo"].ToString());
                        oArticuloDTO.IdCodigoUbso = int.Parse(drd["IdCodigoUbso"].ToString());
                        oArticuloDTO.IdSociedad = int.Parse(drd["IdSociedad"].ToString());
                        oArticuloDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oArticuloDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        lstArticuloDTO.Add(oArticuloDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstArticuloDTO;
        }


    }
}
