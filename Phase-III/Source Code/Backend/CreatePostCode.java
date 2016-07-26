package BloggerSourceCode;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.util.Map.Entry;
import java.util.*;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import javax.servlet.http.Part;
import javax.swing.JOptionPane;

import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.apache.commons.io.FileUtils;
import org.apache.spark.SparkConf;
import org.apache.spark.api.java.JavaPairRDD;
import org.apache.spark.api.java.JavaRDD;
import org.apache.spark.api.java.JavaSparkContext;
import org.apache.spark.api.java.function.FlatMapFunction;
import org.apache.spark.api.java.function.Function2;
import org.apache.spark.api.java.function.PairFunction;

import scala.Tuple2;
import scala.collection.Map;
import edu.stanford.nlp.dcoref.CorefChain;
import edu.stanford.nlp.dcoref.CorefCoreAnnotations;
import edu.stanford.nlp.ling.CoreAnnotations;
import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.neural.rnn.RNNCoreAnnotations;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;
import edu.stanford.nlp.semgraph.SemanticGraph;
import edu.stanford.nlp.semgraph.SemanticGraphCoreAnnotations;
import edu.stanford.nlp.sentiment.SentimentCoreAnnotations;
import edu.stanford.nlp.trees.Tree;
import edu.stanford.nlp.trees.TreeCoreAnnotations;
import edu.stanford.nlp.util.CoreMap;

@WebServlet("/CreatePostCode")
public class CreatePostCode extends HttpServlet {
	private static final long serialVersionUID = 1L;
	static StanfordCoreNLP pipeline;

	public CreatePostCode() {
		super();
	}

	protected void doGet(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {

		String option = request.getParameter("option");
		Properties props = new Properties();
		props.setProperty("annotators",
				"tokenize, ssplit, pos, lemma, ner, parse, dcoref");
		StanfordCoreNLP pipeline1 = new StanfordCoreNLP(props);

		if (option.equals("data12")) {

			String post = request.getParameter("post");
			String title = request.getParameter("title");

			FileUtils.writeStringToFile(new File("input.txt"), post);
			SparkConf conf = new SparkConf().setMaster("local[2]")
					.setAppName("NetworkWordCount")
					.set("spark.ui.port", "4567")
					.set("spark.driver.allowMultipleContexts", "true");
			JavaSparkContext sc = new JavaSparkContext(conf);
			JavaRDD<String> input = sc.textFile("input.txt");
			JavaRDD<String> words = input
					.flatMap(new FlatMapFunction<String, String>() {
						public Iterable<String> call(String x) {
							return Arrays.asList(x.split(" "));
						}
					});
			JavaPairRDD<String, Integer> counts = words.mapToPair(
					new PairFunction<String, String, Integer>() {
						public Tuple2<String, Integer> call(String x) {
							return new Tuple2(x, 1);
						}
					}).reduceByKey(new Function2<Integer, Integer, Integer>() {
				public Integer call(Integer x, Integer y) {
					return x + y;
				}
			});
			List<Tuple2<String, Integer>> out_count = counts.toArray();

			Annotation document = new Annotation(post);
			String lemma = "";
			pipeline1.annotate(document);
			List<CoreMap> sentences = document
					.get(CoreAnnotations.SentencesAnnotation.class);
			List<String> list = new ArrayList<String>();
			for (CoreMap sentence : sentences) {
				for (CoreLabel token : sentence
						.get(CoreAnnotations.TokensAnnotation.class)) {
					String pos = token
							.get(CoreAnnotations.PartOfSpeechAnnotation.class);
					if (pos.equals("NNP") || pos.equals("NN")
							|| pos.equals("NNS") || pos.equals("NNPS")) {
						lemma = token
								.get(CoreAnnotations.LemmaAnnotation.class);
						list.add(lemma);
					}

				}

			}

			Object[] st = list.toArray();
			for (Object s : st) {
				if (list.indexOf(s) != list.lastIndexOf(s)) {
					list.remove(list.lastIndexOf(s));
				}
			}
			HashMap<String, Integer> tagList = new HashMap<String, Integer>();

			for (int i = 0; i < out_count.size(); i++) {
				String temp = out_count.get(i)._1().toString();
				int temp_count = out_count.get(i)._2;
				for (int j = 0; j < list.size(); j++) {
					if (list.get(j).toString().equals(temp)) {
						tagList.put(temp, temp_count);
					}
				}

			}

			int auto_id = 0;
			try {

				Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
				Date dNow = new Date();
				SimpleDateFormat date1 = new SimpleDateFormat(
						"yyyy/MM/dd 'at' hh:mm:ss");
				HttpSession session = request.getSession(false);
				String username = (String) session.getAttribute("username");
				String date = date1.format(dNow).toString();

				Connection conn = DriverManager
						.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
				String sql = "insert into postdetails(username,title,post,date1) values(?,?,?,?)";
				PreparedStatement pst = conn.prepareStatement(sql,
						Statement.RETURN_GENERATED_KEYS);
				pst.setString(1, username);
				pst.setString(2, title);
				pst.setString(3, post);
				pst.setString(4, date);

				pst.executeUpdate();
				ResultSet rs = pst.getGeneratedKeys();
				if (rs.next()) {
					auto_id = rs.getInt(1);
					System.out.println("Auto Generated Primary Key " + auto_id);
				}

				conn.close();
			} catch (Exception ex) {
				System.out.println(ex.toString());
			}

			TreeMap<String, Integer> sortedTagList = sortMapByValue(tagList);
			System.out.println(sortedTagList);

			for (int k = 0; k < sortedTagList.size(); k++) {
				if (k < 8) {
					String tag = (String) sortedTagList.keySet().toArray()[k];
					System.out.println(tag);

					try {

						Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");

						Connection conn = DriverManager
								.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
						String sql12 = "insert into tag(tagname,postid) values(?,?)";
						PreparedStatement pst12 = conn.prepareStatement(sql12);
						pst12.setString(1, tag);
						pst12.setInt(2, auto_id);

						pst12.executeUpdate();
						conn.close();
					} catch (Exception ex) {
						System.out.println(ex.toString());
					}

				} else {
					break;
				}

			}

		}
		else
		{
			String title = request.getParameter("title");
			String path = request.getParameter("filepath");
			//System.out.println(path);
			InputStream in = new FileInputStream(new File(path));
			BufferedReader reader = new BufferedReader(
					new InputStreamReader(in));
			StringBuilder out = new StringBuilder();
			String line;
			while ((line = reader.readLine()) != null) {
				out.append(line);
			}
			String filedata = out.toString();
			reader.close();
			FileUtils.writeStringToFile(new File("input.txt"), filedata);
			SparkConf conf = new SparkConf().setMaster("local[2]")
					.setAppName("NetworkWordCount")
					.set("spark.ui.port", "4567")
					.set("spark.driver.allowMultipleContexts", "true");
			JavaSparkContext sc = new JavaSparkContext(conf);
			JavaRDD<String> input = sc.textFile("input.txt");
			JavaRDD<String> words = input
					.flatMap(new FlatMapFunction<String, String>() {
						public Iterable<String> call(String x) {
							return Arrays.asList(x.split(" "));
						}
					});
			JavaPairRDD<String, Integer> counts = words.mapToPair(
					new PairFunction<String, String, Integer>() {
						public Tuple2<String, Integer> call(String x) {
							return new Tuple2(x, 1);
						}
					}).reduceByKey(new Function2<Integer, Integer, Integer>() {
				public Integer call(Integer x, Integer y) {
					return x + y;
				}
			});
			List<Tuple2<String, Integer>> out_count = counts.toArray();

			Annotation document = new Annotation(filedata);
			String lemma = "";
			pipeline1.annotate(document);
			List<CoreMap> sentences = document
					.get(CoreAnnotations.SentencesAnnotation.class);
			List<String> list = new ArrayList<String>();
			for (CoreMap sentence : sentences) {
				for (CoreLabel token : sentence
						.get(CoreAnnotations.TokensAnnotation.class)) {
					String pos = token
							.get(CoreAnnotations.PartOfSpeechAnnotation.class);
					if (pos.equals("NNP") || pos.equals("NN")
							|| pos.equals("NNS") || pos.equals("NNPS")) {
						lemma = token
								.get(CoreAnnotations.LemmaAnnotation.class);
						list.add(lemma);
					}

				}

			}

			Object[] st = list.toArray();
			for (Object s : st) {
				if (list.indexOf(s) != list.lastIndexOf(s)) {
					list.remove(list.lastIndexOf(s));
				}
			}
			HashMap<String, Integer> tagList = new HashMap<String, Integer>();

			for (int i = 0; i < out_count.size(); i++) {
				String temp = out_count.get(i)._1().toString();
				int temp_count = out_count.get(i)._2;
				for (int j = 0; j < list.size(); j++) {
					if (list.get(j).toString().equals(temp)) {
						tagList.put(temp, temp_count);
					}
				}

			}

			int auto_id1 = 0;
			try {

				Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
				Date dNow = new Date();
				SimpleDateFormat date1 = new SimpleDateFormat(
						"yyyy/MM/dd 'at' hh:mm:ss");
				HttpSession session = request.getSession(false);
				String username = (String) session.getAttribute("username");
				String date = date1.format(dNow).toString();

				Connection conn = DriverManager
						.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
				String sql = "insert into postdetails(username,title,post,date1) values(?,?,?,?)";
				PreparedStatement pst = conn.prepareStatement(sql,
						Statement.RETURN_GENERATED_KEYS);
				pst.setString(1, username);
				pst.setString(2, title);
				pst.setString(3, filedata);
				pst.setString(4, date);

				pst.executeUpdate();
				ResultSet rs = pst.getGeneratedKeys();
				if (rs.next()) {
					auto_id1 = rs.getInt(1);
					System.out.println("Auto Generated Primary Key " + auto_id1);
				}

				conn.close();
			} catch (Exception ex) {
				System.out.println(ex.toString());
			}

			TreeMap<String, Integer> sortedTagList = sortMapByValue(tagList);
			System.out.println(sortedTagList);

			for (int k = 0; k < sortedTagList.size(); k++) {
				if (k < 8) {
					String tag = (String) sortedTagList.keySet().toArray()[k];
					System.out.println(tag);

					try {

						Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");

						Connection conn = DriverManager
								.getConnection("jdbc:sqlserver://127.0.0.1;databasename=BloggerDB;user=sa;password=admin;");
						String sql12 = "insert into tag(tagname,postid) values(?,?)";
						PreparedStatement pst12 = conn.prepareStatement(sql12);
						pst12.setString(1, tag);
						pst12.setInt(2, auto_id1);

						pst12.executeUpdate();
						conn.close();
					} catch (Exception ex) {
						System.out.println(ex.toString());
					}

				} else {
					break;
				}

			}
		}
	}

	public static TreeMap<String, Integer> sortMapByValue(
			HashMap<String, Integer> map) {
		Comparator<String> comparator = new ValueComparator(map);
		// TreeMap is a map sorted by its keys.
		// The comparator is used to sort the TreeMap by keys.
		TreeMap<String, Integer> result = new TreeMap<String, Integer>(
				comparator);
		result.putAll(map);
		return result;
	}

	protected void doPost(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {

		String option = request.getParameter("option");
		System.out.println(option);

		if (option.equals("data12")) {
			
			String post = request.getParameter("post");			
			pipeline = new StanfordCoreNLP("sentiment.properties");
			// System.out.println(post);
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
			if (mainSentiment == 0) {
				String a = "<font color=red size=4>Fully Negative Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else if (mainSentiment == 1) {
				String a = "<font color=orange size=4>Neutral Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else if (mainSentiment == 2) {
				String a = "<font color=purple size=4>Neutral Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else if (mainSentiment == 3) {
				String a = "<font color=lightgreen size=4>Good Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);

			} else if (mainSentiment == 4) {
				String a = "<font color=green size=4>Excellent Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else {
				// System.out.println(mainSentiment);
			}

		} else {

			String path = request.getParameter("filepath");
			//System.out.println(path);
			InputStream in = new FileInputStream(new File(path));
			BufferedReader reader = new BufferedReader(
					new InputStreamReader(in));
			StringBuilder out = new StringBuilder();
			String line;
			while ((line = reader.readLine()) != null) {
				out.append(line);
			}
			String filedata = out.toString();
			reader.close();
			System.out.println(filedata);
			pipeline = new StanfordCoreNLP("sentiment.properties");			
			int mainSentiment = 0;
			if (filedata != null && filedata.length() > 0) {
				int longest = 0;
				Annotation annotation = pipeline.process(filedata);
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
			if (mainSentiment == 0) {
				String a = "<font color=red size=4>Fully Negative Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else if (mainSentiment == 1) {
				String a = "<font color=orange size=4>Neutral Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else if (mainSentiment == 2) {
				String a = "<font color=purple size=4>Neutral Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else if (mainSentiment == 3) {
				String a = "<font color=lightgreen size=4>Good Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);

			} else if (mainSentiment == 4) {
				String a = "<font color=green size=4>Excellent Content</font>";
				request.setAttribute("src", a);
				response.setCharacterEncoding("UTF-8");
				response.getWriter().print(a);
			} else {
				// System.out.println(mainSentiment);
			}

		}
	}
}

class ValueComparator implements Comparator<String> {

	HashMap<String, Integer> map = new HashMap<String, Integer>();

	public ValueComparator(HashMap<String, Integer> map) {
		this.map.putAll(map);
	}

	@Override
	public int compare(String s1, String s2) {
		if (map.get(s1) >= map.get(s2)) {
			return -1;
		} else {
			return 1;
		}
	}
}
