using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DBHelperClass
{
    public class DBHelper
    {
        public static string GetConnectingStr()
        {
            string val = ConfigurationManager.ConnectionStrings["DConnectionRA"].ConnectionString;
            return val;
        }

        public static DataTable DBTableMethod(string cs, string dbcs, List<SqlParameter> list) //SQL表單
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();
                    return dt;
                }
            }
        }

        public static int DBTableENQuery(string cs, string dbcs, List<SqlParameter> list) //處理SQL事件
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    int efr = command.ExecuteNonQuery();
                    return efr;
                }
            }
        }

    }
}
