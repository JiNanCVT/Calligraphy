using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace aspforms.Users
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;   //判断forms验证
                  if (!id.Ticket.UserData.Contains("User"))    
            {   
                  
                Response.Redirect("~/Error/AccessError.html", true);   
            }   
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            //注销票据   
            FormsAuthentication.SignOut();   
            ClientScriptManager csm = this.Page.ClientScript;   
            csm.RegisterStartupScript(this.GetType(), "exit_tip", "alert('您已经安全退出了！');", true);   
        }   

        }   

        }
   