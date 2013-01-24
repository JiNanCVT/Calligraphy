using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace aspforms
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            
            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;//forms判断用户资格   
               
            if (!id.Ticket.UserData.Contains("Admin"))    
            {   
                 
                Response.Redirect("~/Error/AccessError.html", true);   
            }   
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             
            //注销  
            FormsAuthentication.SignOut();   
            ClientScriptManager csm = this.Page.ClientScript;   
            csm.RegisterStartupScript(this.GetType(), "exit_tip", "alert('退出了，删cookie~！');", true);   
        }   

        }   


        }
  