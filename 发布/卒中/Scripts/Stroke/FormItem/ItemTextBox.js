$(function() {
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