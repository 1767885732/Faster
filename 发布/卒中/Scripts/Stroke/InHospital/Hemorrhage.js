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

    // 异步加载默认值
    InitData();

    // 保存
    $("#btn_saveHemorrhage").click(function () {
        SaveSubarachnoidHemorrhage();
    });

    function InitOtherOption() {
        $(".OtherOption").each(function () {
            if ($(this).is(':checked')) {
                $(this).next().next().show();
            }
            else {
                $(this).next().next().hide();
                var nextObj = $(this).next().next().next();
                if (nextObj != undefined) nextObj.hide();
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

    function InitData()
    {
        // 加载选项
        $.ajax({
            type: 'get',
            url: '/Inhospital/GetHemorrhageDefaultValue',
            data: { registId: $("#registId").val() },
            cache: false,
            dataType: 'json',
            async: false,
            success: function (data) {
                $.each(data.Items, function (index, item) {
                    var object = $("#_frm_inhosptial_subarachnoidhemorrhage").find("input[name='" + item.Name + "'][value=" + item.Key + "]");
                    object.attr("checked", true);
                    if (object.hasClass("OtherOption")) {
                        object.next().next().show();
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    function SaveSubarachnoidHemorrhage()
    {
        if (!$("#_frm_inhosptial_subarachnoidhemorrhage").validate().form()) return false;

        var Data = $("#_frm_inhosptial_subarachnoidhemorrhage").serializeObject();
        // 蛛网膜下腔出血致病原因
        Data.SahEtiology = new Array();
        $.each($("input[name='SahEtiology']:checked"), function (index, item) {
            Data.SahEtiology.push($(item).val());
        })

        // 尼莫地平预防血管痉挛治疗方式
        Data.NimodipineCureType = $("input[name='NimodipineCureType']:checked").val();
        var index = Data.NimodipineCureType == "0" ? 0 : 1;
        Data.NimodipineDose = $("input[name='NimodipineDose']").eq(index).val();
        Data.NimodipineFrequency = $("input[name='NimodipineFrequency']").val();
        Data.NimodipineUsageDays = $("input[name='NimodipineUsageDays']").eq(index).val();

        //动脉瘤治疗方式
        Data.AneurysmCureTime = $("input[name='AneurysmCureType']:checked").next().val();

        // 脑脊液分流
        Data.CsfShuntTime = $("input[name='CsfShuntType']:checked").next().val();

        $.ajax({
            type: 'POST',
            url: '/Inhospital/SaveSubarachnoidHemorrhage',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    $.messager.alert("提示", "保存成功", "info", function () {
                        refreshTab("SubarachnoidHemorrhage");
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

    // 尼莫地平预防血管痉挛治疗
    $("input[name='NimodipineCureType']").change(function () {
        $("input[name='NimodipineCureType']").each(function () {
            if ($(this).is(':checked')) {
                $(this).next().show();
                $(this).next().next().show();
            }
            else {
                $(this).next().hide();
                var nextObj = $(this).next().next();
                if (nextObj != undefined) nextObj.hide();
            }
        });
    });

    // 动脉瘤治疗方式
    $("input[name='AneurysmCureType']").change(function () {
        $("input[name='AneurysmCureType']").each(function () {
            if ($(this).is(':checked')) {
                $(this).next().show();
                $(this).next().next().show();
            }
            else {
                $(this).next().hide();
                var nextObj = $(this).next().next();
                if (nextObj != undefined) nextObj.hide();
            }
        });
    });

    // 脑脊液分流
    $("input[name='CsfShuntType']").change(function () {
        $("input[name='CsfShuntType']").each(function () {
            if ($(this).is(':checked')) {
                $(this).next().show();
                $(this).next().next().show();
            }
            else {
                $(this).next().hide();
                var nextObj = $(this).next().next();
                if (nextObj != undefined) nextObj.hide();
            }
        });
    })

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