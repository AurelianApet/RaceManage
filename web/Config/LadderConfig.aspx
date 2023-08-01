<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LadderConfig.aspx.cs" Inherits="Config_LadderConfig" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_MAINCONFIG%></title>
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("Config");
});
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_LADDERCONFIG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr><td class="clsspace"></td></tr>
		    <tr>
                <td class="left">- <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_GAMECONFIG %>"></asp:Literal></td>
            </tr>
            <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
		            <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal22" runat="server" Text="<%$Resources:Str, STR_GAMERESULT %>"></asp:Literal>
                        </th>
                        <td class="left" width="80%">                
                            <asp:RadioButtonList ID="rblGameResult" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$Resources:Str, STR_GAMERESULT_AUTO %>" Value="1"></asp:ListItem>
                                <asp:ListItem Text="<%$Resources:Str, STR_GAMERESULT_PASSIVE %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>  
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="left">- <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_USER_BETRATIO %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
            <tr>
                <td>   
                    <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal34" runat="server" Text="单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">    
                            左右 : <asp:TextBox ID="tbxRatio_statrpos" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            34 : <asp:TextBox ID="tbxRatio_34" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            单双 : <asp:TextBox ID="tbxRatio_oddeven" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal4" runat="server" Text="双式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">    
                            <asp:TextBox ID="tbxMultiRatio" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" Text="<%$Resources:Str, STR_CONFIRM %>"
                        CssClass="button orange" Width="100px"
                        OnClick="btnOK_Click" />&nbsp;
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            </table>
		</div>
	</section>
</section>
</asp:Content>