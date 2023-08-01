<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginuser1.aspx.cs" Inherits="loginuser1" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="headCont" runat="server">
<title><%=Resources.Menu.MENU_CONSTATUS%></title>
</asp:Content>

<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1>
				    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_CONSTATUS %>"></asp:Literal>&nbsp;&nbsp;
				</h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	        <tr>
                <td class="left clssearch">
                    <asp:DropDownList ID="ddlSearchKind" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="tbxSearchValue" CssClass="clsinput" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Str, STR_SEARCH %>" CssClass="button clsbutton"
                        OnClick="btnSearch_Click" />                
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
                <td>
                    <asp:GridView ID="gvContent" runat="server" GridLines="None"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
                        OnSorting="gvContent_Sorting"
                        OnRowDataBound="gvContent_RowDataBound"
                        OnRowCommand="gvContent_RowCommand"
                        OnPageIndexChanging="gvContent_PageIndexChange">
                    <AlternatingRowStyle CssClass="clsaltrow" />
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#string.Format("{0}", Container.DataItemIndex + 1) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINID %>" SortExpression="loginid">
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
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONNPATH %>">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlConnPath" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CHARGEHIST %>">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%# (Eval("chargedate") == DBNull.Value) ? "" : string.Format("{0:MM-dd} / {1:F2}元", Eval("chargedate"), Eval("chargemoney")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_IP %>">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#Eval("user_ip") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINTIME %>" SortExpression="logindate">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#string.Format("{0:MM-dd HH:mm:ss}", Eval("logindate")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONNECTTIME%>" SortExpression="regdate">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Left" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#string.Format("{0:MM-dd HH:mm:ss}", Eval("regdate")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGOUT %>">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" Text="<%$Resources:Str, STR_LOGOUT %>"
                                    CommandName="Logout" Width="60px"
                                    CommandArgument='<%#Eval("id") %>'
                                    CssClass="button clsbutton blue"
                                    style="font-size: 8pt;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_IP_INTERCEPT %>">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" Text="<%$Resources:Str, STR_IP_INTERCEPT %>"
                                    CommandName="IpIntercept" Width="60px"
                                    CommandArgument='<%#Eval("user_ip") %>'
                                    CssClass="button clsbutton blue"
                                    style="font-size: 8pt;" />
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
