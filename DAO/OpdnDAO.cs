using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class OpdnDAO
    {

        public List<OpdnDTO> ObtenerOPDNxEstado(int IdSociedad, ref string mensaje_error, string EstadoOPDN)
        {
            List<OpdnDTO> lstOPDNDTO = new List<OpdnDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarOPDNxEstado", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@EstadoOPDN", EstadoOPDN);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OpdnDTO oOpdnDTO = new OpdnDTO();
                        oOpdnDTO.DT_RowId = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdOPDN = Convert.ToInt32(drd["IdOPDN"].ToString());
                        oOpdnDTO.IdTipoDocumento = Convert.ToInt32(drd["IdTipoDocumento"].ToString());
                        oOpdnDTO.ObjType = (drd["ObjType"].ToString());
                        oOpdnDTO.IdMoneda = Convert.ToInt32(drd["IdMoneda"].ToString());
                        oOpdnDTO.CodMoneda = (drd["CodMoneda"].ToString());
                        oOpdnDTO.TipoCambio = Convert.ToDecimal(drd["TipoCambio"].ToString());
                        oOpdnDTO.IdCliente = Convert.ToInt32(drd["IdCliente"].ToString());
                        oOpdnDTO.FechaContabilizacion = Convert.ToDateTime(drd["FechaContabilizacion"].ToString());
                        oOpdnDTO.FechaDocumento = Convert.ToDateTime(drd["FechaDocumento"].ToString());
                        oOpdnDTO.FechaVencimiento = Convert.ToDateTime(drd["FechaVencimiento"].ToString());
                        oOpdnDTO.IdListaPrecios = Convert.ToInt32(drd["IdListaPrecios"].ToString());
                        oOpdnDTO.Referencia = (drd["Referencia"].ToString());
                        oOpdnDTO.Comentario = (drd["Comentario"].ToString());
                        oOpdnDTO.DocEntrySap = Convert.ToInt32(drd["DocEntrySap"].ToString());
                        oOpdnDTO.DocNumSap = (drd["DocNumSap"].ToString());
                        oOpdnDTO.IdCentroCosto = Convert.ToInt32(drd["IdCentroCosto"].ToString());
                        oOpdnDTO.SubTotal = Convert.ToDecimal(drd["SubTotal"].ToString());
                        oOpdnDTO.Impuesto = Convert.ToDecimal(drd["Impuesto"].ToString());
                        oOpdnDTO.IdTipoAfectacionIgv = Convert.ToInt32(drd["IdTipoAfectacionIgv"].ToString());
                        oOpdnDTO.Total = Convert.ToDecimal(drd["Total"].ToString());
                        oOpdnDTO.IdAlmacen = Convert.ToInt32(drd["IdAlmacen"].ToString());
                        oOpdnDTO.IdSerie = Convert.ToInt32(drd["IdSerie"].ToString());
                        oOpdnDTO.Correlativo = Convert.ToInt32(drd["Correlativo"].ToString());
                        oOpdnDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oOpdnDTO.NombTipoDocumentoOperacion = (drd["NombTipoDocumentoOperacion"].ToString());
                        oOpdnDTO.NombSerie = (drd["NombSerie"].ToString());
                        oOpdnDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oOpdnDTO.DescCuadrilla = (drd["DescCuadrilla"].ToString());
                        oOpdnDTO.NombAlmacen = (drd["NombAlmacen"].ToString());
                        lstOPDNDTO.Add(oOpdnDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstOPDNDTO;
        }



    }
}
