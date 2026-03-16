$().ready(function () {

    window.returnItemTypeValue = "";
    //提交
    $("#submitItemType").click(function () {
        var typeCode = $("#selection_frame_block ul li a.active").attr("codeattr");
        window.returnItemTypeValue = typeCode;
        $('#divPanel1').dialog('close');
    });

    //关闭
    $("#close").click(function () {
        window.returnItemTypeValue = "";
        $('#divPanel1').dialog('close');
    });

});

//选择改变
function selectChange(element) {
    if (!$(element).hasClass("active")) {
        $("#selection_frame_block ul li a.active").removeClass("active");
        $(element).addClass("active");
    }
}