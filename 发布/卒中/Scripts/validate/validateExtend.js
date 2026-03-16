/**
jQuery.validate 扩展
dependon:jquery.js、moment.js
*/

//特殊校验
jQuery.validator.addMethod("IdCardCheck", function (value, element, arg) {  //身份证校验
    if (value == "") return true;
    return validateIdCard(value);
}, "不是合法的身份证号码");

/**
身份证校验方法
*/
function validateIdCard(idCard) {
    //根据身份证的前17位计算校验位
    function getVerifyCode(idCard17) {
        var IERs = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];  //乘数
        var LastPlace = ['1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2'];  //最后一位
        var divisor = 11;  //除数

        var numbers = idCard17.split("");
        var sum = 0;
        for (var i = 0; i < numbers.length; i++) {
            var v = parseInt(numbers[i]);
            sum += v * IERs[i];
        }

        return LastPlace[sum % divisor];
    }
    
    var Distincts = ["11", "12", "13", "14", "15",                         //行政区域
            "21",  "22",  "23",
            "31",  "32",  "33",  "34",  "35",  "36",  "37",
            "41",  "42",  "43",  "44",  "45",  "46",
            "50",  "51",  "52",  "53",  "54",
            "61",  "62",  "63",  "64",  "65",
            "71", "81", "82", "91"];
    
    if (idCard == null || idCard == "" || typeof idCard == 'undefined') return false;

    if (idCard.length != 15 && idCard.length != 18) {
        return false;
    }

    var reg = new RegExp("[0-9]*(X)?");  
    if(!reg.test(idCard)){   //检验是否为数字或尾部为X
        return false;
    }
    
    if (idCard.length == 15) {   //15转18位
        idCard = idCard.substring(0, 6) + "19" + idCard.substring(6);
        idCard += getVerifyCode(idCard);
    }
    
    var distinct = idCard.substring(0, 2);
    if (Distincts.indexOf(distinct) < 0) {   //行政区域校验
        return false;
    }

    var year = idCard.substring(6, 10);
    var month = idCard.substring(10, 12);
    var day = idCard.substring(12, 14);

    if (!moment(year + "-" + month + "-" + day).isValid()) {
        return false;
    }
    return getVerifyCode(idCard.substring(0, 17)) == idCard.substring(17);
}