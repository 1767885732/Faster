/** 
 * 设置未来(全局)的AJAX请求默认选项 
 * 主要设置了AJAX请求遇到Session过期的情况 
 */
$.ajaxSetup({
    complete: function (xhr, status) {
        var sessionStatus = xhr.getResponseHeader('sessionstatus');
        var permissionStatus = xhr.getResponseHeader('permissionstatus');
        if (sessionStatus == 'timeout') {  //session过期，跳转到登录页面
            layer.alert("由于您长时间未进行操作，请重新登录", { title: '友情提示', icon: 0, closeBtn: 0 }, function (index) {
                layer.close(index);
                var top = getTopWindow();
                top.location.href = '/Account/Logout';
                return;
            });
        } else if (permissionStatus == 'denied') {
            layer.alert("未被授权的请求", { title: '友情提示', icon: 2, closeBtn: 0 });
            return;
        } else {
            if (status != 'success') {
                var message = "请求服务出错";
                if (status == 'timeout') {
                    message = "请求超时";
                } else if (status == 'parsererror') {
                    message = "数据解析错误"
                }
                layer.alert(message, { title: '友情提示', icon: 0 });
            }
        }
    },
});

/** 
 * 在页面中任何嵌套层次的窗口中获取顶层窗口 
 * @return 当前页面的顶层窗口对象 
 */
function getTopWindow() {
    var p = window;
    while (p != p.parent) {
        p = p.parent;
    }
    return p;
}

