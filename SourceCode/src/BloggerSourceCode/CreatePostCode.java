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
			 pipeline = new StanfordCoreNLP("sentiment.properties");
			String post = request.getParameter("post");
			 System.out.println(post);
			 int mainSentiment = 0;
		        if (post != null && post.length() > 0) {
		            int longest = 0;
		            Annotation annotation = pipeline.process(post);
		            for (CoreMap sentence : annotation
		                    .get(CoreAnnotations.SentencesAnnotation.class)) {
		                Tree tree = sentence
		                        .get(SentimentCoreAnnotations.SentimentAnnotatedTree.class);
		                int sentiment = RNNCoreAnnotations.getPredictedClass(tree);
		                String partText = sentence.toString();
		                if (partText.length() > longest) {
		                    mainSentiment = sentiment;
		                    longest = partText.length();
		                }

		            }
		        }
		        if(mainSentiment==0)
		        {
		        	String a="<font color=red size=4>Fully Negative Content</font>";
		        	request.setAttribute("src", a); 
		        	response.setCharacterEncoding("UTF-8"); 
		        	response.getWriter().print(a);
		        }
		        else if(mainSentiment==1)
		        {
		        	String a="<font color=orange size=4>Negative Content</font>";
		        	request.setAttribute("src", a); 
		        	response.setCharacterEncoding("UTF-8"); 
		        	response.getWriter().print(a);
		        }
		        else if(mainSentiment==2)
		        {
		        	String a="<font color=purple size=4>Neutral Content</font>";
		        	request.setAttribute("src", a); 
		        	response.setCharacterEncoding("UTF-8"); 
		        	response.getWriter().print(a);
		        }
		        else if(mainSentiment==3)
		        {
		        	String a="<font color=lightgreen size=4>Good Content</font>";
		        	request.setAttribute("src", a); 
		        	response.setCharacterEncoding("UTF-8"); 
		        	response.getWriter().print(a);
		        	
		        }
		        else if(mainSentiment==4)
		        {
		        	String a="<font color=green size=4>Excellent Content</font>";
		        	request.setAttribute("src", a); 
		        	response.setCharacterEncoding("UTF-8"); 
		        	response.getWriter().print(a);
		        }
		        else
		        {
		        	// System.out.println(mainSentiment);
		        }
		        
			
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
