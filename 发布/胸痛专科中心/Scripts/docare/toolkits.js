/* ----------  字符工具 ---------- */
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
}

String.prototype.format = function (args) {
    var result = this;
    if (arguments.length <= 0) return result;
    if (arguments.length == 1 && typeof (args) == "object") {
        for (var key in args) {
            if (args[key] != undefined) {
                var reg = new RegExp("({" + key + "})", "g");
                result = result.replace(reg, args[key]);
            }
        }
    }
    else {
        for (var i = 0; i < arguments.length; i++) {
            if (arguments[i] != undefined) {
                var reg = new RegExp("({)" + i + "(})", "g");
                result = result.replace(reg, arguments[i]);
            }
        }
    }
    return result;
}

String.prototype.toDate = function () {
    var result = this;
    var result = result.replace('Date', '').replace('(', '').replace(')', '').replace(/\//g, '');
    return new Date(parseInt(result));
}



/* ----------  日期工具 ---------- */
DateFormats = {
    Default: "yyyy-MM-dd hh:mm:ss",
    Timeline: "yyyy-MM-dd hh:mm",
    Birthday: "yyyy-MM-dd",
    ToYear: "yyyy",
    ToMonth: "yyyy-MM",
    ToDay: "yyyy-MM-dd",
}

//格式化
Date.prototype.format = function (pattern) {
    var o =
	{
	    "M+": this.getMonth() + 1, //月份 
	    "d+": this.getDate(), //日 
	    "h+": this.getHours(), //小时 
	    "m+": this.getMinutes(), //分 
	    "s+": this.getSeconds(), //秒 
	    "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
	    "S": this.getMilliseconds() //毫秒 
	};
    if (/(y+)/.test(pattern))
        pattern = pattern.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(pattern))
            pattern = pattern.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    }
    return pattern;
}
//时间轴格式
Date.prototype.timeline = function () {
    return this.format(DateFormats.Timeline);
}
//增加天数
Date.prototype.addDays = function (d) {
    this.setDate(this.getDate() + d);
    return this;
}
//增加周数
Date.prototype.addWeeks = function (w) {
    this.addDays(w * 7);
    return this;
}
//增加月数
Date.prototype.addMonths = function (m) {
    var d = this.getDate();
    this.setMonth(this.getMonth() + m);
    if (this.getDate() < d) this.setDate(0);
    return this;
}
//增加年数
Date.prototype.addYears = function (y) {
    var m = this.getMonth();
    this.setFullYear(this.getFullYear() + y);
    if (m < this.getMonth()) this.setDate(0);
    return this;
}

/* 字符工具 */
var StringKit = {

    format: function (s, args) {
        if (arguments.length < 1) return '';
        var result = arguments[0];
        if (!result || result == '') return result;
        if (arguments.length < 2 || !arguments[1]) return result;

        if (typeof (arguments[1]) == "object") {
            return result.format(arguments[1]);
        }
        var _args = {};
        for (var i = 1; i < arguments.length; i++) {
            var index = i - 1;
            _args[")" + index + "("] = arguments[i];
        }
        return result.format(_args);
    },
    //是否空字符
    isNullOrEmpty : function(s) {
        if (typeof s == "undefined" || s == null || s == "") {
            return true;
        }
        return false;
    }
}


var IdCardKit = {
    //校验身份证是否正确
    validate: function (idcard) {
        if (idcard == '') {
            return false;
        }
        if (idcard.length != 15 && idcard.length != 18) {
            return false;
        } else {
            var m = idcard.match(/(^\d{15}$)|(^\d{17}([0-9]|X|x)$)/)
            if (m == null) return false;
        }
        return true;
    },
    //获取出生日期
    getBirthday: function (idcard) {
        if (this.validate(idcard)) {
            var len = (idcard + "").length;
            var strBirthday = "";
            if (len == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                strBirthday = idcard.substr(6, 4) + "/" + idcard.substr(10, 2) + "/" + idcard.substr(12, 2);
            }
            if (len == 15) {
                strBirthday = "19" + idcard.substr(6, 2) + "/" + idcard.substr(8, 2) + "/" + idcard.substr(10, 2);
            }
            return strBirthday;
        }
    },
    //获取年龄
    getAge: function (idcard, expiry) {
        if (this.validate(idcard)) {
            var birthDate = new Date(this.getBirthday(idcard));
            var nowDateTime = new Date();
            var age = nowDateTime.getFullYear() - birthDate.getFullYear();
            //再考虑月、天的因素;.getMonth()获取的是从0开始的，这里进行比较，不需要加1
            if (nowDateTime.getMonth() < birthDate.getMonth() || (nowDateTime.getMonth() == birthDate.getMonth() && nowDateTime.getDate() < birthDate.getDate())) {
                age--;
            }
            return age;
        }
    },
}

var ScriptKit = {
    url: function (url) {
        var random = (new Date().getTime()) ^ Math.random();
        if (StringKit.isNullOrEmpty(url)) return url;
        return url + "?_=" + random
    }
}

var DateKit = {
    toDate: function (date) {
        if (date == null) return null;
        return moment(date).toDate();
    },
    toFormatDate: function (date, format) {
        if (date == null) return null;
        var temp = moment(date).toDate();
        if (temp == null) return "";
        if (!format) return temp.format('yyyy-MM-dd hh:mm');
        return temp.format(format);
    }
}

Array.prototype.remove = function (filter) {
    var copy = new Array();
    for(var i = 0;i < this.length; i++) {
        if (!filter(this[i])) {
            copy.push(this[i]);
        }
    }
    for (var i = 0; i < copy.length; i++) {
        this[i] = copy[i];
    }
    this.length = copy.length;
};