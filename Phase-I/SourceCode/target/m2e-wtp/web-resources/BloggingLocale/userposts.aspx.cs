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

public partial class userposts : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    SqlCommand cmd;
    byte[] raw;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!(IsPostBack))
        {

            displayimage();

            loadGrid();

        }
        else { }
    }

    private void loadGrid()
    {
        try
        {
            DataSet ds = new DataSet();
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

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                int postid = Convert.ToInt32(ds.Tables[0].Rows[i]["postid"]);
                int postid1 = Convert.ToInt32(dt.Rows[i]["postid"]);
                if (postid == postid1)
                {
                    ds.Tables[0].Rows[i]["tagname"] = dt.Rows[i]["tags"].ToString();
                }
            }

            StringBuilder sb = new StringBuilder();
            DataTable dt123 = ds.Tables[0];
            if (dt123 != null)
                if (dt123.Rows.Count > 0)
                    foreach (DataRow dr in dt123.Rows)
                    {

                        sb.Append("<tr>");
                        sb.Append("<td class='postid' hidden='true'>");
                        sb.Append((dr["postid"]));
                        sb.Append("</td>");
                        sb.Append("<td class='name'>");
                        sb.Append((dr["name"]));
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append((dr["title"]));
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append((dr["tagname"]));
                        sb.Append("</td>");
                        sb.Append("<td class='date123'>");
                        sb.Append((dr["date1"]));
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("<button name='button' class='delete'>Delete</button>");
                        sb.Append("</td>");

                        sb.Append("</tr>");

                    }

            ltData.Text = sb.ToString();
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
   
}