using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;
using System.Web.Script.Serialization;

public partial class userhome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    const string connectionString = "mongodb://192.168.0.12";
    MongoClient client = new MongoClient(connectionString);
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!(IsPostBack))
        {

            //displayimage();

           loadGrid();

        }
        else { }
    }

    private void loadGrid()
    {
        try
        {
            MongoServer server1 = client.GetServer();
            MongoDatabase database = server1.GetDatabase("BlogDB");
            MongoCollection<postclass> collection1 = database.GetCollection<postclass>("PostDetails");
            MongoCursor<postclass> members = collection1.FindAll();

            DataTable dt = new DataTable();

            dt.Columns.Add("postid");
            dt.Columns.Add("post");
            dt.Columns.Add("name");
            dt.Columns.Add("title");
            dt.Columns.Add("tagname");
            dt.Columns.Add("date");

            foreach (var items in members)
            {

                DataRow dr = dt.NewRow();
                dr["postid"] = items.Id;
                dr["post"] = items.Post;
                dr["name"] = items.Fullname;
                dr["title"] = items.Title;
                for (int i = 0; i < items.tags.Length; i++)
                {
                    if (i == items.tags.Length - 1)
                    {
                        dr["tagname"] += items.tags[i];
                    }
                    else
                    {
                        dr["tagname"] += items.tags[i] + ", ";
                    }
                }
                dr["date"] = items.Date;
                dt.Rows.Add(dr);
            }

            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        catch (Exception e1)
        {
        }
    }

    protected void change_click(object sender, EventArgs e)
    {

        //Response.Redirect("wait.aspx");
        FileUpload1.Visible = true;
        Button3.Visible = true;
    }


    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "view")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                Label postid1 = GridView2.Rows[rowIndex].FindControl("ID2") as Label;
                Label post1 = GridView2.Rows[rowIndex].FindControl("post_id") as Label;
                Label name1 = GridView2.Rows[rowIndex].FindControl("ID12") as Label;
                Label date1 = GridView2.Rows[rowIndex].FindControl("ID5") as Label;


                string postid11 = postid1.Text;
                string post11 = post1.Text;
                string name11 = name1.Text;
                string date11 = date1.Text;


                Session["postid"] = postid11;
                Session["post"] = post11;
                Session["name"] = name11;
                Session["date"] = date11;
                Response.Redirect("comments.aspx");
            }
        }
        catch (Exception e1)
        {
        }
    }

    protected void gv_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "view")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                Label postid1 = GridView1.Rows[rowIndex].FindControl("ID2") as Label;
                Label post1 = GridView1.Rows[rowIndex].FindControl("post_id") as Label;
                Label name1 = GridView1.Rows[rowIndex].FindControl("ID12") as Label;
                Label date1 = GridView1.Rows[rowIndex].FindControl("ID5") as Label;


                string postid11 = postid1.Text;
                string post11 = post1.Text;
                string name11 = name1.Text;
                string date11 = date1.Text;


                Session["postid"] = postid11;
                Session["post"] = post11;
                Session["name"] = name11;
                Session["date"] = date11;
                Response.Redirect("comments.aspx");
            }
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string title1 = TextBox3.Text;
            string post1 = TextBox2.Text;

            string[] tagname = new string[7];

            if (title1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please mention title' )</script>", false);
            }
            else if (post1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter post data' )</script>", false);
            }
            else if (post1.Length > 3000)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Post data should be greater than 3000 characters' )</script>", false);
            }
            else
            {

                List<string> pl = new List<string>();
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");
                pl.Add("Diagnostic");
                pl.Add("Database");
                pl.Add("Decryption");
                pl.Add("Objects");
                pl.Add("Interface");
                pl.Add("admission");
                pl.Add("Encryption");
                pl.Add("authentication");
                pl.Add("Resolution ");
                pl.Add("Segmentation");
                pl.Add("Average");
                pl.Add("Business");
                pl.Add("Application");
                pl.Add("Programming");
                pl.Add("Deprivation");
                pl.Add("Environment");
                pl.Add("management");
                pl.Add("asynchronous");
                pl.Add("Automatic");
                pl.Add("Communication");
                pl.Add("Configuration");
                pl.Add("Adaption");
                pl.Add("Association");
                pl.Add("clearinghouse");
                pl.Add("acknowledgement");
                pl.Add("Connectionless");
                pl.Add("processor");
                pl.Add("Cartridge");
                pl.Add("abstract");
                pl.Add("Administrative");
                pl.Add("Architecture");
                pl.Add("Desktop");
                pl.Add("Analog");




                Random m_Rand = new Random();

  
                    string post = post1;
                    string title = title1;
                    //post = pl[m + 1] + " " + pl[m + 2] + " " + pl[m + 3] + " " + pl[m + 4] + " " + post + " " + pl[m + 5] + " " + pl[m + 5] + " " + pl[m + 6] + " " + pl[m + 1] + " " + pl[m] + " " + pl[m + 1] + " " + pl[m + 1] + " " + pl[m + 2] + " " + pl[m + 2] + " " + pl[m + 1] + " " + pl[m + 1];
                    //title = pl[m] + " " + pl[m + 1] + " " + title + " " + pl[m + 2] + " " + pl[m];

                    MongoServer server = client.GetServer();
                    MongoDatabase database = server.GetDatabase("BlogDB");
                    string[] postmsg = post.Split(' ');
                    Regex FindDup = new Regex(@"(.+)\1", RegexOptions.IgnoreCase);
                    Dictionary<string, Int32> list = new Dictionary<string, int>();

                    foreach (string word in postmsg)
                    {
                        if (list.ContainsKey(word))
                        {
                            list[word] += 1;
                        }
                        else
                        {
                            list.Add(word, 1);
                        }
                    }

                    foreach (var i in list.Keys.ToList())
                    {
                        if ((i.Equals("in")) || (i.Length < 4) || (i.Equals("am")) || (i.Equals("was")) || (i.Equals("were")) || (i.Equals("if")) || (i.Equals("is")) || (i.Equals("are")) || (i.Equals("were")) || (i.Equals("close")) || (i.Equals("open")) || (i.Equals("a")) || (i.Equals("that")) || (i.Equals("you")) || (i.Equals("to")) || (i.Equals("with")) || (i.Equals("where")) || (i.Equals("who")) || (i.Equals("of")) || (i.Equals("null")) || (i.Equals("or")) || (i.Equals("and")) || (i.Equals("can")) || (i.Equals("the")) || (i.Equals("The")))
                        {
                            list.Remove(i);
                        }
                    }


                    int k = 0;

                    foreach (KeyValuePair<string, Int32> list123 in list.OrderByDescending(key => key.Value))
                    {

                        if (k < 7)
                        {
                            tagname[k] = list123.Key;

                            k++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    MongoCollection collection = database.GetCollection<postclass>("PostDetails");
                    string username = Convert.ToString(Session["username"]);
                    string fullname = Convert.ToString(Session["fullname"]);
                    DateTime date = DateTime.Now;
                    BsonArray tagArray = new BsonArray();
                    foreach (var tag in tagname)
                    {
                        tagArray.Add(tag);
                    }

                    BsonDocument PostDetails = new BsonDocument
             {  
               {"username",username},
               {"fullname",fullname},
               {"title",title},  
               {"post", post},  
               {"postdate", date},
               {"tags",tagArray}
            };

                    collection.Insert(PostDetails);

                    ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('you post has been posted');window.location='userhome.aspx';", true);
                }
                        
        }
        catch (Exception e1)
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
        loadGrid();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox1.Checked)
            {
                if (CheckBox2.Checked)
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_tag_title_post();
                        }
                        else
                        {
                            bind_author_tag_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_tag_post();
                        }
                        else
                        {
                            bind_author_tag();
                        }
                    }
                }
                else
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_title_post();
                        }
                        else
                        {
                            bind_author_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_post();
                        }
                        else
                        {
                            bind_author();
                        }
                    }
                }
            }
            else
            {
                if (CheckBox2.Checked)
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_tag_title_post();
                        }
                        else
                        {
                            bind_tag_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_tag_post();
                        }
                        else
                        {
                            bind_tag();
                        }
                    }
                }
                else
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_title_post();
                        }
                        else
                        {
                            bind_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_post();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please check atleast one option' )</script>", false);
                        }
                    }
                }
            }
        }
        catch (Exception e1)
        {
        }

    }

    public void bind_author()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server11 = client.GetServer();
                MongoDatabase database = server11.GetDatabase("BlogDB");
                MongoCollection<postclass> collection11 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection11.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {

                    if (items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 1;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_tag()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server12 = client.GetServer();
                MongoDatabase database = server12.GetDatabase("BlogDB");
                MongoCollection<postclass> collection12 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection12.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if (fi >= 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        dr["tagname"] = search;
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 2;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }
    public void bind_title()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server13 = client.GetServer();
                MongoDatabase database = server13.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    if (items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 3;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server14 = client.GetServer();
                MongoDatabase database = server14.GetDatabase("BlogDB");
                MongoCollection<postclass> collection14 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection14.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    if (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 4;
                setreponsetime(id, time);

            }
        }
        catch (Exception e1)
        {
        }
    }
    public void bind_author_tag()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 5;
                setreponsetime(id, time);

            }
        }
        catch (Exception e1)
        {
        }
    }
    public void bind_author_title()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 6;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_author_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 7;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_tag_title()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 8;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }
    public void bind_tag_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 9;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_title_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {

                    if ((items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 10;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_author_tag_title()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0) || (items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 11;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }
    public void bind_author_title_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 13;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void bind_author_tag_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0) || (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 12;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }

    }

    public void bind_tag_title_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0) || (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 14;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }


    public void bind_author_tag_title_post()
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            GridView1.Visible = true;
            GridView2.Visible = false;
            search = TextBox1.Text;
            if (search.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please enter the text' )</script>", false);
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();

                MongoServer server15 = client.GetServer();
                MongoDatabase database = server15.GetDatabase("BlogDB");
                MongoCollection<postclass> collection13 = database.GetCollection<postclass>("PostDetails");
                MongoCursor<postclass> members = collection13.FindAll();


                dt.Columns.Add("postid");
                dt.Columns.Add("post");
                dt.Columns.Add("name");
                dt.Columns.Add("title");
                dt.Columns.Add("tagname");
                dt.Columns.Add("date");

                foreach (var items in members)
                {
                    int fi = Array.FindIndex(items.tags, t => t.Equals(search, StringComparison.InvariantCultureIgnoreCase));
                    if ((items.Fullname.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (fi >= 0) || (items.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) || (items.Post.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["postid"] = items.Id;
                        dr["post"] = items.Post;
                        dr["name"] = items.Fullname;
                        dr["title"] = items.Title;
                        for (int i = 0; i < items.tags.Length; i++)
                        {
                            if (i == items.tags.Length - 1)
                            {
                                dr["tagname"] += items.tags[i];
                            }
                            else
                            {
                                dr["tagname"] += items.tags[i] + ", ";
                            }
                        }
                        dr["date"] = items.Date;
                        dt.Rows.Add(dr);
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000d;
                int id = 15;
                setreponsetime(id, time);
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void setreponsetime(int id, double time)
    {
        try
        {
            con.Open();
            SqlCommand cmd22 = new SqlCommand("sp_responsetime_mongo1000000", con);
            cmd22.CommandText = "sp_responsetime_mongo1000000";
            cmd22.CommandType = CommandType.StoredProcedure;
            cmd22.Parameters.AddWithValue("@id", id);
            cmd22.Parameters.AddWithValue("@restimemongo", time);
            int res = cmd22.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e1)
        {
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            if (CheckBox1.Checked)
            {
                if (CheckBox2.Checked)
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_tag_title_post();
                        }
                        else
                        {
                            bind_author_tag_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_tag_post();
                        }
                        else
                        {
                            bind_author_tag();
                        }
                    }
                }
                else
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_title_post();
                        }
                        else
                        {
                            bind_author_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_author_post();
                        }
                        else
                        {
                            bind_author();
                        }
                    }
                }
            }
            else
            {
                if (CheckBox2.Checked)
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_tag_title_post();
                        }
                        else
                        {
                            bind_tag_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_tag_post();
                        }
                        else
                        {
                            bind_tag();
                        }
                    }
                }
                else
                {
                    if (CheckBox3.Checked)
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_title_post();
                        }
                        else
                        {
                            bind_title();
                        }
                    }
                    else
                    {
                        if (CheckBox4.Checked)
                        {
                            bind_post();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please check atleast one option' )</script>", false);
                        }
                    }
                }
            }
        }
        catch (Exception e1)
        {
        }
    }
}