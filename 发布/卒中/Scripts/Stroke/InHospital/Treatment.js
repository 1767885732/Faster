$(function () {
    $("#tt").tabs();

    addTab("并发症及住院期间检查", "Examination");
    addTab("溶栓后NIHSS评分及血压监测", "BloodPressureDetection");
    addTab("蛛网膜下腔出血", "SubarachnoidHemorrhage");
    addTab("出院信息", "Outcome");

    // 选中第一个tab
    $('#tt').tabs('select', "并发症及住院期间检查");


})

function addTab(title, action) {
    $('#tt').tabs('add', {
        title: title,
        href: "/Inhospital/" + action + "?registId=" + $("#registId").val(),
        closable: false
    });
}

function refreshTab(action) {
    var tab = $('#tt').tabs('getSelected');
    $("#tt").tabs('update', {
        tab: tab,
        options: {
            href: "/Inhospital/" + action + "?registId=" + $("#registId").val(),
            closable: false
        }
    });
    tab.panel('refresh');
}