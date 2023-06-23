using System.Data;
using System.Data.Common;

namespace DAO.helpers
{
    public static class HelperDao
    {


        public static int   conversionInt(DbDataReader DataReader, string FieldName, int defecto=0)
        {
            var data = Sql.Read<int>(DataReader, FieldName);
           
            return data;
        }
        public static  string toString(DbDataReader DataReader, string FieldName)
        {
            var data = Sql.Read<string>(DataReader, FieldName);
            if (string.IsNullOrEmpty(data?.ToString()))
            {
                return "";
            }
            return data;
        }
    }

    public static class Sql
    {
        public static T Read<T>(DbDataReader DataReader, string FieldName)
        {
            int FieldIndex;
            try { FieldIndex = DataReader.GetOrdinal(FieldName); }
            catch { return default(T); }

            if (DataReader.IsDBNull(FieldIndex))
            {
                return default(T);
            }
            else
            {
                object readData = DataReader.GetValue(FieldIndex);
                if (readData is T)
                {
                    return (T)readData;
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(readData, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
            }
        }
    }
}
