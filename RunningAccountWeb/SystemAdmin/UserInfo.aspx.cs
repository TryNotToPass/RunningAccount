using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBClassRA;

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
                string acc = this.Session["UserLoginInfo"] as string;
                DataRow dr = UserInfoManager.GetUserInfoByAcc(acc);
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
            this.Session["UserLoginInfo"] = null;
            Response.Redirect("/Login.aspx");
        }
    }
}