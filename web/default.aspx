<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="headCont" runat="server">
<script type="text/javascript" language="javascript">
function onPopupBetList(strGid)
{
    window.open("/popBetList.aspx?gid=" + strGid, 'BetOriginal', 'left=200,top=200,width=940,height=420,resizable=no,scrollbars=yes');
}
$(document).ready(function() { 
    changeMenuSelected("AdminHome");
    
    var bAutoUpdate = getCookie("ACE::AUTOUPDATE::ADMINHOME");
    
    if(bAutoUpdate == "true") {
        $("#chkAutoUpdate").attr("checked", "checked");
        
        autoTimer = setTimeout("refreshPage()", 30000);
    }
    
    $("#chkAutoUpdate").click(function() {
        if($(this).attr("checked") == "checked") {
            setCookie("ACE::AUTOUPDATE::ADMINHOME", "true");
            
            autoTimer = setTimeout("refreshPage()", 30000);
        } else {
            setCookie("ACE::AUTOUPDATE::ADMINHOME", "");
            clearInterval(autoTimer);
        }
    });
});
function refreshPage() {
    document.location.href = "default.aspx";
}
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1>
				    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_ADMINHOME %>"></asp:Literal>&nbsp;&nbsp;
				    <input type="checkbox" id="chkAutoUpdate" class="clscheckbox" style="float:none;" /><label for="chkAutoUpdate"><%=Resources.Str.STR_AUTOUPDATE %></label>
				</h1>
				
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td align="center">
		            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
		            <tr>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Str, STR_LOGINMEMBER %>"></asp:Literal></th>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Str, STR_LOGINMEMBERMONEY %>"></asp:Literal></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_JOINCOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_LEAVECOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_JOINCOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_LEAVECOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal15" runat="server" Text="<%$Resources:Str, STR_MEMBERCOUNT %>"></asp:Literal></th>
                        <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal16" runat="server" Text="<%$Resources:Str, STR_ALLMEMBERMONEY %>"></asp:Literal></th>
                    </tr>
                    <tr>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlLoginCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlLoginCash" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlJoinCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlLeaveCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalJoinCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalLeaveCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlMemberCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlMemberCash" runat="server" Text="0"></asp:Literal></td>
                    </tr>
                    </table>
                <td align="center">
            </tr>
            <tr>
		        <td align="center">
		            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
		            <tr>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_CHARGECOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_CHARGEMONEY %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_DISCHARGECOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_DISCHARGEMONEY %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_TODAY %>&nbsp;<%=Resources.Str.STR_BENIFITMONEY%></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_CHARGECOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_CHARGEMONEY %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_DISCHARGECOUNT %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_DISCHARGEMONEY %></th>
                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WHOLE%>&nbsp;<%=Resources.Str.STR_BENIFITMONEY%></th>
                    </tr>
                    <tr>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlChargeCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlChargeMoney" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlDisChargeCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlDisChargeMoney" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlBenifitMoney" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalChargeCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalChargeMoney" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalDisChargeCount" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalDisChargeMoney" runat="server" Text="0"></asp:Literal></td>
                        <td class="clstablecontent rightborder"><asp:Literal ID="ltlTotalBenifitMoney" runat="server" Text="0"></asp:Literal></td>
                    </tr>
                    </table>
                <td align="center">
            </tr>
		    <tr>
                <td align="center">
                    <asp:Repeater ID="rpSiteStatus" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
                        <tr>
                            <th class="clstableheader rightborder bottomborder" colspan="2"><asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_SEPERATOR %>"></asp:Literal></th>
                            <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_CHARGECOUNT %>"></asp:Literal></th>
                            <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Str, STR_DISCHARGECOUNT %>"></asp:Literal></th>
                            <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Str, STR_BETCOUNT_MONEY %>"></asp:Literal></th>
                            <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Str, STR_WINCOUNT %>"></asp:Literal></th>
                            <th class="clstableheader rightborder bottomborder"><asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Str, STR_JOINCOUNT %>"></asp:Literal></th>
                            <th class="clstableheader bottomborder"><asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Str, STR_VISITCOUNT %>"></asp:Literal></th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="clstablecontent rightborder bottomborder" rowspan="2"><%#Eval("Sep")%></td>
                            <td class="clstablecontent rightborder bottomborder"><asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Str, STR_COUNT %>"></asp:Literal></td>
                            <td class="clstablecontent rightborder bottomborder"><%#null2Zero(Eval("chcount"))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#null2Zero(Eval("dischcount"))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#null2Zero(Eval("betcount"))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#null2Zero(Eval("wincount"))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#null2Zero(Eval("hmcount"))%></td>
                            <td class="clstablecontent rightborder bottomborder" rowspan="2"><%#null2Zero(Eval("joincount"))%></td>
                            <td class="clstablecontent bottomborder" rowspan="2"><%#null2Zero(Eval("visitcount"))%></td>
                        </tr>
                        <tr>
                            <td class="clstablecontent rightborder bottomborder"><asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Str, STR_MONEY %>"></asp:Literal></td>
                            <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:N0}", null2Zero(Eval("chmoney")))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:N0}", null2Zero(Eval("dischmoney")))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:N0}", null2Zero(Eval("betmoney")))%></td>
                            <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:N0}", null2Zero(Eval("winmoney")))%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>  
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="49%" align="left" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th width="49%" align="left">-&nbsp;<%=Resources.Str.STR_REQUEST_CHARGE%></th>
                            </tr>
                            <tr valign="top">
                                <td align="left">
                                    <asp:Repeater ID="rpCharge" runat="server" OnItemDataBound="visibleEmptyRow">
                                    <HeaderTemplate>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
                                    <tr>
                                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_LOGINID %></th>
                                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_CHARGEMONEY %></th>
                                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_REQUESTDATE %></th>
                                        <th class="clstableheader bottomborder"><%=Resources.Str.STR_STATUS %></th>
                                    </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr>
                                        <td class="clstablecontent rightborder bottomborder"><%#Eval("loginid") %></td>
                                        <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:N0}", Eval("reqmoney")) %></td>
                                        <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("regdate")) %></td>
                                        <td class="clstablecontent bottomborder"><%#getMoneyInOutStatus(Convert.ToInt16(Eval("status"))) %></td>
                                    </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>                        
                                    <tr id="rowEmpty" runat="server" visible="false"><td class="clsemptyrow" colspan="10"><%=Resources.Str.STR_NODATA %></td></tr>
                                    </table>
                                    </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            </table>
                        </td>
                        <td width="2%"></td>
                        <td width="49%" class="left top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th align="left">-&nbsp;<%=Resources.Str.STR_REQUEST_DISCHARGE%></th>
                            </tr>
                            <tr>
                                <td align="left">  
                                    <asp:Repeater ID="rpDischarge" runat="server" OnItemDataBound="visibleEmptyRow">
                                    <HeaderTemplate>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
                                    <tr>
                                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_LOGINID %></th>
                                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_DISCHARGEMONEY %></th>
                                        <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_REQUESTDATE %></th>
                                        <th class="clstableheader bottomborder"><%=Resources.Str.STR_STATUS %></th>
                                    </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr>
                                        <td class="clstablecontent rightborder bottomborder"><%#Eval("loginid") %></td>
                                        <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:N0}", Eval("reqmoney")) %></td>
                                        <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("regdate")) %></td>
                                        <td class="clstablecontent bottomborder"><%#getMoneyInOutStatus(Convert.ToInt16(Eval("status"))) %></td>
                                    </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>                        
                                    <tr id="rowEmpty" runat="server" visible="false"><td class="clsemptyrow" colspan="10"><%=Resources.Str.STR_NODATA %></td></tr>
                                    </table>
                                    </FooterTemplate>
                                    </asp:Repeater>  
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td align="center">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th align="left">-&nbsp;<%=Resources.Str.STR_RACE %><%=Resources.Str.STR_BETSTATUS %></th>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Repeater ID="rpBetStatus" runat="server" OnItemDataBound="visibleEmptyRow">
                            <HeaderTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
                            <tr>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_ROUND%></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_GAMEDATE %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK1 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK2 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK3 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK4 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK5 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK6 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK7 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK8 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK9 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_RANK10 %></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_BETMONEY%></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WINMONEY%></th>
                            </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("round") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("sdate")) %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank1") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank2") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank3") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank4") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank5") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank6") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank7") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank8") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank9") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("rank10") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:F2}", Eval("betmoney")) %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:F2}", Eval("winmoney")) %></td>
                            </tr>
                            </ItemTemplate>
                            <FooterTemplate>                        
                            <tr id="rowEmpty" runat="server" visible="false"><td class="clsemptyrow" colspan="10"><%=Resources.Str.STR_NODATA %></td></tr>
                            </table>
                            </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td align="center">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th align="left">-&nbsp;<%=Resources.Str.STR_LADDER %><%=Resources.Str.STR_BETSTATUS %></th>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Repeater ID="rpLadderBetStatus" runat="server" OnItemDataBound="visibleEmptyRow">
                            <HeaderTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="clstableborder">
                            <tr>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_ROUND%></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_GAMEDATE %></th>
                                <th class="clstableheader rightborder bottomborder">左，右</th>
                                <th class="clstableheader rightborder bottomborder">3，4</th>
                                <th class="clstableheader rightborder bottomborder">单，双</th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_BETMONEY%></th>
                                <th class="clstableheader rightborder bottomborder"><%=Resources.Str.STR_WINMONEY%></th>
                            </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                                <td class="clstablecontent rightborder bottomborder"><%#Eval("round") %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("sdate")) %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#getLadderGameResult(1, Convert.ToInt32(Eval("startpos"))) %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#getLadderGameResult(2, Convert.ToInt32(Eval("laddercount")))%></td>
                                <td class="clstablecontent rightborder bottomborder"><%#getLadderGameResult(3, Convert.ToInt32(Eval("oddeven")))%></td>
                                <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:F2}", Eval("betmoney")) %></td>
                                <td class="clstablecontent rightborder bottomborder"><%#string.Format("{0:F2}", Eval("winmoney")) %></td>
                            </tr>
                            </ItemTemplate>
                            <FooterTemplate>                        
                            <tr id="rowEmpty" runat="server" visible="false"><td class="clsemptyrow" colspan="10"><%=Resources.Str.STR_NODATA %></td></tr>
                            </table>
                            </FooterTemplate>
                            </asp:Repeater>
                        </td>
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
