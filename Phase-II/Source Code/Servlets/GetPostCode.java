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

import com.google.gson.Gson;

@WebServlet("/GetPostCode")
public class GetPostCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public GetPostCode() {
        super();
      
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
 
		String postid=request.getParameter("postid");
		System.out.println(postid);
		ArrayList<postDetails> postlist = new ArrayList<postDetails>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("select u.name,u.emailid,p.post,p.date1 from userdetails u,postdetails p where p.username=u.username and p.postid='"+postid+"'");
        ResultSet rs = pst.executeQuery();
        while(rs.next())
        {
        	postDetails p=new postDetails();
            p.Name(rs.getString("name"));
            p.Post1(rs.getString("post"));  
            p.Email(rs.getString("emailid")); 
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
