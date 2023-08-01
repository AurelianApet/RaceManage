<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameHist.aspx.cs" Inherits="GameMng_GameHist" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<script type="text/javascript" language="javascript">
    var OldLeftTime = 0;
    var LeftTime = 0;
    var TimerID = 0;
    $(document).ready(function() {
        changeMenuSelected("GameMng");
        getInfo();
    });
    function getInfo() {
        var getTimerUrl = "/getGameInfo.aspx";
        $.ajax({
            url: getTimerUrl,
            dataType: 'json',
            type: 'POST',
            success: function(data) {
                OldLeftTime = parseInt(data.lefttime);
                LeftTime = parseInt(data.lefttime);
                updateTimer();
                $("#spCurRound").html(data.round);
                $("#spBetMoney").html(data.betmoney);
                $("#spAWinMoney").html(data.awin);
                $("#spBWinMoney").html(data.bwin);
                $("#spCWinMoney").html(data.cwin);
                var refresh = data.refresh;
                if (refresh == "1" || refresh == 1)
                    PageRefresh();
                else
                    TimerID = setInterval(countDown, 1000);
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
            }
        });
    }
    function PageRefresh() {
        location.href = location.href;
    }
    function countDown() {
        LeftTime = LeftTime - 1;
        if (LeftTime > 0)
            updateTimer();
        else {
            clearInterval(TimerID);
            setTimeout(PageRefresh, 3000);
        }
        if (OldLeftTime > (LeftTime + 7)) {
            clearInterval(TimerID);
            getInfo();
        }
    }
    function updateTimer() {
        if (LeftTime > 30) {
            var spMinute = parseInt((LeftTime - 30) / 60);
            var spSecond = (LeftTime - 30) % 60;
            $("#spBetLeftMinute").html("0" + spMinute);
            $("#spBetLeftSecond").html(spSecond < 10 ? "0" + spSecond : spSecond);
        } else {
            $("#spBetLeftMinute").html("00");
            $("#spBetLeftSecond").html("00");
        }

        if (LeftTime > 0) {
            var spMinute = parseInt(LeftTime / 60);
            var spSecond = LeftTime % 60;
            $("#spSelLeftMinute").html("0" + spMinute);
            $("#spSelLeftSecond").html(spSecond < 10 ? "0" + spSecond : spSecond);

            spMinute = parseInt((LeftTime + 45 ) / 60);
            spSecond = (LeftTime + 45) % 60;
            $("#spResultLeftMinute").html("0" + spMinute);
            $("#spResultLeftSecond").html(spSecond < 10 ? "0" + spSecond : spSecond);
        }
    }
function onUpdateResult(gid) {
    if ($("#rowUpdate" + gid).css("display") == "none") {
        $(".rowUpdate").css("display", "none");
        $("#rowUpdate" + gid).css("display", "");
    } else {
        $("#rowUpdate" + gid).css("display", "none");
    }
}
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_GAMEMNG %>"></asp:Literal>&nbsp;(<asp:Literal ID="ltlGameStatus" runat="server" Text=""></asp:Literal>)</h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	        <tr>
                <td class="left clssearch" style="padding:0px 15px;">
                    <table width="100%">
                    <tr>
                        <td width="30%" class="left">
                            <asp:TextBox ID="tbxStartDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldStartDate" runat="server" TargetControlID="tbxStartDate"
                                Format="yyyy-MM-dd" />&nbsp;~&nbsp;
                            <asp:TextBox ID="tbxEndDate" runat="server" onblur="checkDateTime(this, true);" CssClass="clsinput" Width="80px"></asp:TextBox>
                            <ajax:CalendarExtender ID="cldEndDate" runat="server" TargetControlID="tbxEndDate"
                                Format="yyyy-MM-dd" />
                            <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Str, STR_SEARCH %>" CssClass="button clsbutton"
                                OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td width="70%" class="right" style="font-size:14px;">
                            <table cellpadding="0" cellspacing="0" border="1">
                            <tr height="30px">
                                <td class="left" width="300px">本期（<span id="spCurRound" name="spCurRound"></span>）下注金额: <font color='red'><span id="spBetMoney" name="spBetMoney">0</span></font>元 </td>
                                <td colspan="3" class="right"><span style="font-size:16px;font-weight:bold;">选择结果倒计时：&nbsp;</span><span style="font-size:17px;color:Red;font-weight:bold;"><span id="spSelLeftMinute" name="spSelLeftMinute">00</span>:<span id="spSelLeftSecond" name="spSelLeftSecond" style="margin-right:20px;">00</span></span></td>
                            </tr>
                            <tr height="25px">
                                <td class="left">结束投注倒计时&nbsp;:&nbsp;<span id="spBetLeftMinute" name="spBetLeftMinute">00</span>:<span id="spBetLeftSecond" name="spBetLeftSecond">00</span></td>
                                <td class="center" width="100px"><span id="spAWinMoney" name="spAWinMoney"></span></td>
                                <td class="center" width="100px"><span id="spBWinMoney" name="spBWinMoney"></span></td>
                                <td class="center" width="100x"><span id="spCWinMoney" name="spCWinMoney"></span></td>
                            </tr>
                            <tr height="25px">
                                <td class="left">出奖结果倒计时&nbsp;:&nbsp;<span id="spResultLeftMinute" name="spResultLeftMinute">00</span>:<span id="spResultLeftSecond" name="spResultLeftSecond">00</span></td>
                                <td class="center"><asp:Button ID="btnSelA" runat="server" Text="A方案" CssClass="button clsbutton" Width="60px" OnClientClick="return confirm('您确定选择A方案吗？');" OnClick="btnSelA_Click" /></td>
                                <td class="center"><asp:Button ID="btnSelB" runat="server" Text="B方案" CssClass="button clsbutton" Width="60px" OnClientClick="return confirm('您确定选择B方案吗？');" OnClick="btnSelB_Click"  /></td>
                                <td class="center"><asp:Button ID="btnSelC" runat="server" Text="C方案" CssClass="button clsbutton" Width="60px" OnClientClick="return confirm('您确定选择C方案吗？');" OnClick="btnSelC_Click"  /></td>
                            </tr>
                            </table>
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
                        OnPageIndexChanging="gvContent_PageIndexChange"
                        OnRowCommand="gvContent_RowCommand">
                    <AlternatingRowStyle CssClass="clsaltrow" />
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_NUMBER %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="40px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_ROUND%>">
                            <ItemTemplate>
                                <a href='GameDetail.aspx?id=<%#Eval("id") %>&page=<%#PageNumber %>'><b><%#string.Format("{0:MM-dd} {1}", Eval("sdate"), Eval("round"))%></b></a>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="120px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="开始时间">
                            <ItemTemplate>
                                <b><%#string.Format("{0:HH:mm:ss}", Eval("sdate"))%></b>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="80px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK1%>">
                            <ItemTemplate>
                                <%#Eval("rank1") == DBNull.Value ?  "" : Eval("rank1")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK2%>">
                            <ItemTemplate>
                                <%#Eval("rank2") == DBNull.Value ?  "" : Eval("rank2")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK3%>">
                            <ItemTemplate>
                                <%#Eval("rank3") == DBNull.Value ?  "" : Eval("rank3")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK4%>">
                            <ItemTemplate>
                                <%#Eval("rank4") == DBNull.Value ?  "" : Eval("rank4")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK5%>">
                            <ItemTemplate>
                                <%#Eval("rank5") == DBNull.Value ?  "" : Eval("rank5")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK6%>">
                            <ItemTemplate>
                                <%#Eval("rank6") == DBNull.Value ?  "" : Eval("rank6")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK7%>">
                            <ItemTemplate>
                                <%#Eval("rank7") == DBNull.Value ?  "" : Eval("rank7")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK8%>">
                            <ItemTemplate>
                                <%#Eval("rank8") == DBNull.Value ?  "" : Eval("rank8")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK9%>">
                            <ItemTemplate>
                                <%#Eval("rank9") == DBNull.Value ?  "" : Eval("rank9")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_RANK10%>">
                            <ItemTemplate>
                                <%#Eval("rank10") == DBNull.Value ?  "" : Eval("rank10")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="50px" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="===">
                            <ItemTemplate>
                                <a href="javascript:;" onclick="onUpdateResult('<%#Eval("id") %>')"><%# Resources.Str.STR_UPDATERESULT %></a>    
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_BETMONEY %>">
                            <ItemTemplate>
                                <font color='blue'><%#string.Format("{0:F2}" + Resources.Str.STR_WON + " ({1:N0})", Eval("betmoney"), Eval("betcount"))%></font>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_WINMONEY %>">
                            <ItemTemplate>
                                <font color='red'><%#string.Format("{0:F2}" + Resources.Str.STR_WON + " ({1:N0})", Eval("winmoney"), Eval("wincount"))%></font>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:Str, STR_BENEFIT %>">
                            <ItemTemplate>
                                <%# (Convert.ToDouble(Eval("betmoney")) >= Convert.ToDouble(Eval("winmoney")) ? "<font color='green'>" : "<font color='red'>") + string.Format("{0:F2}" + Resources.Str.STR_WON, Convert.ToDouble(Eval("betmoney")) - Convert.ToDouble(Eval("winmoney"))) + "</font>"%>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Str, STR_STATUS %>">
                            <ItemTemplate>
                                <b><asp:Literal ID="ltlStatus" runat="server"></asp:Literal></b>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" />
                            <HeaderStyle CssClass="clstableheader withborder" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="===">
                            <ItemTemplate>
                                &nbsp;<asp:LinkButton ID="lnkValid" runat="server" Text="<%$Resources:Str, STR_VALID %>"
                                OnClientClick="return confirm(MSG_CONFIRM_VALID);"
                                CommandName="CmdValid"
                                CommandArgument='<%#Eval("id") %>'></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="lnkCalc" runat="server" Text="<%$Resources:Str, STR_CALC %>"
                                OnClientClick="return confirm(MSG_CONFIRM_CALC);"
                                CommandName="CmdCalc"
                                CommandArgument='<%#Eval("id") %>'></asp:LinkButton>&nbsp;
                                </td></tr>
                                <tr height="30px" id='rowUpdate<%#Eval("id") %>' class="rowUpdate" style="display: none">
                                    <td colspan="2" class="clstablecontent withborder"></td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal1<%#Eval("id") %>' name='selVal1<%#Eval("id") %>' type="text" value='<%#Eval("rank1") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal2<%#Eval("id") %>' name='selVal2<%#Eval("id") %>' type="text" value='<%#Eval("rank2") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal3<%#Eval("id") %>' name='selVal3<%#Eval("id") %>' type="text" value='<%#Eval("rank3") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal4<%#Eval("id") %>' name='selVal4<%#Eval("id") %>' type="text" value='<%#Eval("rank4") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal5<%#Eval("id") %>' name='selVal5<%#Eval("id") %>' type="text" value='<%#Eval("rank5") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal6<%#Eval("id") %>' name='selVal6<%#Eval("id") %>' type="text" value='<%#Eval("rank6") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal7<%#Eval("id") %>' name='selVal7<%#Eval("id") %>' type="text" value='<%#Eval("rank7") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal8<%#Eval("id") %>' name='selVal8<%#Eval("id") %>' type="text" value='<%#Eval("rank8") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal9<%#Eval("id") %>' name='selVal9<%#Eval("id") %>' type="text" value='<%#Eval("rank9") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td align="center" class="clstablecontent withborder">
                                        <input id='selVal10<%#Eval("id") %>' name='selVal10<%#Eval("id") %>' type="text" value='<%#Eval("rank10") %>' class="clsinput" style="width:30px" />
                                    </td>
                                    <td class="clstablecontent withborder" colspan="6" style="text-align:left">
                                        &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="<%$Resources:Str, STR_UPDATE %>"  CssClass="button blue" Width="80px" Height="30px"                            
                                        OnClientClick="return confirm(MSG_UPDATE_RESULT);"
                                        CommandName="CmdUpdate" CommandArgument='<%#Eval("id") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <ItemStyle CssClass="clstablecontent withborder" Width="150px" />
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