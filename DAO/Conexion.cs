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
        public SqlConnection conectar(string BaseDatos)
        {
            return cn(BaseDatos);
        }
        public SqlConnection cn(string BaseDatos)
        {

            SqlConnection cn = new SqlConnection("Server=192.168.0.209,1433;Database=" + BaseDatos + ";User ID=sa;Password=C0ncy$$@$ql;Trusted_Connection=False");

            return cn;
        }
    }
}
