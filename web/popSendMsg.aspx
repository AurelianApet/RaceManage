<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popSendMsg.aspx.cs" Inherits="popSendMsg" MasterPageFile="~/PopupMaster.master" %>
<%@ MasterType VirtualPath="~/PopupMaster.master" %>

<asp:Content ContentPlaceHolderID="PopupHeadContent" ID="popupHead" runat="server">
<title><%=Resources.Str.STR_SENDMSG%></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="PopupBodyContent" ID="popupBody" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
<tr>
    <th rowspan="4" class="clstableheader rightborder bottomborder" width="30%"><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Str, STR_RECVUSERID %>"></asp:Literal></th>
    <td class="clstablecontent bottomborder"><asp:TextBox ID="tbxRecvID" runat="server" Text="" Width="98%" CssClass="clsinput"></asp:TextBox></td>
</tr>
<tr>
    <td class="clstablecontent bottomborder left">
        <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="ddlSite" runat="server" Enabled="false">        
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="clstablecontent bottomborder left">
        <asp:CheckBox ID="chkRecvAll" runat="server" Text="<%$Resources:Str, STR_RECVALL %>" AutoPostBack="true" CssClass="clscheckbox"
            OnCheckedChanged="chkRecvAll_CheckedChanged" />&nbsp;&nbsp;&nbsp;       
        <asp:CheckBox ID="chkRecvSite" runat="server" Text="<%$Resources:Str, STR_SITE %>" AutoPostBack="true" CssClass="clscheckbox"
            OnCheckedChanged="chkRecvSite_CheckedChanged" />&nbsp;        
    </td>
</tr>
<tr>
    <td class="clstablecontent bottomborder left">
        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Msg, MSG_SENDMSG_COMMENT %>"></asp:Literal>
    </td>
</tr>
<tr>
    <td colspan="2" class="clstablecontent bottomborder">
        <asp:TextBox ID="tbxTitle" runat="server" Text="" Width="98%" placeholder="<%$Resources:Str, STR_TITLE %>" CssClass="clsinput"></asp:TextBox>
    </td>
</tr>
<tr>
    <td colspan="2" class="clstablecontent">
        <asp:TextBox ID="tbxContent" runat="server" Text="" TextMode="MultiLine" placeholder="<%$Resources:Str, STR_CONTENT %>" Width="95%" Height="150px"></asp:TextBox>
    </td>
</tr>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr><td colspan="2" class="clsspace"></td></tr>
<tr>
    <td align="center">
        <asp:Button ID="btnSend" runat="server" Text="<%$Resources:Str, STR_SENDMSG %>" CssClass="button blue" Width="100px"
            OnClick="btnSend_Click" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:Str, STR_CANCEL %>" CssClass="button blue" Width="80px"
            OnClientClick="window.close(); return false;" />
    </td>
</tr>
</table>
</asp:Content>