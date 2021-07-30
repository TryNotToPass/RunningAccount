using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBClassRA;
using DBHelperClass;

namespace RunningAccountWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DBHelper.IsLogined())
            {
                this.ph_login.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
                return;
            }
            else
            {
                this.ph_login.Visible = true;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string inp_Account = this.TxtAcc.Text;
            string inp_PWD = this.TxtPwd.Text;

            string msg;
            if (!DBHelper.TryLogin(inp_Account, inp_PWD, out msg)) 
            {
                this.limsg.Text = msg;
                return;
            }
            Response.Redirect("/SystemAdmin/UserInfo.aspx");
            //if (string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            //{
            //    this.limsg.Text = "空集合";
            //    return;
            //}

            //DataRow drAcc = UserInfoManager.GetUserInfoByAcc(inp_Account);

            //if (drAcc == null)
            //{
            //    this.limsg.Text = "沒這個人";
            //    return;
            //}
            //string password = drAcc["PWD"].ToString();
            //if (string.Compare(password, inp_PWD, false) == 0)
            //{
            //    this.Session["UserLoginInfo"] = drAcc["Account"].ToString();
            //    Response.Redirect("/SystemAdmin/UserInfo.aspx");
            //}
            //else
            //{
            //    this.limsg.Text = "炸裂囉";
            //    return;
            //}  
        }
    }
}