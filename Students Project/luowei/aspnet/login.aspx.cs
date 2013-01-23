using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string userName = TextBox1.Text.Trim();
        string userPwd = TextBox2.Text.Trim();
        SqlConnection con = new SqlConnection(@"Data Source=LW\LW;Initial Catalog=MyForms;Integrated Security=True;Pooling=False");
        SqlCommand cmd = new SqlCommand("select count(*) from users where userName='" + userName + "' and userPwd='" + userPwd + "'", con);
        con.Open();

        int count = (int)cmd.ExecuteScalar();
        if (count > 0)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(this.TextBox1.Text, this.CheckBox1.Checked);
            Response.Redirect("Default.aspx");

            con.Close();
        }
        else
        {
            Response.Write("用户不合法");
        }
    }
}