<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" MasterPageFile="~/MainMaster.master" %>
<%@ MasterType VirtualPath="~/MainMaster.master" %>

<asp:Content ContentPlaceHolderID="HeadContent" ID="headCont" runat="server">
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0" />
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
        var STR_LOGINID="ID";
        var STR_LOGINPWD="Password";
        
        $(".input-loginid").focus(function() {
            var oldValue = $(this).attr ("value");
            if(oldValue == STR_LOGINID) {
                $(this).val("");
            }
        });
        $(".input-loginid").blur(function() {
            var oldValue = $(this).attr ("value");
            if(oldValue == "" || oldValue == STR_LOGINID) {
                $(this).val(STR_LOGINID);
            }
        });
        $(".input-loginpwd").focus(function() {
            var oldValue = $(this).attr("value");
            if(oldValue == STR_LOGINPWD) {
                $(this).val("");
            }
        });
        
        $(".input-loginpwd").blur(function() {
            var oldValue = $(this).attr ("value");
            if(oldValue == "" || oldValue == STR_LOGINPWD) {
                $(this).val(STR_LOGINPWD);
                //$(this).css("color", "#CCCCCC");
            }
        });
        $(".input-loginid").blur();
        $(".input-loginpwd").blur();
    });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyContent" ID="bodyCont" runat="server">
    <section>
		<asp:TextBox ID="tbxLoginID" runat="server" Text="ID" CssClass="input-loginid"></asp:TextBox>
		<asp:TextBox ID="tbxLoginPwd" runat="server" Text="Pass" TextMode="Password" CssClass="input-loginpwd"></asp:TextBox>
		<asp:Button ID="btnLogin" runat="server" CssClass="button blue login_btn" Text="Login" OnClick="btnLogin_Click" />
	</section>
</asp:Content>