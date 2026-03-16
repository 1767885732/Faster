
$(function () {

    // 页面值存储对象
    function PageModel() {
        this.categoryArray = new Array();
    }

    PageModel.prototype.getCategoryItems = function (category) {
        var obj = "";
        $.each(this.categoryArray, function (index, categoryModel) {
            if (categoryModel.category == category)
                obj = categoryModel.items;
        });
        return obj;
    }

    PageModel.prototype.setCategoryItems = function (src) {
        // 如果存在，先移除该分类信息
        var _index = -1;
        $.each(this.categoryArray, function (index, categoryModel) {
            if (categoryModel.category == src.category)
                _index = index;
        });
        if (_index != -1)
            this.categoryArray.splice(_index, 1);
        this.categoryArray.push(src);
    }

    function CategoryModel(category, items, isSuccess) {
        this.category = category;
        this.items = items;
        this.isSuccess = isSuccess;
    }

    // 页面逻辑开始
    var PageModel = new PageModel();
    $(document).on('click', "a[name = 'Category']", function (e) {
        if ($(this).hasClass("active")) return;

        // 取得并保存前分类下的Items
        var prevCategory = $("#tab_list").find(".active").attr("data-id");
        if (prevCategory != undefined)
            PageModel.setCategoryItems(new CategoryModel(prevCategory, GetFormItems(prevCategory), $("#isLoadSuccess").val()));

        // 样式切换
        $("a[name = 'Category']").removeClass("active");
        $("a[name = 'Category'][data-id = '" + $(this).attr("data-id") + "']").addClass("active");

        // 取得并显示新分类下的Items
        var items = PageModel.getCategoryItems($(this).attr("data-id"));
        if (items == "")
            InitFormItems($(this).attr("data-id"));
        else
        {
            var html = "";
            $.each(items, function () {
                html += new FormItem(this).toHtml();
            });
            if (html == "") {
                $("#_formItems").hide();
                $("#div_noFormItem").show();
            }
            else {
                $("#_formItems").html(html);
                $(".easyui-numberbox").numberbox();
                $("#_formItems").show();
                $("#div_noFormItem").hide();
                ReloadBtn();
            } 
        }
    });

    function GetFormItems(category)
    {
        var items = new Array();
        $.each($(".topic_block"), function () {
            var item = new Object();
            item.ID = $(this).find("input[name='Item_ID']").val();
            item.KeyCode = $(this).find("input[name='Item_KeyCode']").val();
            item.Name = $(this).find("input[name='Item_Name']").val();
            item.Type = $(this).find("input[name='Item_Type']").val();
            item.Title = $(this).find("input[name='Item_Title']").val();
            item.Category = category;
            item.Property = GetProperty($(this), $(this).find("input[name='Item_Type']").val());
            item.DefaultValue = item.Property.DefaultValue;
            items.push(item);
        });
        return items;
    }

    function InitFormItems(CategoryId) {
        $("#div_noCategory").hide();
        $("#div_noFormItem").hide();
        var _tmpId = $("#ID").val();
        $.ajax({
            type: 'get',
            url: AddUrlPrefix('/FormItem/InitFormItems'),
            data: { TmpId: _tmpId, CategoryId: CategoryId },
            cache: false,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.IsSuccess) {
                    $("#isLoadSuccess").val(1);
                    var html = "";
                    if (data.Data.length == 0)
                    {
                        $("#_formItems").html("");
                        $("#div_noFormItem").show();
                        return;
                    }
                    $.each(data.Data, function () {
                        html += new FormItem(this).toHtml();
                    });
                    $("#_formItems").html(html);
                    $(".easyui-numberbox").numberbox();
                    ReloadBtn();
                }
            },
            error: function () {
                $("#isLoadSuccess").val(0);
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    };

    // 删除分类
    $(document).on('click', ".remove_sign", function () {
        var item = $(this).parent();
        $.messager.confirm("确认", "确定要移除该分类吗？", function (data)
        {
            if (data)
            {
                // 激活状态下，删除分类时需要判断下一个被激活的分类
                if (item.hasClass("active")) {
                    if ($("a[name = 'Category']").length == 1) {
                        $("#_formItems").hide();
                        $("#div_noFormItem").hide();
                        $("#div_noCategory").show();
                    }
                    else {
                        if (item.attr("data-id") == $("a[name = 'Category']").first().attr("data-id")) {
                            item.next().click();
                        }
                        else {
                            $(this).parent().prev().click();
                        }
                    }
                }
                item.remove();
            }    
        });
        return false;
    });

    // 保存成草稿
    $("#btn_save").click(function (e) {
        save(0);
    });

    // 保存并提交审核
    $("#btn_saveToAudit").click(function (e) {
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
        // 保存前，更新页面缓存数据
        var prevCategory = $("#tab_list").find(".active").attr("data-id");
        PageModel.setCategoryItems(new CategoryModel(prevCategory, GetFormItems(prevCategory)));

        // 表头信息
        var _temp = new Object();
        _temp.ID = $("#ID").val();
        _temp.Name = $("#Name").val();
        _temp.Title = $("#Title").val();
        _temp.EffectiveDate = $("#EffectiveDate").val();
        _temp.InvalidDate = $("#InvalidDate").val();
        _temp.Status = processStatus;
        // 分类信息
        var categories = [];
        $("a[name='Category']").each(function (index) {
            var category = new Object();
            category.Name = $(this).attr("data-name");
            category.Code = $(this).attr("data-id");
            categories[index] = category;
        });
        // 通信协议
        $.ajax({
            type: 'POST',
            url: AddUrlPrefix('/CRFTemplate/SaveTemplate'),
            data: { temp: _temp, items: JSON.stringify(PageModel), categories: JSON.stringify(categories) },
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {
                        window.location.href = "Edit?id=" + _temp.ID;
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    function audit(processStatus)
    {
        var _temp = new Object();
        _temp.ID = $("#ID").val();
        _temp.Status = processStatus;

        // 通信协议
        $.ajax({
            type: 'POST',
            url: AddUrlPrefix('/CRFTemplate/AuditTemplate'),
            data: { temp: _temp },
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {
                        window.location.href = "Edit?id=" + _temp.ID;
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    $("#btn_close").click(function (e) {
        $.messager.confirm("确认", "请确认是否关闭", function (data) {
            if (data) {
                window.history.back();
            }
        });
    });

    // 初始化
    if ($("a[name = 'Category']").length > 0)
        $("a[name = 'Category']").eq(0).click();

    // 页面初次加载时，如果没有分类信息
    if ($("a[name='Category']").length == 0) {
        $("#div_noCategory").show();
    }

    $("#addCatagory").click(function () {
        selectCategories();
    });
});

//新建元素
function createItem(btn) {
    $("<div id='divPanel1'></div>").dialog({
        title: '新建',
        width: 820,
        height: 568,
        href: AddUrlPrefix('/FormItem/SeleteItemType/'),
        modal: true,
        onClose: function () {
            var itemTypeCode = window.returnItemTypeValue;
            var category = $("a[name='Category'].active").attr("data-id");
            // 点击确认的情况下，弹出新增页面
            var _height = (itemTypeCode == "01" || itemTypeCode == "02" || itemTypeCode == "05") ? 480 : 600;
            if (itemTypeCode != ""){
                $("<div id='divEditItem'></div>").dialog({
                    title: '新建',
                    width: 950,
                    height: _height,
                    href: AddUrlPrefix('/FormItem/Add?type=' + itemTypeCode + '&category=' + category),
                    modal: true,
                    onClose: function () {
                        if (window.returnItem != "") {
                            var _item = new FormItem(window.returnItem);
                            // 第一次追加表单项
                            if ($("#_formItems").children().length == 0){
                                $("#_formItems").html(_item.toHtml());
                                $("#div_noFormItem").hide();
                            }  
                            else
                                $(btn).parent().parent().after(_item.toHtml());
                            ReloadBtn();
                        }
                        $(this).remove();
                    }
                });
            }
            $(this).remove();
        }
    });
}

// 编辑元素
function editItem(btn) {
    var element = $(btn).parent().parent();
    var keyCode = element.find("input[name='Item_KeyCode']").val();

    // 通信协议
    $.ajax({
        type: 'GET',
        url: AddUrlPrefix('/CRFTemplate/?keyCode=' + keyCode),
        dataType: 'json',
        success: function (json) {
            if (!json.IsSuccess) {
                $.messager.alert(json.Title, json.Message, json.Tip);
            }
        },
        error: function () {
            $.messager.alert("错误", "请求失败！", "error");
        }
    });


    var type = element.find("input[name='Item_Type']").val();

    var height = type == "01" ? 500 : 600;

    // 获取当前元素
    $("<div id='divEditItem'></div>").dialog({
        title: '编辑',
        width: 950,
        height: height,
        href: AddUrlPrefix('/FormItem/EditByKeyCode?keyCode=' + keyCode),
        modal: true,
        onClose: function () {
            if (window.returnItem != "") {
                element.before(new FormItem(window.returnItem).toHtml());
                element.remove();
            }
            $(this).remove();
        }
    });
}

//选择类别
function selectCategories() {
    //当前模板已经选择的元素目录
    var templateCatagorys = [];
    $("a[name='Category']").each(function (index, element) {
        templateCatagorys.push($(element).attr("data-id"));
    });

    $("<div id='divSelectCategorys'></div>").dialog({
        title: '选择目录',
        width: 400,
        height: 460,
        href: AddUrlPrefix('/FormItem/SelectItemCategorys?templateCatagoryIds=') + JSON.stringify(templateCatagorys),
        modal: true,
        onLoad: function (data) {
            if (data.match("^\{(.+:.+,*){1,}\}$")) {
                var obj = JSON.parse(data);
                if (obj.sessionlose == true) {
                    window.location = "/Account/Login";
                }
            }
        },
        onClose: function () {
            var catagorys = window.returnCategorys;

            var html = "";
            if (catagorys.length > 0) {
                for (var i = 0; i < catagorys.length; i++) {
                    var catagory = catagorys[i];
                    html += "<a href='javaScript:;' name='Category' data-id='" + catagory.Code + "' data-name='" + catagory.Name + "'><i class='remove_sign' title='删除'></i><dt>" + catagory.Name + "</dt></a>"
                }
                $("#addCatagory").before(html);
                $("#div_noCategory").hide();
                if($("#_formItems").children().length == 0)
                    $("#div_noFormItem").show();
                else
                    $("#_formItems").show();

                // 新增表单项分类后的操作
                if ($("a[name='Category'] .active").length == 0)
                    $("a[name='Category']").last().click();
            }
            
            $(this).remove();
        }
    });
}

// 从题库添加
function selectFromLib(btn) {
    var code = $("a[name = 'Category'].active").attr("data-id");
    $("<div id='divSelectFromLib'></div>").dialog({
        title: '从题库添加',
        width: 500,
        height: 500,
        href: AddUrlPrefix("/FormItem/SelectFromLib?category=") + code,
        modal: true,
        //onLoad: function (data) {
        //    if (data.match("^\{(.+:.+,*){1,}\}$")) {
        //        var obj = JSON.parse(data);
        //        if (obj.sessionlose == true) {
        //            window.location = "/Account/Login";
        //        }
        //    }
        //},
        onClose: function () {
            if (window.returnItems != undefined && window.returnItems.length > 0) {
                var _html = "";
                $.each(window.returnItems, function (index, item) {
                    var _item = new FormItem(item);
                    _html += _item.toHtml();
                });

                // 第一次追加表单项
                if ($("#_formItems").children().length == 0) {
                    $("#_formItems").html(_html);
                    $("#div_noFormItem").hide();
                }
                else
                    $(btn).parent().parent().after(_html);

                ReloadBtn();
            }
            $(this).remove();
        }
    });
}

// 上移
function ItemUp(btn)
{
    var _html = "<div class='topic_block'>" + $(btn).parent().parent().html() + "</div>";
    $(btn).parent().parent().prev().before(_html);
    $(btn).parent().parent().remove();
    $("#div_noFormItem").hide();
    ReloadBtn();
}

// 下移
function ItemDown(btn) {
    var _html = "<div class='topic_block'>" + $(btn).parent().parent().html() + "</div>";
    $(btn).parent().parent().next().after(_html);
    $(btn).parent().parent().remove();
    $("#div_noFormItem").hide();
    ReloadBtn();
}

// 删除
function ItemRemove(btn)
{
    $.messager.confirm("确认", "确定要移除该表单项目吗？", function (data) {
        if (data) {
            $(btn).parent().parent().remove();
            if ($("#_formItems").children().length == 0)
                $("#div_noFormItem").show();
            ReloadBtn();
        }
    });
}

function ReloadBtn()
{
    $(".itemUp").show();
    $(".itemDowm").show();
    $(".itemUp").first().hide();
    $(".itemDowm").last().hide();
}