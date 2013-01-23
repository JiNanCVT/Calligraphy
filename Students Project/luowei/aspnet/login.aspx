<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<script runat="server">

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:CheckBox ID="CheckBox1" runat="server" Text="是否记住cookie" />
    </form>
</body>
</html>
