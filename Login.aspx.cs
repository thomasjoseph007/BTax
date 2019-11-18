using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    DBoperation db = new DBoperation();
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = db.exetable("select * from login where username='" + txtuname.Text + "' and password='" + txtpass.Text + "' ");
        if (dt.Rows.Count == 1)
        {
            Session["username"] = dt.Rows[0]["username"].ToString();
            Session["lid"] = dt.Rows[0]["lid"].ToString();

            if ((dt.Rows[0]["role"].ToString() == "admin") && (dt.Rows[0]["status"].ToString() == "1"))
            {
                Response.Redirect("~/ADMIN/admin_home.aspx");

            }

            else if(dt.Rows[0]["role"].ToString() == "user" && dt.Rows[0]["status"].ToString() == "0")
            {
                Response.Redirect("~/USER/user-home-page.aspx");

            }
            else if (dt.Rows[0]["role"].ToString() == "company" && (dt.Rows[0]["status"].ToString() == "0"))
            {
                Session["lid"] = dt.Rows[0]["lid"];
                int id = Convert.ToInt32(Session["lid"]);
            Response.Redirect("~/COMPANY/company-home-page.aspx");
            }
            else if (dt.Rows[0]["role"].ToString() == "CA" && (dt.Rows[0]["status"].ToString() == "1"))
            {
                Session["lid"] = dt.Rows[0]["lid"];
                int id = Convert.ToInt32(Session["lid"]);
                Response.Redirect("~/CHARTERED ACCOUNTANT/homepage_ca.aspx");
            }
            else
            {
                Response.Write("invalid login");
            }
        }

    }
}