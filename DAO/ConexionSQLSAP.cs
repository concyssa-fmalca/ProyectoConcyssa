using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ConexionSQLSAP
    {
        public SqlConnection conectar(string nombreBaseDatos)
        {
            return cn(nombreBaseDatos);
        }
        public SqlConnection cn(string nombreBaseDatos)
        {
            //string nombreBaseDatos = ConfigurationManager.AppSettings["BDAddonRQ"];
    

            SqlConnection cn = new SqlConnection("Server=192.168.0.209,1433;Database=" + nombreBaseDatos + ";User ID=sa;Password=C0ncy$$@$ql;Trusted_Connection=False");

            //SqlConnection cn = new SqlConnection("Server=209.45.52.78,61449;Database=" + nombreBaseDatos + ";User ID=sa;Password=$martcod3**85;Trusted_Connection=False");

            return cn;
        }
    }
}
