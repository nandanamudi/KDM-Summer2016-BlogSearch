<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

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
        .style4
        {
            width: 186px;
            font-family: "Times New Roman", Times, serif;
            font-size: medium;
            text-align: center;
        }
        .style5
        {
            width: 96%;
        }
        .style6
        {
            font-family: "Times New Roman", Times, serif;
            font-size: medium;
            text-align: justify;
        }
        .style7
        {
            font-family: "Times New Roman", Times, serif;
            font-size: medium;
        }
    </style>


  
 <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.11/css/jquery.dataTables.min.css" />

<script type="text/javascript" charset="utf8" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js"></script>

<script type="text/javascript" charset="utf8" src="http://ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.4/jquery.dataTables.min.js"></script>
 



    

</head>
<body>
<form runat="server">
<div id="tooplate_wrapper">
	<div id="tooplate_header">
        <div id="site_title"></div>
    </div> <!-- end of forever header -->
    
    <div id="serach-form">
     
            
    </div> <!-- end of tooplate_menu -->
    
    <div id="tooplate_main">
       	<div id="tooplate_content">
           		<div id="homepage_slider">
                                        
                      
                        <table class="style1">
                            <tr>
                                <td class="style4">
                                    Title</td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" Width="330px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2" colspan="2">
                                    <asp:TextBox ID="TextBox2" Width="100%" TextMode="MultiLine" Rows="5" 
                                        runat="server" Height="149px"></asp:TextBox>
                                </td>
                              
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="Button1" runat="server" Text="Clear" />
                                     <asp:Button ID="Button2" runat="server" Text="Post" />
                                    &nbsp;</td>
                                
                               
                            </tr>
                        </table>

 
              
                    </div>
             
        </div>
        
        <div id="tooplate_sidebar">
        	
			
            <div class="sidbar_box">
		        <h4 class="style6">Login With</h4>
                
			</div>
			
           
            <div class="cleaner">
                <table class="style5">
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox4" placeholder=" Enter User name" runat="server" 
                                Width="247px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox5" TextMode="Password" placeholder="Enter Password" runat="server" 
                                Width="247px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Login" onclick="Button3_Click" /><a href="register.aspx">Register Here</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <div class="cleaner">

         <table>
        <tr>
        <td class="style7">Search By:</td>
        <td class="style8">
            <span class="style6">Author&nbsp;&nbsp; :&nbsp;&nbsp; <asp:CheckBox ID="CheckBox1" runat="server" /><br />
            Tag&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; <asp:CheckBox ID="CheckBox2" runat="server" /><br />
           Title&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; <asp:CheckBox ID="CheckBox3" runat="server" /><br />
           Post&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </span> 
            <asp:CheckBox ID="CheckBox4" runat="server" style="font-size: medium" />
            
        </td>            
         <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>   
         <td><asp:Button ID="Button4" runat="server" Text="Search" onclick="Button4_Click" /></td>     
        </tr>
        
        </table>
        
           <asp:GridView ID="GridView2" runat="server" Width="100%" CellPadding="3" OnRowCommand="gv_RowCommand"
                AllowPaging="True" PageSize="10" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" 
                BorderWidth="1px" CellSpacing="2" HorizontalAlign="Center" 
                AutoGenerateColumns="False" 
                onpageindexchanging="GridView2_PageIndexChanging">
            <Columns>
              <asp:TemplateField Visible="false" HeaderText="Post ID">
                 <ItemTemplate>
                <asp:Label ID="ID2" Visible="false" runat="server" Text='<%#Eval("postid")%>'></asp:Label>
               </ItemTemplate>             
         </asp:TemplateField>

          <asp:TemplateField Visible="false" HeaderText="Post ID">
                 <ItemTemplate>
                <asp:Label ID="post_id" Visible="false" runat="server" Text='<%#Eval("post")%>'></asp:Label>
               </ItemTemplate>             
         </asp:TemplateField>
     
        <asp:TemplateField HeaderText="Screen Name">
                 <ItemTemplate>
                <asp:Label ID="ID12" runat="server" Text='<%#Eval("name")%>'></asp:Label>
               </ItemTemplate>             
         </asp:TemplateField>
       <asp:TemplateField HeaderText="Post Title">
             <ItemTemplate>
                <asp:Label ID="ID3" runat="server" Text='<%#Eval("title") %>'></asp:Label>
               </ItemTemplate>                  
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Tags">
                  <ItemTemplate>
                <asp:Label ID="ID4" runat="server" Text='<%#Eval("tagname") %>'></asp:Label>
               </ItemTemplate>             
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Posted Date">
                      <ItemTemplate>
                <asp:Label ID="ID5" runat="server" Text='<%#Eval("date") %>'></asp:Label>
               </ItemTemplate>         
        </asp:TemplateField>   
       
       <asp:TemplateField HeaderText="VIEW">
     <ItemTemplate> 
      <asp:Button ID="Button1"  runat="server" Text="View" CommandName="view" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
    </ItemTemplate>
    </asp:TemplateField>  

    </Columns>
           
                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
           
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" VerticalAlign="Middle" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>




              <asp:GridView ID="GridView1" Visible="false" runat="server" Width="100%" 
                CellPadding="3" OnRowCommand="gv_RowCommand1"
               BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" 
                AllowPaging="true" PageSize="10"
                BorderWidth="1px" CellSpacing="2" HorizontalAlign="Center" 
                AutoGenerateColumns="False" 
                onpageindexchanging="GridView1_PageIndexChanging">
            <Columns>
              <asp:TemplateField Visible="false" HeaderText="Post ID">
                 <ItemTemplate>
                <asp:Label ID="ID2" Visible="false" runat="server" Text='<%#Eval("postid")%>'></asp:Label>
               </ItemTemplate>             
         </asp:TemplateField>

          <asp:TemplateField Visible="false" HeaderText="Post ID">
                 <ItemTemplate>
                <asp:Label ID="post_id" Visible="false" runat="server" Text='<%#Eval("post")%>'></asp:Label>
               </ItemTemplate>             
         </asp:TemplateField>
     
        <asp:TemplateField HeaderText="Screen Name">
                 <ItemTemplate>
                <asp:Label ID="ID12" runat="server" Text='<%#Eval("name")%>'></asp:Label>
               </ItemTemplate>             
         </asp:TemplateField>
       <asp:TemplateField HeaderText="Post Title">
             <ItemTemplate>
                <asp:Label ID="ID3" runat="server" Text='<%#Eval("title") %>'></asp:Label>
               </ItemTemplate>                  
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Tags">
                  <ItemTemplate>
                <asp:Label ID="ID4" runat="server" Text='<%#Eval("tagname") %>'></asp:Label>
               </ItemTemplate>             
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Posted Date">
                      <ItemTemplate>
                <asp:Label ID="ID5" runat="server" Text='<%#Eval("date") %>'></asp:Label>
               </ItemTemplate>         
        </asp:TemplateField>   
       
       <asp:TemplateField HeaderText="VIEW">
     <ItemTemplate> 
      <asp:Button ID="Button1"  runat="server" Text="View" CommandName="view" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
    </ItemTemplate>
    </asp:TemplateField>  

    </Columns>
           
                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
           
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" VerticalAlign="Middle" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
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