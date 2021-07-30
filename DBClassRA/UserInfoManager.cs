using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DBHelperClass;

namespace DBClassRA
{
    public class UserInfoManager
    {

        public static DataRow GetUserInfoByAcc(string account)
        {
            string cs = DBHelper.GetConnectingStr(); //這種做法可以省空間，不用像ChangeIsGood()方法一樣打一堆
            //詳細作法是，先去參考裡面加入 System.Configuration，然後using它，並創建GetConnectingStr()方法。
            //那之後去資料夾

            //下面字串有@的代表可以帶入參數，使用下列方法，而非使用組字串的方式，否則容易遭受攻擊。
            string dbcs = @"SELECT ID, Account, PWD, Name, Email 
                                FROM UserInfo
                                WHERE Account = @account;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        if (dt.Rows.Count == 0) return null;

                        DataRow dr = dt.Rows[0];
                        return dr;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
        }

    }
}
