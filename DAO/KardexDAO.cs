using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class KardexDAO
    {

        public List<KardexDTO> ObtenerKardex(int IdSociedad,int IdArticulo,int IdAlmacen, DateTime FechaInicio, DateTime FechaTermino, ref string mensaje_error)
        {
            List<KardexDTO> lstKardexDTO = new List<KardexDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_Kardex", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@IdAlmacen", IdAlmacen);
                    da.SelectCommand.Parameters.AddWithValue("@IdArticulo", IdArticulo);
                    da.SelectCommand.Parameters.AddWithValue("@FechaInicio", FechaInicio.ToString("yyyyMMdd"));
                    da.SelectCommand.Parameters.AddWithValue("@FechaTermino", FechaTermino.ToString("yyyyMMdd"));
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        KardexDTO oKardexDTO = new KardexDTO();
                        oKardexDTO.IdKardex = Convert.ToInt32(drd["IdKardex"].ToString());
                        oKardexDTO.IdDetallleMovimiento = Convert.ToInt32(drd["IdDetallleMovimiento"].ToString());
                        oKardexDTO.IdDefinicionGrupoUnidad = Convert.ToInt32(drd["IdDefinicionGrupoUnidad"].ToString());
                        oKardexDTO.CantidadBase = Convert.ToDecimal(drd["CantidadBase"].ToString());
                        oKardexDTO.IdUnidadMedidaBase = Convert.ToInt32(drd["IdUnidadMedidaBase"].ToString());
                        oKardexDTO.PrecioBase = Convert.ToDecimal(drd["PrecioBase"].ToString());
                        oKardexDTO.CantidadRegistro = Convert.ToDecimal(drd["CantidadRegistro"].ToString());
                        oKardexDTO.IdUnidadMedidaRegistro = Convert.ToInt32(drd["IdUnidadMedidaRegistro"].ToString());
                        oKardexDTO.PrecioRegistro = Convert.ToDecimal(drd["PrecioRegistro"].ToString());
                        oKardexDTO.PrecioPromedio = Convert.ToDecimal(drd["PrecioPromedio"].ToString());
                        oKardexDTO.FechaRegistro = Convert.ToDateTime(drd["FechaRegistro"].ToString());
                        oKardexDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oKardexDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oKardexDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oKardexDTO.IdArticulo = Convert.ToInt32(drd["IdArticulo"].ToString());
                        oKardexDTO.Saldo = Convert.ToDecimal(drd["Saldo"].ToString());
                        oKardexDTO.DescArticulo = (drd["DescArticulo"].ToString());
                        oKardexDTO.DescSerie = (drd["DescSerie"].ToString());
                        oKardexDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oKardexDTO.TipoTransaccion = (drd["TipoTransaccion"].ToString());
                        oKardexDTO.DescUnidadMedidaBase = (drd["DescUnidadMedidaBase"].ToString());
                        
                        lstKardexDTO.Add(oKardexDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstKardexDTO;
        }

    }
}
