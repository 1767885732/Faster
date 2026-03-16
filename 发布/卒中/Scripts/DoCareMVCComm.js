
//display error message with modal window
function displayError(message, status) {
    var $dialog = $("<div></div>");
    $dialog.html(message);
    $dialog.dialog({
        modal: true,
        title: status + ' Error',
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
    return false;
};

//show standard modal popup window with easyui library
function showEasyUIDialog(event, loadurl, title, width, height, popupDivIdContainerId) {
    event.preventDefault();
    //var $loading = $('<img src="../Images/ajaxLoading.gif" alt="loading" class="ui-loading-icon">');
    var $dialog = null;
    var popupDivId = "div" + getRandom(10000);

    parent.$('#globalPopupDiv').data(title, popupDivId);
    $dialog = $("<div id='" + popupDivId + "'></div>").appendTo('body');
    $dialog.empty();
    $dialog
    .dialog({
        title: title,
        width: width,
        height: height,
        cache: false,
        href: loadurl,
        modal: true,
        zIndex: 99999
    });
};

function SubmitEasyUIDialog(popupDivIdContainerId, postUrl, targetId, successHandler,isSuccessClose,successMessage) {
    SubmitEasyUIDialogByParam(popupDivIdContainerId, postUrl, targetId, successHandler, isSuccessClose, successMessage, "POST", true);
}

function SubmitEasyUIDialogByFormMethod(popupDivIdContainerId, postUrl, targetId, successHandler, isSuccessClose, successMessage,formMethod)
{
    SubmitEasyUIDialogByParam(popupDivIdContainerId, postUrl, targetId, successHandler, isSuccessClose, successMessage, formMethod, true);
}

function SubmitEasyUIDialogByParam(title, postUrl, targetId, successHandler, isSuccessClose, successMessage, formMethod, isCheckValid) {
    if (isCheckValid) {
        var validateState = $("#" + targetId).valid();
        if (!validateState)
            return false;
    }
    var popupDivId = parent.$('#globalPopupDiv').data(title);
    var dlg = $('#' + popupDivId);
    $.ajax({
        url: postUrl,
        type: formMethod,
        data: $("#" + targetId).serialize(),
        success: function (response) {
            if (isSuccessClose) {
                dlg.dialog('close');
                dlg.empty();
                if (successHandler != null)
                    successHandler();
            }
            if (successMessage != null && successMessage != "") {
                $.messager.alert('提醒', successMessage, 'info');
            }
        },
        error: function (xhr) {
            if (xhr.status == 400) {
                dlg.html(xhr.responseText, xhr.status);
            }
            else {
                alert(xhr.responseText);
                //displayError(xhr.responseText, xhr.status);
                //$.messager.alert('错误', xhr.responseText, 'error');
            }
        }
    });
}

function CloseEasyUIDialog(title) {
    var popupDivId = parent.$('#globalPopupDiv').data(title);
    var dlg = $('#' + popupDivId);
    dlg.dialog('close');
    dlg.empty();
}

/*异步进度条对象的封装*/
var SimpleProgress = function () {
    var that = {};

    // Get the current task ID
    that._taskId = 0;

    // Get the timer ID used to push refreshes
    that._timerId = 0;

    // Get the URL to invoke to read status
    that._progressUrl = "";

    // Get the current interval for progress refresh
    that._interval = 500;

    // Get the user-defined callback that refreshes the UI 
    that._userDefinedProgressCallback = null;

    // Get the user-defined callback that finalizes the call 
    that._taskCompletedCallback = null;

    // Get a new task ID
    that.createTaskId = function () {
        var _minNumber = 100,
            _maxNumber = 1000000000;
        return _minNumber + Math.floor(Math.random() * _maxNumber);
    };

    // Set progress callback
    that.callback = function (userCallback, completedCallback) {
        that._userDefinedProgressCallback = userCallback;
        that._taskCompletedCallback = completedCallback;
        return this;
    };

    // Set frequency of refresh
    that.setInterval = function (interval) {
        that._interval = interval;
        return this;
    };


    // INTERNAL FUNCTION
    that._internalProgressCallback = function () {
        that._timerId = window.setTimeout(that._internalProgressCallback, that._interval);
        $.ajax({
            url: that._progressUrl,
            cache: false,
            headers: { 'X-SimpleProgress-TaskId': that._taskId },
            success: function (status) {
                if (that._userDefinedProgressCallback != null)
                    that._userDefinedProgressCallback(status);
            }
        });
    };

    // Invoke the URL and monitor its progress
    that.start = function (url, progressUrl) {
        that._taskId = that.createTaskId();
        that._progressUrl = progressUrl;
        // Place the Ajax call
        $.ajax({
            url: url,
            cache: false,
            headers: { 'X-SimpleProgress-TaskId': that._taskId },
            success: function (data) {
                if (that._taskCompletedCallback != null)
                    that._taskCompletedCallback(data);
                that.end();
            },
            error: function () {
                that.end();
            }
        });

        // Start the callback (if any)
        if (that._userDefinedProgressCallback == null)
            return this;
        that._timerId = window.setTimeout(that._internalProgressCallback, that._interval);
    };

    // Finalize the task
    that.end = function () {
        that._taskId = 0;
        window.clearTimeout(that._timerId);
    }
    return that;
};

function getRandom(n) {
    return Math.floor(Math.random() * n + 1)
}

//非空检验
function chkIsNull(objID) {
    if ($.trim($('[id$=' + objID + ']').val()) == "") {
        return true;
    }
    return false;
}

//是否数字
function isDigit(s) {
    var patrn = /^[0-9]{1,20}$/;
    if (patrn.exec(s))
        return true; return false;
}

//验证电子邮箱格式是否正确
function isEmail(strEmail) {
    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
        return true; return false;
}

//验证是否是正整数
function isPositiveInteger(s) {
    var pattern = /^[0-9]+$/;
    if (pattern.exec(s))
        return true; return false;
}

function IsNumber(string, sign) {
    var number;
    if (string == null) return false;
    if ((sign != null) && (sign != "-") && (sign != "+")) {
        alert("参数出错");
        return false;
    }
    number = new Number(string);
    if (isNaN(number)) {
        return false;
    }
    else if ((sign == null) || (sign == "-" && number < 0) || (sign == "+" && number > 0)) {
        return true;
    }
    else {
        return false;
    }
}

function isValidDate(dateObj) {
    var temp = dateObj;
    if (!(dateObj.length == 10)) {
        return false;
    }
    var exp = new RegExp("\\d{4}-\\d{2}-\\d{2}");
    if (!exp.test(temp)) {
        return false;
    }
    var month = temp.substring(5, 7);

    if (month < 1 || month > 12) {
        return false;
    }
    var day = temp.substring(8, 10);
    if (day < 1 || day > 31) {
        return false;
    }
    return true;
}

function getTime(/** timestamp=0 **/) {
    var unixtime = "";
    if (arguments[0] != null && arguments[0] != "") {
        var pa = /.*\((.*)\)/;
        if (arguments[0].match(pa) != null && arguments[0].match(pa) != "") {
            unixtime = arguments[0].match(pa)[1].substring(0, 10);
        }
    }
    var ts = unixtime || 0;
    var t, y, m, d, h, i, s;
    t = ts ? new Date(ts * 1000) : new Date();
    y = t.getFullYear();
    m = t.getMonth() + 1;
    d = t.getDate();
    h = t.getHours();
    i = t.getMinutes();
    //s = t.getSeconds();
    // 可根据需要在这里定义时间格式
    return y + '-' + (m < 10 ? '0' + m : m) + '-' + (d < 10 ? '0' + d : d) + ' ' + (h < 10 ? '0' + h : h) + ':' + (i < 10 ? '0' + i : i); // + ':' + (s < 10 ? '0' + s : s);
}

function getProcessStatus(value)
{
    var status = "";
    if (value == null || typeof value == "undefined") {
        return status;
    }
    switch (value)
    {
        case "0":
            status = "草稿";
            break;
        case "1":
            status = "审核中";
            break;
        case "2":
            status = "审核通过";
            break;
        case "3":
            status = "审核拒绝";
            break;
        default:
            status = "";
            break;

    }

    return status;
}

function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

function myparser(s) {
    if (!s) new Date(); messager
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}
//function AdaptiveHeight(height)
//{
//    //var minHeight = 650;
//    if (height < document.body.clientHeight) {
//        height = document.body.clientHeight;
//    }
//    if (height < 900) {
//        height = 900;
//    }
//    //if (height < minHeight) {
//    //    $("#left").height(minHeight);
//    //    $("#divcontent").height(minHeight - 15);
//    //    return;
//    //}
//    $("#left").height(height + 15);
//    $("#divcontent").height(height);
//    //if (height > $("#left").height()) {
//    //    $("#left").height(height + 15);
//    //    $("#divcontent").height(height);
//    //} else {
//    //    $("#divcontent").height($("#left").height() - 15);
//    //}
//}

function print(param) {
    if (param != undefined) {
        var target = $(param);
        var oPop = window.open('', '打印');
        var str = target.html();
        oPop.document.write(str);
        oPop.print();
        oPop.close();
    }
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function ShowNoPermission(div) {
    var win = $(div).window({
        href: '/Error/NoPermission',
        title: '未授权',
        closed: true,
        minimizable: false,
        collapsible: false,
        maximizable: false,
        resizable: false,
        draggable: false,
        width: 800,
        height: 500,
        cache: false,
        modal: true,
        shadow: true
    });
    win.window('open');
}

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}