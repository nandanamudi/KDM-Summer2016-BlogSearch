package BloggerSourceCode;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.text.SimpleDateFormat;
import java.util.Date;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import javax.swing.JOptionPane;

@WebServlet("/CommentPostCode")
public class CommentPostCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public CommentPostCode() {
        super();
        // TODO Auto-generated constructor stub
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
      
		 String comment = request.getParameter("comment"); 
		 String postid = request.getParameter("postid");
		  System.out.println(comment);
		 HttpSession session = request.getSession(false);
		 String username = (String) session.getAttribute("username");
		 Date dNow = new Date();
		 SimpleDateFormat date1 = new SimpleDateFormat("yyyy/MM/dd 'at' hh:mm:ss");
		 String date = date1.format(dNow).toString();	 

		 try{
			
			   Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			 	
     		    Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
     		
	             PreparedStatement pst = conn.prepareStatement("insert into comment_box(postid,username,comment,cdate) values(?,?,?,?)");
	              pst.setString(1,postid);  
	              pst.setString(2,username);   
	              pst.setString(3,comment);  
	              pst.setString(4,date);                       
	              int i = pst.executeUpdate();  	             
			      conn.close();
			    }catch(Exception ex){
			    	  System.out.println(ex.toString());
			    }
	
	}


	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	
		
		
	}

}
