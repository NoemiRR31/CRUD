<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImageSQL.aspx.cs" Inherits="CRUD.UploadImageSQL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
            <asp:HyperLink ID="HyperLink1" runat="server">View Uploaded Image</asp:HyperLink>
        </div>
        <asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Size" HeaderText="Size (bytes)" />
            
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Heigth="100px" Width="100px" 
                     <%--ImageUrl='<%#"data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("ImageData")) %>' ></asp:Image>--%>
                </ItemTemplate>
            </asp:TemplateField>
                <%--<asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="InkSelection" Text="View" runat="server" CommandArgument='<%#Eval("ID")%>' OnClick="InkSelection_Click" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:HyperLinkField HeaderText="View Image" NavigateUrl="ViewImage.aspx" Text="View "  />
                <asp:CheckBoxField ReadOnly="True" Text="Check" />--%>
                </Columns>
        </asp:GridView>
    </form>
</body>
</html>
