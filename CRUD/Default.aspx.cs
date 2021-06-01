using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelError.Visible = false;
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                ///REALIZAR LA CONSULTA  A LA TABLA LOGIN 
                con.Open();
                string query = "SELECT COUNT(1) FROM _LOGIN WHERE USERNAME=@username AND USERPASS=@userpass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", txtUser.Text.Trim());
                cmd.Parameters.AddWithValue("@userpass", txtPassword.Text.Trim());
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        ///PERMITE INICIAR UNA SESION CON LOS DATOS DEL USUARIO DE LA BD 
                        Session["Username"] = txtUser.Text.Trim();
                        Session["Userpass"] = txtPassword.Text.Trim();
                        Response.Redirect("FullAudit.aspx");
                    }
                
                else
                {

                    LabelError.Visible = true;
                    Response.AppendHeader("Cache-Control", "no-store");

                }

            }

        }
    }
}