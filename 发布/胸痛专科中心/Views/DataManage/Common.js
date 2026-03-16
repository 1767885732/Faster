var common = {
    calcGrace: function (age, heartRate, bp, cr, killip, graceEstimate) {
        var score = 0;
        if (age) {
            age = parseInt(age.replace("岁", ""));
            score += ((age < 30 && 0)
                || ((age >= 30 && age <= 39) && 8)
                || ((age >= 40 && age <= 49) && 25)
                || ((age >= 50 && age <= 59) && 41)
                || ((age >= 60 && age <= 69) && 58)
                || ((age >= 70 && age <= 79) && 75)
                || ((age >= 80 && age <= 89) && 91)
                || (age >= 90 && 100));
        };

        if (heartRate) {
            score += ((heartRate < 50 && 0)
                || ((heartRate >= 50 && heartRate <= 69) && 3)
                || ((heartRate >= 70 && heartRate <= 89) && 9)
                || ((heartRate >= 90 && heartRate <= 109) && 15)
                || ((heartRate >= 110 && heartRate <= 149) && 24)
                || ((heartRate >= 150 && heartRate <= 199) && 38)
                || ((heartRate >= 200) && 46)
                || 0);
        }

        if (bp) {
            bp = bp.split("|")[0];
            var sp = parseInt(bp);
            if (sp) {
                score += ((sp < 80 && 58)
                    || ((sp >= 80 && sp <= 99) && 53)
                    || ((sp >= 100 && sp <= 119) && 43)
                    || ((sp >= 120 && sp <= 139) && 34)
                    || ((sp >= 140 && sp <= 159) && 24)
                    || ((sp >= 160 && sp <= 199) && 10)
                    || ((sp >= 200) && 0)
                    || 0);
            }
        };

        if (cr) {
            cr = cr * 0.01131;
            score += (((cr >= 0 && cr <= 0.39) && 1)
                || ((cr >= 0.4 && cr <= 0.79) && 4)
                || ((cr >= 0.8 && cr <= 1.19) && 7)
                || ((cr >= 1.2 && cr <= 1.59) && 10)
                || ((cr >= 1.6 && cr <= 1.99) && 13)
                || ((cr >= 2.0 && cr <= 3.99) && 21)
                || ((cr >= 4) && 28)
                || 0);
        };

        if (killip) {
            score += ({ '1': 0, '2': 20, '3': 39, '4': 59 }[killip] || 0)
        };

        if (graceEstimate) {
            if (graceEstimate.indexOf('1') >= 0) { score += 39; };
            if (graceEstimate.indexOf('2') >= 0) { score += 28; };
            if (graceEstimate.indexOf('3') >= 0) { score += 14; };
        }

        return score;
    },

}

var formatDateTime = function (val) {
    var inputDate = val.$children[0].value;
    if (inputDate != "" != null && inputDate.indexOf(":") == -1) {
        val.value = moment(inputDate, 'YYYY-MM-DD HHmm');
    }
}

//Vue.mixin({
//    methods: {
//        allowFloat: function (e, scale) {
//            if (!scale) scale = 2;
//            var value = e.target.value;
//            //限制只能输入一个小数点
//            var dotpos = value.indexOf(".");
//            if (dotpos != -1) {
//                var str = value.substr(dotpos + 1);
//                if (str.indexOf(".") != -1) {
//                    value = value.substr(0, dotpos + str.indexOf(".") + 1);
//                } else {
//                    value = value.substr(0, dotpos + scale + 1);
//                }
//            }
//            e.target.value = value.replace(/[^\d^\.]+/g, '');
//        },
//        allowLettersAndNumbers: function (e, length) {
//            var value = e.target.value;
//            if (value == null || value.length == 0) return;
//            var msg = '请输入字母、数字、中划线或下划线！';
//            let regex = /[^_a-zA-Z0-9\-]/g;
//            //检查是否包含双字节
//            if (regex.test(value)) {
//                e.target.value = value.replace(regex, '');
//                return;
//            }
//            if (value.length > length) {
//                e.target.value = value.substr(0, length);
//                return;
//            }
//            e.target.value = value;
//        }
//    }
//})