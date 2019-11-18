using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Data;


public partial class forget_password : System.Web.UI.Page
{
    DBoperation db = new DBoperation();
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      //  dr = db.exereader("SELECT tbl_user.*,tbl_login.* from tbl_user inner join tbl_login on tbl_user.user_id=tbl_login.userid where tbl_user.email_id='" + txtemail.Text + "'");
        dr = db.exereader("select * from login  where login.username='" + cc.Text + "' ");
        if (dr.Read())
        {
            Session["pwd"] = dr["password"].ToString();
            dr.Close();
            string mFrom = "foripsrt@gmail.com";  //foripsrt","ipsr123456"
            // string mPwd = "ipsr123456";
            MailMessage msg1 = new MailMessage();
            
            msg1.From = new MailAddress(mFrom);

            msg1.To.Add(new MailAddress(cc.Text));
            
            msg1.Subject = "Forgot password information";
            msg1.Body = "Your password is '" + Session["pwd"] + "'";
            msg1.IsBodyHtml = true;
            SmtpClient smtpc = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicAuthentication = new System.Net.NetworkCredential("foripsrt", "ipsr123456");
            smtpc.EnableSsl = true;
            smtpc.UseDefaultCredentials = false;
            smtpc.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpc.Credentials = basicAuthentication;
            smtpc.Send(msg1);
            Response.Write("<script>alert('Password has been send to your Email ID')</script>");
        }
        else
        {
            Response.Write("<script>alert('Not a Registered Email ID')</script>");
        }
    }
}