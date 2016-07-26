using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class statistics : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    SqlCommand cmd;
    byte[] raw;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!(IsPostBack))
        {

            displayimage();

        }
        else { }
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

    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            Chart1.Visible = true;
            int option = Convert.ToInt32(DropDownList1.SelectedValue);

            if (option == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter First Name' )</script>", false);
            }
            else if (option == 1)
            {
                chart10000();
            }
            else if (option == 2)
            {
                chart100000();
            }
            else if (option == 3)
            {
                chart200000();
            }
            else if (option == 4)
            {
                chart300000();
            }
            else if (option == 5)
            {
                chart400000();
            }
            else if (option == 6)
            {
                chart500000();
            }
            else if (option == 7)
            {
                chart600000();
            }
            else if (option == 8)
            {
                chart700000();
            }
            else if (option == 9)
            {
                chart800000();
            }
            else if (option == 10)
            {
                chart900000();
            }
            else if (option == 11)
            {
                chart1000000();
            }
            else
            {
                chartindex();
            }
        }
        catch (Exception e1)
        {
        }
    }

    public void chart10000()
    {
        try
        {
            DataSet ds = new DataSet();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime10000", con);
            cmd11.CommandText = "sp_getresponsetime10000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }

    }

    public void chart100000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime100000", con);
            cmd11.CommandText = "sp_getresponsetime100000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart200000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime200000", con);
            cmd11.CommandText = "sp_getresponsetime200000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart300000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime300000", con);
            cmd11.CommandText = "sp_getresponsetime300000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart400000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime400000", con);
            cmd11.CommandText = "sp_getresponsetime400000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart500000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime500000", con);
            cmd11.CommandText = "sp_getresponsetime500000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart600000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime600000", con);
            cmd11.CommandText = "sp_getresponsetime600000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart700000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime700000", con);
            cmd11.CommandText = "sp_getresponsetime700000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart800000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime800000", con);
            cmd11.CommandText = "sp_getresponsetime800000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart900000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime900000", con);
            cmd11.CommandText = "sp_getresponsetime900000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }
    public void chart1000000()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetime1000000", con);
            cmd11.CommandText = "sp_getresponsetime1000000";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds);
            con.Close();
            bindchart(ds);
        }
        catch (Exception e1)
        {
        }
    }

    public void chartindex()
    {
        try
        {
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            ds1.Clear();
            ds2.Clear();
            con.Open();
            SqlCommand cmd11 = new SqlCommand("sp_getresponsetimeindex", con);
            cmd11.CommandText = "sp_getresponsetimeindex";
            cmd11.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            da.Fill(ds1);

            bindchart1(ds1);
        }
        catch (Exception e1)
        {
        }
    }

    public void bindchart(DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
                Chart1.BorderlineColor = System.Drawing.Color.FromArgb(26, 59, 105);
                Chart1.BorderlineWidth = 3;
                Chart1.BackColor = Color.RoyalBlue;

                Chart1.ChartAreas.Add("chtArea");
                Chart1.ChartAreas[0].AxisX.Title = "Search Category";
                Chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                Chart1.ChartAreas[0].AxisX.Interval = 1;
                Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
                Chart1.ChartAreas[0].AxisY.Title = "Response Time";
                Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
                Chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                Chart1.ChartAreas[0].BorderWidth = 2;

                Chart1.Legends.Add("restimesql");
                Chart1.Series.Add("restimesql");
                Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                Chart1.Series[0].Points.DataBindXY(ds.Tables[0].DefaultView, "serchtype", ds.Tables[0].DefaultView, "restimesql");

                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].ToolTip = "Data Point Y Value: #VALY{G}";

                // Setting Line Width
                Chart1.Series[0].BorderWidth = 3;
                Chart1.Series[0].Color = Color.Red;


                Chart1.Series.Add("restimemongo");
                Chart1.Series[1].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                Chart1.Series[1].Points.DataBindXY(ds.Tables[0].DefaultView, "serchtype", ds.Tables[0].DefaultView, "restimemongo");

                Chart1.Series[1].IsVisibleInLegend = true;
                Chart1.Series[1].IsValueShownAsLabel = true;
                Chart1.Series[1].ToolTip = "Data Point Y Value: #VALY{G}";

                Chart1.Series[1].BorderWidth = 3;
                Chart1.Series[1].Color = Color.Green;

                // Setting Line Shadow
                //Chart1.Series[0].ShadowOffset = 5;

                //Legend Properties
                Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                Chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
                Chart1.Legends[0].Docking = Docking.Bottom;
            }
        }
        catch (Exception e1)
        {
        }
    }


    public void bindchart1(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            Chart1.BorderlineColor = System.Drawing.Color.FromArgb(26, 59, 105);
            Chart1.BorderlineWidth = 3;
            Chart1.BackColor = Color.RoyalBlue;

            Chart1.ChartAreas.Add("chtArea");
            
            Chart1.ChartAreas[0].AxisX.Title = "Search Category";
            Chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas[0].AxisY.Title = "Response Time";
            Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
            Chart1.ChartAreas[0].BorderWidth = 2;
            Chart1.ChartAreas[0].AxisY.Interval=500;

            Chart1.Legends.Add("sql");
            Chart1.Series.Add("sql");
            Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            Chart1.Series[0].Points.DataBindXY(ds.Tables[0].DefaultView, "serchtype", ds.Tables[0].DefaultView, "sql");

            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].ToolTip = "Data Point Y Value: #VALY{G}";

            // Setting Line Width
            Chart1.Series[0].BorderWidth = 3;
            Chart1.Series[0].Color = Color.Red;


            Chart1.Series.Add("mongo");
            Chart1.Series[1].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            Chart1.Series[1].Points.DataBindXY(ds.Tables[0].DefaultView, "serchtype", ds.Tables[0].DefaultView, "mongo");

            Chart1.Series[1].IsVisibleInLegend = true;
            Chart1.Series[1].IsValueShownAsLabel = true;
            Chart1.Series[1].ToolTip = "Data Point Y Value: #VALY{G}";

            Chart1.Series[1].BorderWidth = 3;
            Chart1.Series[1].Color = Color.Green;



            Chart1.Series.Add("sqlindex");
            Chart1.Series[2].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            Chart1.Series[2].Points.DataBindXY(ds.Tables[0].DefaultView, "serchtype", ds.Tables[0].DefaultView, "sqlindex");

            Chart1.Series[2].IsVisibleInLegend = true;
            Chart1.Series[2].IsValueShownAsLabel = true;
            Chart1.Series[2].ToolTip = "Data Point Y Value: #VALY{G}";

            Chart1.Series[2].BorderWidth = 3;
            Chart1.Series[2].Color = Color.DarkBlue;



            Chart1.Series.Add("mongoindex");
            Chart1.Series[3].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            Chart1.Series[3].Points.DataBindXY(ds.Tables[0].DefaultView, "serchtype", ds.Tables[0].DefaultView, "mongoindex");

            Chart1.Series[3].IsVisibleInLegend = true;
            Chart1.Series[3].IsValueShownAsLabel = true;
            Chart1.Series[3].ToolTip = "Data Point Y Value: #VALY{G}";

            Chart1.Series[3].BorderWidth = 3;
            Chart1.Series[3].Color = Color.DarkViolet;

            // Setting Line Shadow
            //Chart1.Series[0].ShadowOffset = 5;

            //Legend Properties
            Chart1.Legends[0].LegendStyle = LegendStyle.Table;
            Chart1.Legends[0].TableStyle = LegendTableStyle.Wide;
            Chart1.Legends[0].Docking = Docking.Bottom;
        }
    }
}

     