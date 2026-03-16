/*
 * 工具类
 */
var ajaxUtil = function () { };

ajaxUtil = {
    Ajax: function (options) {
        var defaults = {
            async: true,
            cache: true,
            type: 'POST',
            data: ''
        };

        defaults = $.extend(defaults, options);

        return jQuery.ajax({
            url: defaults.url,
            type: defaults.type,
            cache: defaults.cache,
            async: defaults.async,
            data: defaults.data,
            dataType: "JSON",
            success: function (data) {
                if (defaults.success) {
                    defaults.success.call(data);
                }
                return data;
            },
            error: function (data) {
                if (defaults.error) {
                    defaults.error.call(data);
                }
                return data;
            }
        });
    },
    promiseAjax: function (options) {
        var defaults = {
            async: true,
            cache: true,
            type: 'POST',
            data: ''
        };
        defaults = $.extend(defaults, options);
        return new Promise(function (resolve, reject) {
            jQuery.ajax({
                url: defaults.url,
                type: defaults.type,
                cache: defaults.cache,
                async: defaults.async,
                data: defaults.data,
                dataType: "JSON",
                success: function (data) {
                    resolve(data);
                },
                error: function (data) {
                    reject(data);
                }
            })
        });
    }
};

/*
 * 自定义插件
 */
function callback() {
    adminPlugin.tableInit();
    if (typeof pageCallback == 'function') {
        pageCallback();
    }
}
var adminPlugin = function () { };
adminPlugin = {
    //成功提示信息
    successMessage: function (txt, time) {
        adminPlugin.extend.errorMessage("操作成功：" + txt, "green", time);
    },
    //友情提示
    warningMessage: function (txt, time) {
        adminPlugin.extend.errorMessage("友情提示：" + txt, "yellow", time);
    },
    //错误提示信息
    errorMessage: function (txt, time) {
        adminPlugin.extend.errorMessage("错误信息是：" + txt, "red", time, 'icon-question-sign');
    },
    tableInit: function () {
        /*******详情********/
        $('.show-details-btn').on('click', function (e) {
            e.preventDefault();
            $(this).closest('tr').next().toggleClass('open');
            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
        });

        /*******全选********/
        var active_class = 'active';
        $('#simple-table > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
            var th_checked = this.checked;//checkbox inside "TH" table header

            $(this).closest('table').find('tbody > tr').each(function () {
                var row = this;
                if (th_checked) $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
                else $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
            });
        });

        /****row选中样式*****/
        $('#simple-table').on('click', 'td input[type=checkbox]', function () {
            var $row = $(this).closest('tr');
            if ($row.is('.detail-row ')) return;
            if (this.checked) $row.addClass(active_class);
            else $row.removeClass(active_class);
        })

        /***按钮tooltip**/
        $('[data-rel=tooltip]').tooltip();

        /*table th 排序*/
        $("#simple-table").off('click').on("click", "th[class^='sorting']",function () {
            
            var selector = "#simple-table th[class^='sorting']";
            if ($(this).hasClass('sorting')) {
                adminPlugin.extend.InitSortClass(selector);
                $(this).removeClass().addClass("sorting_asc");
            }
            else if ($(this).hasClass('sorting_asc')) {
                adminPlugin.extend.InitSortClass(selector);
                $(this).removeClass().addClass("sorting_desc");
            }
            else if ($(this).hasClass('sorting_desc')) {
                adminPlugin.extend.InitSortClass(selector);
                $(this).removeClass().addClass("sorting_asc");
            }

            $('#OrderField').val($(this).attr('model-field'));
            var sortValue = $('#OrderDesc').val();
            if ($(this).hasClass("sorting_asc")) {
                sortValue = 'True';
            }
            else if ($(this).hasClass("sorting_desc")){
                sortValue = 'False';
            }
            $('#OrderDesc').val(sortValue);

            $('#btnSearch').trigger('click');
        })

        /*layIframe 事件绑定*/
        this.layerIframe();


    },
    //分页控件
    GoToPage: function (ctr, refName, callback) {
        if ($(ctr).text().trim() == '查询') $('#PageIndex').val('1');

        var items = new Array();
        $.each($('form').serialize().split('&'), function (i, datastr) {
            var keyValue = datastr.split('=');
            if (typeof (items[keyValue[0]]) != "undefined")
                items[keyValue[0]] = items[keyValue[0]] + ',' + keyValue[1];
            else
                items[keyValue[0]] = keyValue[1];
        });
        var currHref = window.location.href.split("?");
        if (currHref && currHref.length == 2) {
            $.each(currHref[1].split('&'), function (i, datastr) {
                var keyValue = datastr.split('=');
                if (typeof (items[keyValue[0]]) != "undefined")
                    items[keyValue[0]] = items[keyValue[0]] + ',' + keyValue[1];
                else
                    items[keyValue[0]] = keyValue[1];
            });
        }
        
        var hrefStr = $(ctr).attr('href');//分页按键页码优先
        if (hrefStr) {
            hrefStr = hrefStr.split("?")[1];
            $.each(hrefStr.split('&'), function (i, datastr) {
                var keyValue = datastr.split('=');
                items[keyValue[0]] = keyValue[1];
            });
        }
        var newUrl = "";
        for (var item in items) {
            newUrl += "&" + item + "=" + items[item];
        }
        newUrl = "?dd=" + Math.random() + "&" + newUrl.substr(1, newUrl.length - 1);
        var tarAction = '';
        var actionObj = $("#myaction");
        if (actionObj && actionObj.length > 0) {
            tarAction = actionObj.val();
        } else {
            var uriSplit = $('form').attr('action').split('#');
            tarAction = uriSplit.length > 1 ? uriSplit[1] : uriSplit[0];
        }
        var loadUrl = tarAction + newUrl + " #" + refName + " tr";

        var pageLayer = layer.load(0, { shade: false });//layer.load(0, { shade: [0.8, '#393D49'], time: 3000 });
        try {
            $("#" + refName).load(loadUrl, function (responseText, status, res) {
                if (callback) callback([responseText, status, res]);
                layer.close(pageLayer);
            })
        }
        catch (e) {
            layer.close(pageLayer);
        }
        return false;
    },
    
    layerIframe: function () {
        $("a.iframe").off('click').on('click', function () {
            var ctr = $(this);

            layer.open({
                title: ctr.data("title") ? ctr.data("title"):" ",
                type: 2,//iframe
                area: [ctr.data("minwidth") ? ctr.data("minwidth") : '750px', ctr.data("minheight") ? ctr.data("minheight") : '450px'],//宽高数组
                offset: '100px',//距离顶部100px,水平保持剧中
                icon: 0,
                shadeClose: true,
                fixed: false, //不固定
                maxmin: true,//最大化
                content: [ctr.data('href')],//禁止滚动条,no
                end: function () {
                    if (ctr.data("callback")) {
                        eval(ctr.data("callback"));
                    }
                }
            });
            
        });

   
    },

    AceChosen:function(obj) {
        $(obj).chosen({
            no_results_text: '没有找到',    // 当检索时没有找到匹配项时显示的提示文本
            disable_search_threshold: 4, // 8 个以下的选择项则不显示检索框
            search_contains: true,         // 从任意位置开始检索
            allow_single_deselect: true,
        });

    },

    summernoteinit: function (obj) {
        var $this = obj;
        var placeholderStr = $this.attr("placeholder") || '';
        $this.summernote({
            lang: 'zh-CN',
            placeholder: placeholderStr,
            minHeight: 300,
            dialogsFade: true,// Add fade effect on dialogs
            dialogsInBody: true,// Dialogs can be placed in body, not in
            disableDragAndDrop: false,// default false You can disable drag
            focus: false,
            toolbar: [
                ['style', ['style']],
                ['fontsize', ['fontsize']],
                ['height', ['height']],
                ['font', ['bold', 'underline', 'clear']],
                ['fontname', ['fontname']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']]
            ],
        });

    },

    RedirctLoginIn: function () {
        location.href = "/login/index";
    },

    RedirctError: function () {
        location.href = "/login/error";
    }

};


adminPlugin.extend = {
    //错误提示信息，暂时限制为5中颜色：red、green、blue、yellow、orange
    errorMessage: function (text, color, time, icon) {
        var icon_span = "", html_element, $cont = $('#error-message');
        color = color ? color : 'red';
        time = time ? time : 5000;
        icon_span = icon ? "<i class='icon-warning-sign'></i> " : "<span class='icon-warning-sign'></span> ";
        //创建HTML并且添加到给定的内容中
        html_element = $('<div class="error-message-calss error-message-' + color + '">' + icon_span + text + '</div>').fadeIn('fast');
        $cont.append(html_element);

        html_element.on('click', function () {
            adminPlugin.extend.error_message_close($(this));
        });
        setTimeout(function () {
            adminPlugin.extend.error_message_close($cont.children('.error-message-calss').first());
        }, time);
    },
    error_message_close: function (elem) {
        if (typeof elem !== "undefined") {
            elem.fadeOut('fast', function () {
                $(this).remove();
            });
        } else {
            $('.alert').fadeOut('fast', function () {
                $(this).remove();
            });
        }
    },
    //jquery时间转换
    dateParse: function (date_time) {
        //时间规范
        var minute = 1000 * 60, hour = minute * 60, day = hour * 24, halfamonth = day * 15, month = day * 30;
        var begin_time = new Date();                                        //结束时间
        var millions = begin_time.getTime() - Date.parse(date_time);        //时间差的毫秒数
        var monthC = millions / month, weekC = millions / (7 * day), dayC = millions / day,
            hourC = millions / hour, minC = millions / minute;

        if (monthC >= 1) {
            result = parseInt(monthC) + "个月前";
        }
        else if (weekC >= 1) {
            result = parseInt(weekC) + "周前";
        }
        else if (dayC >= 1) {
            result = parseInt(dayC) + "天前";
        }
        else if (hourC >= 1) {
            result = parseInt(hourC) + "小时前";
        }
        else if (minC >= 1) {
            result = parseInt(minC) + "分钟前";
        } else {
            result = "刚刚";
        }
        return result;
    },
    //正则表达式
    email: function () {
        return /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    },
    phone: function () {
        return /^(((13[0-9]{1})|(14[0-9]{1})|(17[0-9]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
    },
    InitSortClass: function (selector) {
        $(selector).each(function () {
            $(this).removeClass(); $(this).addClass("sorting");
        });
    },
    ClearImg: function (tarid) {
        if (tarid != '') {
            $("#" + tarid).attr('src', '');
            $("#" + tarid).attr('title', '');
            $("input[name=" + tarid + "]").val('');
        }
    },
    ChooseImg: function (name, path) {
        var pid = $("#baseImgId").val();
        if (pid == '') {
            alert('哟,从哪弹出来的,请刷新重试');
            return;
        }

        bootbox.confirm({
            message: "确定选择图片名:" + name + "吗?",
            buttons: {
                confirm: {
                    label: "确定",
                    className: "btn-primary btn-sm",
                },
                cancel: {
                    label: "取消",
                    className: "btn-sm",
                }
            },
            callback: function (result) {
                if (result) {
                   
                    var parObj = $(parent.$.find("#" + pid));
                    var parInput = $(parent.$.find("input[name=" + pid + "]"));
                    if (parObj && parObj.length > 0 && parInput && parInput.length > 0) {
                        parObj.attr('src', path);
                        parObj.attr('title', name);
                        parInput.val(path);
                    }

                    parent.layer.closeAll();
                    parent.layer.msg("图片," + name + "选择成功。", { time: 2000 });
                    
                }
            }
        });
    }
};