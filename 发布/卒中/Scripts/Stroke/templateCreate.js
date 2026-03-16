$().ready(function () {

    //关闭
    $("#close").click(function () {
        $('#divPanel1').dialog('close');
    });

    //制作方法改变
    $("input[name='create_crf']").change(function () {
        var radioValue = $("input[name='create_crf']:checked").val();
        if (radioValue == "new") {
            $("#preview").hide();
            $("#uploadFile").hide();
        } else if (radioValue == "import") {
            $("#preview").show();
            $("#uploadFile").show();
        }
    });

    //用户提交
    $("#submit").click(function () {
        $('#divPanel1').dialog('close');
        var radioValue = $("input[name='create_crf']:checked").val();
        if (radioValue == "new") {
            window.location = AddUrlPrefix("/CRFTemplate/Add");
        }
    });
    
});

//新建模板
function addNew(){
    window.location = AddUrlPrefix("/CRFTemplate/Add");
}
