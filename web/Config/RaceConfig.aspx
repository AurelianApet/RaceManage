<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaceConfig.aspx.cs" Inherits="Config_RaceConfig" MasterPageFile="~/ManageMaster.master" %>
<%@ MasterType VirtualPath="~/ManageMaster.master" %>

<asp:Content ContentPlaceHolderID="ManageHeadContent" ID="manageHead" runat="server">
<title><%=Resources.Menu.MENU_MAINCONFIG%></title>
<script type="text/javascript" language="javascript">
$(document).ready(function() { 
    changeMenuSelected("Config");
});
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="ManageBodyContent" ID="manageBody" runat="server">
<section class="content">
	<section class="widget">
		<header>
			<span class="icon">&#127748;</span>
			<hgroup>
				<h1><asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Menu, MENU_RACECONFIG %>"></asp:Literal></h1>
			</hgroup>
		</header>
		<div class="content">
		    <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr><td class="clsspace"></td></tr>
		    <tr>
                <td class="left">- <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Str, STR_GAMECONFIG %>"></asp:Literal></td>
            </tr>
            <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
		            <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal22" runat="server" Text="<%$Resources:Str, STR_GAMERESULT %>"></asp:Literal>
                        </th>
                        <td class="left" width="80%">                
                            <asp:RadioButtonList ID="rblGameResult" CssClass="clscheckbox" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="<%$Resources:Str, STR_GAMERESULT_AUTO %>" Value="1"></asp:ListItem>
                                <asp:ListItem Text="<%$Resources:Str, STR_GAMERESULT_PASSIVE %>" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>  
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="left">- <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Str, STR_USER_BETRATIO %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
            <tr>
                <td>   
                    <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal26" runat="server" Text="猜冠军"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan = "3">                
                            <asp:TextBox ID="tbxRatio1_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal27" runat="server" Text="猜冠亚军"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio2_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal28" runat="server" Text="猜冠亚军单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio2_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal29" runat="server" Text="猜前三名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio3_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal30" runat="server" Text="猜前三名单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio3_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal23" runat="server" Text="猜前四名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio4_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal31" runat="server" Text="猜前四名单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio4_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal32" runat="server" Text="猜前五名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio5_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal33" runat="server" Text="猜前五名单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio5_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal34" runat="server" Text="前三和值"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">                
                            大 : <asp:TextBox ID="tbxRatio11_1" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            小 : <asp:TextBox ID="tbxRatio11_2" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            单 : <asp:TextBox ID="tbxRatio11_3" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            双 : <asp:TextBox ID="tbxRatio11_4" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <br /><br />
                            6 : <asp:TextBox ID="tbxRatio11_5" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            7 : <asp:TextBox ID="tbxRatio11_6" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            8 : <asp:TextBox ID="tbxRatio11_7" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            9 : <asp:TextBox ID="tbxRatio11_8" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            10 : <asp:TextBox ID="tbxRatio11_9" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            11 : <asp:TextBox ID="tbxRatio11_10" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            12 : <asp:TextBox ID="tbxRatio11_11" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            13 : <asp:TextBox ID="tbxRatio11_12" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <br /><br />
                            14 : <asp:TextBox ID="tbxRatio11_13" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            15 : <asp:TextBox ID="tbxRatio11_14" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            16 : <asp:TextBox ID="tbxRatio11_15" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            17 : <asp:TextBox ID="tbxRatio11_16" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            18 : <asp:TextBox ID="tbxRatio11_17" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            19 : <asp:TextBox ID="tbxRatio11_18" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            20 : <asp:TextBox ID="tbxRatio11_19" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            21 : <asp:TextBox ID="tbxRatio11_20" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <br /><br />
                            22 : <asp:TextBox ID="tbxRatio11_21" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            23 : <asp:TextBox ID="tbxRatio11_22" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            24 : <asp:TextBox ID="tbxRatio11_23" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            25 : <asp:TextBox ID="tbxRatio11_24" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            26 : <asp:TextBox ID="tbxRatio11_25" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            27 : <asp:TextBox ID="tbxRatio11_26" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal35" runat="server" Text="自由双面"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">   
                            <asp:TextBox ID="tbxRatio12_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>             
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal36" runat="server" Text="猜名次"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">   
                            1至3 : <asp:TextBox ID="tbxRatio13_1" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            4至6 : <asp:TextBox ID="tbxRatio13_2" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            7至10 : <asp:TextBox ID="tbxRatio13_3" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            1至5 : <asp:TextBox ID="tbxRatio13_4" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            6至10 : <asp:TextBox ID="tbxRatio13_5" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>             
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal37" runat="server" Text="前五定位胆"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio14_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal38" runat="server" Text="后五定位胆"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio14_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal39" runat="server" Text="趣味猜前四名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio15_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal40" runat="server" Text="趣味猜前五名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio16_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
		        </td>
		    </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td class="left">- <asp:Literal ID="Literal41" runat="server" Text="<%$Resources:Str, STR_MAJANG_BETRATIO %>"></asp:Literal></td>
            </tr>
            <tr><td class="clsminispace"></td></tr>
            <tr>    
		        <td>    
		            <table width="100%" border="0" cellpadding="5" cellspacing="5" class="clstableborder">
		            <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal43" runat="server" Text="猜冠军"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan = "3">                
                            <asp:TextBox ID="tbxRatio_M_1_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal44" runat="server" Text="猜冠亚军"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_2_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal45" runat="server" Text="猜冠亚军单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_2_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal46" runat="server" Text="猜前三名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_3_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal47" runat="server" Text="猜前三名单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_3_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal48" runat="server" Text="猜前四名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_4_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal49" runat="server" Text="猜前四名单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_4_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal50" runat="server" Text="猜前五名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_5_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal51" runat="server" Text="猜前五名单式"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_5_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal52" runat="server" Text="前三和值"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">                
                            大 : <asp:TextBox ID="tbxRatio_M_11_1" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            小 : <asp:TextBox ID="tbxRatio_M_11_2" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            单 : <asp:TextBox ID="tbxRatio_M_11_3" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            双 : <asp:TextBox ID="tbxRatio_M_11_4" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <br /><br />
                            6 : <asp:TextBox ID="tbxRatio_M_11_5" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            7 : <asp:TextBox ID="tbxRatio_M_11_6" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            8 : <asp:TextBox ID="tbxRatio_M_11_7" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            9 : <asp:TextBox ID="tbxRatio_M_11_8" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            10 : <asp:TextBox ID="tbxRatio_M_11_9" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            11 : <asp:TextBox ID="tbxRatio_M_11_10" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            12 : <asp:TextBox ID="tbxRatio_M_11_11" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            13 : <asp:TextBox ID="tbxRatio_M_11_12" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <br /><br />
                            14 : <asp:TextBox ID="tbxRatio_M_11_13" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            15 : <asp:TextBox ID="tbxRatio_M_11_14" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            16 : <asp:TextBox ID="tbxRatio_M_11_15" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            17 : <asp:TextBox ID="tbxRatio_M_11_16" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            18 : <asp:TextBox ID="tbxRatio_M_11_17" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            19 : <asp:TextBox ID="tbxRatio_M_11_18" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            20 : <asp:TextBox ID="tbxRatio_M_11_19" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            21 : <asp:TextBox ID="tbxRatio_M_11_20" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <br /><br />
                            22 : <asp:TextBox ID="tbxRatio_M_11_21" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            23 : <asp:TextBox ID="tbxRatio_M_11_22" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            24 : <asp:TextBox ID="tbxRatio_M_11_23" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            25 : <asp:TextBox ID="tbxRatio_M_11_24" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            26 : <asp:TextBox ID="tbxRatio_M_11_25" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            27 : <asp:TextBox ID="tbxRatio_M_11_26" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal53" runat="server" Text="自由双面"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">   
                            <asp:TextBox ID="tbxRatio_M_12_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>             
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal54" runat="server" Text="猜名次"></asp:Literal>
                        </th>
                        <td class="left" width="30%" colspan="3">   
                            1至3 : <asp:TextBox ID="tbxRatio_M_13_1" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            4至6 : <asp:TextBox ID="tbxRatio_M_13_2" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            7至10 : <asp:TextBox ID="tbxRatio_M_13_3" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            1至5 : <asp:TextBox ID="tbxRatio_M_13_4" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            6至10 : <asp:TextBox ID="tbxRatio_M_13_5" runat="server" CssClass="clsinput" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>             
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal57" runat="server" Text="前五定位胆"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_14_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal58" runat="server" Text="后五定位胆"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_14_2" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal59" runat="server" Text="趣味猜前四名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_15_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                        <th width="20%" class="right">
                            <asp:Literal ID="Literal60" runat="server" Text="趣味猜前五名"></asp:Literal>
                        </th>
                        <td class="left" width="30%">                
                            <asp:TextBox ID="tbxRatio_M_16_1" runat="server" CssClass="clsinput" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
		        </td>
		    </tr>
            <tr><td class="clsspace"></td></tr>
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" Text="<%$Resources:Str, STR_CONFIRM %>"
                        CssClass="button orange" Width="100px"
                        OnClick="btnOK_Click" />&nbsp;
                </td>
            </tr>
            <tr><td class="clsspace"></td></tr>
            </table>
		</div>
	</section>
</section>
</asp:Content>

