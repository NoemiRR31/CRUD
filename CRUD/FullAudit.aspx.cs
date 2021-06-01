using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace CRUD
{
    public partial class FullAudit : System.Web.UI.Page
    {
        Conexion conn = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Cache-Control", "no-store");

            //-------------------------------- Validacion de usuario ------------------------------
            ///INICIA LA SESION DEL USUARIO
            if (Session["USERNAME"] == null)

                Response.Redirect("default.aspx");
            ///ETIQUETA QUE TRAE LOS DATOS DE LA BD DEL USUARIO EN UNA SESSION
            LabelUser.Visible = false;
            LabelUser.Text = "Username: " + Session["USERNAME"];
            ///METODO QUE REMPLAZA EL <<.>> POR UN ESPACIO SEPARANDO EL NOMBRE DEL USUARIO (nombre.apellido)
            ///LA VARIABLE USER GUARDA LA CADENA DE a
            string user = LabelUser.Text;
            user = user.Replace(".", " ");
            ///METODO QUE VUELVE MAYUSCULAS LAS INICIALES DE LOS DATOS EXTRAIDOS EN LA VARIABLE USER
            LabelUsername.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user);

            ///ETIQUETA QUE TRAE EL PASSWORD DEL AUDITOR DE LA BD EN UNA SESION 
            LabelPass.Visible = false;
            LabelPass.Text = "" + Session["USERPASS"];
            ///METODO QUE ELIMINA EL PRIMER CARACTER DE LA CADENA Y LO MUESTRA EN EL TEXTBOX
            string cadena = LabelPass.Text.Remove(0, 1);
            TextBoxAudi.Text = cadena;
            //ELIMINA EL CACHE DEL INICIO DE SESION
            Response.AppendHeader("Cache-Control", "no-store");


            btnUPDATE.Enabled = false;

            if (!IsPostBack)
            {
               
                //CONEXION PARA OBTENER DATOS DE LA TABLA AC_ZONE
           
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM AC_ZONE ", con);
                    con.Open();

                    DropDownACZ.DataSource = cmd.ExecuteReader();
                    DropDownACZ.DataTextField = "DESC_ZONE";
                    DropDownACZ.DataValueField = "ID_ACZ";
                    DropDownACZ.DataBind();
                }

                //CONEXION PARA OBTENER DATOS DE LA TABLA SHIFT_TURNS

                string cs1 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM SHIFT_TURNS ", con);
                    con.Open();
                    DropDownSHIFT.DataTextField = "SHIFT_D";
                    DropDownSHIFT.DataValueField = "ID_SHIFT";
                    DropDownSHIFT.DataSource = cmd1.ExecuteReader();
                    DropDownSHIFT.DataBind();
                }

                //CONEXION PARA OBTENER DATOS DE LA TABLA QUESTIONS

                string cs2 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM LPA_QUESTIONS ", con);
                    con.Open();
                    DropDownQues.DataTextField = "DESC_QUESTION";
                    DropDownQues.DataValueField = "ID_LPAQ";
                    DropDownQues.DataSource = cmd2.ExecuteReader();
                    DropDownQues.DataBind();
                }

                //CONEXION PARA OBTENER DATOS DE LA TABLA LINE 

                string cs3 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd3 = new SqlCommand("SELECT * FROM LINE ", con);
                    con.Open();
                    DropDownLINE.DataTextField = "DESC_LINE";
                    DropDownLINE.DataValueField = "ID_LN";
                    DropDownLINE.DataSource = cmd3.ExecuteReader();
                    DropDownLINE.DataBind();
                }

                //CONEXION PARA OBTENER DATOS DE LA TABLA AC_ZONE

                string cs4 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {

                    SqlCommand cmd4 = new SqlCommand("SELECT * FROM UNITS", con);
                    con.Open();
                    DropDownUnit.DataTextField = "MEASURED_UNIT";
                    DropDownUnit.DataValueField = "ID_U";
                    DropDownUnit.DataSource = cmd4.ExecuteReader();
                    DropDownUnit.DataBind();
                }
                BinData();
            }
        
        }
        ///METODO QUE HACE UNA CONSULTA A LA BASE DE DATOS Y EXTRAE LOS DATOS DEL ID DEL REGISTRO Y
        ///LLENA EL GRIDVIEW DE LOS REGISTROS

        public void BinData()
        {
            if (!IsPostBack)
            {
                string cs5 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs5))
                {

                    ///CONSULTA EL ID DE LOS REGISTROS DEL Nº DE AUDITOR QUE INICIO SESION
                    SqlCommand cmd5 = new SqlCommand("Select * from LPA_FINDINGS where AUDITOR_ID = '" + TextBoxAudi.Text + "' ORDER BY ID_LPAFN DESC ", con);
                    con.Open();
                    ddlID.DataTextField = "ID_LPAFN";
                    ddlID.DataValueField = "ID_LPAFN";
                    ddlID.DataSource = cmd5.ExecuteReader();
                    ddlID.DataBind();
                }
            }
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                    //---------------------------------------------------------------\\
                    ///CONSULTA QUE EXTRAE LOS REGISTROS EN UN GRIDVIEW DEL AUDITOR QUE INICIO SESION

                    SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 LPA_FINDINGS.ID_LPAFN, LPA_FINDINGS._DATE, LPA_FINDINGS.TAIL_NUMBER, LINE.DESC_LINE, LPA_FINDINGS.AUDITOR_ID, LPA_QUESTIONS.DESC_QUESTION, " +
               " AC_ZONE.DESC_ZONE, LPA_FINDINGS.QUANTITY, UNITS.MEASURED_UNIT, SHIFT_TURNS.SHIFT_D, LPA_FINDINGS.FINDING_DESCRIPTION, LPA_FINDINGS.ImageData " +
               " FROM   LPA_FINDINGS INNER JOIN " +
               " LINE ON LPA_FINDINGS.ID_LN = LINE.ID_LN INNER JOIN " +
               " LPA_QUESTIONS ON LPA_FINDINGS.ID_LPAQ = LPA_QUESTIONS.ID_LPAQ INNER JOIN " +
               " AC_ZONE ON LPA_FINDINGS.ID_ACZ = AC_ZONE.ID_ACZ INNER JOIN " +
               " UNITS ON LPA_FINDINGS.ID_U = UNITS.ID_U INNER JOIN " +
               " SHIFT_TURNS ON LPA_FINDINGS.ID_SHIFT = SHIFT_TURNS.ID_SHIFT where AUDITOR_ID = '" + TextBoxAudi.Text + "' order by ID_LPAFN desc", con);

                    ///Contenedor de datos dataTable
                    DataTable dt = new DataTable();
                    ///Llenamos Data table
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
            
        }

        ///METODO  QUE INSERTA LOS DATOS DE LA INTERFACE FULL AUDIT  

        protected void btnINSERT_Click(object sender, EventArgs e)
        {
             
          HttpPostedFile postedFile = FileUpload1.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);

            int fileSize = postedFile.ContentLength;
            ///OBTENER LOS DATOS DE LA IMAGEN 
            int tamanio = FileUpload1.PostedFile.ContentLength;
            byte[] ImagenOriginal = new byte[tamanio];




            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
            {
                FileUpload1.PostedFile.InputStream.Read(ImagenOriginal, 0, tamanio);
                Bitmap ImagenOriginalBinaria = new Bitmap(FileUpload1.PostedFile.InputStream);
                string imagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(ImagenOriginal);
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


                ///Crear imagen Thumbnail
                System.Drawing.Image imtThumbnail;
                int tamanioThumb = 400;
                imtThumbnail = redimencionarImage(ImagenOriginalBinaria, tamanioThumb);
                byte[] bImageThumb = new byte[tamanioThumb];

                ImageConverter convertidor = new ImageConverter();
                bImageThumb = (byte[])convertidor.ConvertTo(imtThumbnail, typeof(byte[]));

                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    /// Esta sentencia nos permite realizar  la inserccion de los datos  a la  tabla LPA_FINDINGS
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO LPA_FINDINGS " +
                                  "(_DATE,TAIL_NUMBER,ID_LN,AUDITOR_ID,ID_LPAQ,ID_ACZ,QUANTITY,ID_U,ID_SHIFT,FINDING_DESCRIPTION,ImageData)" +
                                  " VALUES (@date,@t_n,@line,@auditor,@que,@ac,@quantit,@unit,@sh,@com,@image)";

                    ///OBTENER LOS DATOS FULL AUDIT 
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = TextBoxDate.Text;
                    cmd.Parameters.Add("@t_n", SqlDbType.Text).Value = TextBoxTN.Text;
                    cmd.Parameters.Add("@line", SqlDbType.Int).Value = DropDownLINE.SelectedValue;
                    cmd.Parameters.Add("@auditor", SqlDbType.Text).Value = TextBoxAudi.Text;
                    cmd.Parameters.Add("@que", SqlDbType.Int).Value = DropDownQues.SelectedValue;
                    cmd.Parameters.Add("@ac", SqlDbType.Int).Value = DropDownACZ.SelectedValue;
                    cmd.Parameters.Add("@quantit", SqlDbType.Int).Value = TextBoxQuantity.Text;
                    cmd.Parameters.Add("@sh", SqlDbType.Int).Value = DropDownSHIFT.SelectedValue;
                    cmd.Parameters.Add("@unit", SqlDbType.Int).Value = DropDownUnit.SelectedValue;
                    cmd.Parameters.Add("@com", SqlDbType.Text).Value = TextBoxDesc.Text;
                    cmd.Parameters.Add("@image", SqlDbType.Image).Value = bImageThumb;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                     "alert(' THE FINDING HAS BEEN INSERTED CORRECTLY ')", true);
                    //REFRESCA  LOS DROP Y EL GRID 
                    BinData();
                    lblMessage.Visible = false;
                    Limpiar();
                

                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Only images (.jpg and .png) can be uploaded";
                lblMessage.ForeColor = System.Drawing.Color.Red;

            }

        }


        protected void btnUPDATE_Click(object sender, EventArgs e)
        {
            
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    if (FileUpload1.HasFile)
                    {
                        //OBTENER LOS DATOS DE LA IMAGEN
                        HttpPostedFile postedFile = FileUpload1.PostedFile;
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string fileExtension = Path.GetExtension(fileName);
                        int fileSize = postedFile.ContentLength;

                        int tamanio = FileUpload1.PostedFile.ContentLength;
                        byte[] ImagenOriginal = new byte[tamanio];
                        if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
                        {
                            FileUpload1.PostedFile.InputStream.Read(ImagenOriginal, 0, tamanio);
                            Bitmap ImagenOriginalBinaria = new Bitmap(FileUpload1.PostedFile.InputStream);
                            string imagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(ImagenOriginal);
                            Stream stream = postedFile.InputStream;
                            BinaryReader binaryReader = new BinaryReader(stream);
                            byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                            ///Crear imagen Thumbnail
                            System.Drawing.Image imtThumbnail;
                            int tamanioThumb = 150;
                            imtThumbnail = redimencionarImage(ImagenOriginalBinaria, tamanioThumb);
                            byte[] bImageThumb = new byte[tamanioThumb];

                            ImageConverter convertidor = new ImageConverter();
                            bImageThumb = (byte[])convertidor.ConvertTo(imtThumbnail, typeof(byte[]));


                            //OBTENER LOS DATOS DE LOS CAMPOS DE FULLAUDIT 
                            SqlCommand cmd = new SqlCommand();
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = ddlID.SelectedValue;
                            cmd.Parameters.Add("@date", SqlDbType.Date).Value = TextBoxDate.Text;
                            cmd.Parameters.Add("@t_n", SqlDbType.Text).Value = TextBoxTN.Text;
                            cmd.Parameters.Add("@line", SqlDbType.Int).Value = DropDownLINE.SelectedValue;
                            cmd.Parameters.Add("@auditor", SqlDbType.Text).Value = TextBoxAudi.Text;
                            cmd.Parameters.Add("@que", SqlDbType.Int).Value = DropDownQues.SelectedValue;;
                            cmd.Parameters.Add("@ac", SqlDbType.Int).Value = DropDownACZ.SelectedValue;
                            cmd.Parameters.Add("@quantit", SqlDbType.Int).Value = TextBoxQuantity.Text;
                            cmd.Parameters.Add("@sh", SqlDbType.Int).Value = DropDownSHIFT.SelectedValue;
                            cmd.Parameters.Add("@unit", SqlDbType.Int).Value = DropDownUnit.SelectedValue;
                            cmd.Parameters.Add("@com", SqlDbType.Text).Value = TextBoxDesc.Text;
                            cmd.Parameters.Add("@image", SqlDbType.Image).Value = bImageThumb;
                            cmd.CommandText = "UPDATE LPA_FINDINGS " +
                                          " SET _DATE=@date,TAIL_NUMBER=@t_n,ID_LN=@line,AUDITOR_ID=@auditor,ID_LPAQ=@que,ID_ACZ=@ac,QUANTITY=@quantit,ID_U=@unit, ID_SHIFT=@sh,FINDING_DESCRIPTION=@com,ImageData=@image" +
                                          " WHERE ID_LPAFN=@id;";

                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                        "alert('THE FINDING HAS BEEN PROPERLY ACTULIZED ')", true);
                            //REFRESCA  LOS DROP Y EL GRID 
                            BinData();
                            lblMessage.Visible = false;
                            Limpiar();
                           
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Only images (.jpg and .png) can be uploaded";
                            lblMessage.ForeColor = System.Drawing.Color.Red;

                            ImgPreview.ImageUrl = "https://icons.iconarchive.com/icons/icontoaster/leox-graphite/256/read-only-icon.png";
                        }
                    }
                    else
                    {
                        ///DEFINE LA VARIBLE DEL TIPO DE DATO QUE ESTA EN LA BD Y LO IGUAL AL VALOR QUE TIENE EN EL CAMPO DE TEXTO DE LA INTERFAZ 
                        SqlCommand cmd = new SqlCommand();
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = ddlID.SelectedValue;
                        cmd.Parameters.Add("@date", SqlDbType.Date).Value = TextBoxDate.Text;
                        cmd.Parameters.Add("@t_n", SqlDbType.Text).Value = TextBoxTN.Text;
                        cmd.Parameters.Add("@line", SqlDbType.Int).Value = DropDownLINE.SelectedValue;
                        cmd.Parameters.Add("@auditor", SqlDbType.Text).Value = TextBoxAudi.Text;
                        cmd.Parameters.Add("@que", SqlDbType.Int).Value = DropDownQues.SelectedValue;
                        cmd.Parameters.Add("@ac", SqlDbType.Int).Value = DropDownACZ.SelectedValue;
                        cmd.Parameters.Add("@quantit", SqlDbType.Int).Value = TextBoxQuantity.Text;
                        cmd.Parameters.Add("@sh", SqlDbType.Int).Value = DropDownSHIFT.SelectedValue;
                        cmd.Parameters.Add("@unit", SqlDbType.Int).Value = DropDownUnit.SelectedValue;
                        cmd.Parameters.Add("@com", SqlDbType.Text).Value = TextBoxDesc.Text;

                        cmd.CommandText = "UPDATE LPA_FINDINGS " +
                                 " SET _DATE=@date,TAIL_NUMBER=@t_n,ID_LN=@line,AUDITOR_ID=@auditor,ID_LPAQ=@que,ID_ACZ=@ac,QUANTITY=@quantit,ID_U=@unit, ID_SHIFT=@sh,FINDING_DESCRIPTION=@com " +
                                 " WHERE ID_LPAFN=@id;";

                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert(' THE FINDING HAS BEEN PROPERLY ACTULIZED')", true);
                        //REFRESCA  LOS DROP Y EL GRID 
                        BinData();
                        lblMessage.Visible = false;
                        Limpiar();

                        
                    }
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert(' ERROR UPDATING LOG ')", true);
            }
        }


        //METODO LIMPIA  TODOS LOS CAMPOS UNA  VEZ INSERTADO Y ACTUALIZADO 
        public void Limpiar()
        {
            lblMessage.Text = "Upload Successful";
            ddlID.SelectedIndex = 0;
            TextBoxDate.Text = "";
            TextBoxTN.Text = "";
            DropDownLINE.SelectedIndex = 0;
            DropDownQues.SelectedIndex = 0;
            DropDownACZ.SelectedIndex = 0;
            TextBoxQuantity.Text = "";
            DropDownUnit.SelectedIndex = 0;
            DropDownSHIFT.SelectedIndex = 0;
            TextBoxDesc.Text = "";
            ImgPreview.ImageUrl = "https://icons.iconarchive.com/icons/hopstarter/soft-scraps/256/Image-JPEG-icon.png";
           

        }



        //METODO QUE PERMITE  SELECCIONAR EL ID DEL REGISTRO  QUE DESEEMOS MODIFICAR 
        protected void ddlID_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUPDATE.Enabled = true; /// PERMITE TENER HABILITADO EL BOTON  UPDATE
            btnINSERT.Enabled = false;/// PERMITE TENER INHABILITADO EL BOTON  INSERT

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))

            {
                ///OBTENERMOS LOS DATOS DE UNO DE LOS REGISTROS PARA PODER MOSTRARLOS EN LOS CAMPSOS DE  FULLAUDIT 
                SqlCommand cmd = new SqlCommand("SELECT CONVERT(VARCHAR(25),_DATE,105) as _DATE, TAIL_NUMBER, ID_LN, AUDITOR_ID, ID_LPAQ, ID_ACZ, QUANTITY, ID_U, ID_SHIFT, FINDING_DESCRIPTION, ImageData FROM LPA_FINDINGS WHERE ID_LPAFN='" + ddlID.SelectedValue + "'", con);
                con.Open();
                try

                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert(' DATA HAVE BEEN FOUND FROM THE FINDING')", true);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read() == true)
                    {

                        TextBoxTN.Text = rdr["TAIL_NUMBER"].ToString();
                        TextBoxDate.Text = (Convert.ToDateTime(rdr["_DATE"]).ToString("yyyy-MM-dd"));
                        TextBoxAudi.Text = rdr["AUDITOR_ID"].ToString();
                        TextBoxQuantity.Text = rdr["QUANTITY"].ToString();
                        TextBoxDesc.Text = rdr["FINDING_DESCRIPTION"].ToString();
                        DropDownLINE.SelectedValue = rdr["ID_LN"].ToString();
                        DropDownQues.SelectedValue = rdr["ID_LPAQ"].ToString();
                        DropDownACZ.SelectedValue = rdr["ID_ACZ"].ToString();
                        DropDownUnit.SelectedValue = rdr["ID_U"].ToString();
                        DropDownSHIFT.SelectedValue = rdr["ID_SHIFT"].ToString();
                        byte[] image = (byte[])rdr["ImageData"];
                        ImgPreview.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(image);
                    }
                    else
                    {
                        lblMessage.Text = "ERROR AL CONSULTAR ID";
                    }
                }
                catch (Exception)
                {

                    lblMessage.Text = "ERROR EN SENTENCIA SQL";
                }
                finally
                {
                    con.Close();
                }

            }

        }


       //METODO QUE NOS PERMITE  CERRAR LA SESION DEL USUARIO 


        protected void Button3_Click(object sender, EventArgs e)
        {
            //funcion que cierra la sesion del usuario

            Session.Abandon();
            Response.Redirect("Default.aspx");
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Cache-Control", "no-store");
        }


        /// <summary>
        /// METODO QUE PERMITE LA PAGINACION DEL GRIDVIEW
        /// </summary>
        protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            ///Maneja la paginacion
            GridView1.PageIndex = e.NewPageIndex;
            ///REFRESCA LOS DATOS DE LA CONSULTA
            BinData();
        }

        
        //METOD QUE NOS PERMITE ACTULIZAR TODA LA PAGINA 
        protected void btnREFRESH_Click1(object sender, EventArgs e)
        {
            BinData();
            Limpiar();
            Response.Redirect("FullAudit.aspx");

            btnINSERT.Enabled = true;
            btnUPDATE.Enabled = true;
        }

        public System.Drawing.Image redimencionarImage(System.Drawing.Image ImageOriginal, int alto)
        {
            var Radio = (double)alto / ImageOriginal.Height;
            var NuevoAncho = (int)(ImageOriginal.Width * Radio);
            var NuevoAlto = (int)(ImageOriginal.Height * Radio);
            var NuevaImageRedimencionada = new Bitmap(NuevoAncho, NuevoAlto);
            var g = Graphics.FromImage(NuevaImageRedimencionada);
            g.DrawImage(ImageOriginal, 0, 0, NuevoAncho, NuevoAlto);
            return NuevaImageRedimencionada;
        }
    }
}