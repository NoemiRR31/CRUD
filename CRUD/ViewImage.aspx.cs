using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUD
{
    public partial class ViewImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        

        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetImageById1", con);
    cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paraID = new SqlParameter()
                {
                    ParameterName = "@ID",
                    Value = Request.QueryString["ID"]
                };
    cmd.Parameters.Add(paraID);

                con.Open();
                byte[] bytes = (byte[])cmd.ExecuteScalar();
    string strBase64 = Convert.ToBase64String(bytes);
    Image1.ImageUrl = "data:Image/png;base64," + strBase64;


            }
        }
    }
}