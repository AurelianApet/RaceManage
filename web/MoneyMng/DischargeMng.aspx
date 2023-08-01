﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DischargeMng.aspx.cs" Inherits="MoneyMng_DischargeMng" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<script type="text/javascript" language="javascript">
var autoTimer = null;

$(document).ready(function() {
    changeMenuSelected("MoneyMng");
    
    var bAutoUpdate = getCookie("TOTOJTT::AUTOUPDATE::DISCHARGE");
    
    if(bAutoUpdate == "true") {
        $("#chkAutoUpdate").attr("checked", "checked");
        
        autoTimer = setInterval("refreshPage()", 10000);
    }
    
    $("#chkAutoUpdate").click(function() {
        if($(this).attr("checked") == "checked") {
            setCookie("TOTOJTT::AUTOUPDATE::DISCHARGE", "true");
            
            autoTimer = setInterval("refreshPage()", 10000);
        } else {
            setCookie("TOTOJTT::AUTOUPDATE::DISCHARGE", "");
            clearInterval(autoTimer);
        }
    });
});
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_DISCHARGEMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td class="clssearch">
		            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="10%" class="left"><%=Resources.Str.STR_REQUESTDATE%></th>
                        <td width="55%" class="left">
                            <asp:TextBox ID="tbxStartDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldStartDate" runat="server" TargetControlID="tbxStartDate"
                                        Format="yyyy-MM-dd" />&nbsp;~&nbsp;                
                            <asp:TextBox ID="tbxEndDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldEndDate" runat="server" TargetControlID="tbxEndDate"
                                        Format="yyyy-MM-dd" />&nbsp;    
                            <asp:Literal ID="Literal3" runat="server" Text = "<%$Resources:Str, STR_SITE %>"></asp:Literal>&nbsp;
                            <asp:DropDownList ID="ddlSite" runat="server"></asp:DropDownList>&nbsp;&nbsp;            
                        </td>
                        <th width="10%" class="left"><%=Resources.Str.STR_STATUS%></th>
                        <td width="25%" class="left">
                            <asp:RadioButtonList ID="rblStatus" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">                    
                            </asp:RadioButtonList>
                        </td>
                        <td width="10%" class="center" rowspan="20" valign="middle">
                        </td>
                    </tr>
                    <tr><td colspan="4" class="clssmallspace"></td></tr>
                    <tr>
                        <th width="10%" class="left"><%=Resources.Str.STR_SEARCH%></th>
                        <td class="left">
                            <%=Resources.Str.STR_LOGINID%>&nbsp;<asp:TextBox ID="tbxLoginID" runat="server" CssClass="clsinput"></asp:TextBox>&nbsp;&nbsp;
                            <%=Resources.Str.STR_NICKNAME%>&nbsp;<asp:TextBox ID="tbxNickName" runat="server" CssClass="clsinput"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                        <th width="10%" class="left"></th>
                        <td width="40%" class="left">
                            <asp:Button ID="btnSearch" runat="server" CssClass="button clsbutton green" Text="<%$Resources:Str, STR_SEARCH %>"
                                OnClick="SearchConditions_Changed" />
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
                        <td width="30%" class="left">
                            <asp:Button ID="btnApply" runat="server" Text="<%$Resources:Str, STR_DISCHARGEDO %>"
                                CssClass="button orange" Width="80px"
                                OnClientClick="return confirmCheck(MSG_CONFIRMAPPLY);"
                                OnClick="btnApply_Click" />
                            <asp:Button ID="btnStandby" runat="server" Text="<%$Resources:Str, STR_STANDBYDO %>"
                                CssClass="button blue" Width="80px"
                                OnClientClick="return confirmCheck(MSG_CONFIRMAPPLY);"
                                OnClick="btnStandby_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Str, STR_SELECTCANCEL %>"
                                CssClass="button" Width="80px"
                                OnClientClick="return confirmCheck(MSG_CONFIRMCANCEL);"
                                OnClick="btnCancel_Click" />
                        </td>
                        <td width="40%" class="center">
                            <b>(<asp:Literal ID="ltlTotalCount" runat="server" Text=""></asp:Literal>&nbsp;/&nbsp;<font color="red"><asp:Literal ID="ltlTotalMoney" runat="server"></asp:Literal></font>)</b>
                        </td>
                        <td width="30%" class="right">
                            <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:Str, STR_DELETE %>"
                                CssClass="button orange" Width="80px" Visible="false"
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
                        OnRowDataBound="gvContent_RowDataBound"
                        OnPageIndexChanging="gvContent_PageIndexChange"
                        OnRowCommand="gvContent_RowCommand">
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
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="30px" />
                            <HeaderStyle CssClass="clstableheader withborder" />                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_SITE %>">
                            <ItemStyle CssClass="clstablecontent withborder" Width="60px" />
                            <HeaderStyle CssClass="clstableheader withborder" />            
                            <ItemTemplate>
                                &nbsp;<%#Eval("site_name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_LOGINID %>">
                            <ItemStyle CssClass="clstablecontent withborder" Width="80px" />
                            <HeaderStyle CssClass="clstableheader withborder" />            
                            <ItemTemplate>
                                &nbsp;<a href='?loginid=<%#Eval("loginid") %>'><%#Eval("loginid") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NICKNAME %>">
                            <ItemStyle CssClass="clstablecontent withborder left" Width="100px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                &nbsp;<a href="javascript:;" onclick="showMenu(<%#Eval("user_id") %>, '<%#Eval("loginid") %>', event)"><u><%#Eval("nick") %></u></a>                    
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="账户信息">
                            <ItemStyle CssClass="clstablecontent withborder left" Width="120px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                &nbsp;<%#string.Format("{0}, {1}, {2}", Eval("bankname"), Eval("ownernum"), Eval("ownername"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle CssClass="clstablecontent withborder left" Width="90px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <HeaderTemplate>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_OWNERMONEY %>"></asp:Literal>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Literal ID="ltlOwnMoney" runat="server" Text=""></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle CssClass="clstablecontent withborder left" Width="90px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <HeaderTemplate>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_REQUEST %>"></asp:Literal>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <b><%#string.Format((Convert.ToInt16(Eval("status")) == Ronaldo.common.Constants.MONEYINOUT_STATUS_REQUEST) ? "<font color='red'>{0:N0}</font>" : "{0:N0}", Eval("reqmoney")) %></b>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Str, STR_REQUESTDATE %>" DataField="regdate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            ItemStyle-Width="120px"
                            HeaderStyle-CssClass="clstableheader withborder" 
                            ItemStyle-CssClass="clstablecontent withborder" />
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_STATUS %>">
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px"/>
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#getMoneyInOutStatus(Eval("status"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_DELETE %>">
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px"/>
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <%#getMoneyInOutDelStatus(Eval("deldate"), Eval("deltype"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_PROCESS %>">
                            <ItemStyle CssClass="clstablecontent withborder" Width="130px"/>
                            <HeaderStyle CssClass="clstableheader withborder" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkApply" runat="server" Text='<%#"[" + Resources.Str.STR_DISCHARGE + "]" %>'
                                    CommandName="ApplyCommand"
                                    CommandArgument='<%#Eval("id") %>'
                                    OnClientClick="return confirm(MSG_CONFIRMAPPLY);"></asp:LinkButton>
                                <asp:LinkButton ID="lnkStandby" runat="server" Text='<%#"[" + Resources.Str.STR_STANDBY + "]" %>'
                                    CommandName="StandbyCommand"
                                    CommandArgument='<%#Eval("id") %>'
                                    OnClientClick="return confirm(MSG_CONFIRMAPPLY);"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCancel" runat="server" Text='<%#"[" + Resources.Str.STR_CANCEL + "]" %>'
                                    CommandName="CancelCommand"
                                    CommandArgument='<%#Eval("id") %>'
                                    OnClientClick="return confirm(MSG_CONFIRMCANCEL);"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle VerticalAlign="Middle" />
                    <EmptyDataTemplate>
                        <table class="clstableborder" width="100%">
                        <tr>
                            <td class="clsemptyrow">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Str, STR_NODATA %>"></asp:Literal>
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
        