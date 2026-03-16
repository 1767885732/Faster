$().ready(function () {
    //初始化模板列表
    InitGrid();
    
    //搜索
    $("#search").click(function () {
        search();
    });

    //新建
    $("#create").click(function () {
        Create();
    });

    //批量删除
    $("#batchDelete").click(function () {
        var row = $("#grdDataList").datagrid('getSelections'); //('getSelections'); // ('getSelected');
        if (row.length == 0) {
            $.messager.alert("提示", "请选择要删除的行！", "info");
            return;
        }

        var ids = [];  //获取选中的要删除的行
        for (i = 0; i < row.length; i++) {
            ids.push(row[i].ID);
        }

        deleteTemplate(ids);
    });

});

//搜索
function search() {
    var searchParams = GetSearchParams();
    $("#grdDataList").datagrid("load", searchParams);
}

//获取查询参数
function GetSearchParams() {
    var params = {};
    params.Name = $.trim($("#Name").val());
    params.Title = $.trim($("#Title").val());
    params.CreateDateForm = $.trim($("#CreateDateForm").val());
    params.CreateDateTo = $.trim($("#CreateDateTo").val());
    params.CreateName = $.trim($("#CreateName").val());
    params.Status = $.trim($("#Status").val());

    return params;
}

//新建
function Create() {
    $('#divPanel1').dialog({
        title: '新建CRF',
        width: 820,
        height: 260,
        href: AddUrlPrefix('/CRFTemplate/Create/'),
        modal: true,
        onLoad: function (data) {
            if (data.match("^\{(.+:.+,*){1,}\}$")) {
                var obj = JSON.parse(data);
                if (obj.sessionlose == true) {
                    window.location = "/Account/Login";
                }
            }
        }
    });
}

//编辑
function editTemplate(id) {
    window.location = AddUrlPrefix("/CRFTemplate/Edit?id=") + id;
}

//删除
function deleteTemplate(ids) {
    $.messager.confirm("提示", "确实需要删除吗？", function (r) {
        if (r) {
            $.ajax({
                type: 'POST',
                data: { ids: ids },
                datatype: "json",
                url: AddUrlPrefix("/CRFTemplate/DeleteCRFTemplates/"),
                success: function (data) {
                    if (data.IsSuccess) {
                        search();
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
    var searchParams = GetSearchParams();

    regionGrid = $('#grdDataList').datagrid({
        loadMsg: "数据加载中，请稍后……",
        //view: detailview,
        //detailFormatter: function (index, row) {
        //    return '<div class="ddv" style="padding:5px 0"></div>';
        //},
        pagination: true,
        collapsible: true,
        fitColumns: false,
        sortName: "UpdateDate",
        sortOrder: "desc",
        pageSize: 15,
        pageList: [10, 15, 20],
        url: AddUrlPrefix('/CRFTemplate/GetCRFTemplateList/') + "?_=" + randomNu,
        queryParams: searchParams,
        method: 'post',
        columns: [[
        { field: 'ID', width: 80, checkbox: 'true' },
        { field: 'Name', title: '名称', width: 200, align: 'left', sortable: true },
        { field: 'Title', title: '标题', width: 350, align: 'left', sortable: true },
        {
            field: 'EffectiveDate', title: '生效日期', width: 121, align: 'center', formatter: function (value, row) {
                if (value == null || typeof value == "undefined") {
                    return "";
                }
                return getTime(value);
            }
        },
        {
            field: 'InvalidDate', title: '失效日期', width: 121, align: 'center', formatter: function (value, row) {
                if (value == null || typeof value == "undefined") {
                    return "";
                }
                return getTime(value);
            }
        },
        {
            field: 'Status', title: '审核状态', width: 121, align: 'center', formatter: function (value, row) {
                return getProcessStatus(value);
            }
        },
        { field: 'CreateAccount', title: '创建人', width: 120, align: 'center', sortable: true },
        {
            field: 'UpdateDate', title: '更新时间', width: 131, align: 'center', sortable: true, formatter: function (value, row) {
                if (value == null || typeof value == "undefined") {
                    if (row.CreateDate != null && typeof row.CreateDate != "undefined") {
                        return getTime(row.CreateDate);
                    } else {
                        return "";
                    }
                } 
                return getTime(value);
            }
        },
        {
            field: 'action', title: '操作', width: 122, align: 'center',
            formatter: function (value, row) {
                var item_html = [];
                if (CanEditTemplate)
                {
                    item_html.push("<a onclick='editTemplate(\"" + row.ID + "\")'><img src='" + AddUrlPrefix('/Images/icon/btn_edit.png') + "'/>编辑</a>")
                }
                if (CanDeleteTemplate)
                {
                    item_html.push("<a onclick='deleteTemplate([\"" + row.ID + "\"])' ><img src='" + AddUrlPrefix('/Images/icon/btn_remove.png') + "'/>删除</a>");
                }

                var html = "";
                for (var i = 0; i < item_html.length; i++) {
                    if (i > 0) {
                        html += "  |  ";
                    }
                    html += item_html[i];
                }
                return html;

                //return "<a onclick='editTemplate(\"" + row.ID + "\")'><img src='" + AddUrlPrefix('/Images/icon/btn_edit.png') + "'/>编辑</a> | <a onclick='deleteTemplate([\"" + row.ID + "\"])' ><img src='" + AddUrlPrefix('/Images/icon/btn_remove.png')+ "'/>删除</a>";
            }
        }
        ]]
    });
}

//选择元素类型
//function selectItemType() {
//    $('#divPanel1').dialog({
//        title: '新建',
//        width: 820,
//        height: 550,
//        href: '/CRF/SeleteItemType/',
//        modal: true,
//        onLoad: function (data) {
//            if (data.match("^\{(.+:.+,*){1,}\}$")) {
//                var obj = JSON.parse(data);
//                if (obj.sessionlose == true) {
//                    window.location = "/Account/Login";
//                }
//            }
//        },
//        onClose: function () {
//           var tt = window.returnItemTypeValue;
//        }
//    });
//}




