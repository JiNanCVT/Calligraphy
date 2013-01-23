using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.ClientServices;
using System.Data;
using System.Data.SqlClient;




using System.Data;
using System.Data.SqlClient;


namespace aspforms
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

             
            FormsAuthentication.Initialize();
               
            string[] um = Getuser(Name.Text.Trim(), Password.Text.Trim());
            if (um != null)
            {
                //创建身份验证票据   
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                            um[0],
                                            DateTime.Now,
                                            DateTime.Now.AddMinutes(30),
                                            true,
                                            um[1],//用户所属的角色字符串   
                                            FormsAuthentication.FormsCookiePath);
                 
                string hash = FormsAuthentication.Encrypt(ticket);//加密 
                   
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);//创建cookie 
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                 
                Response.Cookies.Add(cookie);

                //转发到请求的页面   
                try
                {
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(um[0], false));
                }
                catch (Exception)

                    {
                        Response.Write("<script>alert('输入错了')</script>");
                    }
                    
              }
            else
            {
                ClientScriptManager csm = this.Page.ClientScript;
                csm.RegisterStartupScript(this.GetType(), "error_tip", "alert('用户名或密码错误！身份验证失败！');", true);
            }
        }
        //下面这部分为连接数据库部分
        //private string[] HasUser(string name, string password)
        //{
        //    string strConnection = "user id=sa;password=;";
        //    strConnection += "initial catalog=sql;server=.;";
        //    strConnection += "Connect Timeout=30";
        //    SqlConnection objConnection = new SqlConnection(strConnection);
        //    objConnection.Open(); 
        //    using (objConnection)
        //    {
        //        string sql = "select count(*) from Login where name=" + name + "and pass =" + password;


        //        SqlCommand cmd =objConnection.CreateCommand();
        //        cmd.CommandText = sql;
        //        int ret = (int)cmd.ExecuteScalar();
        //        string[] fuck = new string[2];
        //        if (ret > 0)
        //        {
        //            string checkname = "select name from Login where name=" + "Name.Text";
        //            string checkrole = "select role from Login where name=" + "Name.Text";
        //            cmd.CommandText = checkname;
        //            fuck[0] = "select name from Login where name=" + "Name.Text";
        //            fuck[1] = "select role from Login where name=" + "Name.text";

        //        }
        //        else
        //        {
        //            fuck[0] = null;
        //            fuck[1] = null;
        //        }
        //        return fuck;
        //    }
        //}
        private string[] Getuser(string name,string password)
        {
            string[] back = new string[2];
            if(Name.Text=="lw"&&Password.Text=="123")
            {
                back[0]="lw";
                back[1]="AdminandUser";
                return back;
            }
            if(Name.Text=="admin"&&Password.Text=="123")
            {
                 back[0]="admin";
                back[1]="Admin";
                return back;
            }
            if(Name.Text=="user"&&Password.Text=="123")
            {
                back[0]="user";
                back[1]="User";
                return back;
            }
            if (Name.Text != "user" || Name.Text != "admin" || Name.Text != "lw")
            {
                
                back[0] = null;
                back[1] = null;
                return back;
            }
            else
            {
                back[0] = null;
                back[1] = null;
                return back;
            }}

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {

        }

        protected void Answer_TextChanged(object sender, EventArgs e)
        {

        }

               }
            
                
    }
