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

    InitData();

    // 初始化checkbox
    function InitData() {
        // 加载选项
        $.ajax({
            type: 'get',
            url: '/Treatment/GetDefaultThrom',
            data: { registerId: $("#registId").val() },
            cache: false,
            dataType: 'json',
            async: false,
            success: function (data) {
                $.each(data.Items, function (index, item) {
                    var object = $("input[name='" + item.Name + "'][value=" + item.Key + "]");
                    object.attr("checked", true);
                    if (object.hasClass("OtherOption")) {
                        object.next().next().show();
                        object.next().next().val(item.Value);
                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });
    }

    // 保存溶栓决策
    $("#save_ThromDecision").click(function () {

        var Data = $("#_frm_thromDecision").serializeObject();

        Data.RegisterID = $("#registId").val();

        // 入选标准
        Data.InclusionCriteria = new Array();
        $("input[name='ThromInclusionCriteria']:checked").each(function (index, item) {
            Data.InclusionCriteria.push($(item).val());
        });

        // 排除标准
        Data.ExclusionCriteria = new Array();
        $("input[name='ThromExclusionCriteria']:checked").each(function (index, item) {
            Data.ExclusionCriteria.push($(item).val());
        });

        // 发病3h相对排除标准
        Data.RelativeExclusionCriteria3H = new Array();
        $("input[name='ThromRelativeExclusionCriteria3H']:checked").each(function (index, item) {
            Data.RelativeExclusionCriteria3H.push($(item).val());
        });

        // 发病4.5h相对排除标准
        Data.RelativeExclusionCriteria4_5H = new Array();
        $("input[name='ThromRelativeExclusionCriteria4_5H']:checked").each(function (index, item) {
            Data.RelativeExclusionCriteria4_5H.push($(item).val());
        });

        // 未进行溶栓治疗原因
        Data.NoThromReasons = new Array();
        $("input[name='NoThromReasons']:checked").each(function (index, item) {
            Data.NoThromReasons.push($(item).val());
        });

        $.ajax({
            type: 'POST',
            url: '/Treatment/SaveThromDecision',
            data: { model: Data },
            dataType: 'json',
            success: function (json) {
                $.messager.alert(json.Title, json.Message, json.Tip, function () {
                    if (json.IsSuccess) {

                    }
                });
            },
            error: function () {
                $.messager.alert("错误", "请求失败！", "error");
            }
        });

    });
    

    // 获取当前时间
    $("a[name='GetCurrentTime']").click(function () {
        var time = new Date();
        // 程序计时的月从0开始取值后+1
        var m = time.getMonth() + 1;
        var t = time.getFullYear() + "-" + m + "-"
          + time.getDate() + " " + time.getHours() + ":"
          + time.getMinutes();
        $(this).prev().val(t);
    });
});


