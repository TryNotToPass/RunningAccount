<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="RunningAccountWeb.SystemAdmin.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                    <table>
                        <tr>
                            <th>帳號</th>
                            <td>
                                <asp:Literal ID="liAcc" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>名字</th>
                            <td>
                                <asp:Literal ID="liName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>e-mail</th>
                            <td>
                                <asp:Literal ID="liemail" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="Button1" runat="server" Text="登出" OnClick="Button1_Click" />
    </form>
</body>
</html>
