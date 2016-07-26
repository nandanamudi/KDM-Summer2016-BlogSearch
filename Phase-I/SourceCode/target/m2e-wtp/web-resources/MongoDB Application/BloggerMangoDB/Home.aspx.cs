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
using System.Diagnostics;

public partial class Home : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    const string connectionString = "mongodb://192.168.0.12";
    MongoClient client = new MongoClient(connectionString);
    string search = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedin"] == null)
        {
            Button1.Enabled = false;
            Button2.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;

        }

        if (!IsPostBack)
        {
           loadGrid();
        }
    }

    private void loadGrid()
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
            int lm = 0;
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
               
            }

            GridView2.DataSource = dt;
            
            GridView2.DataBind();
      

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
        try
        {

            string username = TextBox4.Text;
            string password = TextBox5.Text;

            string username11 = string.Empty;
            string password11 = string.Empty;
            string fname = string.Empty;
            string lname = string.Empty;



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
                        fname = p12.Fname;
                        lname = p12.Lname;

                    }
                }

                if (password == password11)
                {
                    string fullname = fname + " " + lname;
                    Session["fname"] = fname;
                    Session["lname"] = lname;
                    Session["fullname"] = fullname;
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

