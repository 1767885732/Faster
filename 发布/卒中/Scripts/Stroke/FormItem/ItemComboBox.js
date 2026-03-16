$(function() {
    // 设为默认值
    $(document).on("click", ".SetDefault",function(){
        // 取消原来的默认值
        var oldItem = $(".topic_add_block .default");
        if (oldItem.length > 0) {
            oldItem.removeClass("default");
            oldItem.find("input").attr("data-default", '0');
            oldItem.find(".SetDefault").show();
            oldItem.find(".RemoveDefault").hide();
        }

        // 设置默认值
        $(this).parent().addClass("default");
        $(this).prev().attr("data-default", '1');
        $(this).hide();
        $(this).next().show();
    });

    // 取消默认值
    $(document).on("click", ".RemoveDefault",function () {
        $(this).parent().removeClass("default");
        $(this).parent().find("input").attr("data-default", '0');
        $(this).parent().find(".SetDefault").show();
        $(this).hide();
    });

    // 新增
    $(document).on("click", "#AddRadioOption", function () {
        var width = $("#chkScore").prop("checked") ? "66.6%" : "75%";
        var _display = $("#chkScore").prop("checked") ? "inline-block" : "none";
        var _html = "";
        _html += "<p class='option line_b'>";
        _html += "    <input type='text' value='' style='width:" + width + "' data-default='0'>";
        _html += "    <input name='txtScore' type='text' class='mg_l_5' style='width:80px; display:" + _display + ";' placeholder='选项评分' />";
        _html += "    <a href='javaScript:;' class='SetDefault' title='设为默认值'>";
        _html += "        <span class='tag_deaultvalue'>设为默认值</span>";
        _html += "    </a>";
        _html += "    <a href='javaScript:;' class='RemoveDefault' title='取消默认值' style='display:none;'>";
        _html += "        <span class='tag_deaultvalue_actived'><b class='remove'></b>取消默认值</span>";
        _html += "    </a>";
        _html += "</p>  ";
        _html += "<div class='topic_add_btn_grp fr'>";
        _html += "    <a href='javaScript:;' class='deleteOption' title='删除'><b class='remove'></b>删除</a>";
        _html += "</div> "
        $(".topic_add_block").append(_html);

    });

    // 删除
    $(document).on("click", ".deleteOption", function () {
        $(this).parent().prev().remove();
        $(this).parent().remove();

    });

    // 评分控制
    $(document).on("click", "#chkScore", function () {
        if ($(this).prop("checked")) {
            $.each($("input[name='txtScore']"), function (index) {
                $(this).show();
                if (index > 0) {
                    $(this).prev().css("width", "66.6%");
                }
            });
        }
        else {
            $.each($("input[name='txtScore']"), function (index) {
                $(this).hide();
                if (index > 0) {
                    $(this).prev().css("width", "75%");
                }
            });
        }

    });

    // 评分控制初始化
    if ($("#chkScore").prop("checked")) {
        $.each($("input[name='txtScore']"), function (index) {
            $(this).show();
            if (index > 0) {
                $(this).prev().css("width", "66.6%");
            }
        });
    }
})