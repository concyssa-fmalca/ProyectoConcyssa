
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

      

        public string ObtenerCuentaContable(string Cuenta,string BaseDatosSAP)
        {
            string SysCuenta="";
            using (SqlConnection cn = new ConexionSQL().conectar(BaseDatosSAP))
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

        public List<OPCHDetalle> ObtenerDetallexDocEntry(int DocEntry,string ItemCode,decimal Cantidad,string BaseDatosSAP)
        {
            string mensaje_error = "";
            List<OPCHDetalle> lstOPCHDetalle = new List<OPCHDetalle>();
            using (SqlConnection cn = new ConexionSQL().conectar(BaseDatosSAP))
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
        public string ObtenerCondicionPagoDET(string FormatoDet, string CardCode, string BaseDatosSAP)
        {
            string GroupNum = "";
            using (SqlConnection cn = new ConexionSQL().conectar(BaseDatosSAP))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerCondicionPagoDETSAP", cn);
                    da.SelectCommand.Parameters.AddWithValue("@FormatoDet", FormatoDet);
                    da.SelectCommand.Parameters.AddWithValue("@CardCode", CardCode);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GroupNum = (drd["GroupNum"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {
                }
            }
            return GroupNum;
        }

        public string ObtenerGrupoDetDesdeSGC(string TipoDet, string BaseDatosSAP)
        {
            string GrupoDet = "";
            using (SqlConnection cn = new ConexionSQL().conectar(BaseDatosSAP))
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SMC_ObtenerGrupoDetDesdeSGC", cn);
                    da.SelectCommand.Parameters.AddWithValue("@TipoDet", TipoDet);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader drd = da.SelectCommand.ExecuteReader();
                    while (drd.Read())
                    {
                        GrupoDet = (drd["Code"].ToString());
                    }
                    drd.Close();


                }
                catch (Exception ex)
                {

                }
            }
            return GrupoDet;
        }

    }
}
