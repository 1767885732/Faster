$(function () {
    $("#export1").click(function () {
        if ($("#hdnSetTab").val() == "1") {
            setTab('export', 1, 3);
            setPhase(1);            
        }  
        $("#hdnSetTab").val("0");
    });
    $("#export2").click(function () {
        if ($("#hdnSetTab").val() == "1") {
            setTab('export', 2, 3);
            setPhase(2);
        }  
        $("#hdnSetTab").val("0");
    });
    $("#export3").click(function () {
        if ($("#hdnSetTab").val() == "1") {
            setTab('export', 3, 3);
            setPhase(3);
        }
        $("#hdnSetTab").val("0");
    });

    var searchMembers = [];
    var selectedMembers = []; // 选中检索条件集合

    $.post("/Export/GetSearchMembers", {}, function (result) {
        if (result.IsSuccess) {
            var data = result.Data;
            searchMembers = data;

            // 生成条件选项
            var _html = "";
            for (var i = 0; i < data.length; i++) {
                _html += "<label><input type='checkbox' name='search' value='" + i + "' " + (data[i].IsDefault ? "checked" : " ") + " ><span><nobr>" + data[i].Desc + "</nobr></span></label>";
            }
            $("#fs_SearchMembers").append(_html);

            $(".btn-export").click();
        }
    });

    // 添加选择条件
    $(".btn-export").click(function () {
        selectedMembers = [];
        $("#fs_SearchMembers").find("input[name='search']:checked").each(function () {
            var index = $(this).val();
            selectedMembers.push(searchMembers[index]);
        });

        if (selectedMembers.length == 0) {
            $("#fs_SelectedConditions").html("<legend>已选条件</legend><div class='location'><img src='/App_Themes/Default/img/common/null.png' width='150'><p>暂未发现搜索条件，请先从左侧选择条件并导入！</p></div>");
            return;
        }

        $.post("/Export/GetSearchConditions", { members: selectedMembers }, function (result) {
            if (result.IsSuccess) {
                // 根据类型生成检索条件
                var data = result.Data;
                selectedMembers = data;

                $("#fs_SelectedConditions").html("<legend>已选条件</legend>");
                var _html = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i].TypeName == "DateTime") {
                        _html += "<div class='row-box' data-type='DateTime'><label>" + data[i].Desc + "</label><input class='datetime half-input' onfocus='WdatePicker({dateFmt:\"yyyy-MM-dd\"})'>&nbsp;&ndash;&nbsp;<input class='datetime half-input' onfocus='WdatePicker({dateFmt:\"yyyy-MM-dd\"})'><a class='del'></a></div>";
                    } else if (data[i].TypeName == "Number") {
                        _html += "<div class='row-box' data-type='Number'><label>" + data[i].Desc + "</label><input class='half-input'>&nbsp;&ndash;&nbsp;<input class='half-input'><a class='del'></a></div>";
                    } else if (data[i].TypeName == "Text") {
                        _html += "<div class='row-box' data-type='Text'><label>" + data[i].Desc + "</label><input type='text'><a class='del'></a></div>";
                    } else if (data[i].TypeName == "List") {
                        _html += "<div class='row-box' data-type='List'><label>" + data[i].Desc + "</label><select>";
                        if (data[i].Dictionary != null) {
                            for (var j = 0; j < data[i].Dictionary.length; j++) {
                                _html += "<option value='" + data[i].Dictionary[j].Key + "'>" + data[i].Dictionary[j].Value + "</option>";
                            }
                        }
                        _html += "</select><a class='del'></a></div>";
                    }
                }
                $("#fs_SelectedConditions").append(_html);

                $("#fs_SelectedConditions").find("div").each(function () {
                    var $currentDiv = $(this);
                    $currentDiv.find(".del").click(function () {
                        var delIndex = $currentDiv.index() - 1; // 顶部有一个固定的legend标签，故需减1
                        selectedMembers.splice(delIndex, 1); // 删除数组中成员
                        $currentDiv.remove(); // 删除div节点

                        if (selectedMembers.length == 0) {
                            $("#fs_SelectedConditions").html("<legend>已选条件</legend><div class='location'><img src='/App_Themes/Default/img/common/null.png' width='150'><p>暂未发现搜索条件，请先从左侧选择条件并导入！</p></div>");
                            return;
                        }
                    });
                });
            }
        })
    });

    function GetSearchConditionData() {
        var index = 0;
        $("#fs_SelectedConditions").find("div").each(function () {
            var type = $(this).data("type");
            if (type == "DateTime") {
                selectedMembers[index].DateFrom = $(this).find("input").first().val();
                selectedMembers[index].DateTo = $(this).find("input").last().val();
            } else if (type == "Number") {
                selectedMembers[index].NumberFrom = $(this).find("input").first().val();
                selectedMembers[index].NumberTo = $(this).find("input").last().val();
            } else if (type == "Text") {
                selectedMembers[index].Value = $(this).find("input").first().val();
            } else if (type == "List") {
                selectedMembers[index].Value = $(this).find("select").first().val();
                selectedMembers[index].Text = $(this).find("option:selected").text();
            }

            index++;
        });
    }

    var exportFields = [];
    var selectedFields = [];
    function GetExportFields() {
        if (exportFields.length > 0) return;
        $.post("/Export/GetExportFields", {}, function (result) {
            if (result.IsSuccess) {
                var data = result.Data;
                exportFields = data;

                // 生成条件选项
                var _html = "";
                for (var i = 0; i < data.length; i++) {
                    _html += "<label><input type='checkbox' name='export' value='" + i + "'><span><nobr>" + data[i].Desc + "</nobr></span></label>";
                }
                $(".field").append(_html);
            }
        });
    }

    $("#chkselectall").change(function () {
        console.log($("#chkselectall").is(':checked'));
        $(".field").find("input[name='export']").each(function () {
            if ($("#chkselectall").is(':checked')) {
                $(this).prop("checked", "true");
            }
            else {
                $(this).removeProp("checked");
            }
        });
    });

    function GetSelectedFields() {
        selectedFields = [];
        $(".field").find("input[name='export']:checked").each(function () {
            var index = $(this).val();
            selectedFields.push(exportFields[index]);
        });
    }

    function Search(pageIndex) {
        searchModel.IsSearchLocked = true;
        pageIndex = pageIndex == null ? 1 : pageIndex;
        pageSize = 15; // 默认检索15条数据

        if (selectedFields == null || selectedFields.length == 0) return; // 选择字段不可为空
        $.post("/Export/Search", { fields: selectedFields, members: selectedMembers, pageIndex: pageIndex, pageSize: pageSize }, function (result) {
            if (result.IsSuccess) {
                var table = result.Data;
                console.log(table);

                var _html = "";

                var headers = table.Headers; // 表头
                var rows = table.Rows; // 行数据

                _html += "<tr>";
                for (var index = 0; index < headers.length; index++) {
                    _html += "<th>" + headers[index].Desc + "</th>";
                }
                _html += "</tr>";

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    var rowDatas = row.Values;

                    _html += "<tr>";
                    for (var j = 0; j < rowDatas.length; j++) {
                        var cellValue = rowDatas[j];
                        _html += "<td>" + cellValue.Value + "</td>";
                    }
                    _html += "</tr>";
                }

                $(".basic-table").html(_html);

                // 检索完成，按钮状态控制
                if (pageIndex == 1) {
                    $("#btn_pre").addClass("btn-disabled");

                    if (rows.length < 15) {
                        $("#btn_next").addClass("btn-disabled");
                    } else {
                        $("#btn_next").removeClass("btn-disabled");
                    }
                } else if (pageIndex > 1) {
                    $("#btn_pre").removeClass("btn-disabled");

                    if (rows.length < 15) {
                        $("#btn_next").addClass("btn-disabled");
                    } else {
                        $("#btn_next").removeClass("btn-disabled");
                    }
                }

                searchModel.IsSearchLocked = false;
            }
        });
    }

    var searchModel = {};
    searchModel.CurrentPageIndex = 1;
    searchModel.IsSearchLocked = false;

    // 上一页
    $("#btn_pre").click(function () {
        if ($(this).hasClass("btn-disabled")) return;

        // 防止连续多次点击
        if (searchModel.IsSearchLocked == true) return;

        searchModel.CurrentPageIndex--;
        Search(searchModel.CurrentPageIndex);
    });

    // 下一页
    $("#btn_next").click(function () {
        if ($(this).hasClass("btn-disabled")) return;

        // 防止连续多次点击
        if (searchModel.IsSearchLocked == true) return;

        searchModel.CurrentPageIndex++;
        Search(searchModel.CurrentPageIndex);
    });

    // 导出
    function Export() {
        var index;
        $.post("Export/Export", { fields: selectedFields, members: selectedMembers }, function (result) {
            layer.close(index); 
            if (result.IsSuccess) {
                window.location = "/Export/Download?file=" + result.Data;
            } else {
                layer.alert(result.Message);  // 出错提示
            }
        });
        index = layer.load();
    }

    $("#btn_export").click(function () {
        Export();
    });

    // 选择阶段
    function setPhase(status) {
        if (status == 1) {

        } else if (status == 2) {
            GetExportFields();
        } else if (status == 3) {
            GetSearchConditionData();
            GetSelectedFields();
            searchModel.CurrentPageIndex = 1;
            Search(searchModel.CurrentPageIndex);
        }
    }
});
