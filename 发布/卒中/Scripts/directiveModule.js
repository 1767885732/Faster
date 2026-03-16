/**
Angularjs自定义指令模块
author:songpeng
*/
var mdsdDirectiveModule = angular.module('mdsdDirective', []);

//多选框双向绑定到列表指令
mdsdDirectiveModule.directive('checkList', function () {
    return {
        scope: {
            list: '=checkList',
            value: '@'
        },
        link: function (scope, elem, attrs) {
            var handler = function (setup) {
                var checked = elem.prop('checked');
                var index = scope.list.indexOf(scope.value);

                if (checked && index == -1) {
                    if (setup) elem.prop('checked', false);
                    else scope.list.push(scope.value);
                } else if (!checked && index != -1) {
                    if (setup) elem.prop('checked', true);
                    else scope.list.splice(index, 1);
                }
            };

            var setupHandler = handler.bind(null, true);
            var changeHandler = handler.bind(null, false);

            elem.bind('change', function () {
                scope.$apply(changeHandler);
            });
            scope.$watch('list', setupHandler, true);
        }
    };
});

mdsdDirectiveModule.directive('dateFormat', ['$filter', function ($filter) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$formatters.push(function (value) {
                var format = attrs.dateFormat;
                return $filter('date')(value, format);
            });
        }
    };
}]);


//日期控件指令封装
mdsdDirectiveModule.directive('datePicker', ['$filter', function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var defaultConfig =
                {
                    dateFmt: 'yyyy-MM-dd',
                    onpicked: function () { element.change() },
                    oncleared: function () { element.change() }
                };  //默认配置
            var config = {};
            var jsonStr = attrs.datePicker;
            if (jsonStr) {
                if (jsonStr.substring(0, 1) == "{") {
                    config = JSON.parse(jsonStr);
                } else {
                    config = { dateFmt: jsonStr };
                }
            }
            angular.extend(defaultConfig, config);

            ngModel.$formatters.push(function (value) {
                if (value) {
                    var format = defaultConfig.dateFmt;
                    return $filter('date')(value, format);
                }
            });

            element.bind('focus', function () {
                WdatePicker(defaultConfig);
            });
        }
    };
}]);
