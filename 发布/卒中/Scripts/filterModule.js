/**
Angularjs通用过滤器模块
author:songpeng
*/
var mdsdFilterModule = angular.module('mdsdFilter', []);

/**
时间格式化
format:格式化样式（按moment组件的样式写）
*/
mdsdFilterModule.filter('dateFmt', function () {
    return function (input, format) {
        if (!input) {
            return "";
        } else {
            if (format) {
                return moment(input).format(format);
            } else {
                return moment(input).format("YYYY-MM-DD");  //默认格式化类型
            }
        }
    }
})

/**
取键值
key:键值（输入）
keyValueArray：键值对数组 example:[{Key:'1',Value:'待审核'},{Key:'2',Value:'审核通过'}]
emptyValue:键值为空时返回的默认值
notFindValue:未找到对应键值时返回的默认值
*/
mdsdFilterModule.filter('getValue', function () {
    return function (key, keyValueArray, emptyValue, notFindValue) {
        if (!key) {
            if (emptyValue) {
                return emptyValue;
            } 
        }
        if (!keyValueArray) {
            return key;
        }
        for (var i = 0; i < keyValueArray.length; i++) {
            keyValue = keyValueArray[i];
            if (keyValue.Key == key) {
                return keyValue.Value;
            }
        }
        if (notFindValue) {
            return notFindValue;
        }
        return "";
    }
})

/**
空值替换（用于替换空值，默认--）
*/
mdsdFilterModule.filter('emptyReplace', function () {
    return function (value, replaceValue) {
        if (typeof replaceValue == 'undefined') {
            replaceValue = '--';
        }
        if (value == null || typeof value == 'undefined' || value == '') {
            return replaceValue;
        } else {
            return value;
        }
    }
})

/**
性别格式化 "1": "男", "2": "女" 
*/
mdsdFilterModule.filter("genderName", function () {
    // 姓别定义
    var genderArr = { "1": "男", "2": "女" };
    return function (input) {
        return genderArr[input] || "";
    };
})

