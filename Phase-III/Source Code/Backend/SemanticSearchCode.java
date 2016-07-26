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

import rita.RiWordNet;

import com.google.gson.Gson;
@WebServlet("/SemanticSearchCode")
public class SemanticSearchCode extends HttpServlet {
	private static final long serialVersionUID = 1L;

    public SemanticSearchCode() {
        super();
        // TODO Auto-generated constructor stub
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		String item=request.getParameter("searchterm");
		System.out.println(item);
		ArrayList<post> postlist = new ArrayList<post>();	

		 RiWordNet wordnet = new RiWordNet("C:\\WordNet-3.0");

	       // Demo finding parts of speech
	       
//	        System.out.println("\nFinding parts of speech for " + item + ".");
//	        String[] partsofspeech = wordnet.getPos(item);
//	        for (int i = 0; i < partsofspeech.length; i++) {
//	            System.out.println(partsofspeech[i]);
//	        }
           try	   
           {
	       
	        String[] poss = wordnet.getPos(item);
	        for (int j = 0; j < poss.length; j++) {
	            System.out.println("\n\nSynonyms for " + item + " (pos: " + poss[j] + ")");
	            String[] synonyms = wordnet.getAllSynonyms(item,poss[j],10);
	            for (int i = 0; i < synonyms.length; i++) {
	                System.out.println(synonyms[i]);
	                Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
	        		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
	        		PreparedStatement pst = conn.prepareStatement("SELECT p.postid, p.title FROM postdetails p,tag t where p.postid=t.postid and t.tagname = '"+synonyms[i]+"'" );
	                ResultSet rs = pst.executeQuery();
	                while(rs.next())
	                {
	                	post p=new post();
	                    p.Title(rs.getString("title"));
	                    p.Postid(rs.getString("postid"));	                  
	                    postlist.add(p);
	                    
	                }  
	            }
	        }
	        Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");		 	
    		Connection conn = DriverManager.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
    		PreparedStatement pst1 = conn.prepareStatement("SELECT p.postid,p.title FROM postdetails p,tag t where p.postid=t.postid and t.tagname='"+item+"'");
            ResultSet rs1 = pst1.executeQuery();
            while(rs1.next())
            {
            	post p=new post();
                p.Title(rs1.getString("title"));
                p.Postid(rs1.getString("postid"));                 
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
