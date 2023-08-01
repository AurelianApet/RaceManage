<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainConfig.aspx.cs" Inherits="Config_MainConfig" MasterPageFile="~/ManageMaster.master" %>
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
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_MAINCONFIG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr><td class="clsspace"></td></tr>
		    <tr>
                <td class="left">- <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Menu, MENU_MAINCONFIG %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
		    <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_TITLE %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxSiteTitle" runat="server" CssClass="clsinput" TabIndex="1"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Str, STR_MEMBERJOIN %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:CheckBox ID="chkMemberJoin" CssClass="clscheckbox" runat="server" Text="<%$Resources:Str, STR_APPLY %>" />
                        </td>
                    </tr>
                    <tr>          
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Str, STR_CURRENTCONNECT %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxLoginMinutes" runat="server" CssClass="clsinput" Width="50px"></asp:TextBox><asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Str, STR_MINUTE %>"></asp:Literal>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Str, STR_ROWCOUNTPERPAGE %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxPageRows" runat="server" CssClass="clsinput" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal67" runat="server" Text="<%$Resources:Str, STR_ADMINCONNIP %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxManageIP" runat="server" Height="150px" Width="250px" CssClass="clsinput" TextMode="MultiLine"></asp:TextBox><br />
                            <asp:Literal ID="Literal68" runat="server" Text="<%$Resources:Msg, MSG_ADMINCONNIP %>"></asp:Literal>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Str, STR_INTERCEPTIP %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxInterceptIP" runat="server" Height="150px" Width="250px" CssClass="clsinput" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Msg, MSG_ENTER_SEPERATE %>"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal24" runat="server" Text="<%$Resources:Str, STR_PROHIBITID %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">                
                            <asp:TextBox ID="tbxProhibitID" runat="server" CssClass="clsinput" TextMode="MultiLine" Width="250px" Height="100px"></asp:TextBox>
                            <asp:Literal ID="Literal25" runat="server" Text="<%$Resources:Msg, MSG_COMMA_SEPERATE %>"></asp:Literal>
                        </td>
                    </tr>
                    </table>
		        </td>
		    </tr>
		    <tr><td class="clsspace"></td></tr><tr>
                <td class="left">- <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Str, STR_ZHIFUBAOCONFIG %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
		    <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
		            <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Str, STR_USE %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">                
                            <asp:RadioButtonList ID="rblZhifubaoUse" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="不" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>  
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Str, STR_ZHIFUBAO_INFO %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxZhifubaoInfo" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Str, STR_ZHIFUBAO_KEY %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxZhifubaoKey" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Str, STR_ZHIFUBAO_PARTNER %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">                
                            <asp:TextBox ID="tbxZhifubaoPartner" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
		            </table>
		        </td>
		    </tr>
            <tr><td class="clsspace"></td></tr><tr>
                <td class="left">- <asp:Literal ID="Literal15" runat="server" Text="<%$Resources:Str, STR_WEIXINCONFIG %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
		    <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
		            <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Str, STR_USE %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">                
                            <asp:RadioButtonList ID="rblWeixinUse" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="不" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>  
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal17" runat="server" Text="<%$Resources:Str, STR_WEIXIN_APPID %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxWeixinAppid" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal18" runat="server" Text="<%$Resources:Str, STR_WEIXIN_MCHID %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxWeixinMchid" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal19" runat="server" Text="<%$Resources:Str, STR_WEIXIN_APPSECRET %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxWeixinAppsecret" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal20" runat="server" Text="<%$Resources:Str, STR_WEIXIN_SHOPKEY %>"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxWeixinShopkey" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
		            </table>
		        </td>
		    </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="left">- <asp:Literal ID="Literal42" runat="server" Text="<%$Resources:Str, STR_MANUALCHARGECONFIG %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
            <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
		            <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal55" runat="server" Text="<%$Resources:Str, STR_BANK_GSYH %>"></asp:Literal>
                        </th>
                        <td class="left" width="15%">                
                            <asp:RadioButtonList ID="rblBankGsyhUse" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$Resources:Str, STR_USE %>" Value="1"></asp:ListItem>
                                <asp:ListItem Text="<%$Resources:Str, STR_STOP %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList> 
                        </td>
                        <th width="5%" class="right">
                            <asp:Literal ID="Literal56" runat="server" Text="账户"></asp:Literal>
                        </th>
                        <td class="left" width="60%" colspan="3">
                            银行户名&nbsp;:&nbsp;<asp:TextBox ID="tbxBankGsyhName" runat="server" CssClass="clsinput" Width="200px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            银行账户&nbsp;:&nbsp;<asp:TextBox ID="tbxBankGsyhNum" runat="server" CssClass="clsinput" Width="200px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal61" runat="server" Text="<%$Resources:Str, STR_BANK_CFT %>"></asp:Literal>
                        </th>
                        <td class="left" width="15%">                
                            <asp:RadioButtonList ID="rblBankCftUse" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$Resources:Str, STR_USE %>" Value="1"></asp:ListItem>
                                <asp:ListItem Text="<%$Resources:Str, STR_STOP %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList> 
                        </td>
                        <th width="5%" class="right">
                            <asp:Literal ID="Literal62" runat="server" Text="账户"></asp:Literal>
                        </th>
                        <td class="left" width="60%" colspan="3">
                            银行户名&nbsp;:&nbsp;<asp:TextBox ID="tbxBankCftName" runat="server" CssClass="clsinput" Width="200px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            银行账户&nbsp;:&nbsp;<asp:TextBox ID="tbxBankCftNum" runat="server" CssClass="clsinput" Width="200px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal63" runat="server" Text="<%$Resources:Str, STR_BANK_ZFB %>"></asp:Literal>
                        </th>
                        <td class="left" width="15%">                
                            <asp:RadioButtonList ID="rblBankZfbUse" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$Resources:Str, STR_USE %>" Value="1"></asp:ListItem>
                                <asp:ListItem Text="<%$Resources:Str, STR_STOP %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList> 
                        </td>
                        <th width="5%" class="right">
                            <asp:Literal ID="Literal64" runat="server" Text="账户"></asp:Literal>
                        </th>
                        <td class="left" width="60%" colspan="3">
                            银行户名&nbsp;:&nbsp;<asp:TextBox ID="tbxBankZfbName" runat="server" CssClass="clsinput" Width="200px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            银行账户&nbsp;:&nbsp;<asp:TextBox ID="tbxBankZfbNum" runat="server" CssClass="clsinput" Width="200px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
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