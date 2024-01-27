using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISAP
{
    public class ConexionSQL
    {
        public SqlConnection conectar(string nombreBaseDatos)
        {
            return cn(nombreBaseDatos);
        }
        public SqlConnection cn(string nombreBaseDatos)
        {
            SqlConnection cn = new SqlConnection("Server=192.168.0.209,1433;Database=" + nombreBaseDatos + ";User ID=sa;Password=C0ncy$$@$ql;Trusted_Connection=False");

            return cn;
        }



    }
}
