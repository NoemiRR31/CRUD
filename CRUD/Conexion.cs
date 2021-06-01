using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    public class Conexion
    {
        SqlConnection conn;

        public bool Conectar() 
        {
            try
            {
                string miconexion = "";
                SqlConnectionStringBuilder miscSB = new SqlConnectionStringBuilder();
                miscSB.DataSource = "Prueba1.mssql.somee.com";
                miscSB.InitialCatalog = "Prueba1";
                miscSB.UserID = "auditlpa_SQLLogin_1";
                miscSB.Password = "6vfkzplgfl";
                miscSB.PersistSecurityInfo = true;
                miconexion = miscSB.ToString();
                conn = new SqlConnection(miconexion);
                conn.Open();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        
        }

        public void desconectar()
        {
            conn.Close();
        }
        //LLENA LOS GRIDWIEW Y LOS DROPLIST
        public DataSet DSET(string sentencia) 
        {
            DataSet ds = new DataSet();

            if (Conectar())
            {
                
                
                    SqlDataAdapter SDA = new SqlDataAdapter(sentencia, conn);
                    SDA.Fill(ds, "datos");
                
                
                
            }
            return ds;
        }
        //Metodo para Insertar, eliminar, actualizar
        public int IEA(string sentencia) 
        {
            int res = -1;
            if (Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sentencia, conn);
                    cmd.ExecuteNonQuery();
                    res = 0;
                }
                catch (SqlException mise)
                {
                    int error = Convert.ToInt32(mise);
                }
            }
            return res;
        }
    }
}