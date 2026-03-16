$(function () {
    // 所有时间控件，失去焦点后执行校验方法，校验通过后更新时间轴对应节点
    $("[timeId]").blur(function () {
        //if (validator.validateElement($(this))) {
        var timeIdElement = $("#" + $(this).attr("timeId"));
        if (timeIdElement.length > 0) {
            var dataName = timeIdElement.attr("data-desp");
            var t = $(this).val();
            if (t) {
                t = t.replace('T', " ");
                t = t.substr(0, 10);
            } else {
                t = "";
            }
            timeIdElement.attr("title", dataName + ":" + t);
            timeIdElement.attr("data-value", $(this).val());
            timeIdElement.find("i").html($(this).val());
        }
        //}
    });

    //时间轴显示
    $.ajax({
        type: 'GET',
        url: "/WorkStation/GetTimeLine",
        data: { 'user': $.trim($("#UserId").val()), 'ehrId': $.trim($("#EHRId").val()) },
        cache: false,
        async: true,
        dataType: 'json',
        success: function (data) {
            if (data.Data && data.Data.Nodes.length > 0) {
                $("#tmlDiv").css("display", "block");
                $(".layout-container-box").css("width", "calc(100% - 120px)");
                $(".layout-container-box").css("width", "-moz-calc(100% - 320px)");
                $(".layout-container-box").css("width", "-webkit-calc(100% - 320px)");
                $(".layout-container-box").css("min-width", "980px");
                $("#timeLineUl").empty();
                var htmlStr = "";
                for (var i = 0; i < data.Data.Nodes.length; i++) {
                    var t = data.Data.Nodes[i].Value;
                    var n = data.Data.Nodes[i].Name;
                    var n_desc = n;
                    var k = data.Data.Nodes[i].Key;
                    var d = data.Data.Nodes[i].Description;
                    if (data.Data.Nodes[i].IsKeyNode) {
                        if (t) {
                            t = t.replace('T', " ");
                            t = t.substr(0, 16);
                        } else {
                            t = "";
                        }
                        if (n) {
                            if (n.length >= 10) {
                                n_desc = n.substr(0, 9) + "...";
                            }
                        }
                        htmlStr += "<li title=" + n + ':' + t + " id='" + k + "' data-value='" + t + "'data-desp='" + n + "'>";
                        if (t) {
                            htmlStr += "<span class=\"done\"></span>";
                            htmlStr += "<h5 style=\"color:#424448\">" + n_desc + "<i style=\"color:#006ebc\">" + t + "</i></h5>";
                        }
                        else {
                            htmlStr += "<span class=\"done_no_time\"></span>";
                            htmlStr += "<h5 style=\"color:#777c80\">" + n_desc + "<i style=\"color:#006ebc\">" + t + "</i></h5>";
                        }

                        htmlStr += "</li>";

                        if (d) {
                            var name = d.split(":")[0];
                            var value = d.split(":")[1];
                            var name_desc = name;
                            if (name.length >= 10) {
                                name_desc = name.substr(0, 9) + "...";
                            }
                            htmlStr += "<li title=" + d + " id='desc_" + k + "' data-value='" + value + "'data-desp='" + n + "'>";
                            htmlStr += "<h5 style=\"color:#424448\">&nbsp;" + name_desc + "<i style=\"color:#006ebc\">&nbsp;" + value + "mins</i></h5>";
                            htmlStr += "</li>";
                        }
                    }
                    else {
                        if (t) {
                            t = t.replace('T', " ");
                            t = t.substr(0, 16);
                        } else {
                            t = "";
                        }
                        if (n) {
                            if (n.length >= 10) {
                                n_desc = n.substr(0, 9) + "...";
                            }
                        }
                        htmlStr += "<li style=\"display:none\" + title=" + n + ':' + t + " id='" + k + "' data-value='" + t + "'data-desp='" + n + "'>";
                        htmlStr += "<span class=\"done\"></span>";
                        htmlStr += "<h5>" + n_desc + "<i>" + t + "</i></h5>";
                        htmlStr += "</li>";
                    }
                }
                $("#timeLineUl").append(htmlStr);
                InitTimeLine();
            } else {
                $("#tmlDiv").css("display", "none");
                $(".layout-container-box").css("width", "calc(100% - 120px)");
                $(".layout-container-box").css("width", "-moz-calc(100% - 120px)");
                $(".layout-container-box").css("width", "-webkit-calc(100% - 120px)");
                $(".layout-container-box").css("min-width", "1115px");
            }
        }
    });

    function InitTimeLine() {
        // 所有时间控件，再次与时间轴同步
        $("[timeId]").each(function () {
            var timeIdElement = $("#" + $(this).attr("timeId"));
            if (timeIdElement.length > 0 && $(this).val()) {
                timeIdElement.attr("data-value", $(this).val());
                timeIdElement.find("i").html($(this).val());
            }
        });
    }

    //保存
    $("#save").click(function () {
        debugger;
        var model = getDetail();
        var userId = $.trim($("#UserId").val());
        $.ajax({
            type: 'post',
            url: '/WorkStation/PushEmergency?user=' + userId,
            data: { emergency: model },
            cache: false,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.IsSuccess) {
                    $.messager.alert("友情提示", "保存成功", "info", function () {
                        window.location.reload();
                    });
                } else {
                    $.messager.alert("错误", "保存失败", "error", function () {
                        window.location.reload();
                    });
                }

            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    });
})

function getDetail() {
    detail.RegistId = $.trim($("#RegistId").val());
    detail.EHRId = $.trim($("#EHRId").val());
    detail.AttackTime = $.trim($("#AttackTime").val());
    detail.AttackAddress = $.trim($("#AttackAddress").val());
    detail.ArrivedHospitalTime = $.trim($("#ArrivedHospitalTime").val());
    detail.AdmissionTime = $.trim($("#AdmissionTime").val());
    detail.FmcTime = $.trim($("#FmcTime").val());
    detail.EcgTime = $.trim($("#EcgTime").val());
    detail.EcgDiagnoseTime = $.trim($("#EcgDiagnoseTime").val());
    detail.OpenVeinTime = $.trim($("#OpenVeinTime").val());
    detail.MainComplaint = $.trim($("#MainComplaint").val());
    detail.PresentIllness = $.trim($("#PresentIllness").val());
    detail.FmcDoctor = $.trim($("#FmcDoctor").val());
    detail.DiagnosisDoctor = $.trim($("#DiagnosisDoctor").val());
    detail.DiagnosisTime = $.trim($("#DiagnosisTime").val());
    detail.CtIsHemorrhage = $("input[name='CtIsHemorrhage']:checked").val();
    detail.Diagnosis = $("input[name='Diagnosis']:checked").val();
    return detail;
}