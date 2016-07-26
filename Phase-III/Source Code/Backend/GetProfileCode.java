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

@WebServlet("/GetProfileCode")
public class GetProfileCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public GetProfileCode() {
        super();
       
    }


	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		 HttpSession session = request.getSession(false);
		 String username = (String) session.getAttribute("username");
		ArrayList<profile> pd = new ArrayList<profile>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("select name,username,emailid,address from userdetails where username='"+username+"'");
        ResultSet rs = pst.executeQuery();
        while(rs.next())
        {
        	profile p=new profile();
        	p.Name123(rs.getString("name"));        	
            p.Username123(rs.getString("username"));
            p.Emailid123(rs.getString("emailid"));             
            p.Address123(rs.getString("address"));
           System.out.println(rs.getString("username"));
            pd.add(p);
            
        }   
	   }
	   catch(Exception e)
	   {
	   }
	   
	     response.setContentType("application/json");
	     new Gson().toJson(pd, response.getWriter());
	    // System.out.println(pd);
		
		
	}


	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

	}

}
