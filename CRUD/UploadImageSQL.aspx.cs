using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CRUD
{
    public partial class UploadImageSQL : System.Web.UI.Page
    {
        ViewImage como = new ViewImage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Visible = false;
                HyperLink1.Visible = false;
                LoadImages();
            }
            
        }

        private void LoadImages() 
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from tbImages", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                GridView1.DataSource = rdr;
                GridView1.DataBind();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;
            

            if (fileExtension.ToLower()== ".jpg" || fileExtension.ToLower()== ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes= binaryReader.ReadBytes((int)stream.Length);

               
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs)) 
                {
                    SqlCommand cmd = new SqlCommand("spUploadImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paraName = new SqlParameter()
                    {
                        ParameterName = "@name",
                        Value = fileName
                    };
                    cmd.Parameters.Add(paraName);
                    
                    SqlParameter paraSize = new SqlParameter()
                    {
                        ParameterName = "@Size",
                        Value = fileSize
                    };
                    cmd.Parameters.Add(paraSize);

                    SqlParameter paraImageData = new SqlParameter()
                    {
                        ParameterName= "@ImageData",
                        Value=bytes
                    };

                    cmd.Parameters.Add(paraImageData);

                    SqlParameter paraNewId = new SqlParameter()
                    {
                        ParameterName = "@NewId",
                        Value = -1,
                        Direction=ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paraNewId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblMessage.Visible = true;
                    lblMessage.Text = "Upload Successful";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    HyperLink1.Visible = true;     
                    HyperLink1.NavigateUrl = "~/ViewImage.aspx?Id=" + cmd.Parameters["@NewId"].Value.ToString();
                  

                        

                    LoadImages();
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Only images (.jpg and .png) can be uploaded";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                HyperLink1.Visible = false;
            }
        }

        protected void InkSelection_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            HyperLink1.NavigateUrl = "~/ViewImage.aspx?Id=" + ID;



        }
    }
}