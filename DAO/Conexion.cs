using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Conexion
    {
        public SqlConnection conectar()
        {
            return cn();
        }
        public SqlConnection cn()
        {
            //string nombreBaseDatos = ConfigurationManager.AppSettings["BDAddonRQ"];
            string nombreBaseDatos = "AddonsConcyssa";


            SqlConnection cn = new SqlConnection("Server=209.45.52.78,61449;Database=" + nombreBaseDatos + ";User ID=sa;Password=$martcod3**85;Trusted_Connection=False");

            return cn;
        }
    }
}
