using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;

public partial class Home_new : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);

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
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            DataSet ds1 = new DataSet();

            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_tags", con);
            cmd11.CommandText = "sp_tags";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd11);
            da1.Fill(ds1);
            con.Close();

            Dictionary<int, string> tags = new Dictionary<int, string>();
            int pid;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                pid = Convert.ToInt32(ds1.Tables[0].Rows[i]["postid"]);
                if (tags.ContainsKey(pid))
                {
                    string temp = tags[pid];
                    temp = temp + ", " + ds1.Tables[0].Rows[i]["tagname"].ToString();
                    tags[pid] = temp;
                }
                else
                {
                    tags.Add(Convert.ToInt32(ds1.Tables[0].Rows[i]["postid"]), ds1.Tables[0].Rows[i]["tagname"].ToString());
                }
            }
            DataTable dt = new DataTable();

            dt.Columns.Add("postid");
            dt.Columns.Add("tags");
            foreach (var items in tags)
            {

                DataRow dr = dt.NewRow();
                dr["postid"] = items.Key;
                dr["tags"] = items.Value;
                dt.Rows.Add(dr);
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_getpostdetails", con);
            cmd.CommandText = "sp_getpostdetails";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            ds.Tables[0].Columns.Add(new DataColumn("tagname"));
            int lm = 0;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
               
                    int postid = Convert.ToInt32(ds.Tables[0].Rows[i]["postid"]);

                    int postid1 = 0;

                    if (dt.Rows.Count > 0)
                    {
                        postid1 = Convert.ToInt32(dt.Rows[i]["postid"]);
                    }
                    if (postid == postid1)
                    {
                        ds.Tables[0].Rows[i]["tagname"] = dt.Rows[i]["tags"].ToString();
                    }
              
            }

            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
        catch (Exception e1)
        {
        }

    }

    //protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    GridView1.DataBind();
    //   // loadGrid();

    //}
     protected void Gridview2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
        loadGrid();

    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "view")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string postid = GridView1.Rows[rowIndex].Cells[0].Text;
                string name = GridView1.Rows[rowIndex].Cells[1].Text;
                string date = GridView1.Rows[rowIndex].Cells[2].Text;


                Label postid1 = GridView1.Rows[rowIndex].FindControl("ID2") as Label;
                Label name1 = GridView1.Rows[rowIndex].FindControl("ID12") as Label;
                Label date1 = GridView1.Rows[rowIndex].FindControl("ID5") as Label;


                string postid11 = postid1.Text;
                string name11 = name1.Text;
                string date11 = date1.Text;


                Session["postid"] = postid11;
                Session["name"] = name11;
                Session["date"] = date11;
                Response.Redirect("comments.aspx");
            }
        }
        catch (Exception e1)
        {
        }
    }

    protected void gv_RowCommand2(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "view")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string postid = GridView2.Rows[rowIndex].Cells[0].Text;
                string name = GridView2.Rows[rowIndex].Cells[1].Text;
                string date = GridView2.Rows[rowIndex].Cells[2].Text;


                Label postid1 = GridView2.Rows[rowIndex].FindControl("ID2") as Label;
                Label name1 = GridView2.Rows[rowIndex].FindControl("ID12") as Label;
                Label date1 = GridView2.Rows[rowIndex].FindControl("ID5") as Label;


                string postid11 = postid1.Text;
                string name11 = name1.Text;
                string date11 = date1.Text;


                Session["postid"] = postid11;
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
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_logindetails", con);
                cmd.CommandText = "sp_logindetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();



                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    username11 = ds.Tables[0].Rows[0]["username"].ToString();
                    password11 = ds.Tables[0].Rows[0]["password"].ToString();
                }


                if ((username == username11) && (password == password11))
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_authorsearch", con);
                cmd.CommandText = "sp_authorsearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@author", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                DataSet ds12 = commoncode1(ds, dt);

                GridView1.DataSource = ds12;
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
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_tagsearch", con);
                cmd.CommandText = "sp_tagsearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tag", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_titlesearch", con);
                cmd.CommandText = "sp_titlesearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@title", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_postsearch", con);
                cmd.CommandText = "sp_postsearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@post", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_tag_search", con);
                cmd.CommandText = "sp_author_tag_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_title_search", con);
                cmd.CommandText = "sp_author_title_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_post_search", con);
                cmd.CommandText = "sp_author_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_tag_title_search", con);
                cmd.CommandText = "sp_tag_title_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_tag_post_search", con);
                cmd.CommandText = "sp_tag_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_title_post_search", con);
                cmd.CommandText = "sp_title_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_tag_title_search", con);
                cmd.CommandText = "sp_author_tag_title_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_title_post_search", con);
                cmd.CommandText = "sp_author_title_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_tag_post_search", con);
                cmd.CommandText = "sp_author_tag_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_tag_tilte_post_search", con);
                cmd.CommandText = "sp_tag_tilte_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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
                DataTable dt = commoncode();
                DataSet ds = new DataSet();
                ds.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_author_tag_title_post_search", con);
                cmd.CommandText = "sp_author_tag_title_post_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@text", search);
                cmd.CommandTimeout = 6000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                DataSet ds12 = commoncode1(ds, dt);
                GridView1.DataSource = ds12;
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

    public DataTable commoncode()
    {
        DataTable dt = new DataTable();
        try
        {
            DataSet ds1 = new DataSet();
            ds1.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_tags", con);
            cmd11.CommandText = "sp_tags";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd11);
            da1.Fill(ds1);
            con.Close();

            Dictionary<int, string> tags = new Dictionary<int, string>();
            int pid;

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                pid = Convert.ToInt32(ds1.Tables[0].Rows[i]["postid"]);
                if (tags.ContainsKey(pid))
                {
                    string temp = tags[pid];
                    temp = temp + ", " + ds1.Tables[0].Rows[i]["tagname"].ToString();
                    tags[pid] = temp;
                }
                else
                {
                    tags.Add(Convert.ToInt32(ds1.Tables[0].Rows[i]["postid"]), ds1.Tables[0].Rows[i]["tagname"].ToString());
                }
            }
           
            dt.Clear();
            dt.Columns.Add("postid");
            dt.Columns.Add("tags");
            foreach (var items in tags)
            {

                DataRow dr = dt.NewRow();
                dr["postid"] = items.Key;
                dr["tags"] = items.Value;
                dt.Rows.Add(dr);
            }
           
        }
        catch (Exception e1)
        {
        }
        return dt;
    }

    public DataSet commoncode1(DataSet ds,DataTable dt)
    {
        try
        {
            ds.Tables[0].Columns.Add(new DataColumn("tagname"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int postid = Convert.ToInt32(ds.Tables[0].Rows[i]["postid"]);
                int postid1 = 0;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    postid1 = Convert.ToInt32(dt.Rows[j]["postid"]);

                    if (postid == postid1)
                    {
                        ds.Tables[0].Rows[i]["tagname"] = dt.Rows[j]["tags"].ToString();
                    }
                }
            }
          
        }
        catch (Exception e1)
        {
        }
        return ds;
    }

    public void setreponsetime(int id, double time)
    {
        try
        {
            con.Open();
            SqlCommand cmd22 = new SqlCommand("sp_responsetime_sql1000000", con);
            cmd22.CommandText = "sp_responsetime_sql1000000";
            cmd22.CommandType = CommandType.StoredProcedure;
            cmd22.Parameters.AddWithValue("@id", id);
            cmd22.Parameters.AddWithValue("@restimesql", time);
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
