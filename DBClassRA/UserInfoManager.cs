using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DBClassRA
{
    public class UserInfoManager
    {
        public static string GetConnectingStr()
        {
            string val = ConfigurationManager.ConnectionStrings["DConnectionRA"].ConnectionString;
            return val;
        }
        public static void InsertUser(string username, string pwd) //注入新的使用者資料
        {
            string cs =
                    @"Data Source = DESKTOP-KGL1300\SQLEXPRESS; Initial Catalog = CSharpLesson; Integrated Security = True;";
            //下面字串有@的代表可以帶入參數，使用下列方法，而非使用組字串的方式，否則容易遭受攻擊。
            string dbcs = @"INSERT INTO UserInfo (Name, PWD)
                            VALUES (@name, @pwd)";
            //string dbcs = @"DELETE TestTable1 WHERE ID = @id;"; //刪除
            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@pwd", pwd);

                    try
                    {
                        connection.Open();
                        int eft = command.ExecuteNonQuery();
                        Console.WriteLine($"{eft} has changed");
                        //connection.Close(); //加了using，它會自動回收非用到的記憶體，這個也會自動使用，不用呼叫這個
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }
        }

        public static DataRow GetUserInfo(string id)
        {
            string cs = GetConnectingStr(); //這種做法可以省空間，不用像ChangeIsGood()方法一樣打一堆
            //詳細作法是，先去參考裡面加入 System.Configuration，然後using它，並創建GetConnectingStr()方法。
            //那之後去資料夾

            //下面字串有@的代表可以帶入參數，使用下列方法，而非使用組字串的方式，否則容易遭受攻擊。
            string dbcs = @"SELECT ID, Name 
                                FROM UserInfo
                                WHERE ID = @id;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
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


        public static DataRow GetUserInfoByAcc(string account)
        {
            string cs = GetConnectingStr(); //這種做法可以省空間，不用像ChangeIsGood()方法一樣打一堆
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
        public static DataTable GetUserInfoList() //獲取表單
        {
            string cs = GetConnectingStr(); //這種做法可以省空間，不用像ChangeIsGood()方法一樣打一堆
            //詳細作法是，先去參考裡面加入 System.Configuration，然後using它，並創建GetConnectingStr()方法。
            //那之後去資料夾

            //下面字串有@的代表可以帶入參數，使用下列方法，而非使用組字串的方式，否則容易遭受攻擊。
            string dbcs = @"SELECT ID, Name 
                                FROM UserInfo;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
        }

        public static void EditUser(string id, string username, string pwd)
        {
            string cs = GetConnectingStr();
            //下面字串有@的代表可以帶入參數，使用下列方法，而非使用組字串的方式，否則容易遭受攻擊。
            string dbcs = @"UPDATE UserInfo
                            SET 
                                Name = @name,
                                PWD = @pwd
                            WHERE ID = @id;";
            //string dbcs = @"DELETE TestTable1 WHERE ID = @id;"; //刪除
            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@pwd", pwd);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int eft = command.ExecuteNonQuery();
                        Console.WriteLine($"{eft} has changed");
                        //connection.Close(); //加了using，它會自動回收非用到的記憶體，這個也會自動使用，不用呼叫這個
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }
        }
        public static void DeleteUser(string id)
        {
            string cs = GetConnectingStr();
            string dbcs = @"DELETE UserInfo 
                                WHERE ID = @id;";
            using (SqlConnection connection = new SqlConnection(cs))
            {
                using (SqlCommand command = new SqlCommand(dbcs, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        int eft = command.ExecuteNonQuery();
                        Console.WriteLine($"{eft} has changed");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }
        }
    }
}
