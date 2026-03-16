$().ready(function () {
    
    //添加CRF分类
    $("#addCatagory").click(function () {
        //移除列表选中样式
        $("#catagory_table_body").children("tr").removeClass("active");

        //输入框元素内容设为空
        $("#category_id").val("");
        $("#category_code").val("");
        $("#category_name").val("");
        $("#category_description").val("");
    });

    //保存CRF分类
    $("#saveCatagory").click(function () {
        var id = $("#category_id").val();
        var code = $("#category_code").val();
        var name = $("#category_name").val();
        var description = $("#category_description").val();

        if (code.length > 10) {
            $.messager.alert("错误", "编号长度超出10个字符！", "error");
            return;
        } else if (code == "") {
            $.messager.alert("错误", "编号不能为空！", "error");
            return;
        }
        if (name.length > 32) {
            $.messager.alert("错误", "名称长度超出32个字符！", "error");
            return;
        } else if (name == "") {
            $.messager.alert("错误", "名称不能为空！", "error");
            return;
        }

        if (description.length > 32) {
            $.messager.alert("错误", "描述长度超出250个字符！", "error");
            return;
        }

        $.ajax({
            type: 'POST',
            url: AddUrlPrefix('/Setting/SaveCategory'),
            data: { id: id, code: code, name: name, description: description },
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {
                        //window.history.back();
                        location.reload();
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });

    });

    //删除分类
    $("#deleteCatagory").click(function () {
        var element = $("#catagory_table_body").children("tr.active:first");
        if (element.length == 0){
            $.messager.alert("错误", "请选择要删除的行！", "error");
            return;
        }

        var id = $(element[0]).attr("data-id");

        $.messager.confirm("提示", "确实需要删除吗？", function (r) {
            if (r) {
                $.ajax({
                    type: 'POST',
                    url: AddUrlPrefix('/Setting/DeleteCategory'),
                    data: { id: id },
                    dataType: 'json',
                    success: function (json) {
                        $.messager.alert(json.Title, json.Message, json.Tip, function () {
                            if (json.IsSuccess) {
                                //window.history.back();
                                location.reload();
                            }
                        });
                    },
                    error: function () {
                        $.messager.alert("错误", "请求失败！", "error");
                    }
                });
            }
        });
    });

});


//CRF分类列表点击事件
function selectCategory(dom) {
    var tr = $(dom);
    var isSelected = tr.hasClass("active");
    if (isSelected) {
        return;
    }
    tr.siblings(".active").removeClass("active");
    
    tr.addClass("active");

    $("#category_id").val(tr.attr("data-id"));
    $("#category_code").val(tr.attr("data-code"));
    $("#category_name").val(tr.attr("data-name"));
    $("#category_description").val(tr.attr("data-description"));
}