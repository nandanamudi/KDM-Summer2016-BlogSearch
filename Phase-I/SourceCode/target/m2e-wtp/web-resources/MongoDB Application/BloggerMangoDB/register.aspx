<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

<script type="text/javascript" src="tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript" language="javascript">
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups",
           
        });
    </script>

    <style type="text/css">
        .style1
        {
            width: 51%;
        }
        .style2
        {
            text-align: center;
            font-family: "Times New Roman", Times, serif;
            font-size: x-large;
        }
        .style3
        {
            width: 379px;
        }
        .style4
        {
            width: 379px;
            font-family: "Times New Roman", Times, serif;
            font-size: large;
            text-align: justify;
        }
        .style5
        {
            width: 102px;
        }
    </style>

    </head>
<body>
<form id="Form1" runat="server">
<div id="tooplate_wrapper">
	<div id="tooplate_header">
        <div id="site_title"></div>
    </div> <!-- end of forever header -->
    
    <div id="serach-form">
       
        <asp:ImageButton ID="ImageButton1" Width="70" Height="40" runat="server" 
            ImageUrl="~/images/home.png" onclick="ImageButton1_Click" />
     
         
    </div> <!-- end of tooplate_menu -->
    
    <div id="tooplate_main">
       	
        
       
        
        <div class="cleaner">
            <table width="100%">
                <tr>
                    <td class="style2">
                        Register Here
                    </td>
                </tr>
            </table>
            <br />
            <table align="center" class="style1">
                <tr>
                    <td class="style4">
                        First Name</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Last Name</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        User Name</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Password</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox4" TextMode="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="style4">
                       Confirm Password</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox8" TextMode="password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Email ID</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Age</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Ph.No</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td class="style5">
                        <asp:Button ID="Button1" runat="server" Text="Register" 
                            onclick="Button1_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <br />
              
                 
                   
        </div>
	</div> <!-- end of main -->
    
    <div id="tooplate_footer_wrapper">
     
        
        <div id="tooplate_copyright">
		
            Copyright © 2016 <a href="www.blogginglocale.somee.com">Blogging Locale</a>
			
        </div>
	</div>
</div> <!-- end of wrapper -->
</form>
</body>
</html>