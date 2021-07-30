using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
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

        public static int ModifyData(string cs, string dbcs, List<SqlParameter> list) //處理SQL事件
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

        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null) return false;
            else return true;
        }

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

        public static UserInfoModel GetCurrenctUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;
            if (account == null) return null;

            DataRow dr = GetUserInfoByAcc(account);
            if (dr == null) return null;

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }

        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }

        public static bool TryLogin(string account, string pwd, out string errorMsg) 
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd)) 
            {
                errorMsg = "沒輸入";
                return false;
            }
            var dr = GetUserInfoByAcc(account);
            if (dr == null)
            {
                errorMsg = $"{account}不存在";
                return false;
            }
            if (string.Compare(dr["Account"].ToString(), account, true) == 0 && string.Compare(dr["PWD"].ToString(), pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入大失敗，帳密有問題";
                return false;
            }
        }
    }
}
