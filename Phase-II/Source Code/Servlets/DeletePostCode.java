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

@WebServlet("/DeletePostCode")
public class DeletePostCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public DeletePostCode() {
        super();
     
    }


	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	
		
		 String postid = request.getParameter("postid");		 
		  System.out.println(postid);

		 try{
			
			   Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			 	
     		    Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
     		
	             PreparedStatement pst = conn.prepareStatement("delete postdetails where postid='"+postid+"'");	                                
	              int i = pst.executeUpdate(); 
	              System.out.println(i);
	               if(i>0)
	               {
	            	   PreparedStatement pst1 = conn.prepareStatement("delete tag where postid='"+postid+"'");	
	            	   int j = pst1.executeUpdate();
	            	   System.out.println(j);
	               }
			      conn.close();
			    }catch(Exception ex){
			    	  System.out.println(ex.toString());
			    }
		
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

	}

}
