using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBClassRA;
using DBHelperClass;
using System.Data;
using System.Drawing;

namespace RunningAccountWeb.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DBHelper.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            UserInfoModel currenctUser = DBHelper.GetCurrenctUser();
            if (currenctUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }
            DataRow dr = UserInfoManager.GetUserInfoByAcc(currenctUser.Account);

            DataTable dt = AccountingManager.GetAccountingList(dr["ID"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.PlaceHolder1.Visible = false;
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
            else 
            {
                this.GridView1.Visible = false;
                this.PlaceHolder1.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gv_RowDataB(object sender, GridViewRowEventArgs e) 
        {
            var row = e.Row;
            //Literal ltl = row.FindControl("liactt") as Literal;
            Label lbl = row.FindControl("Label1") as Label;

            if (row.RowType == DataControlRowType.DataRow)
            {
                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");
                if (actType == 0) lbl.Text = "支出";
               // if (actType == 0) ltl.Text = "支出";
               // else ltl.Text = "收入";
                else lbl.Text = "收入";
                if (dr.Row.Field<int>("Amount") > 1500)
                {
                    lbl.ForeColor = Color.Red;
                }
            }

        }

    }
}