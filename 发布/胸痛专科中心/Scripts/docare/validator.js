function Validator(view, form) {
    $.validator.setDefaults({
        /*关闭键盘输入时的实时校验*/
        onkeyup: null,
        /*添加校验成功后的执行函数--修改提示内容，并为正确提示信息添加新的样式(默认是valid)*/
        success: function (label) {
            /*label的默认正确样式为valid，需要通过validClass来重置，否则这里添加的其他样式不能被清除*/
            label.text('').addClass('valid');
        }
    });
    $.validator.errorItem = [];
    $.validator.requiredItem = [];

    var offset = 0;
    var messages = new Array();
    var members = new Array();

    var requestUrl = "/Validate/GetJSValidate";
    var viewID = viewID;
    var Validator = $(form).validate({
        errorPlacement: function (error, element) {
            error.appendTo(element.parent());
        }
    });

    var randomNu = (new Date().getTime()) ^ Math.random();
    var url = requestUrl + "?view=" + view + "?_=" + randomNu;
    AddUserMethods();

    var AddMessage = function (key, message)
    {
        messages[key] = message;
    }

    var AddMember = function (key, memberName) {
        members[key] = memberName;
    }

    this.AddRule = function (Name, Rule) {
        AddRule(Name, Rule);
    }
     
    var AddRule = function (Name, Rule) {
        var rule = eval('(' + Rule + ')');
        var element = $("[validateName='" + Name + "']");
        if (element == undefined || element.length == 0) {
            // console.log("undefinedElement:" + Name);
        }
        else if (element.length > 1) {
            if (element.eq(0).tagName == "INPUT" &&
                (element.eq(0).attr("type") == "radio" ||
                 element.eq(0).attr("type") == "checkbox"))
                element.eq(0).rules("add", rule);
            else
                element.each(function (index, item) {
                    $(item).rules("add", rule);
                })
        }
        else {
            element.rules("add", rule);
        }

        // 如果是必填项，添加*
        if (rule.RequiredCheck == true) {
            SetCssForRequireElement(Name);
        }
        else if (rule.RequiredCheck == false) {
            SetCssForImportElement(Name);
        }
    }

    this.RemoveRule = function (Name, Rule) {
        $("[validateName='" + Name + "']").rules("remove", Rule);
    }

    this.DoValidate = function () {
        offset = 0;
        $.validator.errorItem = [];
        $.validator.requiredItem = [];
        AddUserMethods();
        $.validator.addMethod("TimeSequenceCheck", function (value, element, arg) { return true; });
        var rtn = Validator.form();
        AddUserMethods();
        if (offset != 0) {
            $("#divcontent").parent().animate({ scrollTop: offset }, 500);
        }
        return rtn;
    }

    this.validateElement = function (element) {
        return $(form).validate().element(element);
    }

    // 获取校验规则
    $.get(url, function (result) {
        $.each(result.Data, function (index, item) {
            AddRule(item.Name, item.Property);
            AddMessage(item.Name + "_" + item.Type, item.Message);
            AddMember(item.Name + "_" + item.Type, item.MemberName);
        });
    });

    function AddUserMethods() {
        // 必填项校验（下拉列表默认项的值为""时也能适用）
        $.validator.addMethod("RequiredCheck", function (value, element, arg) {
            $.validator.messages.RequiredCheck = messages[$(element).attr("validateName") + "_RequiredCheck"];
            var flg = true;
            if (element.tagName == "SELECT") {
                flg = arg == true ? $(element).val() != "" : true;
            }
            else if (element.tagName == "TEXTAREA") {
                flg = $(element).val() != "";
            }
            else if (element.tagName == "INPUT") {
                switch ($(element).attr("type")) {
                    case "text":
                        flg = arg == true ? value != "" : true;
                        break;
                    case "datetime":
                        flg = arg == true ? value != "" : true;
                        break;
                    case "radio":
                        flg = $("[Name = '" + $(element).attr("Name") + "']:checked").length > 0;
                        break;
                    case "checkbox":
                        flg = $("[ValidateName = '" + $(element).attr("ValidateName") + "']:checked").length > 0;
                        break;
                    default:
                        flg = true;
                        break;
                }
            }

            if (!flg)
            {
                $.validator.requiredItem.push("必填项：[" + members[$(element).attr("validateName") + "_RequiredCheck"] + "]未填写");
                var _offset = $(element).offset().top;
                if (offset == 0 || offset > _offset) offset = _offset;
            }

            return flg;
        });

        // 重要项校验
        $.validator.addMethod("ImportCheck", function (value, element, arg) {
            var flg = true;
            if (element.tagName == "SELECT") {
                flg = arg == true ? $(element).val() != "" : true;
            }
            else if (element.tagName == "TEXTAREA") {
                flg = $(element).val() != "";
            }
            else if (element.tagName == "INPUT") {
                switch ($(element).attr("type")) {
                    case "text":
                        flg = arg == true ? value != "" : true;
                        break;
                    case "datetime":
                        flg = arg == true ? value != "" : true;
                        break;
                    case "radio":
                        flg = $("[Name = '" + $(element).attr("Name") + "']:checked").length > 0;
                        break;
                    case "checkbox":
                        flg = $("[ValidateName = '" + $(element).attr("ValidateName") + "']:checked").length > 0;
                        break;
                    default:
                        flg = true;
                        break;
                }
            }
            if (!flg) $.validator.errorItem.push("重要项：[" + members[$(element).attr("validateName") + "_ImportCheck"] + "]未填写");

            return true;
        });

        // 最大值校验
        $.validator.addMethod("MaxValueCheck", function (value, element, arg) {
            $.validator.messages.MaxValueCheck = messages[$(element).attr("validateName") + "_MaxValueCheck"];
            if (value == "") return true;
            return arg >= value;
        });

        // 最小值校验
        $.validator.addMethod("MinValueCheck", function (value, element, arg) {
            $.validator.messages.MinValueCheck = messages[$(element).attr("validateName") + "_MinValueCheck"];
            if (value == "") return true;
            return arg <= value;
        });

        // 最大长度校验
        $.validator.addMethod("MaxLengthCheck", function (value, element, arg) {
            $.validator.messages.MaxLengthCheck = messages[$(element).attr("validateName") + "_MaxLengthCheck"];
            if (value == "") return true;
            return arg >= value.length;
        });

        // 多选一校验
        $.validator.addMethod("SingleSelectCheck", function (value, element, arg) {
            if (arg == "" || value != "") return true;
            $.validator.messages.SingleSelectCheck = messages[$(element).attr("validateName") + "_SingleSelectCheck"];
            var arr = arg.split(";");
            for (var index = 0; index < arr.length; index++) {
                var name = arr[index];

                // 通用控件
                var obj = $("[validateName='" + name + "']");
                if (obj != undefined &&
                    ((obj.val() != undefined && obj.val() != "") || // text
                    obj.is(':checked') // checkbox
                    )) {
                    return true;
                }

                // combobox
                obj = $("[validateName='" + name + "'] option:selected");
                if (obj.val() != undefined && obj.val() != "") {
                    return true;
                }
            }
            return false;
        });

        // 正则表达式校验
        $.validator.addMethod("RegexCheck", function (value, element, arg) {
            if (value == "") return true;
            $.validator.messages.RegexCheck = messages[$(element).attr("validateName") + "_RegexCheck"];
            var reg = new RegExp(arg);
            return reg.test(value);
        });

        // 时间先后顺序校验校验(只记录不报错)
        $.validator.addMethod("TimeSequenceConfirm", function (value, element, arg) {
            if (value == "" || arg == "") return true;

            var arr = arg.split(";");
            var isPrev = true;
            var elementName = "";
            
            for (var index = 0; index < arr.length; index++) {
                var name = arr[index];
                if (name == $(element).attr("validateName"))
                {
                    isPrev = false;
                    elementName = arr[index + 1];
                    index++;
                    continue;
                }
                
                var timeValue = $("#" + name).attr("data-value");
                if (timeValue == undefined || timeValue == "" || timeValue == "null") continue;
                var timeName = $("#" + name).attr("data-desp");
                // 前置时间
                if(isPrev)
                {
                    var prevDate = new Date(timeValue);
                    if(prevDate > new Date(value)){
                        // $.validator.messages.TimeSequenceCheck = "不能早于" + timeName;
                        // return false;
                        $.validator.errorItem.push(messages[$(element).attr("validateName") + "_TimeSequenceCheck"] + "不能早于[" + timeName + "]");
                    }
                }
                // 后置时间 
                else{
                    var nextDate = new Date(timeValue);
                    if (new Date(value) > nextDate) {
                        // $.validator.messages.TimeSequenceCheck = "不能晚于" + timeName;
                        // return false;
                        $.validator.errorItem.push(messages[$(element).attr("validateName") + "_TimeSequenceCheck"] + "不能晚于[" + timeName + "]");
                    }
                }
            }
            return true;
        });

        $.validator.addMethod("TimeSequenceCheck", function (value, element, arg) {
            if (value == "" || arg == "") return true;

            var arr = arg.split(";");
            var isPrev = true;
            var elementName = "";

            for (var index = 0; index < arr.length; index++) {
                var name = arr[index];
                if (name == $(element).attr("validateName")) {
                    isPrev = false;
                    elementName = arr[index + 1];
                    index++;
                    continue;
                }

                var timeValue = $("#" + name).attr("data-value");
                if (timeValue == undefined || timeValue == "" || timeValue == "null") continue;
                var timeName = $("#" + name).attr("data-desp");
                // 前置时间
                if (isPrev) {
                    var prevDate = new Date(timeValue);
                    if (prevDate > new Date(value)) {
                        $.validator.messages.TimeSequenceCheck = "不能早于" + timeName;
                        return false;
                        // $.validator.errorItem.push(messages[$(element).attr("validateName") + "_TimeSequenceCheck"] + "不能早于[" + timeName + "]");
                    }
                }
                    // 后置时间 
                else {
                    var nextDate = new Date(timeValue);
                    if (new Date(value) > nextDate) {
                        $.validator.messages.TimeSequenceCheck = "不能晚于" + timeName;
                        return false;
                        // $.validator.errorItem.push(messages[$(element).attr("validateName") + "_TimeSequenceCheck"] + "不能晚于[" + timeName + "]");
                    }
                }
            }
            return true;
        });
    }

    // 必填项
    function SetCssForRequireElement(eleName) {
        var ele = $("[validateName='" + eleName + "']");
        if (ele.length == 0)
            ele = $("input[name='" + eleName + "']");
        if (ele.length > 1 && ($(ele).attr("type") == "checkbox" || $(ele).attr("type") == "radio"))
            ele = $(ele).eq(0);

        if (ele.prev() == undefined || ele.prev().length == 0) {
            var tmp = "<b class='bx' style='vertical-align: top;'>&#10035;</b>";
            ele.before(tmp);
        }
        else {
            var tmp = "<b class='bx'>&#10035;</b>";
            // 防止多次加载
            if(ele.prev().find(".bx").length == 0)
                ele.prev().html(tmp + ele.prev().html());
        }     
    }

    // 重要项
    function  SetCssForImportElement(eleName) {
        var ele = $("[validateName='" + eleName + "']");
        if (ele.length == 0 && ($(ele).attr("type") == "checkbox" || $(ele).attr("type") == "radio"))
            ele = $("input[name='" + eleName + "']");


        if (ele.prev() == undefined || ele.prev().length == 0) {
            var tmp = "<b class='bx' style='vertical-align: top;'>&#10026;</b>";
            ele.before(tmp);
        }
        else {
            var tmp = "<b class='bx'>&#10026;</b>";
            // 防止多次加载
            if (ele.prev().find(".bx").length == 0)
                ele.prev().html(tmp + ele.prev().html());
        }
    }
}




