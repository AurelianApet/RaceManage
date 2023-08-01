<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popMemberInfo.aspx.cs" Inherits="popMemberInfo" MasterPageFile="~/PopupMaster.master" %>
<%@ MasterType VirtualPath="~/PopupMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="PopupHeadContent" ID="popupHead" runat="server">
<title><%=Resources.Str.STR_MEMBERINFO%></title>
<script language="javascript" type="text/javascript" src="/js/jquery-1.7.min.js"></script>
<script language="javascript" type="text/javascript" src="/js/common.js"></script>
<script type="text/javascript" language="javascript">
function btnOk_Click()
{
    getElement("hdLeaveDate").value = getElement('<%=tbxDeleteDate.ClientID %>').value;
    getElement("hdInterceptDate").value = getElement('<%=tbxInterceptDate.ClientID %>').value;
}
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="PopupBodyContent" ID="popupBody" runat="server">
<asp:HiddenField ID="hdID" runat="server" Value="" />
<input type="hidden" id="hdLeaveDate" name="hdLeaveDate" value="" />
<input type="hidden" id="hdInterceptDate" name="hdInterceptDate" value="" />
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr><td class="clsspace"></td></tr>
<tr><td class="left">- <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_MEMBERINFO %>"></asp:Literal></td></tr>
<tr><td class="clsminispace"></td></tr>
<tr>
    <td>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder" style="border-collapse:collapse;">
        <tr>
            <th width="15%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_LOGINID %>"></asp:Literal>
            </th>
            <td width="35%" class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxLoginID" runat="server"
                    MaxLength="50"
                    CssClass="clsinput"
                    onkeydown="return false;">
                </asp:TextBox>
            </td>
            <th width="15%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Str, STR_NICKNAME %>"></asp:Literal>
            </th>
            <td width="35%" class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxNickName" runat="server"
                    MaxLength="255"
                    CssClass="clsinput">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNickName" runat="server" ValidationGroup="vGroup01" CssClass="error-message"
                    ControlToValidate="tbxNickName" 
                    ErrorMessage="<%$Resources:Err, ERR_NICKNAME_INPUT %>"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Str, STR_TELNO %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxTelNo" runat="server"
                    MaxLength="50"
                    CssClass="clsinput">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTelNo" runat="server" ValidationGroup="vGroup01" CssClass="error-message"
                    ControlToValidate="tbxTelNo" 
                    ErrorMessage="<%$Resources:Err, ERR_TELNO_INPUT %>"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revTelNo" runat="server" ValidationGroup="vGroup01" CssClass="error-message"
                    ControlToValidate="tbxTelNo"
                    ErrorMessage="<%$Resources:Err, ERR_TELNO_FORMAT %>"
                    Display="Dynamic"
                    ValidationExpression="<%$Resources:RegEx, REGEX_TELNO %>"></asp:RegularExpressionValidator>
            </td>
            <th class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Str, STR_LOGINPWD %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxLoginPwd" runat="server"
                    MaxLength="255"
                    CssClass="clsinput">
                </asp:TextBox>
                <asp:RegularExpressionValidator ID="revLoginPwd" runat="server" ValidationGroup="vGroup01" CssClass="error-message"
                    ControlToValidate="tbxLoginPwd"
                    ErrorMessage="<%$Resources:Err, ERR_LOGINPWD_FORMAT %>"
                    Display="Dynamic"
                    ValidationExpression="<%$Resources:RegEx, REGEX_LOGINPWD %>"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Str, STR_SITE %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:DropDownList ID="ddlSite" runat="server" Width="130px"></asp:DropDownList>
            </td>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal14" runat="server" Text="收款账号"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxBankName" runat="server" MaxLength="50" CssClass="clsinput"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal15" runat="server" Text="收款户名"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxBankOwner" runat="server" MaxLength="50" CssClass="clsinput"></asp:TextBox>
            </td>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal16" runat="server" Text="账户号码"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxBankNum" runat="server" MaxLength="50" CssClass="clsinput"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal18" runat="server" Text="<%$Resources:Str, STR_REGDATE %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:Literal ID="ltlRegdate" runat="server" Text=""></asp:Literal>
            </td>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal19" runat="server" Text="<%$Resources:Str, STR_LASTCONNECT %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:Literal ID="ltlLastConn" runat="server" Text=""></asp:Literal>
            </td>
        </tr>
        <tr>
            <th width="10%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal20" runat="server" Text="<%$Resources:Str, STR_DELETEDATE %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxDeleteDate" runat="server" onblur="checkDateTime(this);" CssClass="clsinput" Width="80px"></asp:TextBox>
                <ajax:CalendarExtender ID="cldDeleteDate" runat="server" TargetControlID="tbxDeleteDate"
                    Format="yyyy-MM-dd">
                </ajax:CalendarExtender>
                <asp:CheckBox ID="chkInitLeaveDate" CssClass="clscheckbox" runat="server" Text="<%$Resources:Str, STR_UNSELECT %>" />
            </td>
            <th width="10%" class="clstableheader rightborder">
                <asp:Literal ID="Literal21" runat="server" Text="<%$Resources:Str, STR_INTERCEPTDATE %>"></asp:Literal>
            </th>
            <td class="clstablecontent bottomborder left">                
                <asp:TextBox ID="tbxInterceptDate" runat="server" onblur="checkDateTime(this);" CssClass="clsinput" Width="80px"></asp:TextBox>
                <ajax:CalendarExtender ID="cldInterceptDate" runat="server" TargetControlID="tbxInterceptDate"
                    Format="yyyy-MM-dd">
                </ajax:CalendarExtender>
                <asp:CheckBox ID="chkInitBlock" CssClass="clscheckbox" runat="server" Text="<%$Resources:Str, STR_UNSELECT %>" />
            </td>
        </tr>
        </table>
    </td>
</tr>
<tr><td class="clsspace"></td></tr>
<tr><td class="left">- <asp:Literal ID="ltlMoneyInfo" runat="server" Text="<%$Resources:Str, STR_MONEYINFO %>"></asp:Literal> </td></tr>
<tr><td class="clsminispace"></td></tr>
<tr>
    <td>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder" style="border-collapse:collapse;">
        <tr>
            <th width="15%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Str, STR_OWNERMONEY %>"></asp:Literal>
            </th>
            <td width="20%" class="clstablecontent bottomborder left">                 
                <asp:Literal ID="ltlUserMoney" runat="server" Text=""></asp:Literal>
            </td>
            <th width="15%" class="clstableheader rightborder bottomborder">
                <asp:Literal ID="Literal22" runat="server" Text="<%$Resources:Str, STR_ADMINMONEY %>"></asp:Literal>
            </th>
            <td width="50%" class="clstablecontent bottomborder left">                 
                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Str, STR_MONEY %>"></asp:Literal>&nbsp;:&nbsp;<asp:TextBox ID="tbxCreditMoney" CssClass="clsinput" runat="server" MaxLength="10" Width="90px"></asp:TextBox>&nbsp;
                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Str, STR_ADDINFO %>"></asp:Literal>&nbsp;:&nbsp;<asp:TextBox ID="tbxCreditDesc" CssClass="clsinput" runat="server" Width="200px"></asp:TextBox>&nbsp;
                <asp:Button ID="btnCreditDo" runat="server" Text="<%$Resources:Str, STR_ADDMONEY %>" CssClass="button clsbutton" OnClick="btnCreditDo_Click" Width="50px" />
            </td>
        </tr>
        </table>
    </td>
</tr>
<tr><td class="clsspace"></td></tr>
<tr>
    <td>
        <asp:Button ID="btnOK" runat="server" Text="<%$Resources:Str, STR_CONFIRM %>"
            ValidationGroup="vGroup01"
            CssClass="button orange" Width="80px"
            OnClientClick="btnOk_Click();"
            OnClick="btnOK_Click" />
        <asp:Button ID="btnList" runat="server" Text="<%$Resources:Str, STR_CLOSE %>"
            CssClass="button blue" Width="80px"
            OnClick="btnList_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="<%$Resources:Str, STR_DELETE %>"
            CssClass="button" Width="80px"
            OnClientClick="return confirm(MSG_CONFIRMDELETE);"
            OnClick="btnDelete_Click" />
    </td>
</tr>
<tr><td class="clsspace"></td></tr>
<tr>
    <td>
        <table  width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
        <tr>
            <td width="33%" class="left">
                -&nbsp;<asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Str, STR_CHARGEHIST %>"></asp:Literal>(<asp:Literal ID="ltlTotalCharge" runat="server" Text="0"></asp:Literal>)
            </td>
            <td width="33%" class="left">
                -&nbsp;<asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Str, STR_DISCHARGEHIST %>"></asp:Literal>(<asp:Literal ID="ltlTotalDischarge" runat="server" Text="0"></asp:Literal>)
            </td>
            <td width="33%" class="left">
                -&nbsp;<asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Str, STR_LOGININFO %>"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="border:solid 2px black;vertical-align:top;height:300px;">
                <asp:GridView ID="gvChargeHist" runat="server" GridLines="None"
                    Width="100%"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    AllowSorting="true"
                    OnPageIndexChanging="gvChargeHist_PageIndexChange">
                <AlternatingRowStyle CssClass="clsaltrow" />
                <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>
                    <ItemStyle CssClass="clstablecontent withborder" Width="30px" />
                    <HeaderStyle CssClass="clstableheader withborder" />                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_MONEY %>">
                    <ItemStyle CssClass="clstablecontent withborder" Width="90px" />
                    <HeaderStyle CssClass="clstableheader withborder" />
                    <ItemTemplate>
                        <%#string.Format("{0:N0}", Eval("reqmoney")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:Str, STR_REQUESTDATE %>" DataField="regdate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    ItemStyle-Width="100px"
                    HeaderStyle-CssClass="clstableheader withborder" 
                    ItemStyle-CssClass="clstablecontent withborder" />
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_STATUS %>">
                    <ItemStyle CssClass="clstablecontent withborder" Width="50px"/>
                    <HeaderStyle CssClass="clstableheader withborder" />
                    <ItemTemplate>
                        <%#getMoneyInOutStatus(Eval("status"))%>
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
            <td style="border:solid 2px black;vertical-align:top;height:300px;">
                <asp:GridView ID="gvDischargeHist" runat="server" GridLines="None"
                    Width="100%"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    AllowSorting="true"
                    OnPageIndexChanging="gvDischargeHist_PageIndexChange">
                <AlternatingRowStyle CssClass="clsaltrow" />
                <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>
                    <ItemStyle CssClass="clstablecontent withborder" Width="30px" />
                    <HeaderStyle CssClass="clstableheader withborder" />                
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemStyle CssClass="clstablecontent withborder left" Width="90px" />
                    <HeaderStyle CssClass="clstableheader withborder" />
                    <HeaderTemplate>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_MONEY %>"></asp:Literal>
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
            <td style="border:solid 2px black;vertical-align:top;height:300px;">
                <asp:GridView ID="gvLoginHist" runat="server" GridLines="None"
                    Width="100%"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    AllowSorting="true"
                    OnPageIndexChanging="gvLoginHist_PageIndexChange">
                <AlternatingRowStyle CssClass="clsaltrow" />
                <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>
                    <ItemStyle CssClass="clstablecontent withborder" Width="30px" />
                    <HeaderStyle CssClass="clstableheader withborder" />                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONNIP %>">
                    <ItemStyle CssClass="clstablecontent withborder" />
                    <HeaderStyle CssClass="clstableheader withborder" />
                    <ItemTemplate>
                        <%#Eval("user_ip") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Str, STR_CONNDATE1 %>">
                    <ItemStyle CssClass="clstablecontent withborder" />
                    <HeaderStyle CssClass="clstableheader withborder" />
                    <ItemTemplate>
                        <%#Eval("regdate") %>
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
        </table>
    </td>
</tr>
<tr><td class="clsspace"></td></tr>
</table>
</asp:Content>