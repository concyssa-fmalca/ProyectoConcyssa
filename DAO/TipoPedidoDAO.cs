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
    public class TipoPedidoDAO
    {
        public List<TipoPedidoDTO> ObtenerTipoPedido(int IdSociedad, ref string mensaje_error, int Estado = 3)
        {
            List<TipoPedidoDTO> lstTipoPedidoDTO = new List<TipoPedidoDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ListarTipoPedido", cn);
                    da.SelectCommand.Parameters.AddWithValue("@IdSociedad", IdSociedad);
                    da.SelectCommand.Parameters.AddWithValue("@Estado", Estado);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        TipoPedidoDTO oTipoPedidoDTO = new TipoPedidoDTO();
                        oTipoPedidoDTO.IdTipoPedido = int.Parse(drd["IdTipoPedido"].ToString());
                        oTipoPedidoDTO.IdSociedad = Convert.ToInt32(drd["IdSociedad"].ToString());
                        oTipoPedidoDTO.Codigo = (drd["Codigo"].ToString());
                        oTipoPedidoDTO.Descripcion = (drd["Descripcion"].ToString());
                        oTipoPedidoDTO.Estado = Convert.ToBoolean(drd["Estado"].ToString());
                        oTipoPedidoDTO.Eliminado = Convert.ToBoolean(drd["Eliminado"].ToString());
                        lstTipoPedidoDTO.Add(oTipoPedidoDTO);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
            }
            return lstTipoPedidoDTO;
        }
    }
}
