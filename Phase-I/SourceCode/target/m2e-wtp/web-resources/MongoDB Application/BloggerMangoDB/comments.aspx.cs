using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using System.Web.Script.Serialization;
using BloggerMongoDB1;
public partial class comments : System.Web.UI.Page
{
    const string connectionString = "mongodb://192.168.0.12";
    MongoClient client = new MongoClient(connectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedin"] == null)
        {
            ImageButton1.Enabled = false;
            TextBox6.Enabled = false;
          
        }

        if (!IsPostBack)
        {
         //   loadcomments();
        }

        string postid = Convert.ToString(Session["postid"]);
        string post = Convert.ToString(Session["post"]);
        string name = Convert.ToString(Session["name"]);
        string date = Convert.ToString(Session["date"]);        

        TextBox1.Text = post;
        Label1.Text = name;
        Label2.Text = date;

    }

    //public void loadcomments()
    //{
    //    DataSet ds11 = new DataSet();
    //    string postid = Convert.ToString(Session["postid"]);
    //    con.Open();
    //    SqlCommand cmd1111 = new SqlCommand("sp_getcomments", con);
    //    cmd1111.CommandText = "sp_getcomments";
    //    cmd1111.CommandType = CommandType.StoredProcedure;
    //    cmd1111.Parameters.AddWithValue("@postid", postid);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd1111);
    //    da.Fill(ds11);
    //    con.Close();
    //    GridView1.DataSource = ds11;
    //    GridView1.DataBind();
    //}

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            string username = TextBox4.Text;
            string password = TextBox5.Text;

            string username11 = string.Empty;
            string password11 = string.Empty;



            if (username.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter User Name')</script>", false);
            }
            else if (password.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Password')</script>", false);
            }
            else
            {

                MongoServer server = client.GetServer();

                MongoDatabase database = server.GetDatabase("BlogDB");

                MongoCollection<program> collection = database.GetCollection<program>("UserDetails");

                MongoCursor<program> members = collection.FindAll();

                foreach (program p12 in members)
                {
                    if (username == p12.Username)
                    {
                        password11 = p12.Password;
                    }
                }

                if (password == password11)
                {
                    Session["username"] = username;
                    Session["loggedin"] = "created";
                    Response.Redirect("userhome.aspx");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('User Name or Password incorrect')</script>", false);
                    //Response.Redirect(ResolveUrl("login.aspx"));
                }
            }
        }
        catch (Exception e1)
        {
        }
        
    }
}