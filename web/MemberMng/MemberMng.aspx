<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberMng.aspx.cs" Inherits="MemberMng_MemberMng" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_MEMBERINFO %></title>
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("MemberMng");
});
function OnSendSms() {
    if($("#trSmsPart").css("display") == "none")
        $("#trSmsPart").css("display", "block");
    else
        $("#trSmsPart").css("display", "none");
}
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_MEMBERINFO %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	        <tr>
                <td class="left clssearch">
                    <table width="100%">
                    <tr>
                        <td width="35%" class="left">
                            <asp:Literal ID="Literal12" runat="server" Text = "<%$Resources:Str, STR_SITE %>"></asp:Literal>&nbsp;
                            <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="btnSearch_Click"></asp:DropDownList>
                            <asp:Literal ID="ltlUserCount" runat="server"></asp:Literal>&nbsp;                
                        </td>
                        <th width="3%" class="left"><%=Resources.Str.STR_STATUS%></th>
                        <td width="12%" class="left">
                            <asp:RadioButtonList ID="rblStatus" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">                    
                            </asp:RadioButtonList>&nbsp;
                        </td>
                        <td width="15%" class="right">
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
                            <asp:Button ID="btnUpdate" runat="server" Text="<%$Resources:Str, STR_SELECTUPDATE %>"
                                CssClass="button orange" Width="80px"
                                OnClientClick="return confirmCheck(MSG_CONFIRMAPPLY);"
                                OnClick="btnUpdate_Click" Visible="false" />
                            <asp:Button ID="btnExcelDown" runat="server" Text="<%$Resources:Str, STR_EXCELDOWN %>"
                                CssClass="button red" Width="80px"
                                OnClick="btnExcelDown_Click" Visible="false" />
                            <asp:Button ID="btnRegDist" runat="server" Text="<%$Resources:Str, STR_REGTODIST %>"
                                CssClass="button green" Width="100px"
                                OnClick="btnRegDist_Click" />
                        </td>
                        <td width="50%" class="right">
                            <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:Str, STR_LEAVE %>"
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
                        Width="100%" ForeColor="CornflowerBlue"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        AllowSorting="true"
                        OnSorting="gvContent_Sorting"
                        OnRowDataBound="gvContent_RowDataBound"
                        OnRowCommand="gvContent_RowCommand"
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
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINID %>" SortExpression="loginid">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" ForeColor="White" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlLoginID" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NICKNAME %>" SortExpression="nick">
                            <ItemStyle CssClass="clstablecontent withborder left" />
                            <HeaderStyle CssClass="clstableheader withborder" ForeColor="White" />
                            <ItemTemplate>
                                <u><asp:Literal ID="ltlNick" runat="server"></asp:Literal></u>
                                <a href='javascript:;' onclick="onPopupMemberInfo('<%#Eval("id") %>')"><img src="/images/popUser.png" height="15px" width="15px" border="0" /></a>
                                <a href='javascript:;' onclick="onPopupSendMsg('<%#Eval("loginid") %>')"><img src="/images/sendMemo.png" height="15px" width="15px" border="0" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CASH %>" SortExpression="cash">
                            <ItemStyle CssClass="clstablecontent withborder" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="clstableheader withborder" ForeColor="White" />
                            <ItemTemplate>
                                <a href='../MoneyMng/CashMng.aspx?loginid=<%#Eval("loginid") %>'><font color="red"><%#string.Format("{0:F2}", Eval("cash")) %></font></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CHARGEDISCARGE %>" SortExpression="point">
                            <ItemStyle CssClass="clstablecontent withborder right" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="clstableheader withborder" ForeColor="White" />
                            <ItemTemplate>
                                <%#Resources.Str.STR_PREF_CHARGE%>)&nbsp;<a href='../MoneyMng/ChargeMng.aspx?loginid=<%#Eval("loginid") %>'><font color="green"><%#string.Format("{0:F2}", Eval("charge_money")) %></font></a><br />
                                <%#Resources.Str.STR_PREF_DISCHARGE%>)&nbsp;<a href='../MoneyMng/DischargeMng.aspx?loginid=<%#Eval("loginid") %>'><font color="red"><%#string.Format("{0:F2}", Eval("discharge_money")) %></font></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_CONTRIBUTE %>" DataField="benefit" DataFormatString="{0:N0}" SortExpression="benefit" HeaderStyle-ForeColor="White"
                            HeaderStyle-CssClass="clstableheader withborder" ItemStyle-CssClass="clstablecontent withborder right" />
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_PERMISSION %>" SortExpression="ulevel">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" ForeColor="White" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlPermission" runat="server" Text=""></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SITE %>">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlSite" runat="server" Text=""></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="账户">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:Literal ID="ltlBankInfo" runat="server" Text=""></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONNDATE %>" SortExpression="regdate">
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" ForeColor="White" />
                            <ItemTemplate>
                                <%# string.Format("{0:yyyy-MM-dd HH:mm}<br /><font color=\"#999999\">{1:yyyy-MM-dd HH:mm}</font>", Eval("logindate"), Eval("regdate"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_IP %>" DataField="user_ip"
                            HeaderStyle-CssClass="clstableheader withborder" 
                            ItemStyle-CssClass="clstablecontent withborder" />
                        <asp:TemplateField HeaderText="===">
                            <ItemStyle CssClass="clstablecontent withborder" Width="90px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <a href='javascript:;' onclick="onPopupMemberInfo('<%#Eval("id") %>')"><input type="button" value="<%=Resources.Str.STR_UPDATE %>" class="button clsbutton" style="font-size: 8pt;" /></a>
                                <asp:Button ID="Button1" runat="server" Text="<%$Resources:Str, STR_UPDATE %>" style="font-size: 8pt"
                                    CssClass="button clsbutton" Visible="false" />
                                <asp:Button ID="Button2" runat="server" Text="<%$Resources:Str, STR_DELETE %>" style="font-size: 8pt"
                                    CommandName="MemberDelete" CssClass="button clsbutton"
                                    OnClientClick="return confirm(MSG_CONFIRMDELETE);"
                                    CommandArgument='<%#Eval("id") %>' />
                                <asp:Literal ID="ltlDetail" runat="server" Text=""></asp:Literal>
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