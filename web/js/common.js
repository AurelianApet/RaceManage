var COOKIE_KEY_SUNGMUPAE_BET = "sungmupaecont";
var COOKIE_KEY_HANDIOVER_BET = "handiovercont";
var COOKIE_KEY_SINGLE_BET = "singlebetcont";
var COOKIE_KEY_REALTIME_BET = "realbetcont";
var COOKIE_KEY_SPEC_BET = "specbetcont"; //"commbetcont";
var COOKIE_KEY_COMM_BET = "commbetcont";

Array.prototype.remove = function(from, to) {
	var rest = this.slice((to || from)+1 || this.length);
	this.length = from<0 ? this.length+from : from;
	return this.push.apply(this, rest);
}

Array.prototype.indexOf = function(v) {
	for(var i = 0; i < this.length; i++) {
		if(v == this[i]) {
			return i;
			break;
		}
	}

	return -1;
}

document.onkeydown = function(event) {
    if(typeof(event) == "undefined") {
        return true;
    }
    if(event.keyCode == 116) {
        event.keyCode = 0;
        event.cancelBubble = true;
        document.location.href = document.location.href;
        return false;
    }
    
    return true;
}

var currentX = 0;
var currentY = 0;
var strIECode = "<object id=\"objPlaySound\" width=\"1\" height=\"1\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codeBase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" altHtml=\"\">" +
                    "<param name=\"_cx\" value=\"2725\" />" +
                    "<param name=\"_cy\" value=\"1164\" />" +
                    "<param name=\"flashvars\" value=\"\" />" +
                    "<param name=\"movie\" value=\"/images/playsound.swf\" />" +
                    "<param name=\"src\" value=\"/images/playsound.swf\" />" +
                    "<param name=\"wmode\" value=\"transparent\" />" +
                    "<param name=\"play\" value=\"0\" />" +
                    "<param name=\"loop\" value=\"-1\" />" +
                    "<param name=\"quality\" value=\"high\" />" +
                    "<param name=\"salign\" value=\"\" />" +
                    "<param name=\"menu\" value=\"-1\" />" +
                    "<param name=\"base\" value=\"\" />" +
                    "<param name=\"allowscriptaccess\" value=\"\" />" +
                    "<param name=\"scale\" value=\"showall\" />" +
                    "<param name=\"devicefont\" value=\"0\" />" +
                    "<param name=\"embedmovie\" value=\"0\" />" +
                    "<param name=\"bgcolor\" value=\"\" />" +
                    "<param name=\"swremote\" value=\"\" />" +
                    "<param name=\"moviedata\" value=\"\" />" +
                    "<param name=\"seamlesstabbing\" value=\"1\" />" +
                    "<param name=\"profile\" value=\"0\" />" +
                    "<param name=\"profileaddress\" value=\"\" />" +
                    "<param name=\"profileport\" value=\"0\" />" +
                    "<param name=\"allownetworking\" value=\"all\" />" +
                    "<param name=\"allowfullscreen\" value=\"false\" />" +                             						
                    "<embed name=\"objPlaySoundName\" height=\"1\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?p1_prod_version=shockwaveflash\" width=\"1\" src=\"\" quality=\"high\" loop=\"true\" autoplay=\"false\">" +
                "</object>";
var strOtherCode = "<embed name=\"objPlaySoundName\" src=\"/images/playsound.swf\" quality=\"high\" style=\"width:1px; height:1px;\" align=\"middle\" allowScriptAccess=\"always\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?p1_prod_version=shockwaveflash\" autoplay=\"false\" loop=\"true\"></embed>";

$(document).ready(function() {
    $(document).mousemove(function(e){
        currentX = e.pageX;
        currentY = e.pageY;
    });
    if(navigator.appName.indexOf("Internet Explorer") < 0)
    {
        $("#divPlaySound").html(strOtherCode);
    }else{
        $("#divPlaySound").html(strIECode);
    }
});

function getElement(strID)
{    
    return document.getElementById(strID);
}
function getFlashElement(strID, strName)
{
    var isIE = true;
    if(navigator.appName.indexOf("Internet Explorer") < 0)
        isIE = false;
    
    if(isIE)
        return document.getElementById(strID);
    else
        return document[strName];
}

function isCheckedNo()
{
    var isChk = false;
    var chkObj = document.getElementsByName("chkNo");
    for(var i = 0; i < chkObj.length; i++)
    {
        if(chkObj[i].checked == true)
        {
            isChk = true;
        }
    }
    
    return isChk;
}

function checkAll(obj)
{
    var chkObj = document.getElementsByName("chkNo");
    for(var i = 0; i < chkObj.length; i++)
    {
        chkObj[i].checked = obj.checked;
        
        var arr = chkObj[i].id.split("_");
        
        if(arr.length == 2)
        {
            if(chkObj[i].checked) {
                $("#rowGameItem_" + arr[1]).css("background-color", "#FFC69D");
            } else {
                $("#rowGameItem_" + arr[1]).css("background-color", "#" + $("#rowGameItem_" + arr[1]).attr("class"));
            }
        }
    }
}

function onSelGameItem(gameID, bgColor)
{
    if($("#chkNo_" + gameID))
    {
        if($("#chkNo_" + gameID).attr("checked") == "checked")
        {
            $("#rowGameItem_" + gameID).css("background-color", bgColor);
            $("#chkNo_" + gameID).removeAttr("checked");
        }
        else
        {
            $("#rowGameItem_" + gameID).css("background-color", "#FFC69D");
            $("#chkNo_" + gameID).attr("checked", "checked");            
        }
    }
}
function confirmCheck(confirmStr)
{   
    if(!isCheckedNo())
    {
        alert(MSG_NOSELECTITEM);
        return false;
    }
    
    if(typeof(confirmStr) == "undefined" || confirmStr == "") {
        return true;
    } else {
        return confirm(confirmStr);
    }    
}
function zero2Blank(iValue) {
    if(parseInt(iValue) == 0)
        return "";
        
    return iValue;
}
function setCookie(key, val) {
    var today = new Date();
    document.cookie = key + "=" + escape(val) + "; path=/;";
}
function getCookie(key) {
    var nameOfCookie = key + "="; 
    var x = 0; 
    while (x <= document.cookie.length ) 
    { 
        var y = (x + nameOfCookie.length); 
        if ( document.cookie.substring(x, y) == nameOfCookie) { 
            if ((endOfCookie = document.cookie.indexOf(";", y)) == -1) 
                endOfCookie = document.cookie.length;
            return unescape(document.cookie.substring(y, endOfCookie)); 
        } 
        x = document.cookie.indexOf(" ", x) + 1; 
        if (x == 0) 
            break; 
    } 
    return ""; 
}
function deleteCookie(key) {
    document.cookie = key + "=; path=/;";    
}
function ch_img_src(sID, imgUrl)
{
    $("#" + sID).attr("src", imgUrl);
}
function MoneyFormat(str)
{
	var re="";
	str = str + "";
	str=str.replace(/-/gi,"");
	str=str.replace(/ /gi,"");
	
	str2=str.replace(/-/gi,"");
	str2=str2.replace(/,/gi,"");
	str2=str2.replace(/\./gi,"");	
	
	if(isNaN(str2) && str!="-") return "";
	try
	{
		for(var i=0;i<str2.length;i++)
		{
			var c = str2.substring(str2.length-1-i,str2.length-i);
			re = c + re;
			if(i%3==2 && i<str2.length-1) re = "," + re;
		}
		
	}catch(e)
	{
		re="";
	}
	
	if(str.indexOf("-")==0)
	{
		re = "-" + re;
	}

	return re;
}

function numberKey(event)
{
    if (event.charCode > 0) {        
        if (event.charCode < 48 || event.charCode > 57)
            return false;
    } else {  
        if (event.keyCode < 48 || event.keyCode > 57)
            return false;
    }
    return true;
}
/*
 * 날짜포맷에 맞는지 검사
 */
function isDateFormat(d) {
    var df = /[0-9]{4}-[0-9]{2}-[0-9]{2}/;
    return d.match(df);
}

/*
 * 윤년여부 검사
 */
function isLeaf(year) {
    var leaf = false;

    if(year % 4 == 0) {
        leaf = true;

        if(year % 100 == 0) {
            leaf = false;
        }

        if(year % 400 == 0) {
            leaf = true;
        }
    }

    return leaf;
}
/*
 * 날짜가 유효한지 검사
 */
function isValidDate(d) {
    // 포맷에 안맞으면 false리턴
    if(!isDateFormat(d)) {
        return false;
    }

    var month_day = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    var dateToken = d.split('-');
    var year = Number(dateToken[0]);
    var month = Number(dateToken[1]);
    var day = Number(dateToken[2]);
    
    // 날짜가 0이면 false
    if(day == 0) {
        return false;
    }

    var isValid = false;

    // 윤년일때
    if(isLeaf(year)) {
        if(month == 2) {
            if(day <= month_day[month-1] + 1) {
                isValid = true;
            }
        } else {
            if(day <= month_day[month-1]) {
                isValid = true;
            }
        }
    } else {
        if(day <= month_day[month-1]) {
            isValid = true;
        }
    }

    return isValid;
}
function getDateStrFromDateObject(dateObject){
    var str = null;

    var month = dateObject.getMonth() + 1;
    var day = dateObject.getDate();

    if(month < 10)
        month = '0' + month;
    if(day < 10)
        day = '0' + day;

    str = dateObject.getFullYear() + '-' + month + '-' + day;
    return str;
}
function checkDateTime(dtObj, bEmpty)
{
    if(!isValidDate(dtObj.value))
    {
        if(bEmpty == true) {
            dtObj.value = "";
        } else {
            dtObj.value = getDateStrFromDateObject(new Date());
        }
    }
}
function cutString(str,limit){
    var tmpStr = str;
    var byte_count = 0;
    var len = str.length;
    var dot = "";

    for(i=0; i<len; i++){
        byte_count += chr_byte(str.charAt(i));
        if(byte_count == limit-1){
            if(chr_byte(str.charAt(i+1)) == 2){
                tmpStr = str.substring(0,i+1);
                dot = "...";
            }else {
                if(i+2 != len) dot = "...";
                tmpStr = str.substring(0,i+2);
            }
            break;
        }else if(byte_count == limit){
            if(i+1 != len) dot = "...";
            tmpStr = str.substring(0,i+1);
            break;
        }
    }
    
    return tmpStr+dot;
}
function chr_byte(chr){
    if(escape(chr).length > 4)
        return 2;
    else
        return 1;
}
function number_format(nVal, nLength)
{
    nVal = nVal + "";
    var nLen = nVal.length;
    if(nLen >= nLength)
        return nVal;
        
    for(var i = 0; i < nLength - nLen; i++)
    {
        nVal = "0" + nVal;
    }
    
    return nVal;
}
function removeComma(obj)
{
    if(obj == null) {
        return;
    }
    
    var curVal = obj.value;
    
    while(curVal.indexOf(',') >= 0) { 
        curVal = curVal.replace(/,/, "");
    }
    
    obj.value = curVal;
}


function MoneyFormat(str) {
    var re = "";
    str = str + "";
    str = str.replace(/-/gi, "");
    str = str.replace(/ /gi, "");

    str2 = str.replace(/-/gi, "");
    str2 = str2.replace(/,/gi, "");
    str2 = str2.replace(/\./gi, "");

    if (isNaN(str2) && str != "-") return "";
    try {
        for (var i = 0; i < str2.length; i++) {
            var c = str2.substring(str2.length - 1 - i, str2.length - i);
            re = c + re;
            if (i % 3 == 2 && i < str2.length - 1) re = "," + re;
        }

    } catch (e) {
        re = "";
    }

    if (str.indexOf("-") == 0) {
        re = "-" + re;
    }

    return re;
}

function goLink(m) {
    var boolPopup = false;
    var w = 100;
    var h = 100;
    var url = "";


    switch (m.toLowerCase()) {
        case "game1":
            url = "/TotoBettingMng/popVirtualBetting.aspx?bettype=11";
            break;

        case "game2":
            url = "/TotoBettingMng/popVirtualBetting.aspx?bettype=14";
            break;

        case "game3":
            url = "/TotoBettingMng/popVirtualBetting_one.aspx";
            break;
    }
    if (url == "")
        return;
        
    if (boolPopup)
        window.open(url, m, "width=" + w + ", height=" + h);
    else
        document.location.href = url;
}