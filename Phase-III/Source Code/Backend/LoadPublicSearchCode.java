package BloggerSourceCode;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import com.google.gson.Gson;

@WebServlet("/LoadPublicSearchCode")
public class LoadPublicSearchCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public LoadPublicSearchCode() {
        super();

    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		ArrayList<postDetails> postlist = new ArrayList<postDetails>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("select p.postid,p.post, u.name,p.title,p.date1 from userdetails u,postdetails p where p.username=u.username");
        ResultSet rs = pst.executeQuery();
        while(rs.next())
        {
        	postDetails p=new postDetails();
        	p.Postid11(rs.getString("postid"));
        	String id=rs.getString("postid");
        	p.Post1(rs.getString("post"));
            p.Name(rs.getString("name"));
            p.Title(rs.getString("title"));             
            p.Date1(rs.getString("date1")); 
            p.ViewUrl("<a href='ViewPost.html?postid="+id+"' class='btn btn-md btn-success'>View</a>");
            postlist.add(p);
            
        }   
	   }
	   catch(Exception e)
	   {
	   }
	   
	     response.setContentType("application/json");
	     new Gson().toJson(postlist, response.getWriter());
		 System.out.println(postlist.size());
	
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		
	}

}
