using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class comments : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["loggedin"] == null)
            {
                ImageButton1.Enabled = false;
                TextBox6.Enabled = false;

            }

            if (!IsPostBack)
            {
                loadcomments();
            }

            string post = string.Empty;
            DataSet ds = new DataSet();
            //string postid = Request.QueryString["postid"];
            //string name = Request.QueryString["name"];
            //string date = Request.QueryString["date"];
            string postid = Convert.ToString(Session["postid"]);
            string name = Convert.ToString(Session["name"]);
            string date = Convert.ToString(Session["date"]);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_getpostmessage", con);
            cmd.CommandText = "sp_getpostmessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@postid", postid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {

                post = ds.Tables[0].Rows[0][0].ToString();
            }

            TextBox1.Text = post;
            Label1.Text = name;
            Label2.Text = date;
        }
        catch (Exception e1)
        {
        }

    }

    public void loadcomments()
    {
        try
        {
            DataSet ds11 = new DataSet();
            string postid = Convert.ToString(Session["postid"]);
            con.Open();
            SqlCommand cmd1111 = new SqlCommand("sp_getcomments", con);
            cmd1111.CommandText = "sp_getcomments";
            cmd1111.CommandType = CommandType.StoredProcedure;
            cmd1111.Parameters.AddWithValue("@postid", postid);
            SqlDataAdapter da = new SqlDataAdapter(cmd1111);
            da.Fill(ds11);
            con.Close();
            GridView1.DataSource = ds11;
            GridView1.DataBind();
        }
        catch (Exception e1)
        {
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Home_new.aspx");
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
}