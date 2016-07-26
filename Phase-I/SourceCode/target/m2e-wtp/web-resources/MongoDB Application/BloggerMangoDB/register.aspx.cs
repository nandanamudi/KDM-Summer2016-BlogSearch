using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using System.Web.Script.Serialization;
using BloggerMongoDB1;
public partial class register : System.Web.UI.Page
{
    //IMongoClient client = new MongoClient(ConfigurationManager.AppSettings["connectionString"]);
     //MongoClientSettings settings = new MongoClientSettings();
    
   
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox7.MaxLength = 10;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            const string connectionString = "mongodb://192.168.0.12";
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase database = server.GetDatabase("BlogDB");

            MongoCollection<program> collection = database.GetCollection<program>("UserDetails");
            MongoCursor<program> members = collection.FindAll();
            List<string> username11 = new List<string>();
            foreach (program p12 in members)
            {
                username11.Add(p12.Username);
            }

            string fname1 = string.Empty;
            string lname1 = string.Empty;
            string username1 = string.Empty;
            string password1 = string.Empty;
            string emailid1 = string.Empty;
            string cpassword1 = string.Empty;
            string age1 = string.Empty;
            string phno1 = string.Empty;

            fname1 = TextBox1.Text;
            lname1 = TextBox2.Text;
            username1 = TextBox3.Text;
            password1 = TextBox4.Text;
            cpassword1 = TextBox8.Text;
            emailid1 = TextBox5.Text;
            age1 = TextBox6.Text;
            phno1 = TextBox7.Text;

            var regexItem = new Regex("^[0-9 ]*$");
            bool pass = password1.Equals(cpassword1);
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailid1);

            if (fname1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter First Name' )</script>", false);

            }
            else if (regexItem.IsMatch(fname1))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Alphabets only' )</script>", false);
            }
            else if (lname1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Last Name' )</script>", false);

            }
            else if (regexItem.IsMatch(lname1))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Alphabets only' )</script>", false);
            }
            else if (username1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter User name' )</script>", false);

            }

            else if (username11.Contains(username1))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('User name Already Exists' )</script>", false);

            }
            else if (password1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Password' )</script>", false);

            }
            else if ((password1.Length < 6) || (password1.Length > 10))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Password Range should be 6-10 characters' )</script>", false);

            }
            else if (cpassword1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter confirm Password' )</script>", false);

            }

            else if (pass.Equals(false))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Check Confirm Password' )</script>", false);

            }
            else if (emailid1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter email address' )</script>", false);
            }
            else if (!(match.Success))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Email Address was not in correct Format' )</script>", false);
            }
            else if (age1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Age' )</script>", false);
            }
            else if (!(regexItem.IsMatch(age1)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Age Should have Numeric Values' )</script>", false);
            }
            else if (Convert.ToInt32(age1) < 15)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Age should be Greater than 15' )</script>", false);
            }

            else if (phno1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Phone Number' )</script>", false);
            }
            else if (!(regexItem.IsMatch(phno1)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Phone No should have Numeric Values' )</script>", false);
            }
            else if ((phno1.Length < 10) || (phno1.Length > 10))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter 10 digit Ph.no' )</script>", false);
            }

            else
            {

                MongoCollection collection1 = database.GetCollection<program>("UserDetails");

                //MongoCollection<program> collection1 = database.GetCollection<program>("UserDetails");

                BsonDocument UserDetails = new BsonDocument
             {  
               {"fname",fname1},  
               {"lname",lname1},  
               {"username",username1},  
               {"password", password1},  
               {"emailid", password1},  
               {"age",age1}  ,
               {"phno",@phno1}
            };

                collection1.Insert(UserDetails);


                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Your Account has been created');window.location='home.aspx';", true);

            }
        }
        catch (Exception e1)
        {
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}