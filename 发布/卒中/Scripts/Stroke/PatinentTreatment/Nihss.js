$(function () {
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    // 初始化Tab标签
    $('#tt').tabs({
        border: false,
        onBeforeClose: function (title, index) {
            var target = this;
            // 是否确认关闭
            $.messager.confirm("确认", "是否要删除当前NIHSS评分！", function (data) {
                if (data) {
                    var id = $(target).tabs('getTab', index).find("#ID").val();
                    if (id != "") {
                        // 删除当前NIHSS评分记录
                        $.ajax({
                            type: 'POST',
                            url: '/Treatment/RemoveNIHSS',
                            data: { id: id },
                            dataType: 'json',
                            async: false,
                            success: function (json) {
                                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                                    if (json.IsSuccess) {
                                        // 移除当前tab标签
                                        var opts = $(target).tabs('options');
                                        var bc = opts.onBeforeClose;
                                        opts.onBeforeClose = function () { };  // allowed to close now
                                        $(target).tabs('close', index);
                                        opts.onBeforeClose = bc;  // restore the event function
                                    }
                                });
                            },
                            error: function () {

                                $.messager.alert("错误", "请求失败！", "error");
                            }
                        });
                    }
                    else {
                        // 移除当前tab标签
                        var opts = $(target).tabs('options');
                        var bc = opts.onBeforeClose;
                        opts.onBeforeClose = function () { };  // allowed to close now
                        $(target).tabs('close', index);
                        opts.onBeforeClose = bc;  // restore the event function
                    }
                }
            });
            return false;
        },
    });

    // 新增NIHSS评分选项卡内容
    $("#addNIHSS").click(function () { addTab("New", "", $("#registId").val()) });

    // 加载NIHSS评分
    $.ajax({
        type: 'get',
        url: '/Treatment/GetNIHSSScores',
        data: { registerID: $("#RegisterID").val() },
        cache: false,
        dataType: 'json',
        async: false,
        success: function (json) {
            if (json.IsSuccess && json.Data != undefined) {
                $.each(json.Data, function (index, item) {
                    addTab("评分第" + (index + 1) + "次", item.ID, item.RegisterID)
                });
            }
        },
        error: function () {
            $.messager.alert("错误", "请求失败！", "error");
        }
    });

    function addTab(title, nihssId, registerId) {
        $('#tt').tabs('add', {
            title: title,
            href: "/Treatment/InitNIHSS?nihssId=" + nihssId + "&registerID=" + registerId,
            closable: true
        });
    }

    $(document).off("change", "input[type='radio']");
    $(document).on("change", "input[type='radio']", function () {
        $(this).closest("td").next().html($(this).val() + "分");

        var totalScore = 0;
        var form = $(this).closest("form");
        $(form).find("input[type='radio']:checked").each(function () {
            totalScore += parseInt($(this).val());
        });

        $(form).find("b[name='total_score']").html(totalScore);
    });

    $(document).off("click", "#save_nihss");
    $(document).on("click","#save_nihss", function () {
        Save($(this).closest("form"));
    });

    function Save(form) {
        var Data = form.serializeObject();

        $.ajax({
            type: 'POST',
            url: '/Treatment/SaveNIHSS',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    $.messager.alert("提示", "保存成功", json.Tip, function () {
                        var nihssId = json.Data;
                        var tab = $('#tt').tabs('getSelected');
                        $("#tt").tabs('update', {
                            tab: tab,
                            options: {
                                href: "/Treatment/InitNIHSS?nihssId=" + nihssId + "&registerID=" + $("#registId").val(),
                                closable: true
                            }
                        });
                        tab.panel('refresh');
                    });
                } else {
                    $.messager.alert("错误", "保存失败：" + json.Message,json.Tip);
                }
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });

        event.stopPropagation();
    }
    // 关闭
    $(document).on('click', '#close_nihss', function () {
        $("#nihssDialog").dialog('close');
    });


    // 获取当前时间
    $(document).on('click', "a[name='GetCurrentTime']", function () {
        var timeStr = moment().format("YYYY-MM-DD HH:mm");
        $(this).prev().val(timeStr);
    });
});



