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
public partial class register : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox7.MaxLength = 10;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> username11 = new List<string>();
            DataSet ds = new DataSet();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_username", con);
            cmd.CommandText = "sp_username";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                username11.Add(ds.Tables[0].Rows[i][0].ToString());
            }


            string fname = string.Empty;
            string lname = string.Empty;
            string username = string.Empty;
            string password = string.Empty;
            string emailid = string.Empty;
            string cpassword = string.Empty;
            string age = string.Empty;
            string phno = string.Empty;

            fname = TextBox1.Text;
            lname = TextBox2.Text;
            username = TextBox3.Text;
            password = TextBox4.Text;
            cpassword = TextBox8.Text;
            emailid = TextBox5.Text;
            age = TextBox6.Text;
            phno = TextBox7.Text;

            var regexItem = new Regex("^[0-9 ]*$");
            bool pass = password.Equals(cpassword);
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailid);

            if (fname.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter First Name' )</script>", false);

            }
            else if (regexItem.IsMatch(fname))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Alphabets only' )</script>", false);
            }
            else if (lname.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Last Name' )</script>", false);

            }
            else if (regexItem.IsMatch(lname))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Alphabets only' )</script>", false);
            }
            else if (username.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter User name' )</script>", false);

            }

            else if (username11.Contains(username))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('User name Already Exists' )</script>", false);

            }
            else if (password.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Password' )</script>", false);

            }
            else if ((password.Length < 6) || (password.Length > 10))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Password Range should be 6-10 characters' )</script>", false);

            }
            else if (cpassword.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter confirm Password' )</script>", false);

            }

            else if (pass.Equals(false))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Check Confirm Password' )</script>", false);

            }
            else if (emailid.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter email address' )</script>", false);
            }
            else if (!(match.Success))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Email Address was not in correct Format' )</script>", false);
            }
            else if (age.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Age' )</script>", false);
            }
            else if (!(regexItem.IsMatch(age)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Age Should have Numeric Values' )</script>", false);
            }
            else if (Convert.ToInt32(age) < 15)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Age should be Greater than 15' )</script>", false);
            }

            else if (phno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter Phone Number' )</script>", false);
            }
            else if (!(regexItem.IsMatch(phno)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Phone No should have Numeric Values' )</script>", false);
            }
            else if ((phno.Length < 10) || (phno.Length > 10))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Please Enter 10 digit Ph.no' )</script>", false);
            }

            else
            {
                con.Open();

                SqlCommand cmd1 = new SqlCommand("sp_userdetails", con);
                cmd1.CommandText = "sp_userdetails";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@fname", fname);
                cmd1.Parameters.AddWithValue("@lname", lname);
                cmd1.Parameters.AddWithValue("@username", username);
                cmd1.Parameters.AddWithValue("@password", password);
                cmd1.Parameters.AddWithValue("@emailid", emailid);
                cmd1.Parameters.AddWithValue("@age", age);
                cmd1.Parameters.AddWithValue("@phno", phno);

                int result = cmd1.ExecuteNonQuery();
                if (result > 0)
                {
                    string to = emailid;
                    string from = "ashok.yaganti68@gmail.com";
                    string subject = "Blogging Locale Account";
                    string body = "Your account has been created with Blogging Locale. Please use the username :" + username + " and password : " + password + " to login";
                    using (MailMessage mm = new MailMessage(from, to))
                    {
                        mm.Subject = subject;
                        mm.Body = body;
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(from, "Ysatyam1991");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                    }
                    ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Your Account has been created');window.location='Home_new.aspx';", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('Something went wrong with account creation')</script>", false);
                    Response.Redirect(ResolveUrl("register.aspx"));
                }

            }
        }
        catch (Exception e1)
        {
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Home_new.aspx");
    }
}