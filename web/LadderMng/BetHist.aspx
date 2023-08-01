<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BetHist.aspx.cs" Inherits="LadderMng_BetHist" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("GameMng");
});
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_BETTINGMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	        <tr>
                <td class="left clssearch">
                    <table width="100%">
                    <tr>
                        <td width="100%" class="left">
                            <asp:TextBox ID="tbxStartDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldStartDate" runat="server" TargetControlID="tbxStartDate"
                                Format="yyyy-MM-dd" />&nbsp;~&nbsp;
                            <asp:TextBox ID="tbxEndDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldEndDate" runat="server" TargetControlID="tbxEndDate"
                                Format="yyyy-MM-dd" />&nbsp;&nbsp;&nbsp;
                            <asp:Literal ID="Literal12" runat="server" Text = "<%$Resources:Str, STR_SITE %>"></asp:Literal>&nbsp;
                            <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="btnSearch_Click"></asp:DropDownList>&nbsp;&nbsp;
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
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SITE %>">
                            <ItemStyle CssClass="clstablecontent withborder" Width="100px" />
                            <HeaderStyle CssClass="clstableheader withborder" />            
                            <ItemTemplate>
                                &nbsp;<a href='?site=<%#Eval("site") %>'><%# Eval("site_name") == DBNull.Value ? "无归属" : Eval("site_name")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="会员">
                            <ItemTemplate>
                                <asp:Literal ID="ltlUser" runat="server"></asp:Literal>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="期数">
                            <ItemTemplate>
                                <b><%# string.Format("{0:yyyy-MM-dd} <font color='blue'>{1}</font>", Eval("sdate"), Eval("round")) %></b>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="regdate" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderStyle-CssClass="clstableheader withborder">
                            <ItemStyle CssClass="clstablecontent withborder"/>
                        </asp:BoundField>     
                        <asp:TemplateField HeaderText="投注方式">
                            <ItemTemplate>
                                <font color='green'><%#string.IsNullOrEmpty(Eval("descript").ToString()) ? "普通投注" : Eval("descript") %></font>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>           
                        <asp:TemplateField HeaderText="投注类别">
                            <ItemTemplate>
                                <asp:Literal ID="ltrBetMode" runat="server"></asp:Literal>                                 
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>     
                        <asp:TemplateField HeaderText="下注情况">
                            <ItemTemplate>
                                <asp:Literal ID="ltrBetPos" runat="server"></asp:Literal>                                 
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="betmoney" HeaderText="下注金额" DataFormatString="{0:F2}" HeaderStyle-CssClass="clstableheader withborder">
                            <ItemStyle CssClass="clstablecontent withborder"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ratio" HeaderText="下注金额" DataFormatString="{0:F2}" HeaderStyle-CssClass="clstableheader withborder">
                            <ItemStyle CssClass="clstablecontent withborder"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="winmoney" HeaderText="中奖金额" DataFormatString="{0:F2}" HeaderStyle-CssClass="clstableheader withborder">
                            <ItemStyle CssClass="clstablecontent withborder"/>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="结果">
                            <ItemTemplate>
                                <asp:Literal ID="ltrResult" runat="server"></asp:Literal> 
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
            </table>
		</div>
	</section>
</section>
</asp:Content>


