<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConnectHist.aspx.cs" Inherits="MemberMng_ConnectHist" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_CONHIST%></title>
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("MemberMng");
});
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_CONHIST %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	        <tr>
                <td class="left clssearch">
                    <table width="100%">
                    <tr>
                        <th width="10%" class="left"><%=Resources.Str.STR_REQUESTDATE%></th>
                        <td width="40%" class="left">
                            <asp:TextBox ID="tbxStartDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldStartDate" runat="server" TargetControlID="tbxStartDate"
                                        Format="yyyy-MM-dd" />&nbsp;~&nbsp;
                            <asp:TextBox ID="tbxEndDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldEndDate" runat="server" TargetControlID="tbxEndDate"
                                        Format="yyyy-MM-dd" />&nbsp;
                        </td>
                        <td width="50%" class="right">
                            <asp:DropDownList ID="ddlSearchKind" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="tbxSearchValue" CssClass="clsinput" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Str, STR_SEARCH %>" CssClass="button clsbutton"
                                OnClick="btnSearch_Click" />                
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="left">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" class="left">
                            <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:Str, STR_SELECTDELETE %>"
                                CssClass="button blue" Width="80px"
                                OnClientClick="return confirmCheck(MSG_CONFIRMDELETE);"
                                OnClick="btnDelete_Click" />
                        </td>
                        <td width="50%" class="right">
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td>
                    <asp:GridView ID="gvContent" runat="server" GridLines="None"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
                        OnSorting="gvContent_Sorting"
                        OnRowDataBound="gvContent_RowDataBound"
                        OnPageIndexChanging="gvContent_PageIndexChange">
                    <AlternatingRowStyle CssClass="clsaltrow" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input type="checkbox" class="clscheckbox" value="All" onclick="checkAll(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" class="clscheckbox" name="chkNo" value='<%#Eval("id") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="30px" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINID %>"  SortExpression="loginid">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <a href='?loginid=<%#Eval("loginid") %>'><b><%#Eval("loginid") %></b></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NAME %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <u><%#Eval("nick") %></u>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SITE %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#Eval("site") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONNDOMAIN %>">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlConnDomain" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_IP%>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#Eval("user_ip") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINTIME %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                
                                <%# String.Format("{0:MM-dd HH:mm:ss}", Eval("logindate")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGOUTTIME %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%# (Convert.ToInt32(Eval("logout")) > 0) ? String.Format("{0:MM-dd HH:mm:ss}", Eval("regdate")) : ""%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SUCCESS %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%# (Convert.ToInt32(Eval("success")) > 0) ? "<font color='green'>" + Resources.Str.STR_SUCCESS + "</font>" : "<font color='green'>" + Resources.Str.STR_FAILE + "</font>"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle VerticalAlign="Middle" />
                    <EmptyDataTemplate>
                        <table class="clstableborder" width="100%">
                        <tr>
                            <td class="clsemptyrow">
                                <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_NODATA %>"></asp:Literal>
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
            <tr><td class="clsspace"></td></tr>
            </table>
		</div>
	</section>
</section>
</asp:Content>