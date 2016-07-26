﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rich Text Editor Demo : John Bhatt</title>
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
        .auto-style1 {
            width: 59px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <table>
       <tr>
           <td align="center" colspan="2"><h1>Rich Text Editor Demo </h1>
               <h3>(Create New Post - Blog of P.Yar.B)</h3>
               <p>&nbsp;</p></td>
       </tr>
       <tr>
           <td class="auto-style1">
               Post Title : 
           </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
            </td>
       </tr>
       <tr>
           <td valign="top" class="auto-style1">
            Body :
           </td>
           <td>
               <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="327px" 
                   Width="528px"></asp:TextBox>
               <br />
               <br />
           </td>
       </tr>
       <tr>
           <td class="auto-style1">
               Tags :
           </td>
           <td>
               <asp:TextBox ID="TextBox3" runat="server" Height="22px" Width="267px"></asp:TextBox>
           </td>
       </tr>
       <tr>
           <td colspan="2" align="left">
               <asp:Button ID="Button1" runat="server" Text="Post" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
               <asp:Button ID="Button2" runat="server" Text="Clear" />
           </td>
       </tr>

   </table>
  
    </form>
</body>
</html>
