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

public partial class userhome : System.Web.UI.Page
{
    //DataSet ds24 = new DataSet();
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    SqlCommand cmd;
    byte[] raw;
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!(IsPostBack))
        {

            displayimage();
            loadGrid();
            
        }
        else
        {
            
        }
    }
 
    private void loadGrid()
    {
        try
        {
            DataSet ds24 = new DataSet();
            //ds24.Clear();
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
            da.Fill(ds24);
            con.Close();

            ds24.Tables[0].Columns.Add(new DataColumn("tagname"));

            for (int i = 0; i <= ds24.Tables[0].Rows.Count - 1; i++)
            {
                int postid = Convert.ToInt32(ds24.Tables[0].Rows[i]["postid"]);
                int postid1 = Convert.ToInt32(dt.Rows[i]["postid"]);
                if (postid == postid1)
                {
                    ds24.Tables[0].Rows[i]["tagname"] = dt.Rows[i]["tags"].ToString();
                }
            }

            GridView2.DataSource = ds24;
            GridView2.DataBind();
        }
        catch (Exception e1)
        {
        }

    }

    public DataSet BindDatatable()
    {
        DataSet ds = new DataSet();
        try
        {
           
            con.Open();
            SqlCommand cmd2 = new SqlCommand("sp_getpost", con);
            cmd2.CommandText = "sp_getpost";
            cmd2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            da.Fill(ds);
           
        }
        catch (Exception e1)
        {
        }
        return ds;
    }

    protected void change_click(object sender, EventArgs e)
    {

        //Response.Redirect("wait.aspx");
        FileUpload1.Visible = true;
        Button3.Visible = true;
    }

   
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
            if (e.CommandName == "view")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //string postid = GridView1.Rows[rowIndex].Cells[0].Text;
                //string name = GridView1.Rows[rowIndex].Cells[1].Text;
                //string date = GridView1.Rows[rowIndex].Cells[2].Text;


                Label postid1 = GridView1.Rows[rowIndex].FindControl("ID2") as Label;
                Label name1 = GridView1.Rows[rowIndex].FindControl("ID12") as Label;
                Label date1 = GridView1.Rows[rowIndex].FindControl("ID5") as Label;


                string postid11 = postid1.Text;
                string name11 = name1.Text;
                string date11 = date1.Text;


                Session["postid"] = postid11;
                Session["name"] = name11;
                Session["date"] = date11;
                Response.Redirect("usercomments.aspx");
            }
      
    }

    protected void gv_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
          if (e.CommandName == "view")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //string postid = GridView2.Rows[rowIndex].Cells[0].Text;
                //string name = GridView2.Rows[rowIndex].Cells[1].Text;
                //string date = GridView2.Rows[rowIndex].Cells[2].Text;


                Label postid1 = GridView2.Rows[rowIndex].FindControl("ID2") as Label;
                Label name1 = GridView2.Rows[rowIndex].FindControl("ID12") as Label;
                Label date1 = GridView2.Rows[rowIndex].FindControl("ID5") as Label;


                string postid11 = postid1.Text;
                string name11 = name1.Text;
                string date11 = date1.Text;


                Session["postid"] = postid11;
                Session["name"] = name11;
                Session["date"] = date11;
                Response.Redirect("usercomments.aspx");
            }
       
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if ((FileUpload1.FileName != ""))
            {
                FileUpload1.Visible = false;
                Button3.Visible = false;
                //to allow only jpg gif and png files to be uploaded.
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (((extension == ".jpg") || ((extension == ".gif") || (extension == ".png"))))
                {
                    string id = Convert.ToString(Session["username"]);

                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/pics/") + fileName);

                    FileStream fs = new FileStream(Server.MapPath("~/pics/") + fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);


                    raw = new byte[fs.Length];
                    fs.Read(raw, 0, Convert.ToInt32(fs.Length));

                    cmd = new SqlCommand("sp_userimage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@photo", raw);
                    cmd.Parameters.AddWithValue("@date1", DateTime.Now);
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                    {
                        Label7.Visible = false;
                        displayimage();


                    }
                    else
                    {
                        string script = "<script>alert('Something went wrong')</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", script);
                    }
                }
                else
                {
                    Label7.Text = "Only Jpg,gif or Png files are permitted";
                    Label7.Visible = true;
                }
            }
            else
            {
                Label7.Text = "Kindly Select a File.....";
                Label7.Visible = true;
            }
        }
        catch (Exception e1)
        {
        }
    }
    public void displayimage()
    {
        try
        {
            Label7.Visible = false;
            string id = Convert.ToString(Session["username"]);

            con.Open();
            cmd = new SqlCommand("Select img from userimage where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {

                Image1.ImageUrl = "~/Handler.ashx?id=" + id;
            }
            con.Close();
        }
        catch (Exception e1)
        {
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string title1 = TextBox3.Text;
            string post1 = TextBox2.Text;

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
                    //post = pl[m + 1] + " " + pl[m + 2] + " " + pl[m + 3] + " " + pl[m + 4] +" "+ post + " " + pl[m + 5] + " " + pl[m + 5] + " " + pl[m + 6] + " " + pl[m + 1] + " " + pl[m] + " " + pl[m + 1] + " " + pl[m + 1] + " " + pl[m + 2] + " " + pl[m + 2] + " " + pl[m + 1] + " " + pl[m + 1];
                    //title = pl[m] + " " + pl[m + 1] + " " + title + " " + pl[m + 2] + " " + pl[m];
                    con.Open();
                    string username = Convert.ToString(Session["username"]);
                    DateTime date = DateTime.Now;
                    SqlCommand cmd1 = new SqlCommand("sp_postdetails", con);
                    cmd1.CommandText = "sp_postdetails";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@postid", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd1.Parameters.AddWithValue("@username", username);
                    cmd1.Parameters.AddWithValue("@title", title);
                    cmd1.Parameters.AddWithValue("@post", post);
                    cmd1.Parameters.AddWithValue("@date1", date);
                    cmd1.CommandTimeout = 6000000;
                    cmd1.ExecuteNonQuery();
                    string result = cmd1.Parameters["@postid"].Value.ToString();
                    int result1 = Convert.ToInt32(result);
                    con.Close();
                    if (result1 > 0)
                    {
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
                                con.Open();
                                SqlCommand cmd2 = new SqlCommand("sp_tag", con);
                                cmd2.CommandText = "sp_tag";
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.Add("@tagid", SqlDbType.Int).Direction = ParameterDirection.Output;
                                cmd2.Parameters.AddWithValue("@tagname", list123.Key);
                                cmd2.Parameters.AddWithValue("@postid", result1);
                                cmd2.CommandTimeout = 6000000;
                                cmd2.ExecuteNonQuery();
                                string result123 = cmd2.Parameters["@tagid"].Value.ToString();
                                int result12 = Convert.ToInt32(result123);
                                con.Close();
                                k++;
                            }
                            else
                            {
                                break;
                            }
                        }


                        ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('you post has been posted');window.location='userhome.aspx';", true);
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Something went wrong with this')</script>", false);
                        Response.Redirect(ResolveUrl("userhome.aspx"));
                    }
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

        //loadGrid();
        GridView2.PageIndex = e.NewPageIndex;
       // GridView2.DataBind();
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

    public DataSet commoncode1(DataSet ds, DataTable dt)
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