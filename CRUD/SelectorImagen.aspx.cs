using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD
{
    public partial class SelectorImagen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
            string Ruta = @"https://drive.google.com/drive/folders/1FfUkoy7bIwz1aBDKR7kZgz9_GDNP3zQ8";
            if (FileUpload1.HasFile)
            {
                if (extension==".jpg"||extension==".png")
                {
                    if (!File.Exists(Ruta + FileUpload1.FileName))
                    {
                        FileUpload1.SaveAs(Ruta + FileUpload1.FileName);
                        LabelMessage.Text = "FILE UPLOADED";
                    }
                    else
                    {
                        LabelMessage.Text = "Ya existe ese archivo";
                    }
                    
                }
                else
                {
                    LabelMessage.Text = "YOU MUST SELECT A FILE WITH EXTENSION JPG O PNG";
                }
                
            }
            else
            {
                LabelMessage.Text = "YOU MUST SELECT A FILE";
            }
        }
    }
}