<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="aspforms.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 21px" Text="登陆" />
        </p>
    <div>
    
    </div>
    </form>
</body>
</html>
