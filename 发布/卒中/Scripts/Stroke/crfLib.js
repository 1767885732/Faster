$().ready(function () {
    InitGrid();

    $("#query").click(function () {
        query();
    });

    $("#add").click(function () {
        addItem();
    });

    $("#delete").click(function () {
        var row = $("#grdDataList").datagrid('getSelections'); //('getSelections'); // ('getSelected');
        if (row.length == 0) {
            $.messager.alert("提示", "请选择要删除的行！", "info");
            return;
        }

        var ids = [];  //获取选中的要删除的行
        for (i = 0; i < row.length; i++) {
            ids.push(row[i].ID);
        }

        deleteItems(ids);
    });

    $("#export").click(function (e) {
        var param = GetQueryParams();
        $.ajax({
            type: 'POST',
            data: param,
            datatype: "json",
            url: AddUrlPrefix("/CRFLibrary/Export"),
            async: false,
            success: function (response) {
                if (response.IsSuccess) {
                    window.location = AddUrlPrefix('/CRFLibrary/Download?file=') + response.Data;
                } else {
                    $.messager.alert(response.Title, response.Message, response.Tip);  //出错了才会有提示
                }
            }
        });
    });
});

//搜索
function query() {
    var queryParams = GetQueryParams();
    $("#grdDataList").datagrid("load", queryParams);
}

//编辑
function editItem(id, type) {
    var height = 600;
    if (type == "01") {
        height = 500;
    }

    $("<div id='divEditItem'></div>").dialog({
        title: '编辑',
        width: 950,
        height: height,
        href: AddUrlPrefix('/FormItem/Edit?id=' + id),
        modal: true,
        onClose: function () {
            if (window.returnItem != "") {
                query();
            }
            $(this).remove();
        }
    });
    
}

//新增元素
function addItem() {
    $("<div id='divPanel1'></div>").dialog({
        title: '新建',
        width: 820,
        height: 550,
        href: AddUrlPrefix('/FormItem/SeleteItemType/'),
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
            var itemTypeCode = window.returnItemTypeValue;
            //var category = $("a[name='Category'].active").attr("data-id");
            // 点击确认的情况下，弹出新增页面
            var _height = itemTypeCode == "01" ? 500 : 600;
            if (itemTypeCode != "") {
                $("<div id='divEditItem'></div>").dialog({
                    title: '新建',
                    width: 950,
                    height: _height,
                    href: AddUrlPrefix('/FormItem/Add?type=' + itemTypeCode),
                    modal: true,
                    onClose: function () {
                        if (window.returnItem != "") {
                            query();
                        }
                        $(this).remove();
                    }
                });
            }
            $(this).remove();
        }
    });
}

//删除（可批量）
function deleteItems(ids) {
    $.messager.confirm("提示", "确实需要删除吗？", function (r) {
        if (r) {
            $.ajax({
                type: 'POST',
                data: { ids: ids },
                datatype: "json",
                url: AddUrlPrefix("/CRFLibrary/BatchDelete/"),
                success: function (data) {
                    if (data.IsSuccess) {
                        query();
                    }
                    else {
                        $.messager.alert(data.Title, data.Message, data.Tip);
                    }
                }
            });
        }
    });
}

//初始化列表
function InitGrid() {
    var randomNu = (new Date().getTime()) ^ Math.random();
    var queryParams = GetQueryParams();

    regionGrid = $('#grdDataList').datagrid({
        loadMsg: "数据加载中，请稍后……",
        pagination: true,
        collapsible: true,
        fitColumns: false,
        sortName: "TITLE",
        sortOrder: "asc",
        pageSize: 15,
        pageList: [10, 15, 20],
        url: AddUrlPrefix('/CRFLibrary/Query/' + "?_=" + randomNu),
        queryParams: queryParams,
        method: 'post',
        columns: [[
        { field: 'ID', width: 80, checkbox: 'true' },
        { field: 'Title', title: '题目', width: 600, align: 'left' },
        {
            field: 'TypeName', title: '题型', width: 200, align: 'center', 
        },
        {
            field: 'CategoryName', title: '所属类别', width: 300, align: 'center', 
        },
        {
            field: 'XXXX', title: '操作', width: 202, align: 'center', sortable: true,
            formatter: function (value, row) {
                var item_html = [];
                if (CanEditLib)
                {
                    item_html.push("<a onclick='editItem(\"" + row.ID + "\", \"" + row.Type + "\")'><img src='" + AddUrlPrefix('/Images/icon/btn_edit.png') + "'/>编辑</a>");
                }
                if (CanDeleteLib)
                {
                    item_html.push("<a onclick='deleteItems([\"" + row.ID + "\"])' ><img src='" + AddUrlPrefix('/Images/icon/btn_remove.png') + "'/>删除</a>");
                }
                var html = "";
                for (var i = 0; i < item_html.length; i++) {
                    if (i > 0) {
                        html += "  |  ";
                    }
                    html += item_html[i];
                }
                return html;
                //return "<a onclick='editItem(\"" + row.ID + "\", \"" + row.Type + "\")'><img src='" + AddUrlPrefix('/Images/icon/btn_edit.png') + "'/>编辑</a> | <a onclick='deleteItems([\"" + row.ID + "\"])' ><img src='" + AddUrlPrefix('/Images/icon/btn_remove.png') + "'/>删除</a>";
            }
        }
        ]]
    });
}

//获取查询参数
function GetQueryParams() {
    var params = {};
    params.title = $.trim($("#itemTitle").val());
    params.type = $.trim($("#itemType").val());
    params.category = $.trim($("#itemCategory").val());

    return params;
}