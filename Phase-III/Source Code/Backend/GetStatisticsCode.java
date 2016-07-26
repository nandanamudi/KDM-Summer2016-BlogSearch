package BloggerSourceCode;

import java.io.File;
import java.io.FileWriter;
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

@WebServlet("/GetStatisticsCode")
public class GetStatisticsCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public GetStatisticsCode() {
        super();
        
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {


		 File outputFile = new File(getServletContext().getRealPath("/") + "/statistics.csv");	
	        FileWriter fw= new FileWriter(outputFile);
	        fw.append("Date");
			fw.append(',');
			fw.append("Count");
			fw.append("\n");
		
		ArrayList<Statistics> postlist = new ArrayList<Statistics>();
	   try
	   {
		Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
		PreparedStatement pst = conn.prepareStatement("SELECT count(*) as count,LEFT([date1], CHARINDEX('at', [date1]) - 1) AS [date] FROM postdetails group by LEFT([date1], CHARINDEX('at', [date1]) - 1)");
        ResultSet rs = pst.executeQuery();
        while(rs.next())
        {
        	//Statistics s=new Statistics();
        	String count=rs.getString("count"); 
            String date=rs.getString("date");           
            fw.append(date);
			fw.append(',');
			fw.append(count);
			fw.append("\n");
           
            
        }   
	   }
	   catch(Exception e)
	   {
	   }

		fw.close();
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

	}

}
