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
    //InitData();

    // 保存
    $("#btn_saveOutcome").click(function () {
        SaveOutcome();
    });

    // 计算总价
    $(document).off('change',"#DrugExpense,#ExamineExpense");  
    $(document).on('change',"#DrugExpense,#ExamineExpense",function(){
        var total = parseInt($("#DrugExpense").val()) + parseInt($("#ExamineExpense").val());
        $("#AdmissionTotalAmount").html(total);
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

    //function InitData()
    //{
    //    // 加载选项
    //    $.ajax({
    //        type: 'get',
    //        url: '/Inhospital/GetOutcomeDefaultValue',
    //        data: { registId: $("#registId").val() },
    //        cache: false,
    //        dataType: 'json',
    //        async: false,
    //        success: function (data) {
    //            $.each(data.Items, function (index, item) {
    //                var object = $("#_frm_inhosptial_outcome").find("input[name='" + item.Name + "'][value=" + item.Key + "]");
    //                object.attr("checked", true);
    //                if (object.hasClass("OtherOption")) {
    //                    object.next().next().show();
    //                    object.next().next().val(item.Value);
    //                }
    //            });
    //        },
    //        error: function () {
    //            $.messager.alert("错误", "请求失败！", "error");
    //        }
    //    });
    //}

    function SaveOutcome()
    {
        if (!$("#_frm_inhosptial_outcome").validate().form()) return false;

        var Data = $("#_frm_inhosptial_outcome").serializeObject();
        
        // 用药
        Data.DrugUses = new Array();
        // 抗血小板类
        $("input[name='Antiplatelet']:checked").each(function (index, item) {
            Data.DrugUses.push(CreateDrug("01", "抗血小板类", $(this)));
        });
        // 抗凝类
        $("input[name='AntiCoagulation']:checked").each(function (index, item) {
            Data.DrugUses.push(CreateDrug("02", "抗凝类", $(this)));
        });
        // 降压类
        $("input[name='Antihypertensive']:checked").each(function (index, item) {
            Data.DrugUses.push(CreateDrug("03", "降压类", $(this)));
        });
        // 降糖类
        $("input[name='Hypoglycemic']:checked").each(function (index, item) {
            Data.DrugUses.push(CreateDrug("04", "降糖类", $(this)));
        });
        // 调脂类
        $("input[name='LipidModulation']:checked").each(function (index, item) {
            Data.DrugUses.push(CreateDrug("05", "调脂类", $(this)));
        });
        // 其他
        $("input[name='OtherDrugs']:checked").each(function (index, item) {
            Data.DrugUses.push(CreateDrug("99", "其他", $(this)));
        });
        

        $.ajax({
            type: 'POST',
            url: '/Inhospital/SaveOutcome',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    $.messager.alert("提示", "保存成功", "info", function () {
                        refreshTab("Outcome");
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

    function CreateDrug(DrugTypeCode, DrugTypeName, obj) {
        var DrugUse = new Object();
        DrugUse.DrugTypeCode = DrugTypeCode;
        DrugUse.DrugTypeName = DrugTypeName;
        DrugUse.DrugGenericCode = obj.val();
        DrugUse.DrugGenericName = DrugUse.DrugGenericCode == "99" ? obj.parent().find("input[type='text']").val() : obj.next().html();
        return DrugUse;
    }

    // 获取当前时间
    $("a[name='GetCurrentTime']").click(function () {
        var timeStr = moment().format("YYYY-MM-DD HH:mm");
        $(this).prev().val(timeStr);
    });
});