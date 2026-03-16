// 对Date的扩展，将 Date 转化为指定格式的String   
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
// 例子：   
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18   
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

$(document).ready(function () {
    GetReportMain();
});

function GetReportMain() {
    generateChart(reportName, "line");
}

function changeTab(targetId, srcId, value) {
    var menuTarget = document.getElementById(targetId);
    var menuSrc = document.getElementById(srcId);
    menuTarget.className = "hover";
    menuSrc.className = "";
    checkChange(value);
}

function checkChange(value) {
    var chartType = "line";
    if (value == 1) {
        chartType = "line";
    }
    if (value == 2) {
        chartType = "bar";
    }
    generateChart(reportName, chartType);
}

function generateChart(reportName, chartType) {
    if (reportName == "Report6-1") {
        RequestReport("GetReportMain", "神经功能缺损评分比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-2") {
        RequestReport("GetReportMain", "急诊20分钟内完成头颅CT影像学检查的比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-3") {
        RequestReport("GetReportMain", "急诊就诊在30分钟内获得临床实验室诊断信息的比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-4") {
        RequestReport("GetReportMain", "发病2小时内到院，发病3小时以内rt-PA静脉溶栓治疗比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-5") {
        RequestReport("GetReportMain", "DNT", "统计月份", "", chartType, reportName, "");
    }
    //else if (reportName == "Report6-6") {
    //    RequestReport("GetReportMain", "预防深静脉血栓比率", "统计月份", "", chartType, reportName, "");
    //}
    //else if (reportName == "Report6-7") {
    //    RequestReport("GetReportMain", "住院1周内接受血管评价的比率", "统计月份", "", chartType, reportName, "");
    //}
    else if (reportName == "Report6-8") {
        RequestReport("GetReportMain", "住院期间患者使用他汀类药物治疗的比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-9") {
        RequestReport("GetReportMain", "出院时给予抗栓治疗的比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-10") {
        RequestReport("GetReportMain", "出院时合并高血压的脑梗死患者降压治疗比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-11") {
        RequestReport("GetReportMain", "出院时给予他汀类药物治疗比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-12") {
        RequestReport("GetReportMain", "出院时合并糖尿病患者使用降糖药物比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-13") {
        RequestReport("GetReportMain", "出院时房颤患者给予抗凝治疗比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-14") {
        RequestReport("GetReportMain", "平均住院日", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-15") {
        RequestReport("GetReportMain", "平均住院费用", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-16") {
        RequestReport("GetReportMain", "平均住院药物费用", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-17") {
        RequestReport("GetReportMain", "住院病死率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-18") {
        RequestReport("GetReportMain", "溶栓DNT", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-19") {
        RequestReport("GetReportMain", "溶栓OTT", "统计月份", "", chartType, reportName, "");
    } else if (reportName == "Report6-20") {
        RequestReport("GetReportMain", "终止溶栓率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-21") {
        RequestReport("GetReportMain", "发病4.5小时以内rt-PA静脉溶栓治疗比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-22") {
        RequestReport("GetReportMain", "静脉溶栓的急性脑梗死患者到院到给药时间小于60分钟的比率", "统计月份", "", chartType, reportName, "");
    }
    else if (reportName == "Report6-23") {
        RequestReport("GetReportMain", "发病6小时内脑梗死患者血管内机械取栓率", "统计月份", "", chartType, reportName, "");
    }
    //else if (reportName == "Report6-24") {
    //    RequestReport("GetReportMain", "脑梗死患者吞咽功能筛查率", "统计月份", "", chartType, reportName, "");
    //}
    //else if (reportName == "Report6-25") {
    //    RequestReport("GetReportMain", "发病24小时内非致残性脑梗死患者双重强化抗血小板药物治疗率", "统计月份", "", chartType, reportName, "");
    //}
    else if (reportName == "Report6-26") {
        RequestReport("GetReportMain", "住院期间合并房颤的脑梗死患者抗凝治疗率", "统计月份", "", chartType, reportName, "");
    }
    //else if (reportName == "Report6-27") {
    //    RequestReport("GetReportMain", "脑梗死患者康复评估率", "统计月份", "", chartType, reportName, "");
    //}
}

function RequestReport(url, reportTitle, yAxisTitle, plotLinesText, chartType, reportName, plotLinesValue) {
    $.ajax({
        type: "POST",
        url: "/Report/GetReportMain",
        dataType: "json",
        data: { sTime: sTime, eTime: eTime, reportType: reportType, reportName: reportName, chartType: chartType, reportTitle: reportTitle, hospitalID: hospitalID },
        success: function (data) {
            if (data != null) {
                ShowChart(data[0].Value.categories, data[0].Value.series, reportTitle, yAxisTitle, plotLinesText, chartType, plotLinesValue, reportName, hospitalName);
                RequestTable(data[1].Value, reportName);
            }
        }
    });
}

function ShowChart(pCategories, pSeries, reportTitle, yAxisTitle, plotLinesText, chartType, plotLinesValue, reportName, hospitalName) {
    var visible = false;
    var titleName = "";
    if (hospitalName != "{}") {
        titleName = hospitalName + " | 报表生成时间 " + (new Date()).Format("yyyy-MM-dd hh:mm") + "<br>"
        $("#generateTime_Line").html(titleName);
    }
    var xAxisMax = 0;
    //if (pCategories.length > 10) {
    //    visible = true;
    //    xAxisMax = 10;
    //}
    //else {
    //    xAxisMax = pCategories.length - 1;
    //}
    //pSeries[0].name = chartSeriesName(reportName);

    var series = [];
    for (var s in pSeries) {
        series.push({
            name: pSeries[s].name, type: chartType, data: pSeries[s].data, label: {
                normal: {
                    show: true,
                }
            }
        });
    }
    echarts.init(document.getElementById('chart_main'), 'macarons').setOption({
        title: {
            text: reportTitle,
            subtext: sTime + '-' + eTime,
            x: 'center'
        },
        tooltip: {
            trigger: 'axis'
        },
        xAxis: {
            type: 'category',
            //boundaryGap: false,
            data: pCategories,
            axisLabel: {
                formatter: function (value, index) {
                    if (reportType == "W") {
                        var year = value.substring(0, 4);
                        var week = value.substring(4, 6);
                        return year + "年第" + week + "周";
                    }
                    else {
                        return value;
                    }
                }
            }
        },
        yAxis: {
            axisLabel: {
                formatter: function (value, index) {
                    if (reportName == 'Report6-14')
                    { return value + '天'; }
                    else if (reportName == 'Report6-15' || reportName == 'Report6-16')
                    { return value + '元'; }
                    else if (reportName == 'Report6-18' || reportName == 'Report6-19' || reportName == 'Report6-5' || reportName == 'Report6-6')
                    { return value + '分钟'; }
                    else {
                        return value + '%';
                    }
                }
            }
        },
        series: series
    });

    $("#detail_table").html("");
    $("#chart_detail").html("");
}

function RequestTable(data, reportName) {
    if (data != null && data != "") {
        if (reportName == "Report6-14") {
            ShowTable(data, "住院天数(天)", reportName);
        }
        else if (reportName == "Report6-15" || reportName == "Report6-16") {
            ShowTable(data, "住院费用(元)", reportName);
        }
        else if (reportName == "Report6-17") {
            ShowTablePercent(data, "死亡例数", reportName);
        }
        else if (reportName == "Report6-18") {
            ShowTable(data, "DNT(分钟)", reportName);
        }
        else if (reportName == "Report6-19") {
            ShowTable(data, "OTT(分钟)", reportName);
        }
        else if (reportName == "Report6-5") {
            ShowTable(data, "DNT(分钟)", reportName);
        }
        else if (reportName == "Report6-6") {
            ShowTable(data, "DPT(分钟)", reportName);
        }
        else {
            ShowTablePercent(data, "患者例数", reportName);
        }
    }
}

function chartSeriesName(reportName) {
    if (reportName == "Report6-14") {
        return "住院日";
    }
    else if (reportName == "Report6-15" || reportName == "Report6-16") {
        return "费用";
    }
    else if (reportName == "Report6-18" || reportName == "Report6-19") {
        return "时间";
    }
    else { return "比率"; }
}

function ShowTablePercent(data, typename, reportName) {
    console.log(data);
    var table = "<table class='basic-table'>";
    table += "<tr>";
    if (reportType == "M") {
        table += "<td >月份</td> ";
    }
    else {
        table += "<td >周数</td> ";
    }
    for (var o in data) {
        var timeStr;
        if (reportType == "W") {
            var year = data[o].TimeExpress.substring(0, 4);
            var week = data[o].TimeExpress.substring(4, 6);
            timeStr = year + "年第" + week + "周";
        }
        else {
            timeStr = data[o].TimeExpress;
        }

        table += "<td style='cursor:pointer;color:#72ACE3' onclick=getDetailReport('" + reportName + "','" + data[o].TimeExpress + "')>" + timeStr + "</td>";
        //table += "<td style='cursor:pointer;color:#72ACE3'>" + timeStr + "</td>";
    }
    var divshow = $("#introduce");
    divshow.text("点击表格上的月份可查看数据明细");
    table += "</tr>";
    table += "<tr>";
    table += "<td>百分比</td> ";
    for (var o in data) {
        table += "<td>" + data[o].Percent + "%</td>";
    }
    table += "</tr>";
    table += "<tr>";
    table += "<td> " + typename + "</td> ";
    for (var o in data) {
        table += "<td>" + data[o].CurrentCount + "</td>";
    }
    table += "</tr>";
    table += "<tr>";
    table += "<td>总数</td> ";
    for (var o in data) {
        table += "<td>" + data[o].Total + "</td>";
    }
    table += "</tr></table>";
    $("#report_table").html(table);
}

function ShowTable(data, typename, reportName) {
    var table = "<table class='basic-table'>";
    table += "<tr >";
    if (reportType == "M") {
        table += "<td >月份</td> ";
    }
    else {
        table += "<td >周数</td> ";
    }

    for (var o in data) {
        var timeStr;
        if (reportType == "W") {
            var year = data[o].TimeExpress.substring(0, 4);
            var week = data[o].TimeExpress.substring(4, 6);
            timeStr = year + "\r\n年第\r\n" + week + "周";
        }
        else {
            timeStr = data[o].TimeExpress;
        }
        //table += "<td style='cursor:pointer;color:#72ACE3' onclick=getDetailReport('" + reportName + "','" + data[o].TimeExpress + "')>" + timeStr + "</td>";
        table += "<td style='cursor:pointer;color:#72ACE3'>" + timeStr + "</td>";
    }

    var divshow = $("#introduce");
    divshow.text("点击表格上的月份可查看数据明细");

    table += "</tr>";
    table += "<tr>";
    table += "<td>" + typename + "</td> ";
    for (var o in data) {
        table += "<td>" + data[o].AvgValue + "</td>";
    }
    table += "</tr>";
    if (reportName == "Report6-5" || reportName == "Report6-6") {
        table += "<tr>";
        table += "<td> 中位数</td> ";
        for (var o in data) {
            table += "<td>" + data[o].MedianValue + "</td>";
        }
        table += "</tr>";
    } else {
        table += "<tr>";
        table += "<td  > 最大值</td> ";
        for (var o in data) {
            table += "<td>" + data[o].MaxValue + "</td>";
        }
        table += "</tr>";
        table += "<tr>";
        table += "<td  > 最小值</td> ";
        for (var o in data) {
            table += "<td>" + data[o].MinValue + "</td>";
        }
        table += "</tr>";
    }
    table += "<tr>";
    table += "<td  >符合条件数量</td> ";
    for (var o in data) {
        table += "<td>" + data[o].Total + "</td>";
    }
    table += "</tr></table>";
    $("#report_table").html(table);
}

function getDetailReport(reportName, selectedTime) {
    if (reportName == "Report6-1") {
        RequestPercentDetailReport("GetReportDetail", '是否评分', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-2") {
        RequestPercentDetailReport("GetReportDetail", '20分钟检查', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-3") {
        RequestPercentDetailReport("GetReportDetail", '30分钟诊断', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-4") {
        RequestPercentDetailReport("GetReportDetail", '3小时溶栓', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-5") {
        RequestDetailReport("GetReportDetail", 'DNT', selectedTime, reportName, 1);
    }
    //if (reportName == "Report6-6") {
    //    RequestPercentDetailReport("GetReportDetail", '深静脉血栓预防', selectedTime, reportName, 0);
    //}
    //if (reportName == "Report6-7") {
    //    RequestPercentDetailReport("GetReportDetail", '血管评价', selectedTime, reportName, 0);
    //}
    if (reportName == "Report6-8") {
        RequestPercentDetailReport("GetReportDetail", '他汀类用药', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-9") {
        RequestPercentDetailReport("GetReportDetail", '给予抗栓药物', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-10") {
        RequestPercentDetailReport("GetReportDetail", '给予降压药物', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-11") {
        RequestPercentDetailReport("GetReportDetail", '给予他汀类药物', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-12") {
        RequestPercentDetailReport("GetReportDetail", '给予降糖药物', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-13") {
        RequestPercentDetailReport("GetReportDetail", '给予抗凝药物', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-14") {
        RequestDetailReport("GetReportDetail", '住院日（日）', selectedTime, reportName, 1);
    }
    if (reportName == "Report6-15") {
        RequestDetailReport("GetReportDetail", '住院费用（元）', selectedTime, reportName, 1);
    }
    if (reportName == "Report6-16") {
        RequestDetailReport("GetReportDetail", '药物费用（元）', selectedTime, reportName, 1);
    }
    if (reportName == "Report6-17") {
        RequestPercentDetailReport("GetReportDetail", '是否死亡', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-18") {
        RequestDetailReport("GetReportDetail", '溶栓DNT（分钟）', selectedTime, reportName, 1);
    }
    if (reportName == "Report6-19") {
        RequestDetailReport("GetReportDetail", '溶栓OTT（分钟）', selectedTime, reportName, 1);
    }
    if (reportName == "Report6-20") {
        RequestPercentDetailReport("GetReportDetail", '是否终止溶栓', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-21") {
        RequestPercentDetailReport("GetReportDetail", '4.5小时溶栓', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-22") {
        RequestPercentDetailReport("GetReportDetail", 'DTN60分钟', selectedTime, reportName, 0);
    }
    if (reportName == "Report6-23") {
        RequestPercentDetailReport("GetReportDetail", '机械取栓', selectedTime, reportName, 0);
    }
    //if (reportName == "Report6-24") {
    //    RequestPercentDetailReport("GetReportDetail", '吞咽障碍评估', selectedTime, reportName, 0);
    //}
    //if (reportName == "Report6-25") {
    //    RequestPercentDetailReport("GetReportDetail", '双重强化抗血小板', selectedTime, reportName, 0);
    //}
    if (reportName == "Report6-26") {
        RequestPercentDetailReport("GetReportDetail", '抗凝治疗', selectedTime, reportName, 0);
    }
    //if (reportName == "Report6-27") {
    //    RequestPercentDetailReport("GetReportDetail", '康复评估', selectedTime, reportName, 0);
    //}
}

function RequestDetailReport(url, sColName, selectedTime, reportName, isShowChart) {
    $.ajax({
        type: "POST",
        url: "/Report/GetReportDetail",
        dataType: "json",
        data: {
            selectedTime: selectedTime, reportType: reportType, reportName: reportName, hospitalID: hospitalID
        },
        success: function (data) {
            ShowDetailTable(data[1].Value, sColName, selectedTime);

            // 判断是否需要显示详细图表
            //if (isShowChart) {
            //    ShowDetailChart(data[0].Value.categories, data[0].Value.series, "", "", "", "bar", "", reportName, hospitalName);
            //}
        }
    });
}

function RequestPercentDetailReport(url, sColName, selectedTime, reportName) {
    $.ajax({
        type: "POST",
        url: "/Report/GetReportDetail",
        dataType: "json",
        data: {
            selectedTime: selectedTime, reportType: reportType, reportName: reportName, hospitalID: hospitalID
        },
        success: function (data) {
            ShowPercentDetailTable(data[1].Value, sColName, selectedTime);
        }
    });
}

function ShowDetailTable(data, sColName, selectedTime) {
    var effectiveCount = 0;
    var invalidCount = 0;
    for (var item in data) {
        if (data[item].IsValid == 'FALSE') {
            invalidCount++;
        }
        else {
            effectiveCount++;
        }
    }
    var table = "<table class='basic-table'>";
    table += "<tr >";
    table += "<td  >" + selectedTime + "</td> ";
    if (effectiveCount > 0)
    { table += "<td  colspan='" + effectiveCount + "'>有效数据(<span style='color:red;'>点击患者姓名可以查看时间轴</span>)</td> "; }
    if (invalidCount > 0)
    { table += "<td style='color:red;font-weight:bold;'  colspan='" + invalidCount + "'>无效数据</td> "; }

    table += "</tr>";
    table += "<tr>";
    table += "<th  style='width: 15%;'>姓名</th> ";
    for (var o in data) {
        if (o >= effectiveCount)
        { table += "<th><a style='color:red;font-weight:bold;' href='javascript:void(0);'   onclick='showtimeline(this)'  data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></th>"; }
        else
        {
            table += "<th><a href='javascript:void(0);'   onclick='showtimeline(this)'  data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></th>";
        }
    }
    table += "</tr>";
    //table += "<tr>";
    //table += "<td  style='width: 15%;'>" + sColName + "</td> ";
    //for (var o in data) {
    //    table += "<td>" + (data[o].StartTime == null ? "" : data[o].StartTime) + "</td>";
    //}
    //table += "</tr>";
    //table += "<tr>";
    //table += "<td  style='width: 15%;'>" + eColName + "</td> ";
    //for (var o in data) {
    //    table += "<td>" + (data[o].EndTime == null ? "" : data[o].EndTime) + "</td>";
    //}
    //table += "</tr>";
    table += "<tr>";
    table += "<td   style='width: 15%;'>" + sColName + "</td> ";
    for (var o in data) {
        table += "<td >" + (data[o].IsValid == 'TRUE' ? data[o].Result : "") + "</td>";
    }
    table += "</tr>";
    table += "</table > ";
    $("#detail_table").html(table);
}

function ShowPercentDetailTable(data, sColName, selectedTime) {
    var effectiveCount = 0;
    var invalidCount = 0;
    for (var item in data) {
        if (data[item].IsValid == 'FALSE') {
            invalidCount++;
        }
        else {
            effectiveCount++;
        }
    }
    var table = "<table class='basic-table'>";
    table += "<tr >";
    table += "<td  >" + selectedTime + "</td> ";
    if (effectiveCount > 0)
    { table += "<td  colspan='" + effectiveCount + "'>有效数据</td> "; }
    if (invalidCount > 0)
    {
        table += "<td style='color:red;font-weight:bold;' colspan='" + invalidCount + "'>无效数据</td> ";
    }

    table += "</tr>";
    table += "<tr>";
    table += "<th  style='width: 15%;'>姓名</td> ";
    for (var o in data) {
        if (o >= effectiveCount)
        { table += "<th><a style='color:red;font-weight:bold;' href='javascript:void(0);'  onclick='showtimeline(this)'  data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></th>"; }
        else { table += "<th><a href='javascript:void(0);'  onclick='showtimeline(this)'  data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></th>"; }
        //if (o >= effectiveCount)
        //{ table += "<td><a style='color:red;font-weight:bold;' href='javascript:void(0);'   data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></td>"; }
        //else { table += "<td><a href='javascript:void(0);'  data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></td>"; }
    }
    table += "</tr>";
    table += "<tr>";
    table += "<td  style='width: 15%;'>" + sColName + "</td> ";
    for (var o in data) {
        table += "<td >" + (data[o].Result != null ? data[o].Result : "") + "</td>";
    }

    table += "</tr></table>";
    $("#detail_table").html(table);
}

function showtimeline(ct) {
    var id = $(ct).data("registerid");
    $.post('/DataManage/GetTimeline', { id: id }, function (str) {
        layer.open({
            type: 1,
            title: '时间轴',
            maxmin: false,
            shadeClose: false, //点击遮罩关闭层
            area: ['1000px', '480px'],
            content: str,
        });
    });

    //var win = $("#_timeline_window").window({
    //    href: '/CPC/CPCTimeLine/TimeLineView?RegistId=' + id,
    //    title: '胸痛急救时间轴',
    //    closed: true,
    //    minimizable: false,
    //    collapsible: false,
    //    maximizable: false,
    //    resizable: false,
    //    draggable: false,
    //    width: 1000,
    //    height: 500,
    //    cache: false,
    //    modal: true,
    //    shadow: true,
    //});
    //win.window('open');
}