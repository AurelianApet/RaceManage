<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SosokMng.aspx.cs" Inherits="MemberMng_SosokMng" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_SOSOKMNG%></title>
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
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_SOSOKMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
                <td class="left clssearch">
                    <table width="100%">
                    <tr>
                        <td width="100%" class="left">
                            <asp:Literal ID="ltlSosok" runat="server" Text="<%$Resources:Str, STR_SITE %>"></asp:Literal>
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
                        OnRowCommand="gvContent_RowCommand"
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
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SITE %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <input type="text" name='siteName<%#Eval("id")%>' value='<%#Eval("site_name") %>' class="clsinput" style="width:90%;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_DOMAIN %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <input type="text" name='siteDomain<%#Eval("id")%>' value='<%#Eval("site_url") %>' class="clsinput" style="width:90%;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_USERNUM %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlUserNum" runat="server" Text=""></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="===">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" Text="<%$Resources:Str, STR_UPDATE %>"
                                    CommandName="updateSite" CssClass="button clsbutton"
                                    CommandArgument='<%#Eval("id") %>' />
                                <asp:Button ID="Button2" runat="server" Text="<%$Resources:Str, STR_DELETE %>"
                                    OnClientClick="return confirm(MSG_CONFIRMDELETE);"
                                    CommandName="deleteSite" CssClass="button clsbutton"
                                    CommandArgument='<%#Eval("id") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="100px" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
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
		    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
		    <tr><td width="100%" colspan="2" class="left">-<%=Resources.Str.STR_ADDSITE %></td></tr>
            <tr>
                <th class="clstableheader withborder"><%=Resources.Str.STR_SITE %></th>
                <th class="clstableheader withborder"><%=Resources.Str.STR_DOMAIN %></th>
                <th class="clstableheader withborder" width="100px"><%=Resources.Str.STR_ADDSITE%></th>
            </tr>
            <tr>
                <td class="clstablecontent withborder"><asp:TextBox ID="tbxSiteName" runat="server" Text="" CssClass="clsinput" Width="90%"></asp:TextBox></td>
                <td class="clstablecontent withborder"><asp:TextBox ID="tbxSiteDomain" runat="server" Text="" CssClass="clsinput" Width="90%"></asp:TextBox></td>
                <td class="clstablecontent withborder">
                    <asp:Button ID="Button3" runat="server" Text="<%$Resources:Str, STR_ADDSITE %>"
                        CssClass="button clsbutton" Width="90px"
                        OnClick="btnAdd_Click" />  
                </td>
            </tr>
            </table>
		</div>
	</section>
</section>
</asp:Content>