'use strict';

angular.module('ngDataManageIndexExport', []);

angular.module('ngDataManageIndexExport').
    component('listExport', {
        template: '<button class="btn-primary" ng-click="$ctrl.ExportData()">导出</button>',
        controller: function ExportController() {
            this.ExportData = function () {
                layer.open({
                    type: 2,
                    title: '导出',
                    btn: ['上一步', '下一步', '确定', '取消'],
                    btn1: function (index, layero) {
                        var iframe = layero.find("iframe")[0];
                        var ctrl = $(iframe).contents();
                        var index = $(iframe).contents().find("li.hover").index();
                        if (index == 1) {
                            $(iframe).contents().find("#btn_1").click();
                            ctrl.find("#hdnSetTab").val("1");
                            ctrl.find("#export1").click();
                            $(".layui-layer-btn0").hide();
                        } 
                        if (index == 2) {                            
                            ctrl.find("#hdnSetTab").val("1");
                            ctrl.find("#export2").click();
                            $(iframe).contents().find("#btn_2").click();
                        }
                        $(".layui-layer-btn1").show();
                        $(".layui-layer-btn2").hide();
                        return false;
                    },
                    btn2: function (index, layero) {
                        var iframe = layero.find("iframe")[0];                        
                        if ($(iframe).contents().find("li.hover").index() == 1) {
                            if ($(iframe).contents().find("input[name='export']:checked").length <= 0) {
                                layer.alert("请选择导出字段", { icon: 1 });
                                return false;
                            }
                        }
                        var ctrl = $(iframe).contents();
                        var selectId = ctrl.find(".data:visible").attr("id");
                        switch (selectId) {
                            case "con-export-1":
                                ctrl.find("#hdnSetTab").val("1");
                                ctrl.find("#export2").click();
                                break;
                            case "con-export-2":
                                ctrl.find("#hdnSetTab").val("1");
                                ctrl.find("#export3").click();
                                $(".layui-layer-btn1").hide();
                                $(".layui-layer-btn2").show();
                                break;
                            default:
                                ctrl.find("#hdnSetTab").val("1");
                                var exportBtn = ctrl.find("#btn_export");
                                $(exportBtn).click();
                        }
                        $(".layui-layer-btn0").show();
                        return false;
                    },
                    btn3: function (index, layero) {
                        var iframe = layero.find("iframe")[0];
                        var ctrl = $(iframe).contents();
                        ctrl.find("#hdnSetTab").val("1");
                        var exportBtn = $(iframe).contents().find("#btn_export");
                        console.log(exportBtn);
                        $(exportBtn).click();
                        return false;
                    },
                    btn4: function (index, layero) {
                    },
                    success: function (index, layero) {
                        $(".layui-layer-btn0").hide();
                        $(".layui-layer-btn2").hide();
                    },
                    maxmin: false,
                    shadeClose: false, //点击遮罩关闭层
                    area: ['1000px', '480px'],
                    content: '/Export/ExportView'
                });
                //layer.open({
                //    type: 2,
                //    title: '导出',
                //    btn: ['上一部', '下一步', '确定', '取消'],
                //    yes: function (index, layero) {
                //        var iframe = layero.find("iframe")[0];

                //        var ctrl = $(iframe).contents()

                //        var selectId = ctrl.find(".data:visible").attr("id");

                //        switch (selectId) {
                //            case "con-export-1":
                //                ctrl.find("#export2").click();
                //                break;
                //            case "con-export-2":
                //                ctrl.find("#export3").click();
                //                break;
                //            default:
                //                var exportBtn = ctrl.find("#btn_export");
                //                console.log(exportBtn);
                //                $(exportBtn).click();
                //        }
                //    },
                //    maxmin: false,
                //    shadeClose: false, //点击遮罩关闭层
                //    area: ['1000px', '480px'],
                //    content: '/Export/ExportView'
                //});
            }
        }
    });