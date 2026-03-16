$(function () {
    // 初始化
    window.returnItem = "";

    // 加载选项
    $.ajax({
        type: 'get',
        url: AddUrlPrefix('/FormItem/InitOptions'),
        data: { id: $("#Item_ID").val(), type: $("#Item_Type").val() },
        cache: false,
        dataType: 'html',
        async: false,
        success: function (data) {
            $("#ItemOptions").html(data);
        },
        error: function () {
            $.messager.alert("错误", "请求失败！", "error");
        }
    });

    // 新建Item
    $("#btn_item_save").click(function (e) {
        // alert($("#_content_item_form").validate().form());
        var item = new Object();
        item.ID = $("#Item_ID").val();
        item.KeyCode = $("#Item_KeyCode").val();
        item.Name = $("#Item_Name").val();
        item.Type = $("#Item_Type").val();
        item.Title = $("#Item_Title").val();
        item.Category = $("input[name='itemCategory']").val();
        item.Property = GetProperty($("#ItemOptions"),$("input[name='Item.Type']").val());
        // 取得保存方向
        var foward = $(this).attr("data-foward");
        var _url = foward == "temporary" ? AddUrlPrefix("/FormItem/SaveFormItemTemporary")
            : AddUrlPrefix("/CRFTemplate/SaveFormItem");

        $.ajax({
            type: 'POST',
            url: _url,
            data: { type: item.Type, item: JSON.stringify(item)},
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {
                        item.DefaultValue = item.Property.DefaultValue;
                        window.returnItem = item;
                        $("#divEditItem").dialog("close");
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });

        
    });

    $("#btn_item_cancel").click(function (e) {
        window.returnItem = "";
        $("#divEditItem").dialog("close");
    });
})