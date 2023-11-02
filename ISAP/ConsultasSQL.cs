
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ISAP
{
    public class ConsultasSQL
    {

      

        public string ObtenerCuentaContable(string Cuenta)
        {
            string SysCuenta="";
            using (SqlConnection cn = new ConexionSQL().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerCuentaContable", cn);
                    da.SelectCommand.Parameters.AddWithValue("@Cuenta", Cuenta);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        SysCuenta = (drd["AcctCode"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return SysCuenta;
        }

        public List<OPCHDetalle> ObtenerDetallexDocEntry(int DocEntry,string ItemCode,decimal Cantidad)
        {
            string mensaje_error = "";
            List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
            using (SqlConnection cn = new ConexionSQL().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerDetallexDocEntry", cn);
                    da.SelectCommand.Parameters.AddWithValue("@DocEntry", DocEntry);
                    da.SelectCommand.Parameters.AddWithValue("@ItemCode", ItemCode);
                    da.SelectCommand.Parameters.AddWithValue("@Cantidad", Cantidad);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        OPCHDetalle oOPCHDetalle = new OPCHDetalle();
                        oOPCHDetalle.ItemCode =drd["ItemCode"].ToString();
                        oOPCHDetalle.LineNum = Convert.ToInt32(drd["LineNum"].ToString());
                        lstOPCHDetalle.Add(oOPCHDetalle);
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                    mensaje_error = ex.Message.ToString();
                }
                return lstOPCHDetalle;
            }
        }


    }
}
