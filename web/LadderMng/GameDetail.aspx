<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameDetail.aspx.cs" Inherits="LadderMng_GameDetail" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="ltlTitle" runat="server" Text=""></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr><td class="clsspace"></td></tr>
	        <tr>
	            <td>
	                <asp:GridView ID="gvContent" runat="server" GridLines="None"
                        Width="100%"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
                        ShowFooter="true"
                        OnRowDataBound="gvContent_RowDataBound"
                        OnPageIndexChanging="gvContent_PageIndexChange">
                    <AlternatingRowStyle CssClass="clsaltrow" />
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="40px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_BETUSER %>">
                            <ItemTemplate>
                                <asp:Literal ID="ltlUser" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SEPERATOR %>">
                            <ItemTemplate>
                                <asp:Literal ID="ltlBetMode" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_BETPOS %>">
                            <ItemTemplate>
                                <asp:Literal ID="ltrBetPos" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_BETMONEY %>">
                            <ItemTemplate>
                                <font color='green'><%#string.Format("{0:F2} " + Resources.Str.STR_WON, Eval("betmoney"))%></font>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_WINMONEY %>">
                            <ItemTemplate>
                                <font color='blue'><%#string.Format("{0:F2} " + Resources.Str.STR_WON, Eval("winmoney"))%></font>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_BENEFIT %>">
                            <ItemTemplate>
                                <%# (Convert.ToDouble(Eval("betmoney")) >= Convert.ToDouble(Eval("winmoney")) ? "<font color='green'>" : "<font color='red'>") + string.Format("{0:F2} " + Resources.Str.STR_WON, Convert.ToDouble(Eval("betmoney")) - Convert.ToDouble(Eval("winmoney"))) + "</font>"%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_BETDATE %>">
                            <ItemTemplate>
                                <%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("regdate")) %>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
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
	        <tr>
                <td height="50px" align="center">
                    <asp:Button ID="btnList" runat="server" CssClass="orange" Width="100px" Text="<%$Resources:Str, STR_VIEWLIST %>" OnClick="btnList_Click" />
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
	        </table>
		</div>
	</section>
</section>
</asp:Content>