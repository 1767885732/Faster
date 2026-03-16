function GetRules(obj, itemID) {
    var ValidateRules = new Array();
    // 新增Item时获取单个选项
    $("#ValidateRules").find(".rule").each(function () {
        var item = new Object();
        item.Name = $(this).find("input[name='Rule.Name']").val();
        item.Type = $(this).find("input[name='Rule.Type']").val();
        item.Message = $(this).find("input[name='Rule.Message']").val();
        switch(item.Type){
            case "Required":
                item.Required = $("input[name='is_required']:checked").val();
                break;
            default:
                break;
        }
        
        ValidateRules.push(item);
    });

    if (ValidateRules.length > 0)
        return ValidateRules;

    // 模板编辑页面，获取Item列表中单个选项
    $(obj).find(".rule").each(function () {
        var item = new Object();
        item.Name = $(this).find("input[name='Rule_Name']").val();
        item.Type = $(this).find("input[name='Rule_Type']").val();
        item.Message = $(this).find("input[name='Rule_Message']").val();
        switch (item.Type) {
            case "Required":
                item.Required = $("input[name='is_required_" + itemID + "']:checked").val();
                break;
            default:
                break;
        }

        ValidateRules.push(item);
    });

    return ValidateRules;
}

function GetProperty(obj,type) {
    var property = new Object();
    
    property.DefaultValue = new Array();
    var itemID = "";

    // 分数
    if ($(".topic_add_block").length > 0) {
        property.ScoreEnabled = $("#chkScore").prop("checked");
        property.ScoreValue = $("input[name='txtScore']").eq(0).val();
    }
    else {
        property.ScoreEnabled = obj.find("input[name='hidScoreEnabled']").val();
        property.ScoreValue = obj.find("input[name='hidScoreValue']").val();
    }
    switch (type)
    {
        case "01":
            if ($(".topic_add_block").length > 0) {
                property.DefaultValue.push($(".topic_add_block input").eq(2).val());
            }
            else {
                property.DefaultValue.push($(obj).find(".option").find("input").val());
                itemID = $(obj).find("input[name='Item_ID']").val();
            }
            break;
        case "02":
            if ($(".topic_add_block").length > 0) {
                property.DefaultValue.push($(".topic_add_block input").eq(2).val());
                property.Format = "yyyy-MM-dd HH:mm:ss";
            }
            else {
                property.DefaultValue.push($(obj).find(".option").find("input").val());
                itemID = $(obj).find("input[name='Item_ID']").val();
                property.Format = "yyyy-MM-dd HH:mm:ss";
            }
            break;
        case "03":
        case "04":
            property.EnumItems = new Array();

            // 新增Item时获取单个选项
            $(".topic_add_block").find(".option").each(function (index) {
                if ($(this).hasClass("default")) {
                    property.DefaultValue.push(index + 1);
                }

                var item = new Object();
                item.Value = index + 1;
                item.Text = $(this).find("input").eq(0).val();
                if (property.ScoreEnabled)
                {
                    item.ScoreValue = $(this).find("input").eq(1).val();
                }
                property.EnumItems.push(item);
            });

            if (property.EnumItems.length > 0)
                break;

            // 模板编辑页面，获取Item列表中单个选项
            $(obj).find(".option").each(function (index) {
                if ($(this).find("input:checked").length > 0) {
                    property.DefaultValue.push(index + 1);
                }

                var item = new Object();
                item.Value = $(this).find("input").attr("data-val");
                item.Text = $(this).find("input").attr("data-text");

                if (property.ScoreEnabled) {
                    item.ScoreValue = $(this).find("input").attr("data-score");
                }

                property.EnumItems.push(item);

                itemID = $(obj).find("input[name='Item_ID']").val();
            });
            break;
        case "05":
            property.EnumItems = new Array();

            // 新增Item时获取单个选项
            $(".topic_add_block").find(".option").each(function (index) {
                if ($(this).hasClass("default")) {
                    property.DefaultValue.push(index + 1);
                }

                var item = new Object();
                item.Value = index + 1;
                item.Text = $(this).find("input").eq(0).val();

                if (property.ScoreEnabled) {
                    item.ScoreValue = $(this).find("input").eq(1).val();
                }

                property.EnumItems.push(item);
            });

            if (property.EnumItems.length > 0)
                break;

            // 模板编辑页面，获取Item列表中单个选项
            property.DefaultValue.push($(obj).find("option:selected").val() + 1);
            $(obj).find("select option").each(function (index) {
                var item = new Object();
                item.Value = $(this).val();
                item.Text = $(this).text();

                if (property.ScoreEnabled) {
                    item.ScoreValue = $(this).attr("data-score");
                }

                property.EnumItems.push(item);
                itemID = $(obj).find("input[name='Item_ID']").val();
            });
            break;
        case "06":
            if ($(".topic_add_block").length > 0) {
                property.DefaultValue.push($(".topic_add_block textarea").val());
            }
            else {
                property.DefaultValue.push($(obj).find(".option").find("textarea").val());
                itemID = $(obj).find("input[name='Item_ID']").val();
            }
            break;
        case "09":
            if ($(".topic_add_block").length > 0) {
                property.DefaultValue.push($(".topic_add_block input").eq(2).val());
                property.HasUnit = $("#unit").prop("checked");
                if (property.HasUnit)
                {
                    property.UnitItems = new Array();
                    $(".topic_add_block #unitContent").find(".option").each(function (index) {
                        if ($(this).hasClass("default")) {
                            property.DefaultValue.push(index + 1);
                        }

                        var item = new Object();
                        item.Value = index + 1;
                        item.Text = $(this).find("input").eq(0).val();

                        property.UnitItems.push(item);
                    });
                }
            }
            else
            {
                property.DefaultValue.push($(obj).find(".option").find("input").val());
                // 有默认值的情况
                if ($(obj).find(".option select").length > 0) {
                    property.HasUnit = true;
                    property.UnitItems = new Array();
                    property.DefaultValue.push($(obj).find("option:selected").val() + 1);
                    $(obj).find("select option").each(function (index) {
                        var item = new Object();
                        item.Value = $(this).val();
                        item.Text = $(this).text();
                        property.UnitItems.push(item);
                        itemID = $(obj).find("input[name='Item_ID']").val();
                    });
                
                }
            }
        default:
            break;
    }

    // 校验规则
    property.ValidateRules = GetRules(obj, itemID);

    return property;
}

function CreateOptionHtml(item)
{
    var _html = "<input name='hidScoreEnabled' type='hidden' value='" + item.Property.ScoreEnabled + "'>";
    _html += "<input name='hidScoreValue' type='hidden' value='" + item.Property.ScoreValue + "'>";
    var type = item.Type;
    
    if (item.Property != undefined)
    {
        switch (type) {
            case "01":
                _html += "<p class='option'><input type='text' value='" + item.DefaultValue + "'></p>";
                break;
            case "02":
                _html += "<p class='option'><input type='text' class='Wdate fixWidth' value='" + item.DefaultValue + "' onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})\"></p>";
                break;
            case "03":
                if (item.Property.EnumItems != undefined &&
                    item.Property.EnumItems.length > 0) {
                    var defaultValue = item.DefaultValue;
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
                    var defaultValue = item.DefaultValue;
                    $.each(item.Property.EnumItems, function () {
                        var _checked = defaultValue[0] == this.Value ? "checked" : "";
                        _html += "<p class='option'><input type='radio' name='" + "Radio" + item.ID + "' " + _checked + " data-val='" + this.Value + "' data-text='" + this.Text + "' data-score='" + this.ScoreValue + "'><span>" + this.Text + "</span></p>";
                    });
                }
                break;
            case "05":
                _html += " <select class='easyui-combobox' style='width:400px;'>";
                if (item.Property.EnumItems != undefined &&
                   item.Property.EnumItems.length > 0) {
                    var defaultValue = item.DefaultValue;
                    $.each(item.Property.EnumItems, function () {
                        var _selected = defaultValue[0] == this.Value ? "selected" : "";
                        _html += "<option value='" + this.Value + "' data-score='" + this.ScoreValue + "' " + _selected + ">" + this.Text + "</option>";
                    });
                }
                _html += "</select> ";
                break;
            case "06":
                _html += "<p class='option' style='height:135px;'><textArea style='width:97%'>" + item.DefaultValue + "</textArea></p>";
                break;
            case "09":
                _html += "";
                _html += "<div style='margin:5px;padding-top:5px;' class='option'><input type='text' class='easyui-numberbox' value='" + item.DefaultValue[0] + "' style='width:400px;'>";
                if (item.Property.HasUnit){
                    _html += " <select class='easyui-combobox' style='width:100px;'>";
                    if (item.Property.UnitItems != undefined &&
                       item.Property.UnitItems.length > 0) {
                        var defaultValue = item.DefaultValue[1];
                        $.each(item.Property.UnitItems, function () {
                            var _selected = defaultValue[0] == this.Value ? "selected" : "";
                            _html += "<option value='" + this.Value + "' " + _selected + ">" + this.Text + "</option>";
                        });
                    }
                    _html += "</select> ";
                }
                _html += "    </div>";
                break;
        }   
    }
    return _html;
}

function CreateRuleHtml(type,item,rule)
{
    var _html = "";
    switch (type) {
        // 必填项校验
        case "Required":
            _html += "<label>是否必填</label>";
            _html += ("<input class='mg_l_10' type='radio' name='is_required_" + item.ID + "' value='1' " + (rule.Required || rule.Required == "1" ? "checked" : "") + ">");
            _html += "<span>是</span>";
            _html += ("<input  class='mg_l_10' type='radio' name='is_required_" + item.ID + "' value='0' " + ((!rule.Required || rule.Required == "0") ? "checked" : "") + ">");
            _html += "<span>否</span>";
            break;
    }

    return _html;
}