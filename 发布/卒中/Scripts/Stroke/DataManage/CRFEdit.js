var CRFId;  //CRF业务表单ID
var PatientID; //患者ID
var ItemValue = new Array();  //记录元素的选项
var ErrorArray = [];  //报表填报错误信息

$(function () {
    CRFId = $("#ID").val();
    PatientID = $("input[name='patientID']").val();

    $("a[name = 'Category']").click(function (e) {
        // 取得并保存前分类下的Items答案
        var prevCategory = $("#tab_list").find(".active").attr("data-id");
        //分类切换
        if (prevCategory == $(this).attr("data-id")) {
            return;
        } else {
            SetCategoryItemsValue();  //将当前页面的元素键值对保存在ItemValue中
        }
        // 样式切换
        $("a[name = 'Category']").removeClass("active");
        $("a[name = 'Category'][data-id = '" + $(this).attr("data-id") + "']").addClass("active");
        //拼接页面元素html并展现
        InitFormItems($(this).attr("data-id"));
        //显示页面元素填报的错误信息
        showItemError();
    });

    //保存业务表单（保存为草稿）
    $("#btn_save").click(function () {
        save(0);
    });

    //保存业务表单（并提交审核）
    $("#btn_saveToAudit").click(function () {
        save(1);
    });

    // 审核通过
    $("#btn_agree").click(function (e) {
        audit(2);
    });


    // 审核拒绝
    $("#btn_refuse").click(function (e) {
        audit(3);
    });

    function save(processStatus)
    {
        SetCategoryItemsValue();

        $.ajax({
            type: 'POST',
            url: AddUrlPrefix('/DataManage/SaveCRF'),
            data: { crfId: CRFId, patientID:PatientID, itemValue: JSON.stringify(ItemValue), status: processStatus },
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {
                        ErrorArray = [];
                    } else {
                        ErrorArray = json.Data;
                    }
                    showError();
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    function audit(processStatus) {
        // 通信协议
        $.ajax({
            type: 'POST',
            url: AddUrlPrefix('/DataManage/AuditCRF'),
            data: { patientID: PatientID, status: processStatus },
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {
                        window.location.href = "CRFEdit?patientID=" + PatientID;
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    // 初始化
    if ($("a[name = 'Category']").length > 0)
        $("a[name = 'Category']")[0].click();

});

//根据目录ID从表单元素库中获取元素并展现
function InitFormItems(categoryCode) {
    $.ajax({
        type: 'get',
        url: AddUrlPrefix('/DataManage/GetItemsByCRFIdAndCategoryCode'),
        data: { crfId: CRFId, categoryCode: categoryCode },
        cache: false,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.IsSuccess) {
                var html = "";
                if (data.Data.length == 0) {
                    $("#_formItems").html("");
                    return;
                }
                $.each(data.Data, function () {
                    html += ItemHtml(this);
                });
                $("#_formItems").html(html);
            }
        },
        error: function () {
            $.messager.alert("错误", "请求失败！", "error");
        }
    });
}

//生成元素HTML
function ItemHtml(item) {
    var _html = "<div class='div_item topic_block'>";
    _html += "<input type='hidden' name='Item_ID' value='" + item.ID + "' itemType='" + item.Type + "'>";
    _html += "  <div class='tit_bar line_b'>" + item.Title + "<span class='error' style='color:red;margin-left:20px;display:none;'>(该项填写不符合要求)</span></div>";
    //_html += " <p class='error' style='color:red;display:none'></p> ";
    // 选项内容
    _html += "<div class='div_item_option'>" + CreateOptionHtml(item) + "</div>";
    _html += "</div>";
    return _html;
}


//设置目录下元素答案（当目录切换或保持答案时）
function SetCategoryItemsValue() {
    var ss = $("#_formItems").children(".div_item");
    $("#_formItems").children(".div_item").each(function (index, element) {
        var itemInfo = $(element).children("[name = 'Item_ID']").first();
        var id = $(itemInfo).val();
        var itemType = $(itemInfo).attr("itemType");
        var value = "";

        var div_item_option = $(element).children(".div_item_option").first();
        switch (itemType) {
            case "01":
                value = $(div_item_option).find(":input").first().val();
                break;
            case "02":
                value = $(div_item_option).find(":input").first().val();
                break;
            case "03":
                $(div_item_option).find(":checked").each(function (ind, ele) {
                    value += $(ele).attr("data-val") + ";";
                });
                if (value.endsWith(";")) {
                    value = value.substr(0, value.length - 1);
                }
                break;
            case "04":
                var name = "Radio" + id;
                value = $(div_item_option).find(':checked').attr("data-val");
                break;
            case "05":
                value = $(div_item_option).find("select").val();
                break;
            case "06":
                value = $(div_item_option).find("textarea").val();
                break;
        }
        var item_old = "";
        $.each(ItemValue, function (index, item) {
            if (item.Key == id) {
                item_old = item;
                return false;
            }
        });
        if (item_old != "") {
            var i = ItemValue.indexOf(item_old);
            if (i > -1) {
                ItemValue.splice(i, 1);
            }
        }
        ItemValue.push({ Key: id, Value: value });

    });
    
}

//生产元素选项HTML
function CreateOptionHtml(item) {
    var _html = "";
    var type = item.Type;
    var value = item.Value;
    //如果ItemValue中包含该元素的值，则以ItemValue为准
    for (var i = 0; i < ItemValue.length; i++) {
        if (ItemValue[i].Key == item.ID) {
            value = ItemValue[i].Value;
            break;
        }
    }

    if (item.Property != undefined) {
        switch (type) {
            case "01":
                _html += "<p class='option'><input type='text' value='" + value + "'></p>";
                break;
            case "02":
                _html += "<p class='option'><input type='text' class='Wdate fixWidth' value='" + value + "' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})\"></p>";
                break;
            case "03":
                if (item.Property.EnumItems != undefined &&
                    item.Property.EnumItems.length > 0) {
                    var defaultValue = [];
                    if (value != null && $.trim(value) != "" && typeof value != "undefined") {
                        defaultValue = value.split(";");
                    }
                    $.each(item.Property.EnumItems, function () {
                        var _checked = "";
                        var _value = this.Value;
                        $.each(defaultValue, function () {
                            if (this == _value)
                                _checked = "checked";
                        });
                        _html += "<p class='option'><input type='checkbox' name='" + "Check" + item.ID + "' " + _checked + " data-val='" + this.Value + "' data-text='" + this.Text + "' data-score='" + this.ScoreValue + "'><span>" + this.Text + "</span></p>";
                    });
                }
                break;
            case "04":
                if (item.Property.EnumItems != undefined &&
                    item.Property.EnumItems.length > 0) {
                    $.each(item.Property.EnumItems, function () {
                        var _checked = value == this.Value ? "checked" : "";
                        _html += "<p class='option'><input type='radio' name='" + "Radio" + item.ID + "' " + _checked + " data-val='" + this.Value + "' data-text='" + this.Text + "' data-score='" + this.ScoreValue + "'><span>" + this.Text + "</span></p>";
                    });
                }
                break;
            case "05":
                _html += " <select class='easyui-combobox' style='width:400px;'>";
                if (item.Property.EnumItems != undefined &&
                   item.Property.EnumItems.length > 0) {
                    $.each(item.Property.EnumItems, function () {
                        var _selected = value == this.Value ? "selected" : "";
                        _html += "<option value='" + this.Value + "' data-score='" + this.ScoreValue + "' " + _selected + ">" + this.Text + "</option>";
                    });
                }
                _html += "</select> ";
                break;
            case "06":
                _html += "<p class='option' style='height:135px;'><textArea style='width:97%'>" + value + "</textArea></p>";
                break;
        }
    }
    return _html;
}

//显示填报的错误信息
function showError() {
    showCategoryError();
    showItemError();
}

//显示填报目录错误信息
function showCategoryError() {
    //对出错的目录标红色星花
    $("a[name = 'Category']").each(function () {
        var categoryId = $(this).attr("data-id");
        var flag = false;
        for (var i = 0; i < ErrorArray.length; i++) {
            if (categoryId == ErrorArray[i].Key) {
                flag = true;
                break;
            }
        }
        if (flag == true) {
            $(this).find(".categoryError").show();
        } else {
            $(this).find(".categoryError").hide();
        }
    });
}

//在当前目录的元素上显示错误信息
function showItemError() {
    var prevCategory = $("#tab_list").find(".active").attr("data-id");
    var itemErrors = [];
    //查找当前目录下的错误消息
    for (var i = 0; i < ErrorArray.length; i++) {
        if (ErrorArray[i].Key == prevCategory) {
            itemErrors = ErrorArray[i].Value;
            break;
        }
    }
    //显示错误消息
    $("#_formItems").children().each(function () {
        var itemId = $(this).children("input[name='Item_ID']").val();
        var message = "";
        for (var i = 0; i < itemErrors.length; i++) {
            if (itemErrors[i].Key == itemId) {
                message = itemErrors[i].Value;
                if (message == "") {
                    message = "该项填写不符合要求";
                }
                break;
            }
        }
        //var errorItem = $(this).children("p.error").first();
        var errorItem = $(this).find("span.error").first();
        if (message != "") {
            $(errorItem).show();
        } else {
            $(errorItem).hide();
        }
        $(errorItem).text(message);
    });
}

