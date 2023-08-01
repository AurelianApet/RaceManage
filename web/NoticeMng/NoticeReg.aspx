<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoticeReg.aspx.cs" Inherits="NoticeMng_NoticeReg" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("NoticeMng");
});
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<asp:HiddenField ID="hdNoticeID" runat="server" Value="0" />
<asp:HiddenField ID="hdRetType" runat="server" Value="" />
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_NOTICEMNG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	        <tr>
	            <td>
	                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
	                <tr>
                        <th width="10%" class="clstableheader rightborder bottomborder">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_WRITER %>"></asp:Literal>
                        </th>
                        <td class="clstablecontent bottomborder left">
                            <asp:TextBox ID="tbxWriter" runat="server" CssClass="clsinput"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th width="10%" class="clstableheader rightborder bottomborder">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_TITLE %>"></asp:Literal>
                        </th>
                        <td class="clstablecontent bottomborder left">
                            <asp:TextBox ID="tbxTitle" runat="server" CssClass="clsinput" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="10%" class="clstableheader rightborder">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Str, STR_CONTENT %>"></asp:Literal>
                        </th>
                        <td class="clstablecontent left">
                            <asp:TextBox ID="tbxContent" runat="server" Width="98%" Height="200px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
	            </td>
	        </tr>
	        <tr><td class="clsspace"></td></tr>
	        <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" CssClass="button" Width="80px" OnClick="btnOK_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnList" runat="server" CssClass="button" Width="80px" Text="<%$Resources:Str, STR_VIEWLIST %>"
                        OnClick="btnList_Click" />
                </td>
            </tr>
		    </table>
		</div>
	</section>
</section>
</asp:Content>
