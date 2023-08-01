<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashMng.aspx.cs" Inherits="MoneyMng_CashMng" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_CASHMNG %></title>
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
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_CASHMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td class="left clssearch">
                    <asp:Literal ID="Literal12" runat="server" Text = "<%$Resources:Str, STR_SITE %>"></asp:Literal>&nbsp;
                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="SearchConditions_Changed"></asp:DropDownList>&nbsp;&nbsp;
		            <asp:TextBox ID="tbxStartDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                    <ajax:CalendarExtender ID="cldStartDate" runat="server" TargetControlID="tbxStartDate"
                                Format="yyyy-MM-dd" />&nbsp;~&nbsp;
                    <asp:TextBox ID="tbxEndDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                    <ajax:CalendarExtender ID="cldEndDate" runat="server" TargetControlID="tbxEndDate"
                                Format="yyyy-MM-dd" />&nbsp;
                    <asp:DropDownList ID="ddlSearchKey" runat="server" Width="100px">
                    </asp:DropDownList>
                    <asp:TextBox ID="tbxSearchValue" CssClass="clsinput" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button clsbutton green" Text="<%$Resources:Str, STR_SEARCH %>"
                        OnClick="SearchConditions_Changed" />
		        </td>
		    </tr>
		    <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="left">
                    <!--<asp:Button ID="btnDelete" runat="server" Text="<%$Resources:Str, STR_SELECTDELETE %>"
                        CssClass="button" Width="80px"
                        OnClientClick="return confirmCheck(MSG_CONFIRMDELETE);"
                        OnClick="btnDelete_Click" />-->
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:GridView ID="gvContent" runat="server" GridLines="None"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
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
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINID %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <a href='?loginid=<%#Eval("loginid") %>'><b><%#Eval("loginid") %></b></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NICKNAME %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#Eval("nick") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_DATE %>" DataField="regdate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            HeaderStyle-CssClass="clstableheader withborder"
                            ItemStyle-CssClass="clstablecontent withborder" />
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONTENT %>">
                            <ItemStyle CssClass="clstablecontent withborder left" Width="40%" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlContent" runat="server" Text=""></asp:Literal>        
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_MONEYCHANGE %>">
                            <ItemStyle HorizontalAlign="Right" CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#Convert.ToInt64(Eval("credit")) > 0 ? string.Format("<font color='green'>{0:F2}</font>", Eval("credit")) :
                                    string.Format("<font color='red'>-{0:F2}</font>", Eval("debit")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_CHANGEDMONEY %>" DataField="curmoney" DataFormatString="{0:F2}"
                            ItemStyle-HorizontalAlign="Right"
                            ItemStyle-ForeColor="green"
                            HeaderStyle-CssClass="clstableheader withborder" 
                            ItemStyle-CssClass="clstablecontent withborder" />
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
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="right">
                    <b><asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_CREDIT %>"></asp:Literal><asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Str, STR_SUM %>"></asp:Literal>:
                    <font color="red"><asp:Literal runat="server" ID="ltlSumCredit"></asp:Literal></font>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Str, STR_DEBIT %>"></asp:Literal><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Str, STR_SUM %>"></asp:Literal>:
                    <font color="green"><asp:Literal runat="server" ID="ltlSumDebit"></asp:Literal></font>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Str, STR_CURRENT %>"></asp:Literal><asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Str, STR_SUM %>"></asp:Literal>:
                    <font color="orange"><asp:Literal runat="server" ID="ltlSumCurrent"></asp:Literal></font></b>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
                    <tr>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Str, STR_LOGINID %>"></asp:Literal></th>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Str, STR_CONTENT %>"></asp:Literal></th>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Str, STR_MONEY %>"></asp:Literal></th>
                        <th class="clstableheader bottomborder">===</th>
                    </tr>
                    <tr>
                        <td class="clstablecontent rightborder" width="100px"><asp:TextBox ID="tbxCreditLoginID" CssClass="clsinput" runat="server"></asp:TextBox></td>
                        <td class="clstablecontent rightborder" align="left"><asp:TextBox ID="tbxCreditDesc" CssClass="clsinput" runat="server" Width="99%"></asp:TextBox></td>
                        <td class="clstablecontent rightborder" width="100px"><asp:TextBox ID="tbxCreditMoney" CssClass="clsinput" runat="server" MaxLength="10" Width="90px"></asp:TextBox></td>
                        <td class="clstablecontent" width="50px"><asp:Button ID="btnCreditDo" runat="server" Text="<%$Resources:Str, STR_CONFIRM %>" CssClass="button clsbutton" OnClick="btnCreditDo_Click" /></td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
		    </table>
        </div>
    </section>
</section>
</asp:Content>