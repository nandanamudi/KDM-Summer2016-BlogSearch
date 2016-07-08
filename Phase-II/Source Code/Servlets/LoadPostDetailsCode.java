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

/**
 * Servlet implementation class LoadPostDetailsCode
 */
@WebServlet("/LoadPostDetailsCode")
public class LoadPostDetailsCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public LoadPostDetailsCode() {
        super();
 
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	
		 HttpSession session = request.getSession(false);
		 String username = (String) session.getAttribute("username");
		ArrayList<postDetails> postlist = new ArrayList<postDetails>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("select p.postid, u.name,p.title,p.date1 from userdetails u,postdetails p where p.username=u.username and p.username='"+username+"'");
        ResultSet rs = pst.executeQuery();
        while(rs.next())
        {
        	postDetails p=new postDetails();
        	p.Postid11(rs.getString("postid"));
            p.Name(rs.getString("name"));
            p.Title(rs.getString("title"));             
            p.Date1(rs.getString("date1")); 
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
