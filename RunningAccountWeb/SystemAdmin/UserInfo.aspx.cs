using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBClassRA;
using DBHelperClass;

namespace RunningAccountWeb.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) 
            { 
                if (this.Session["UserLoginInfo"] is null)
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                UserInfoModel currenct = DBHelper.GetCurrenctUser();
                if (currenct == null)
                {
                    this.Session["UserLoginInfo"] = null;
                    Response.Redirect("/Login.aspx");
                    return;
                }
                //string account = this.Session["UserLoginInfo"] as string;
                //DataRow dr = UserInfoManager.GetUserInfoByAcc(account);
                DataRow dr = UserInfoManager.GetUserInfoByAcc(currenct.Account);
                if (dr == null) 
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.liAcc.Text = dr["Account"].ToString();
                this.liName.Text = dr["Name"].ToString();
                this.liemail.Text = dr["Email"].ToString();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.Session["UserLoginInfo"] = null;
            DBHelper.Logout();
            Response.Redirect("/Login.aspx");
        }
    }
}