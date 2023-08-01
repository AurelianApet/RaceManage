<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleMng.aspx.cs" Inherits="MoneyMng_SaleMng" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_SALEMNG%></title>
<script type="text/javascript" language="javascript">
$(document).ready(function() {
    changeMenuSelected("MoneyMng");
});
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_SALEMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td class="clssearch left">
		            <asp:TextBox ID="tbxStartDate" runat="server" onblur="checkDateTime(this);" CssClass="clsinput" Width="80px"></asp:TextBox>
                    <ajax:CalendarExtender ID="cldStartDate" runat="server" TargetControlID="tbxStartDate"
                                Format="yyyy-MM-dd" />&nbsp;~&nbsp;
                    
                    <asp:TextBox ID="tbxEndDate" runat="server" onblur="checkDateTime(this);" CssClass="clsinput" Width="80px"></asp:TextBox>
                    <ajax:CalendarExtender ID="cldEndDate" runat="server" TargetControlID="tbxEndDate"
                                Format="yyyy-MM-dd" />&nbsp;
                    <asp:Literal ID="Literal2" runat="server" Text = "<%$Resources:Str, STR_SITE %>"></asp:Literal>&nbsp;
                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="btnSearch_Click"></asp:DropDownList>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlSearchKind" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="tbxSearchValue" CssClass="clsinput" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Str, STR_SEARCH %>" CssClass="button clsbutton"
                        OnClick="btnSearch_Click" />
		        </td>
		    </tr>
		    <tr><td class="clsspace"></td></tr>
            <tr>
                <td>
                    <asp:GridView ID="gvContent" runat="server" GridLines="None"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AllowPaging="false"
                        AllowSorting="true"
                        ShowFooter="true"
                        OnRowDataBound="gvContent_RowDataBound"
                        OnPageIndexChanging="gvContent_PageIndexChange">
                    <AlternatingRowStyle CssClass="clsaltrow" />
                    <Columns>        
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_DATE %>" DataField="regdate" DataFormatString="{0:yyyy-MM-dd}"
                            HeaderStyle-CssClass="clstableheader withborder" 
                            ItemStyle-CssClass="clstablecontent withborder" />
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CHARGEMONEY %>">
                            <ItemStyle CssClass="clstablecontent withborder" ForeColor="Blue" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#string.Format("{0:F2}({1:}/<font color='red'>{2:N0}</font>)", Eval("charge"), Eval("charge_count"), Eval("charge_one_count")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_DISCHARGEMONEY %>">
                            <ItemStyle CssClass="clstablecontent withborder" ForeColor="Red" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#string.Format("{0:F2}({1})", Eval("discharge"), Eval("discharge_count"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate><%=Resources.Str.STR_TODAY%>&nbsp;<%=Resources.Str.STR_BETMONEY %></HeaderTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" ForeColor="Blue" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%# string.Format("{0:F2}({1:N0})", Eval("betmoney"), Eval("betcount")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate><%=Resources.Str.STR_TODAY%>&nbsp;<%=Resources.Str.STR_WINMONEY %></HeaderTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" ForeColor="Red" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%# string.Format("{0:F2}({1:N0})", Eval("winmoney"), Eval("wincount"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate><%=Resources.Str.STR_TODAY%>&nbsp;<%=Resources.Str.STR_ALLSALEMONEY%></HeaderTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%# string.Format("{0:F2}", Eval("betsalemoney")) %>
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