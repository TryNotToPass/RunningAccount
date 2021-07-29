<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="RunningAccountWeb.SystemAdmin.AccountingList" %>

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
                    <asp:Button ID="Button1" runat="server" Text="加入" OnClick="Button1_Click" />   
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound ="gv_RowDataB">
                        <Columns>
                            <asp:BoundField HeaderText ="標題" DataField ="Caption" />
                            <asp:BoundField HeaderText ="金額" DataField ="Amount" />

                            <asp:TemplateField HeaderText ="In/Out">
                                <ItemTemplate>
                                    <%--以下兩種做法，都可以用--%>
                                    <%--<%# ((int)Eval("ActType") == 0) ? "支出" : "收入" %>--%>
                                    <asp:Literal ID="liactt" runat="server"></asp:Literal>
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText ="日期" DataField ="CreateDate" DataFormatString ="{0:yyyy-MM-dd}"/>
                            <asp:TemplateField HeaderText ="Act">
                                <ItemTemplate>
                                    <a href ="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <p>
                            我來到一個島，他叫卡加布列島
                        </p>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
