package BloggerSourceCode;

import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import javax.swing.JOptionPane;

@WebServlet("/LoginCode")
public class LoginCode extends HttpServlet {
	private static final long serialVersionUID = 1L;
  
    public LoginCode() {
        super();
        }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		response.getWriter().append("Served at: ").append(request.getContextPath());
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	
		 String username = request.getParameter("username");
		 String password = request.getParameter("password");
		 String username11="";
		 String password11="";
		 
		 try{
				
			   Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			 	
   		        Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
   		
	             PreparedStatement pst = conn.prepareStatement("SELECT username, password FROM userdetails where username='"+username+"'");
	             ResultSet rs = pst.executeQuery();
	             while(rs.next())
	             {
	              username11=rs.getString("username");
	              password11=rs.getString("password");   
	             }   
	             
	               if ((username.equals(username11)) && (password.equals(password11)))
	                {
	            	   HttpSession session = request.getSession();
	            	   session.setAttribute("username", username);
	            	   response.sendRedirect("UserHome.html");  
	                }
	             
	                else
	                {
	                	JOptionPane.showMessageDialog(null, "Username or Password incorrect");	           	
		            	
		            	    response.sendRedirect("LoginPage.html");
	                } 	            	  
	             
			      conn.close();
			    }catch(Exception ex){
			    	  System.out.println(ex.toString());
			    }
		
	}

}
