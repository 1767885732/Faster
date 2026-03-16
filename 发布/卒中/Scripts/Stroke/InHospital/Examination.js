$(function () {

    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    // 初始化页面控件
    InitOtherOption();
    ShowNext();

    // 异步加载默认值
    InitData();

    // 保存
    $("#btn_saveExamination").click(function () {
        SaveExamination();
    });

    function InitOtherOption() {
        $(".OtherOption").each(function () {
            if ($(this).is(':checked')) {
                $(this).next().next().show();
            }
        });

        $(".OtherOption").click(function () {
            if ($(this).is(':checked')) {
                $(this).next().next().show();
            }
            else {
                $(this).next().next().hide();
                var nextObj = $(this).next().next().next();
                if (nextObj != undefined) nextObj.hide();
            }
        });
    }

    function ShowNext()
    {
        $(".showNext").each(function () {
            if ($(this).is(':checked')) {
                $(this).next().next().show();
                $(this).next().next().next().show();
            }
            else {
                $(this).next().next().hide();
                var nextObj = $(this).next().next().next();
                if (nextObj != undefined) nextObj.hide();
                nextObj = $(this).next().next().next().next();
                if (nextObj != undefined) nextObj.hide();
            }
        });

        $(".showNext").click(function () {
            if ($(this).is(':checked')) {
                $(this).next().next().show();
                $(this).next().next().next().show();
            }
            else {
                $(this).next().next().hide();
                var nextObj = $(this).next().next().next();
                if (nextObj != undefined) nextObj.hide();
                nextObj = $(this).next().next().next().next();
                if (nextObj != undefined) nextObj.hide();
            }
        });
    }

    function InitData()
    {
        // 加载选项
        $.ajax({
            type: 'get',
            url: '/Inhospital/GetExaminationDefaultValue',
            data: { registId: $("#registId").val() },
            cache: false,
            dataType: 'json',
            async: false,
            success: function (data) {
                $.each(data.Items, function (index, item) {
                    var object = $("#_frm_inhospital_examination").find("input[name='" + item.Name + "'][value=" + item.Key + "]");
                    object.attr("checked", true);
                    if (object.hasClass("OtherOption")) {
                        object.next().next().show();
                        object.next().next().val(item.Value);
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    function SaveExamination()
    {
        if (!$("#_frm_inhospital_examination").validate().form()) return false;

        var Data = $("#_frm_inhospital_examination").serializeObject();
        // 患者其他症状
        Data.OtherSymptoms = new Array();
        $.each($("input[name='OtherSymptom']:checked"), function (index, item) {
            Data.OtherSymptoms.push($(item).val());
        })

        // 住院期间并发症
        Data.Complications = new Array();
        $.each($("input[name='InHospitalComplication']:checked"), function (index,item) {
            Data.Complications.push($(item).val());
        })

        // 

        $.ajax({
            type: 'POST',
            url: '/Inhospital/SaveExamination',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    $.messager.alert("提示", "保存成功", "info", function () {
                        refreshTab("Examination");
                    });
                } else {
                    $.messager.alert("错误", "保存失败:" + json.Message, "error");
                }
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
        
    }

    // 获取当前时间
    $("a[name='GetCurrentTime']").click(function () {
        var time = new Date();
        // 程序计时的月从0开始取值后+1
        var m = time.getMonth() + 1;
        var t = time.getFullYear() + "-" + m + "-"
          + time.getDate() + " " + time.getHours() + ":"
          + time.getMinutes();
        $(this).prev().val(t);
    });
});