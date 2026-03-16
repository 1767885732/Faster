$(function () {

    var Items = new Array();

    var _total = $("input[name='count']").val();
    var category = $("a[name = 'Category'].active").attr("data-id");

    $('#pn').pagination({
        total: _total,
        pageSize: 10,
        onSelectPage: function (pageNumber, pageSize) {
            $.get(AddUrlPrefix("/FormItem/GetFormItemsFormLib"), { category: category, pIndex: pageNumber, pSize: pageSize },
                function (result) {
                    var _html = "";
                    if (result.IsSuccess) {
                        Items = result.Data;
                        $.each(result.Data, function (index,item) {
                            var _label = "";
                            switch (item.Type) {
                                case "01":
                                    _label = "[单行文本]";
                                    break;
                                case "02":
                                    _label = "[时　　间]";
                                    break;
                                case "03":
                                    _label = "[复　　选]";
                                    break;
                                case "04":
                                    _label = "[单　　选]";
                                    break;
                                case "05":
                                    _label = "[下拉列表]";
                                    break;
                                case "06":
                                    _label = "[多行文本]";
                                    break;
                                case "09":
                                    _label = "[数　　字]";
                                    break;
                                default:
                                    break;
                            }
                            _html += ("<p><input type='checkbox' data-id='" + item.ID + "'>" + _label + " " + item.Title + "</p>");
                        });
                        $(".checkbox_selection_block").html(_html);
                    }
                    else
                    {

                    }
                }
            );
        }
    });

    // 全选功能
    $("#selectAll").click(function () {
        $(".checkbox_selection_block").find("input[type='checkbox']").prop("checked", $(this).is(':checked'));
    });

    $("#select_ok").click(function () {
       
        var selectedIds = $(".checkbox_selection_block").find("input[type='checkbox']:checked");
        window.returnItems = getItems(selectedIds, Items);
        $("#divSelectFromLib").dialog("close");
    })

    $("#select_cancel").click(function () {
        window.returnItems = "";
        $("#divSelectFromLib").dialog("close");
    })

    $('#pn').pagination('select', 1);
})

function getItems(selectIds, Items) {
    var items = new Array();
    $.each(selectIds, function (index1, selectId) {
        var id = $(selectId).attr("data-id");
        $.each(Items, function (index2, item) {
            if (item.ID == id) {
                items.push(item);
                return;
            }
        });
    });

    return items;
}