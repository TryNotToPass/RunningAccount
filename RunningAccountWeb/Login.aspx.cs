using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBClassRA;

namespace RunningAccountWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] is null)
            {
                this.ph_login.Visible = true;
            }
            else
            {
                this.ph_login.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string inp_Account = this.TxtAcc.Text;
            string inp_PWD = this.TxtPwd.Text;

            if (string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.limsg.Text = "空集合";
                return;
            }

            DataRow drAcc = UserInfoManager.GetUserInfoByAcc(inp_Account);

            if (drAcc == null)
            {
                this.limsg.Text = "沒這個人";
                return;
            }
            string password = drAcc["PWD"].ToString();
            //if (string.Compare(drAcc["Account"].ToString();, inp_Account, true) == 0 && string.Compare(password, inp_PWD, false) == 0)
            if (string.Compare(password, inp_PWD, false) == 0)
            {
                this.Session["UserLoginInfo"] = drAcc["Account"].ToString();
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.limsg.Text = "炸裂囉";
                return;
            }  
        }
    }
}