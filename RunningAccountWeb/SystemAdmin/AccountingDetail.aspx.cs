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
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = this.Session["UserLoginInfo"] as string;
            DataRow dr = UserInfoManager.GetUserInfoByAcc(account);

            if (dr == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["ID"] == null) this.Button2.Visible = false;
                else
                {
                    this.Button2.Visible = true;
                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        DataRow drAccounting = AccountingManager.GetAccounting(id, dr["ID"].ToString());
                        //有dr["ID"].ToString()，可以防止持有者亂看其他持有者持有的帳戶。若只有ID，則其他使用者登入後，可以偷看其他持有者持有的帳戶
                        if (drAccounting == null)
                        {
                            this.Literal1.Text = "資料不存在";
                            this.Button1.Visible = false;
                            this.Button2.Visible = false;
                        }
                        else
                        {
                            this.DropDownList1.SelectedValue = drAccounting["ActType"].ToString();
                            this.TextAmount.Text = drAccounting["Amount"].ToString();
                            this.TextCaption.Text = drAccounting["Caption"].ToString();
                            this.TextDesc.Text = drAccounting["Body"].ToString();
                        }
                    }
                    else
                    {
                        this.Literal1.Text = "該ID無法轉換";
                        this.Button1.Visible = false;
                        this.Button2.Visible = false;
                    }

                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.Literal1.Text = string.Join("<br/>", msgList);
                return;
            }
            string account = this.Session["UserLoginInfo"] as string;
            DataRow dr = UserInfoManager.GetUserInfoByAcc(account);
            if (dr == null) 
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            this.Literal1.Text = String.Empty;

            string userID = dr["ID"].ToString();
            string actTypeT = this.DropDownList1.SelectedValue;
            string amountT = this.TextAmount.Text;
            string captionT = this.TextCaption.Text;
            string bodyT = this.TextDesc.Text;

            int amount = Convert.ToInt32(amountT);
            int actType = Convert.ToInt32(actTypeT);

            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))
            {
                AccountingManager.CreateAccounting(userID, captionT, amount, actType, bodyT);
            }
            else 
            {
                int id;
                if (int.TryParse(idText, out id))
                {
                    AccountingManager.UpadateAccounting(id, userID, captionT, amount, actType, bodyT);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");

        }

        private bool CheckInput(out List<string> erroMsgList)
        {
            List<string> msgList = new List<string>();

            if (this.DropDownList1.SelectedValue != "0" && this.DropDownList1.SelectedValue != "1")
            {
                msgList.Add("需要數值0或1");
            }

            if (string.IsNullOrWhiteSpace(this.TextAmount.Text))
            {
                msgList.Add("需要數值");
            }
            else
            { 
                int tempInt;
                if (!int.TryParse(this.TextAmount.Text, out tempInt))
                {
                    msgList.Add("必須是數字");
                }

                if (tempInt<0 || tempInt > 100000) 
                {
                    msgList.Add("數字必須是正整數且在一百萬之下");
                }
            }

            erroMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText)) return;

            int id;
            if (int.TryParse(idText, out id))
            {
                AccountingManager.DeleteAccounting(id);
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}