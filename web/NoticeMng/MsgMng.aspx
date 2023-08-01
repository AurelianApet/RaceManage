<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgMng.aspx.cs" Inherits="NoticeMng_MsgMng" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_MSGMNG%></title>
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("NoticeMng");
});
function onViewMsg(lID)
{
    $(".rowContent").css("display", "none");       
    $("#rowContent" + lID).css("display", "");
}
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_MSGMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td class="clssearch left" colspan="2">
		            <asp:DropDownList ID="ddlSearchKind" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="tbxSearchValue" runat="server" CssClass="clsinput"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Str, STR_SEARCH %>" CssClass="button clsbutton"
                        OnClick="btnSearch_Click" />     
		        </td>
		    </tr>
		    <tr><td class="clsspace" colspan="2"></td></tr>
            <tr>
                <td width="50%" class="left">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:Str, STR_SELECTDELETE %>"
                        OnClientClick="return confirmCheck(MSG_CONFIRMDELETE);" CssClass="button" Width="100px"
                        OnClick="btnDelete_Click" />
                </td>
                <td width="50%" class="right">
                    <asp:Button ID="btnSendMsg" runat="server" Text="<%$Resources:Str, STR_SENDMSG %>" CssClass="button" Width="100px"
                    OnClientClick="onPopupSendMsg(); return false;" />
                </td>
            </tr>
            <tr><td class="clsspace" colspan="2"></td></tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvContent" runat="server" GridLines="None"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        OnPageIndexChanging="gvContent_PageIndexChange"
                        OnRowDataBound="gvContent_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" class="clscheckbox" value="All" onclick="checkAll(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" class="clscheckbox" name="chkNo" value='<%#Eval("id") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="40px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                            <ItemTemplate>
                                <%#Eval("id")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NAME %>">
                            <ItemTemplate>
                                <%#Eval("user_loginid") %>&nbsp;(<%#(Eval("writer") == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(Eval("writer")))) ? Eval("user_nick") : Eval("writer") %>)
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="150px" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONTENT %>">
                            <ItemTemplate>
                                <%#cutString(Convert.ToString(Eval("htmlcontent")), 20) %>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_DATE %>" DataField="regdate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            HeaderStyle-CssClass="clstableheader withborder" 
                            ItemStyle-CssClass="clstablecontent withborder" ItemStyle-Width="150px" />
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_READTIME %>" DataField="chkdate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            HeaderStyle-CssClass="clstableheader withborder" 
                            ItemStyle-CssClass="clstablecontent withborder" ItemStyle-Width="150px" />
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_STATUS %>">
                            <ItemTemplate>
                                <%#Eval("deldate") != DBNull.Value ?
                                    (Convert.ToInt16(Eval("deltype")) == 1 ?
                                        "<font color='blue'>" + Resources.Str.STR_ADMIN + Resources.Str.STR_DELETE + "</font>" :
                                        "<font color='blue'>" + Resources.Str.STR_DELETE + "</font>") :
                                    (Eval("chkdate") == DBNull.Value ? 
                                        "<font color='red'>" + Resources.Str.STR_NOCONFIRM + "</font>" : 
                                        "<font color='green'>" + Resources.Str.STR_CONFIRM + "</font>") %>
                                </td></tr>
                                <tr height="30px" id='rowContent<%#Eval("id") %>' class="rowContent" style="display: none"><td colspan="3" class="withborder">&nbsp;</td><td class="clstablecontent withborder" colspan="4" align="left"><%#Eval("htmlcontent") %></td></tr>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="80px" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="clsaltrow" />
                    <EmptyDataRowStyle VerticalAlign="Middle" />
                    <EmptyDataTemplate>
                        <table class="clstableborder" width="100%">
                        <tr>
                            <td class="clsemptyrow">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Str, STR_NODATA %>"></asp:Literal>
                            </td>
                        </tr>
                        </table>
                    </EmptyDataTemplate>
                    <PagerSettings Mode="Numeric" Position="Bottom"
                        FirstPageText="<%$Resources:Str, STR_FIRST %>"
                        PreviousPageText="<%$Resources:Str, STR_PREV %>"
                        NextPageText="<%$Resources:Str, STR_NEXT %>"
                        LastPageText="&nbsp;<%$Resources:Str, STR_LAST %>"
                        PageButtonCount="10" />
                    <PagerStyle CssClass="clspager" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
            <tr><td class="clsspace" colspan="2"></td></tr>
		    </table>
		</div>
	</section>
</section>
</asp:Content>