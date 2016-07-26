<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usercomments.aspx.cs" Inherits="usercomments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Blogging Locale</title>
<meta name="keywords" content="" />
<meta name="description" content="" />
<!--
Template 2030 Elegant
http://www.tooplate.com/view/2030-elegant
-->
<link href="tooplate_style.css" rel="stylesheet" type="text/css" />
<link href="css/cbdb-search-form.css" type="text/css" rel="stylesheet" media="screen" />
<link rel="stylesheet" href="css/nivo-slider.css" type="text/css" media="screen" />
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
<script src="js/jquery.nivo.slider.js" type="text/javascript"></script>



    <style type="text/css">
        .style1
        {
            width: 101%;
            height: 189px;
        }
        .style2
        {
            height: 157px;
        }
        .style5
        {
            width: 96%;
        }
        .style6
        {
            font-family: "Times New Roman", Times, serif;
            font-size: x-large;
        }
        .style7
        {
            width: 65%;
        }
        .style8
        {
            width: 102px;
        }
        .style9
        {
            height: 28px;
        }
    </style>


  
 <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.11/css/jquery.dataTables.min.css" />

<script type="text/javascript" charset="utf8" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js"></script>

<script type="text/javascript" charset="utf8" src="http://ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.4/jquery.dataTables.min.js"></script>
 



    <script type="text/javascript">
        $(function () {
            $('#graduates').dataTable();

            $('.view').click(function () {
                event.preventDefault();
                var id = $(this).closest("tr").find(".postid").text();
                var name = $(this).closest("tr").find(".name").text();
                var date = $(this).closest("tr").find(".date123").text();
                window.location.href = "test1.aspx?postid=" + id + "&name=" + name + "&date=" + date;
            });
        });
</script>

</head>
<body>
<form id="Form1" runat="server">
<div id="tooplate_wrapper">
	<div id="tooplate_header">
        <div id="site_title"></div>
    </div> <!-- end of forever header -->
    
     <div id="tooplate_menu">
        <ul>
            <li><a href="userhome.aspx" class="current">Home</a></li>
            <li><a href="userposts.aspx">My Posts</a></li>
            <li><a href="statistics.aspx">Statistics</a></li>
            <li><a href="logout.aspx" class="last">Logout</a></li>
        </ul>    	
    </div> <!-- end of tooplate_menu -->
    
    <div id="tooplate_main">
       	<div id="tooplate_content">
           		<div id="homepage_slider">
                                        
                      
                        <table class="style1">
                           <tr>
                           <td class="style9"> Posted By :<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                           <td class="style9">On :<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
                           </tr>
                            <tr>
                                <td class="style2" colspan="2">
                                    <asp:TextBox ID="TextBox1" Width="100%" disabled="true" TextMode="MultiLine" Rows="5" 
                                        runat="server" Height="120px"></asp:TextBox>
                                </td>
                              
                            </tr>
                           
                        </table>

        <br />
                        
              
                       
                        
              
                    </div>
             
        </div>
        
        <div id="tooplate_sidebar">
        	
			
            <div class="sidbar_box">
		        <asp:Image ID="Image1" runat="server" 
                    ImageUrl='<%# "~/Handler.ashx?ID=" + Eval("id")%>' Width="238px" 
                    Height="155px"/>           
            <table> 
             <tr> <td colspan="3" style="color: #FF0066">      
             <asp:LinkButton ID="LinkButton1" OnClick="change_click"  Font-Underline="false" runat="server"><font 
                     color="white">Change profile Picture</font></asp:LinkButton>
             </td></tr> 
              <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" Visible="false" runat="server" Width="256px" />
                </td>
            </tr>
                    
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button3" runat="server" Visible="false" OnClick="Button3_Click" Text="Submit" /></td>
            </tr>
             <tr><td colspan="3">
              <asp:Label ID="Label7" runat="server" Visible="false" Text="Label"></asp:Label></td></tr>
           
  </table>
            </div>
        </div>
        
        <div class="cleaner">
         <table class="style7">
                            <tr>
                                <td class="style8">
                                    Your Comments</td>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" Rows="4" TextMode="MultiLine" 
                                        Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                  </td>
                                 
                                 <td>
                                    <asp:ImageButton ID="ImageButton1" Width="100" Height="30" runat="server" 
                                         ImageUrl="~/images/comment.jpg" onclick="ImageButton1_Click" />
                                </td>
                            </tr>
                           
                        </table>
          <br />

        
        <asp:GridView ID="GridView1" Width="100%" runat="server" ShowHeader="false" BackColor="LightGoldenrodYellow" 
                BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" 
                GridLines="None">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        </div>
	</div> <!-- end of main -->
    
    <div id="tooplate_footer_wrapper">
     
        
        <div id="tooplate_copyright">
		
            Copyright © 2016 <a href="https://www.blogginglocale.somee.com">Blogging Locale</a>
			
        </div>
	</div>
</div> <!-- end of wrapper -->
</form>
</body>
</html>