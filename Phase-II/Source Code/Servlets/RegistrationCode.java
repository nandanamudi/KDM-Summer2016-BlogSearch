package BloggerSourceCode;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.swing.JOptionPane;

import scala.util.matching.Regex;

/**
 * Servlet implementation class RegistrationCode
 */
@WebServlet("/RegistrationCode")
public class RegistrationCode extends HttpServlet {
	private static final long serialVersionUID = 1L;
	
    public RegistrationCode() {
      
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	
		response.getWriter().append("Served at: ").append(request.getContextPath());
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		 String name = request.getParameter("name");
		 String username = request.getParameter("username");
		 String password = request.getParameter("password");
		 String cpassword = request.getParameter("cpassword");
		 String emailid = request.getParameter("emailid");
		 String address = request.getParameter("address");
		  
		 try{
			
			   Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			 	
     		    Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
     		
	             PreparedStatement pst = conn.prepareStatement("insert into userdetails(name,username,password,emailid,address) values(?,?,?,?,?)");
	              pst.setString(1,name);  
	              pst.setString(2,username);   
	              pst.setString(3,password);  
	              pst.setString(4,emailid);  
	              pst.setString(5,address);                
	              
	              int i = pst.executeUpdate();  
	              if(i!=0){  
	            	  JOptionPane.showMessageDialog(null, "Your registration was successful");
	            	  response.sendRedirect("LoginPage.html");
	              }  
	              else{  
	            	  JOptionPane.showMessageDialog(null, "Something went wrong with this request. Please try again.");
	            	  response.sendRedirect("LoginPage.html");               }  
			  
			      conn.close();
			    }catch(Exception ex){
			    	  System.out.println(ex.toString());
			    }
		 
	}

}
