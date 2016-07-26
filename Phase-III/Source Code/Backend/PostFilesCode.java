package BloggerSourceCode;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Writer;
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

import org.apache.commons.io.FileUtils;

@WebServlet("/PostFilesCode")
public class PostFilesCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public PostFilesCode() {
        super();
       
    }


	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		 BufferedWriter bufferedWriter = null;
		 File outputFile = new File(getServletContext().getRealPath("/") + "/statistics.csv");	
	      
		ArrayList<Statistics> postlist = new ArrayList<Statistics>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("SELECT post from postdetails");
       ResultSet rs = pst.executeQuery();
       PreparedStatement pst1 = conn.prepareStatement("SELECT count(*) as count from postdetails");
       ResultSet rs1 = pst1.executeQuery();
      
       int i=1;
       int count=0;
      
         while(rs1.next())
         { 
            count=rs1.getInt("count");  
         }
         System.out.println(count);
          while(rs.next())
           { 
        	  System.out.println(count);
    	    if(count>1)    	 
    	    {
    		    String post=rs.getString("post");       
          	    FileOutputStream os = new FileOutputStream("C:/Users/ashok/Downloads/CS5560-T13 -SourceCode/W13 - Source Code/Spark2Ont/data/Categories/Blog/file"+i+".txt");        
                File myFile = new File("C:/Users/ashok/Downloads/CS5560-T13 -SourceCode/W13 - Source Code/Spark2Ont/data/Categories/Blog/file"+i+".txt");     
                Writer writer = new FileWriter(myFile);
	   	        bufferedWriter = new BufferedWriter(writer);
	   	        bufferedWriter.write(post);
	   	        bufferedWriter.close();
	   	        os.close();
    		 
    	  }
    	  else
    	  {
    		  System.out.println("last");
    		  String post=rs.getString("post");       
         	  FileOutputStream os = new FileOutputStream("C:/Users/ashok/Downloads/CS5560-T13 -SourceCode/W13 - Source Code/Spark2Ont/data/test/file"+i+".txt");        
              File myFile = new File("C:/Users/ashok/Downloads/CS5560-T13 -SourceCode/W13 - Source Code/Spark2Ont/data/test/file"+i+".txt");     
              Writer writer = new FileWriter(myFile);
  	          bufferedWriter = new BufferedWriter(writer);
  	          bufferedWriter.write(post);
  	          bufferedWriter.close();
  	          os.close();
    	  }
    	   count--;

           i++;
       }   
	   }
	   catch(Exception e)
	   {
	   }

	
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		
	}

}
