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
    InitNoticeBtn();
    // 异步加载默认值
    InitData();

    // 保存
    $("#btn_save").click(function () {
        Save();
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

    function InitNoticeBtn()
    {
        $(document).on("click", "input[name='PrehEval.NoticeCT'],input[name='PrehEval.NoticeNeurology']", function () {
            $(this).is(':checked') ? $(this).next().next().show() : $(this).next().next().hide();
        });

        if ($("input[name='PrehEval.NoticeCT']").is(':checked')) $("input[name='PrehEval.NoticeCT']").next().next().show();
        if ($("input[name='PrehEval.NoticeNeurology']").is(':checked')) $("input[name='PrehEval.NoticeNeurology']").next().next().show();
    }

    function InitData()
    {
        // 加载选项
        $.ajax({
            type: 'get',
            url: '/FirstAid/GetDefaultValue',
            data: { registId: $("#RegisterInfo_ID").val() },
            cache: false,
            dataType: 'json',
            async: false,
            success: function (data) {
                $.each(data.Items, function (index, item) {
                    var object = $("input[name='" + item.Name + "'][value=" + item.Key + "]");
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

    // 发病前mRS评分
    $("input[name='mrs']").click(function () {
        $("#mrsScore").html($(this).val());
    });

    function Save()
    {
        if (!$("#_frm_prehospital").validate(
        {
            //提示报错  
            messages:
            { 
                DiseaseTime: "发病时间必须输入"
            }
        }).form()) return false;

        var Data = $("#_frm_prehospital").serializeObject();
        // 发病时间
        Data["PrehEval.DiseaseTime"] = $("#DiseaseTime").val();
        if ($("#PrehEval_NoticeCT").is(':checked')) Data["PrehEval.NoticeCTArriveTime"] = $("#NoticeCTArriveTime").val();
        if ($("#PrehEval_NoticeNeurology").is(':checked')) Data["PrehEval.NoticeNeurologyArriveTime"] = $("#NoticeNeurologyArriveTime").val();

        // 既往病史
        Data.DiseaseHistories = new Array();
        $("input[name='PastHistory']:checked").each(function (index, item) {
            var DiseaseHistory = new Object();
            DiseaseHistory.DiseaseCode = $(this).val();
            DiseaseHistory.DiseaseName = DiseaseHistory.DiseaseCode == "99" ? $(this).parent().find("input[type='text']").val() : $(this).next().html();
            Data.DiseaseHistories.push(DiseaseHistory);
        });
        
        // 用药史
        Data.DrugHistories = new Array();
        // 抗血小板类
        $("input[name='Antiplatelet']:checked").each(function (index, item) {
            Data.DrugHistories.push(CreateDrug("01", "抗血小板类", $(this)));
        });
        // 抗凝类
        $("input[name='AntiCoagulation']:checked").each(function (index, item) {
            Data.DrugHistories.push(CreateDrug("02", "抗凝类", $(this)));
        });
        // 降压类
        $("input[name='Antihypertensive']:checked").each(function (index, item) {
            Data.DrugHistories.push(CreateDrug("03", "降压类", $(this)));
        });
        // 降糖类
        $("input[name='Hypoglycemic']:checked").each(function (index, item) {
            Data.DrugHistories.push(CreateDrug("04", "降糖类", $(this)));
        });
        // 调脂类
        $("input[name='LipidModulation']:checked").each(function (index, item) {
            Data.DrugHistories.push(CreateDrug("05", "调脂类", $(this)));
        });
        // 其他
        $("input[name='OtherDrugs']:checked").each(function (index, item) {
            Data.DrugHistories.push(CreateDrug("99", "其他", $(this)));
        });

        //发病期mRS评分
        Data["PreMrsAssessment.Score"] = $("#mrsScore").html();

        $.ajax({
            type: 'POST',
            url: '/FirstAid/Save',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    $.messager.alert("提示", "保存成功", "info", function () {
                        refreshPartialView(2);
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

    function CreateDrug(DrugTypeCode, DrugTypeName, obj)
    {
        var DrugHistory = new Object();
        DrugHistory.DrugTypeCode = DrugTypeCode;
        DrugHistory.DrugTypeName = DrugTypeName;
        DrugHistory.DrugGenericCode = obj.val();
        DrugHistory.DrugGenericName = DrugHistory.DrugGenericCode == "99" ? obj.parent().find("input[type='text']").val() : obj.next().html();
        return DrugHistory;
    }

    // 获取当前时间
    $("a[name='GetCurrentTime']").click(function () {
        var timeStr = moment().format("YYYY-MM-DD HH:mm");
        $(this).prev().val(timeStr);
    });
});