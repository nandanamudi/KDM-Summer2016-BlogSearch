<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userposts.aspx.cs" Inherits="userposts" %>

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
<link rel="stylesheet" href="css/nivo-slider.css" type="text/css" media="screen" />
<script type="text/javascript" charset="utf8" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js"></script>

<script type="text/javascript" charset="utf8" src="http://ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.4/jquery.dataTables.min.js"></script>
 <script src="js/jquery.nivo.slider.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(function () {
         $('#slider').nivoSlider({
             effect: 'random',
             slices: 15,
             animSpeed: 500,
             pauseTime: 3000,
             startSlide: 0, //Set starting Slide (0 index)
             directionNav: false,
             directionNavHide: false, //Only show on hover
             controlNav: false, //1,2,3...
             controlNavThumbs: false, //Use thumbnails for Control Nav
             pauseOnHover: true, //Stop animation while hovering
             manualAdvance: false, //Force manual transitions
             captionOpacity: 0.8, //Universal caption opacity
             beforeChange: function () { },
             afterChange: function () { },
             slideshowEnd: function () { } //Triggers after all slides have been shown
         });
     });
</script>



    <script type="text/javascript">
        $(function () {
            $('#graduates').dataTable();

            $('.delete').click(function () {
                event.preventDefault();
                var id = $(this).closest("tr").find(".postid").text();
             
                window.location.href = "editpost.aspx?postid=" + id + "&name=" + name + "&date=" + date;
            });
        });
</script>

    <style type="text/css">
        .display_paginate 
        {
        color: #FB4C19;
        }
        .style1
        {
            width: 101%;
            height: 189px;
        }
        .style2
        {
            height: 157px;
        }
        .style4
        {
            width: 186px;
            font-family: "Times New Roman", Times, serif;
            font-size: medium;
            text-align: center;
        }
        .style5
        {
            height: 26px;
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
                <div id="slider">
                        <a href="#"><img src="images/slideshow/01.jpg" width="570" height="210" alt="Slide 01" title="Nam fermentum lacus suscipit diam feugiat fringilla." /></a>
                        <a href="#"><img src="images/slideshow/02.jpg" width="570" height="210" alt="Slide 02" title="Proin bibendum est id velit tincidunt ut sodales ligula facilisis." /></a>
                        <a href="#"><img src="images/slideshow/03.jpg" width="570" height="210" alt="Slide 03" title="Fusce tincidunt diam eu metus iaculis hendrerit." /></a>
                        <a href="#"><img src="images/slideshow/04.jpg" width="570" height="210" alt="Slide 04" title="Nulla faucibus luctus quam eget placerat. " /></a>
                        <a href="#"><img src="images/slideshow/05.jpg" width="570" height="210" alt="Slide 05" title="Aliquam quis velit et sem vestibulum dignissim." /></a>
                    </div>
                     
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
          <table id="graduates" class="display" cellspacing="0" width="100%" style="background-color: #996633">
                        <thead>
                            <tr>
                                <th hidden="true">
                                   Post ID 
                                </th>
                                <th>
                                  Name
                                </th>
                                <th>
                                   Title
                                </th>
                                <th>
                                   Tags
                                </th>
                                 <th>
                                   Posted Date
                                </th>
                                 <th>
                                   &nbsp;
                                </th>
                                
                               </tr>
                        </thead>
                        <tbody>
                            <asp:Literal runat="server" ID="ltData"></asp:Literal>
                        </tbody>
                    </table>
         
        
         
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