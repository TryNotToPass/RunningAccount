<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="RunningAccountWeb.SystemAdmin.AccountingDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <h3>小橋流水人家 古道西風瘦馬 斷腸人在天涯</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a>
                    <br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
                    Type:<asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Value ="0">支出</asp:ListItem>
                            <asp:ListItem Value ="1">收入</asp:ListItem>
                         </asp:DropDownList>
                    <br />
                    Amount:
                    <asp:TextBox ID="TextAmount" runat="server" TextMode="Number"></asp:TextBox>
                    <br />
                    Caption:
                    <asp:TextBox ID="TextCaption" runat="server"></asp:TextBox>
                    <br />
                    Desc:
                    <asp:TextBox ID="TextDesc" runat="server" TextMode="Multiline"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="存入" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="刪除" OnClick="Button2_Click"/>
                    <br />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
