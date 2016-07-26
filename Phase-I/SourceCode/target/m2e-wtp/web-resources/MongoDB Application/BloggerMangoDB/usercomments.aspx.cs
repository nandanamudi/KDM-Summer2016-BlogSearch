using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using System.Web.Script.Serialization;
public partial class usercomments : System.Web.UI.Page
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
            // displayimage();
            loadcomments();
        }
        string postid = Convert.ToString(Session["postid"]);
        string post = Convert.ToString(Session["post"]);
        string name = Convert.ToString(Session["name"]);
        string date = Convert.ToString(Session["date"]);

        TextBox1.Text = post;
        Label1.Text = name;
        Label2.Text = date;

    }

    protected void change_click(object sender, EventArgs e)
    {

        //Response.Redirect("wait.aspx");
        FileUpload1.Visible = true;
        Button3.Visible = true;
    }

     public void loadcomments()
     {
         try
         {
             DataTable dt = new DataTable();
             string postid = Convert.ToString(Session["postid"]);
             MongoServer server1 = client.GetServer();
             MongoDatabase database = server1.GetDatabase("BlogDB");
             MongoCollection<commentclass> collection1 = database.GetCollection<commentclass>("CommentBox");
             MongoCursor<commentclass> members = collection1.FindAll();

             dt.Columns.Add("Name");
             dt.Columns.Add("Comment");
             dt.Columns.Add("Date");

             foreach (var items in members)
             {
                 string postid11 = Convert.ToString(items.Postid);
                 if (postid == postid11)
                 {
                     DataRow dr = dt.NewRow();
                     dr["Name"] = items.Username;
                     dr["Comment"] = items.Comment;
                     dr["Date"] = items.Commentdate;
                     dt.Rows.Add(dr);
                 }
             }

             GridView1.DataSource = dt;
             GridView1.DataBind();
         }
         catch (Exception e1)
         {
         }
     }

    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    public void displayimage()
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("BlogDB");
            string username = Convert.ToString(Session["username"]);
            string postid = Convert.ToString(Session["postid"]);
            DateTime cdate = DateTime.Now;
            string comment = TextBox6.Text;

            if (comment.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Comment')</script>", false);
            }
            else
            {
                MongoCollection collection = database.GetCollection<commentclass>("CommentBox");

                BsonDocument CommentBox = new BsonDocument
             {  
               {"postid",postid},  
               {"username",username},  
               {"comment", comment},  
               {"commentdate", cdate}
            };

                collection.Insert(CommentBox);

                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Your comments has been recorded');window.location='usercomments.aspx';", true);


            }
        }
        catch (Exception e1)
        {
        }
    }
}
    
    

   
    
