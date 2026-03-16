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

    // 页面打钩图标初始化
    $(".valueNullCheck").each(function (index, item) {
        dateChanged(item);
    })

    // 保存
    $("#btn_save").click(function () {
        Save();
    });

    function Save()
    {
        var Data = $("#_frm_patientTreatment").serializeObject();

        $.ajax({
            type: 'POST',
            url: '/Treatment/Save',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    $.messager.alert("提示", "保存成功", "info", function () {
                        refreshPartialView(4);
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

    // NIHSS评分
    $("#nihss").click(function () {
        $("<div id='nihssDialog'></div>").dialog({
            title: 'NIHSS评分',
            width: 1000,
            height: 568,
            href: '/Treatment/NIHSSScore?registerId=' + $("#RegisterID").val(),
            modal: true,
            onClose: function () {
                // 更新最近一次NIHSS评分
                $.ajax({
                    type: 'get',
                    url: '/Treatment/GetLastNIHSSScore',
                    data: { registerID: $("#RegisterID").val() },
                    cache: false,
                    dataType: 'json',
                    async: false,
                    success: function (json) {
                        if (json.IsSuccess) {
                            if (json.Data != "") {
                                var time = json.Data.AssessmentTime;
                                var timeHtml = "时间：";
                                if (time != null && typeof time != "undefined" && time != "") {
                                    timeHtml = timeHtml + moment(time).format("YYYY-MM-DD HH:mm");
                                }
                                $("#nihssTime").html(timeHtml);
                                $("#nihssScore").html("NIHSS得分：" + json.Data.Score + " 分");
                            }
                            else {
                                $("#nihssTime").html("");
                                $("#nihssScore").html("");
                            }
                        }
                        
                    },
                    error: function () {
                        $.messager.alert("错误", "请求失败！", "error");
                    }
                });

                $(this).remove();
            }
        });
    });

    // NIHSS评分
    $("#hat").click(function () {
        $("<div id='hatDialog'></div>").dialog({
            title: 'Hat评分',
            width: 1000,
            height: 568,
            href: '/Treatment/HatScore?registerId=' + $("#RegisterID").val(),
            modal: true,
            onClose: function () {
                $(this).remove();
            }
        });
    });

    // 
    $(document).off("change", "input[name='TreatmentType']");
    $(document).on("change", "input[name='TreatmentType']", function () {
        if ($(this).val() == "1") {
            $("#thromPanel").show();
        }
        else {
            $("#thromPanel").hide   ();
        }
    })

    // 溶栓
    $("#thrombolysis").click(function () {
        $("<div id='thrombolysisDialog'></div>").dialog({
            title: '患者溶栓',
            width: 800,
            height: 568,
            href: '/Treatment/Thrombolysis?step=1&registerId=' + $("#RegisterID").val(),
            modal: true,
            onClose: function () {
                $(this).remove();
            }
        });
    });
    
    $(document).on("propertychange", ".valueNullCheck", function () {
        alert($(this).val());
        
    });

    // 获取当前时间
    $("a[name='GetCurrentTime']").click(function () {
        var timeStr = moment().format("YYYY-MM-DD HH:mm");
        $(this).prev().val(timeStr);
    });
});

function dateChanged(obj) {
    var obj1 = $(obj).parent();
    var obj2 = $(obj).parent().parent().find("i");
    if ($(obj).val() != "") {
        obj1.addClass("actived");
        obj2.addClass("actived");
    }
    else {
        obj1.removeClass("actived");
        obj2.removeClass("actived");
    }
}

function datePicked() {
    var obj1 = $(this).parent().addClass("actived");
    var obj2 = $(this).parent().parent().find("i").addClass("actived");
}

function dateCleared() {
    var obj1 = $(this).parent().removeClass("actived");
    var obj2 = $(this).parent().parent().find("i").removeClass("actived");
}



