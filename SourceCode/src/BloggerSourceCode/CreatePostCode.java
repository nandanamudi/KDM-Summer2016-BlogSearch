package BloggerSourceCode;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;

import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import edu.stanford.nlp.ling.CoreAnnotations;
import edu.stanford.nlp.neural.rnn.RNNCoreAnnotations;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;
import edu.stanford.nlp.sentiment.SentimentCoreAnnotations;
import edu.stanford.nlp.trees.Tree;
import edu.stanford.nlp.util.CoreMap;

@WebServlet("/CreatePostCode")
public class CreatePostCode extends HttpServlet {
	private static final long serialVersionUID = 1L;
	 static StanfordCoreNLP pipeline;
	 private final String UPLOAD_DIRECTORY = "D:/uploads";
	 
    public CreatePostCode() {
        super();      
    }

	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	
		response.getWriter().append("Served at: ").append(request.getContextPath());
	}

	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		String option = request.getParameter("data");
		// System.out.println(option);
		
		
		if(option.equals("data12"))
		{
			 
			
		}
		else
		{
			   
				try 
				{
	                List<FileItem> multiparts = new ServletFileUpload(
	                                         new DiskFileItemFactory()).parseRequest(request);
	              
	                for(FileItem item : multiparts)
	                {
	                    if(!item.isFormField())
	                    {
	                        String name = new File(item.getName()).getName();
	                        item.write( new File(UPLOAD_DIRECTORY + File.separator + name));
	                    }
	                }
	           
	               //File uploaded successfully
	                System.out.println("File Uploaded Successfully");
	               
	            } 
				catch (Exception ex) 
				{
					System.out.println(ex);
	               
	            }          
	         
	        }
	    
	      
	     
	    }   
		
		
		
	

}
