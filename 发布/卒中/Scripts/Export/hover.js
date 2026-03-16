/* tab切换 */
function setTab(name, cursel, n) {
    for (i = 1; i <= n; i++) {
        var menu = document.getElementById(name + i);
        var con = document.getElementById("con-" + name + "-" + i);
        menu.className = i == cursel ? "hover" : "";
        con.style.display = i == cursel ? "block" : "none";
    }
}
/* 二级导航收起展开方式 */
$(function () {
    //页面中的DOM已经装载完成时，执行的代码

    function domHover(domObj) {
        domObj.hover(function () {
            $(this).children("div").stop(true, true).slideDown("fast");
        }, function () {
            $(this).children("div").stop(true, true).slideUp("fast");
        });
    }
    domHover($(".module"));
    domHover($(".module-user"));

    $(document).on("click", ".center_enable", function () {
        window.location.href = $(this).attr("data-url");
    })
});