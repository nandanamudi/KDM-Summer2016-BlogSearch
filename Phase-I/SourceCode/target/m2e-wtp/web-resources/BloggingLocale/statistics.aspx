<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statistics.aspx.cs" Inherits="statistics" %>

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
                window.location.href = "comments.aspx?postid=" + id + "&name=" + name + "&date=" + date;
            });
        });
</script>

    <style type="text/css">
        .display_paginate 
        {
        color: #FB4C19;
        }
        .style2
        {
            color: #FF6666;
        }
        </style>


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
                 <h4 align="center" class="style2">Statistics on SQL Server and MongoDB</h4>
                   <table align="center" height="100">
                   <tr>
                   <td> Select Option :</td>
                   <td>
                     <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="0">-------Select---------</asp:ListItem>
                        <asp:ListItem Value="1">1 - 10,000</asp:ListItem>
                        <asp:ListItem Value="2">1 - 100,000</asp:ListItem>
                        <asp:ListItem Value="3">1 - 200,000</asp:ListItem>
                        <asp:ListItem Value="4">1 - 300,000</asp:ListItem>
                        <asp:ListItem Value="5">1 - 400,000</asp:ListItem>
                        <asp:ListItem Value="6">1 - 500,000</asp:ListItem>
                        <asp:ListItem Value="7">1 - 600,000</asp:ListItem>
                        <asp:ListItem Value="8">1 - 700,000</asp:ListItem>
                        <asp:ListItem Value="9">1 - 800,000</asp:ListItem>
                        <asp:ListItem Value="10">1 - 900,000</asp:ListItem>
                        <asp:ListItem Value="11">1 - 10,00,000</asp:ListItem>
                        <asp:ListItem Value="12">Index on 1M Posts</asp:ListItem>
                    </asp:DropDownList>

                   </td>
                   <td><asp:Button ID="Button4" runat="server" Text="Submit" onclick="Button4_Click" /></td>
                   </tr>
                   </table>
   
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
         
       
        <asp:Chart ID="Chart1" Width="900" Height="600" runat="server" Visible="false"></asp:Chart>
    
        <asp:Literal ID="lt" runat="server"></asp:Literal>
        <div id="chart_div"></div>
         
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