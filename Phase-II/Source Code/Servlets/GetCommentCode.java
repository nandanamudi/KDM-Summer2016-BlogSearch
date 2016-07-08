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

@WebServlet("/GetCommentCode")
public class GetCommentCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public GetCommentCode() {
        super();

    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		String postid=request.getParameter("postid");
		System.out.println(postid);
		ArrayList<commentDetails> postlist = new ArrayList<commentDetails>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("select ROW_NUMBER() OVER (ORDER BY c.cdate) AS sno, u.name,c.comment,c.cdate from userdetails u,comment_box c where c.username=u.username and c.postid='"+postid+"'");
        ResultSet rs = pst.executeQuery();
        while(rs.next())
        {
        	commentDetails c=new commentDetails();
            c.Name1(rs.getString("name"));
            c.Comment(rs.getString("comment"));  
            c.Date(rs.getString("cdate")); 
            c.SNO(rs.getString("sno"));
           
            postlist.add(c);
            
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
