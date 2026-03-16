var workplatGrid = null;
String.prototype.padStart =
function (maxLength, fillString) {
    let str = String(this);
    if (str.length >= maxLength) {
        return str;
    }

    fillString = String(fillString);
    if (fillString.length === 0) {
        fillString = ' ';
    }

    let fillLen = maxLength - str.length;
    let timesToRepeat = Math.ceil(fillLen / fillString.length);
    let truncatedStringFiller = fillString
        .repeat(timesToRepeat)
        .slice(0, fillLen);
    return truncatedStringFiller + str;
};

function GetTime(x) {
    if (x == undefined) return "";
    x = /\d+/.exec(x)[0];
    x = new Date(+x);
    let y = x.getFullYear();
    let m = x.getMonth() + 1 + '';
    let d = x.getDate() + '';
    let h = x.getHours() + '';
    let mi = x.getMinutes() + '';
    return y + '-' + m.padStart(2, '0') + '-' + d.padStart(2, '0') + ' ' + h.padStart(2, '0') + ':' + mi.padStart(2, '0');// `${y}-${m.padStart(2, '0')}-${d.padStart(2, '0')} ${h.padStart(2, '0')}:${mi.padStart(2, '0')}`;
}

$(function () {

    //画面打开时不查询
    InitPatients();

    $("#btnStationSearch").click(function (e) {
        QueryPatients();
    });

    /* tab切换 */
    $("#hospitalized-nav a").click(function () {
        $("#hospitalized-nav a").removeClass("hover");
        $(this).addClass("hover");
        var source = $(this).attr("source");
        $("#hdnType").val(source);

        InitPatients();
    });

    $("#btnSynchro").click(function (e) {
        var type = $("#hdnType").val();
        var org = $('#sltStationHopitalName').find(":selected").val();
        var row = $("#grdSationDataList").datagrid('getSelected');
        if (row == null) {
            layer.alert("请选择患者。", { title: '提示', icon: 0, closeBtn: 0, move: false, zIndex: layer.zIndex });
            return;
        }
        var maskIndex = layer.load();
        $.ajax({
            type: 'POST',
            data: { org: org, source: type, sourceId: row.UniqueNo },
            datatype: "json",
            url: "/DataManage/Extract",
            success: function (data) {
                if (data.IsSuccess) {
                    var ehrId = data.Data;
                    layer.close(maskIndex); // 关闭等待层
                    window.open(AddUrlPrefix("/DataManage/CreateExtracted?org=" + org + "&ehrId=" + ehrId + "&source=" + type));
                }
                else {
                    layer.close(maskIndex); // 关闭等待层
                    layer.alert(data.Message, { title: '错误', icon: 2, closeBtn: 0, move: false, zIndex: layer.zIndex });
                }
            },
            error: function (req, status, error) {
                layer.close(maskIndex); // 关闭等待层
                // 请求失败执行代码
                layer.alert("请求失败！", { icon: 2 });
            }
        });
    });

    $("#btnRelation").click(function (e) {
        var type = $("#hdnType").val();
        var org = $('#sltStationHopitalName').find(":selected").val();
        var row = $("#grdSationDataList").datagrid('getSelected');
        if (row == null) {
            layer.alert("请选择患者。", { title: '提示', icon: 0, closeBtn: 0, move: false, zIndex: layer.zIndex });
            return;
        }
        else {
            layer.confirm("确实需要关联吗？", function (r) {
                window.parent.$("#IsConfirm").val("1");
                window.parent.$("#Patient").val(JSON.stringify(row));
                layer.closeAll();
            });
        }
    });

    function ShowVisitId(source) {
        if (source == '3' || source == '5') {
            $('#visitIdTitle').css("display", "");
            $('#visitId').css("display", "");
            if (source == '3') $('#visitIdTitle').text("门诊号:");
            if (source == '5') $('#visitIdTitle').text("住院号:");
            return;
        }
        $('#visitIdTitle').css("display", "none");
        $('#visitId').css("display", "none");
    }

    //计算天数差的函数，通用  
    function DateDiff(sDate1, sDate2) {
        var aDate, oDate1, oDate2, iDays;
        aDate = sDate1.replace("年", "-").replace("月", "-").replace("日", "").split("-");
        oDate1 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0]);    //转换为12-18-2006格式
        aDate = sDate2.replace("年", "-").replace("月", "-").replace("日", "").split("-");
        oDate2 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0]);
        iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);    //把相差的毫秒数转换为天数
        return iDays;
    }

    function QueryPatients() {
        var sDate = $("#txtStationStartDate").val();
        var eDate = $("#txtStationEndDate").val();
        var organCode = $('#sltStationHopitalName').find(":selected").val();
        var name = $("#patientName").val();
        var diagnosisType = $("#txtDiagnosisType").val();
        var visitId = $("#visitId").val();
        if (organCode == "") {
            var index = layer.alert('请选择医院！', { icon: 1 }, function (index) {
                layer.close(index)
            });
            return;
        }
        if (sDate == "" || eDate == "") {
            var index = layer.alert('请选择查询开始时间和结束时间！', { icon: 1 }, function (index) {
                layer.close(index)
            });
            return;
        }
        else if (DateDiff(eDate, sDate) > 2) {
            layer.alert('选择的查询时间范围不能超过2天！', { icon: 1 }, function (index) {
                layer.close(index)
            });
            return;
        }
        var params = {
            From: "",
            SchemaOnly: false,
            OrgCode: organCode,
            DateFrom: sDate,
            DateTo: eDate,
            Name: name,
            No: "",
            Diagnosis: diagnosisType,
        };
        var source = $("#hdnType").val();
        params.From = source;
        if (source == '3') params.OutpatientNo = visitId;
        if (source == '5') params.InpatientNo = visitId;
        LoadPatients(source, true, params);
    }

    function InitPatients() {
        var sDate = new Date();
        sDate.setDate(sDate.getDate() - 1)
        $("#txtStationStartDate").val(sDate.getFullYear() + "年" + (sDate.getMonth() + 1) + "月" + sDate.getDate() + "日");
        var eDate = new Date();
        $("#txtStationEndDate").val(eDate.getFullYear() + "年" + (eDate.getMonth() + 1) + "月" + eDate.getDate() + "日");
        var name = $("#hdnName").val();
        $("#patientName").val(name);
        var source = $("#hdnType").val();
        ShowVisitId(source);
        if (source == null || typeof source == "undefined") {
            return;
        }
        var source = $("#hdnType").val();        
        LoadPatients(source, false, { SchemaOnly: true });
    }

    function GetGridSchema(source) {
        var defgrid = {};
        if (source == '2') {
            defgrid.Action = "QueryIF";
            defgrid.TimeTitle = "分诊时间";
            defgrid.DisplayTitle = "急诊号";
        }
        if (source == '3') {
            defgrid.Action = "QueryIF";
            defgrid.TimeTitle = "挂号时间";
            defgrid.DisplayTitle = "门诊号";
        }
        if (source == '5') {
            defgrid.Action = "QueryIF";
            defgrid.TimeTitle = "入院时间";
            defgrid.DisplayTitle = "住院号";
        }
        return defgrid;
    }

    function LoadPatients(source, reload, params) {
        var schema = GetGridSchema(source);
        if (schema.Action == '') return;
        var randomNu = (new Date().getTime()) ^ Math.random();
        if (workplatGrid != null && reload) {
            $("#grdSationDataList").datagrid("load", params);
            return;
        }
        workplatGrid = $('#grdSationDataList').datagrid({
            loadMsg: "数据加载中，请稍后……",
            pagination: false,
            collapsible: true,
            fitColumns: true,
            sortName: "Time",
            sortOrder: "DESC",
            pageSize: 15,
            pageList: [10, 15, 20],
            url: '/DataManage/' + schema.Action + '?_=' + randomNu + '',
            queryParams: params,
            method: 'post',
            columns: [[
                { field: 'DisplayNo', title: schema.DisplayTitle, width: 120, align: 'center'},
                { field: 'Time', title: schema.TimeTitle, width: 120, align: 'center', sortable: true },
                { field: 'Name', title: '姓名', width: 100, align: 'center', sortable: true },
                { field: 'Gender', title: '性别', width: 70, align: 'center' },
                //{ field: 'Age', title: '年龄', width: 50, align: 'center' },
                { field: 'BirthDate', title: '出生日期', width: 150, align: 'center'},
                //{ field: 'IdNo', title: '身份证号', width: 150, align: 'center', sortable: true },
                //{ field: 'PhoneNumer', title: '联系电话', width: 100, align: 'center' },
                { field: 'Complaint', title: '初步诊断', width: 180, align: 'center' }
            ]]
        });
    }
});