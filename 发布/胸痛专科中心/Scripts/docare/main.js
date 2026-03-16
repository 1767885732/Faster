window.onload = function () {
    axios.get("/Account/GetUser").then(function (res) {
        if (res.data.IsSuccess) {
            document.getElementById("user_name").innerText = res.data.Data.Name;
        }
    }).catch(function (error) {
        console.log(error);
    });

    var el = document.getElementsByClassName("module-user")[0];
    hover(el, elover, elout);
}

function elover() {
    document.getElementById("function_list").style.display = "block";
}

function elout() {
    document.getElementById("function_list").style.display = "none";
}

function onMenuClick(obj, url) {
    window.location.href = url;
}

function bind(elem, ev, callback) {
    if (document.all) {
        elem.attachEvent("on" + ev, callback);
    } else {
        elem.addEventListener(ev, callback, false);
    }
}
function unbind(elem, ev, callback) {
    if (typeof (callback) == "function") {
        if (document.all) {
            elem.detachEvent("on" + ev, callback);
        } else {
            elem.removeEventListener(ev, callback, false);
        }
    } else {
        if (document.all) {
            elem.detachEvent("on" + ev);
        } else {
            elem.removeEventListener(ev, false);
        }
    }
}
function hover(elem, overCallback, outCallback) {//实现hover事件
    var isHover = false;//判断是否悬浮在上方
    var preOvTime = new Date().getTime();//上次悬浮时间
    function over(e) {
        var curOvTime = new Date().getTime();
        isHover = true;//处于over状态
        if (curOvTime - preOvTime > 10) {//时间间隔超过10毫秒，认为鼠标完成了mouseout事件
            overCallback(e, elem);
        }
        preOvTime = curOvTime;
    }
    function out(e) {
        var curOvTime = new Date().getTime();
        preOvTime = curOvTime;
        isHover = false;
        setTimeout(function () {
            if (!isHover) {
                outCallback(e, elem);
            }
        }, 10);
    }
    bind(elem, "mouseover", over);
    bind(elem, "mouseout", out);
};
