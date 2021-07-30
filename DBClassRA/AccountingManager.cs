using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DBHelperClass;

namespace DBClassRA
{
    public class AccountingManager
    {

        public static DataTable GetAccountingList(string userid) //獲取表單
        {
            string cs = DBHelper.GetConnectingStr();
            string dbcs = @"SELECT ID, UserID, Caption, Amount, ActType, CreateDate
                                FROM Accounting
                                WHERE UserID = @userid;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", userid));

            try 
            {
                return DBHelper.DBTableMethod(cs, dbcs, list);
            } 
            catch(Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
            
        }

        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body) 
        {
            if (amount < 0 || amount > 1000000) throw new ArgumentException("我去，你的錢太多囉。");
            if (actType < 0 || actType > 1) throw new ArgumentException("只能是1或0");

            string cs = DBHelper.GetConnectingStr();
            string dbcs = @"INSERT INTO [dbo].[Accounting]
                                (UserID
                                ,Caption
                                ,Amount
                                ,ActType
                                ,CreateDate
                                ,Body)
                            VALUES
                                (@userid
                                ,@caption
                                ,@amount
                                ,@actType
                                ,@createDate
                                ,@body)";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userid", userID));
            list.Add(new SqlParameter("@caption", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@actType", actType));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            list.Add(new SqlParameter("@body", body));

            try
            {
                DBHelper.DBTableENQuery(cs, dbcs, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            #region 以備不時之需
            //using (SqlConnection connection = new SqlConnection(cs))
            //{
            //    using (SqlCommand command = new SqlCommand(dbcs, connection))
            //    {
            //        command.Parameters.AddWithValue("@userid", userID);
            //        command.Parameters.AddWithValue("@caption", caption);
            //        command.Parameters.AddWithValue("@amount", amount);
            //        command.Parameters.AddWithValue("@actType", actType);
            //        command.Parameters.AddWithValue("@createDate", DateTime.Now);
            //        command.Parameters.AddWithValue("@body", body);
            //        try
            //        {
            //            connection.Open();
            //            command.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.WriteLog(ex);
            //        }
            //    }
            //}
            #endregion
        }

        public static bool UpadateAccounting(int id, string userID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000) throw new ArgumentException("我去，你的錢太多囉。");
            if (actType < 0 || actType > 1) throw new ArgumentException("只能是1或0");

            string cs = DBHelper.GetConnectingStr();
            string dbcs = @"UPDATE [Accounting]
                                SET
                                UserID        = @userid
                                ,Caption       = @caption
                                ,Amount        = @amount
                                ,ActType       = @actType
                                ,CreateDate    = @createDate
                                ,Body         = @body
                           WHERE
                                ID = @id";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userid", userID));
            list.Add(new SqlParameter("@caption", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@actType", actType));
            list.Add(new SqlParameter("@createDate", DateTime.Now));
            list.Add(new SqlParameter("@body", body));

            try
            {
                int efr = DBHelper.DBTableENQuery(cs, dbcs, list);
                if (efr == 1) return true;
                else return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
            #region 以備不時之需
            //using (SqlConnection connection = new SqlConnection(cs))
            //{
            //    using (SqlCommand command = new SqlCommand(dbcs, connection))
            //    {
            //        command.Parameters.AddWithValue("@id", id);
            //        command.Parameters.AddWithValue("@userid", userID);
            //        command.Parameters.AddWithValue("@caption", caption);
            //        command.Parameters.AddWithValue("@amount", amount);
            //        command.Parameters.AddWithValue("@actType", actType);
            //        command.Parameters.AddWithValue("@createDate", DateTime.Now);
            //        command.Parameters.AddWithValue("@body", body);
            //        try
            //        {
            //            connection.Open();
            //            int efr = command.ExecuteNonQuery();

            //            if (efr == 1) return true;
            //            else return false;
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.WriteLog(ex);
            //            return false;
            //        }
            //    }
            //}
            #endregion
        }

        public static DataRow GetAccounting(int id, string userID) 
        {
            string cs = DBHelper.GetConnectingStr();
            string dbcs = @"SELECT ID, Caption, Amount, ActType, CreateDate, Body
                                FROM Accounting
                                WHERE ID = @id AND UserID = @userID;";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                DataTable dt = DBHelper.DBTableMethod(cs, dbcs, list);
                if (dt.Rows.Count == 0) return null;
                DataRow dr = dt.Rows[0];
                return dr;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
            #region 以備不時之需
            //using (SqlConnection connection = new SqlConnection(cs))
            //{
            //    using (SqlCommand command = new SqlCommand(dbcs, connection))
            //    {
            //        command.Parameters.AddWithValue("@id", id);
            //        command.Parameters.AddWithValue("@userID", userID);
            //        try
            //        {
            //            connection.Open();
            //            SqlDataReader reader = command.ExecuteReader();

            //            DataTable dt = new DataTable();
            //            dt.Load(reader);
            //            if (dt.Rows.Count == 0) return null;
            //            DataRow dr = dt.Rows[0];
            //            return dr;
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.WriteLog(ex);
            //            return null;
            //        }
            //    }
            //}
            #endregion
        }

        public static void DeleteAccounting(int id)
        {
            string cs = DBHelper.GetConnectingStr();
            string dbcs = @"DELETE [Accounting] 
                                WHERE ID = @id;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));

            try
            {
                DBHelper.DBTableENQuery(cs, dbcs, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            #region 以備不時之需
            //using (SqlConnection connection = new SqlConnection(cs))
            //{
            //    using (SqlCommand command = new SqlCommand(dbcs, connection))
            //    {
            //        command.Parameters.AddWithValue("@id", id);

            //        try
            //        {
            //            connection.Open();
            //            command.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.WriteLog(ex);
            //        }
            //    }
            //}
            #endregion

        }


    }
}
