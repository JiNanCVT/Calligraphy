using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace aspforms
{
    public partial class reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (Upassword.Text == Upasswordcheck.Text)
            {
                if (Admincheck.Text == "123456")
                {
                    SqlConnection con = new SqlConnection(@"Data Source=LW\LW;Initial Catalog=MyForms;Integrated Security=True;Pooling=False");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert userName,userPwd,right values Uname.Text,Upassword.Text,'Admin'", con);
                    con.Close();
                    //Response.Write("<script>alert('输入了错误的管理者验证码')</script>");
                }
                else
                {
                    //SqlConnection con = new SqlConnection(@"Data Source=LW\LW;Initial Catalog=MyForms;Integrated Security=True;Pooling=False");
                    //con.Open();
                    //SqlCommand cmd = new SqlCommand("Insert userName,userPwd,right values Uname.Text,Upassword.Text,'Admin'", con);
                    //con.Close();
                    Response.Write("<script>alert('输入了错误的管理者验证码')</script>");
                }
            }
        }
    }
}