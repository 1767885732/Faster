Vue.directive('int', {
    inserted: function (el, binding, vnode) {
        let input = vnode.elm.getElementsByTagName("input")[0];
        input.addEventListener('compositionstart', function () {
            vnode.inputLocking = true
        });
        input.addEventListener('compositionend', function () {
            vnode.inputLocking = false
            input.dispatchEvent(new Event('input'))
        });
        input.addEventListener('input', function () {
            if (vnode.inputLocking) {
                return;
            }
            if (!binding.value) binding.value = {};
            if (!binding.value.min) binding.value.min = 0;
            if (!binding.value.max) binding.value.max = 1000;
            var msg = '请输入' + binding.value.min + '-' + binding.value.max + '之间的整数。';

            let oldValue = input.value;
            let newValue = input.value;
            newValue = newValue.replace(/[^\d]/g, '');
            newValue = newValue.replace(/^(\-)*(\d+)$/, '$1$2')
            if (newValue) {
                let arr = newValue.split('.')
                newValue = Number(newValue) + ''; // 去掉开头多余的0
                if (Number(newValue) > binding.value.max) {
                    vnode.context.$message({ type: 'warning', message: msg });
                    newValue = newValue.substr(0, newValue.length - 1);
                    //if (vnode.data.model != null) {
                    //    if (vnode.data.model.value){
                    //        newValue = vnode.data.model.value;
                    //    }
                    //    else {
                    //        newValue = ""
                    //    }
                    //} else { newValue = "" };
                }
            }
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue;
                input.dispatchEvent(new Event('input')); // 通知v-model更新
            }
        });
        input.addEventListener('blur', function () {
            if (!binding.value) binding.value = {};
            if (!binding.value.min) binding.value.min = 0;
            if (!binding.value.max) binding.value.max = 1000;
            var msg = '请输入' + binding.value.min + '-' + binding.value.max + '之间的整数。';
            let oldValue = input.value;
            let newValue = input.value;
            if (newValue) {
                if (Number(newValue) < binding.value.min) {
                    vnode.context.$message({ type: 'warning', message: msg });
                    newValue = "";
                } else{
                    newValue = Number(newValue).toString();
                }
            }
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue
                input.dispatchEvent(new Event('input')) // 通知v-model更新
            }
        });

        //el.addEventListener("keypress", function (e) {
        //    e = e || window.event;
        //    let charcode = typeof e.charCode == 'number' ? e.charCode : e.keyCode;
        //    let re = /\d/;
        //    if (!re.test(String.fromCharCode(charcode)) && charcode > 9 && !e.ctrlKey) {
        //        if (e.preventDefault) {
        //            e.preventDefault();
        //        } else {
        //            e.returnValue = false;
        //        }
        //    }
        //});
        //el.addEventListener("change", function (e) {
        //    e = e || window.event;
        //    var strValue = e.target.value;
        //    if (strValue == null || strValue == '') return;
        //    if (!binding.value) binding.value = {};
        //    if (!binding.value.min) binding.value.min = 0;
        //    if (!binding.value.max) binding.value.max = 1000;
        //    var msg = '请输入' + binding.value.min + '-' + binding.value.max + '之间的整数。';

        //    //检查是否包含双字节
        //    let hzregex = /[\u4e00-\u9fa5]/g;
        //    if (hzregex.test(strValue)) {
        //        e.target.value = null;
        //        vnode.context.$message({ type: 'warning', message: msg });
        //        return;
        //    }
        //    //检查是否是整数
        //    var reg = eval('/^(0|[1-9][0-9]*)/g');
        //    var r = strValue.match(reg);
        //    if (r == null || r.length == 0) {
        //        e.target.value = e.target.value.replace(hzregex, '');
        //        vnode.context.$message({ type: 'warning', message: msg });
        //        return;
        //    }
        //    //检查整数范围
        //    var value = parseInt(strValue);
        //    if (value < binding.value.min || value > binding.value.max) {
        //        e.target.value = null;
        //        vnode.context.$message({ type: 'warning', message: msg });
        //        return;
        //    }

        //    e.target.value = strValue;
        //});
    }
});

Vue.directive('float', {
    inserted: function (el, binding, vnode) {
        let input = vnode.elm.getElementsByTagName("input")[0];
        input.addEventListener('compositionstart', function () {
            vnode.inputLocking = true
        });
        input.addEventListener('compositionend', function () {
            vnode.inputLocking = false
            input.dispatchEvent(new Event('input'))
        });
        input.addEventListener('input', function () {
            if (vnode.inputLocking) {
                return;
            }
            if (!binding.value) binding.value = {};
            if (!binding.value.min) binding.value.min = 0;
            if (!binding.value.max) binding.value.max = 1000;
            if (!binding.value.scale) binding.value.scale = 2;
            var msg = '请输入' + binding.value.min + '-' + binding.value.max + '之间带' + binding.value.scale + '位小数的数字。';

            let oldValue = input.value;
            let newValue = input.value;
            newValue = newValue.replace(/[^\d.]/g, '');
            newValue = newValue.replace(/^\./g, '');
            newValue = newValue.replace('.', '$#$').replace(/\./g, '').replace('$#$', '.');
            newValue = newValue.replace(eval('/^(\\-)*(\\d+)\\.(\\d{0,' + binding.value.scale + '}).*$/'), '$1$2.$3')
            if (newValue) {
                let arr = newValue.split('.')
                newValue = Number(arr[0]) + (arr[1] === undefined ? '' : '.' + arr[1]) // 去掉开头多余的0
                if (Number(newValue) > binding.value.max) {
                    vnode.context.$message({ type: 'warning', message: msg });
                    newValue = Number(arr[0].substr(0, arr[0].length - 1)) + (arr[1] === undefined ? '' : '.' + arr[1]);
                    //if (vnode.data.model != null && vnode.data.model.value) {
                    //    if (vnode.data.model.value) {
                    //        newValue = vnode.data.model.value;
                    //    }
                    //    else if (vnode.data.model.expression) {
                    //        newValue = eval("vnode.context." + vnode.data.model.expression);
                    //    }
                    //    else {
                    //        newValue = ""
                    //    }
                    //} else { newValue = "" };
                }
            }
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue;
                input.dispatchEvent(new Event('input')); // 通知v-model更新
            }
        });
        input.addEventListener('blur', function () {
            if (!binding.value) binding.value = {};
            if (!binding.value.min) binding.value.min = 0;
            if (!binding.value.max) binding.value.max = 1000;
            if (!binding.value.scale) binding.value.scale = 2;
            var msg = '请输入' + binding.value.min + '-' + binding.value.max + '之间带' + binding.value.scale + '位小数的数字。';

            let oldValue = input.value;
            let newValue = input.value;
            if (newValue) {
                if (Number(newValue) < binding.value.min) {
                    vnode.context.$message({ type: 'warning', message: msg });
                    newValue = "";
                } else{
                    newValue = Number(newValue).toString();
                }
            }
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue
                input.dispatchEvent(new Event('input')) // 通知v-model更新
            }
        });
    }
});

Vue.directive('char', {
    inserted: function (el, binding, vnode) {
        let input = vnode.elm.getElementsByTagName("input")[0];
        input.addEventListener('compositionstart', function () {
            vnode.inputLocking = true
        });
        input.addEventListener('compositionend', function () {
            vnode.inputLocking = false
            input.dispatchEvent(new Event('input'))
        });
        input.addEventListener("input", function (e) {
            if (!binding.value) binding.value = {};
            var msg = '请输入字母、数字、中划线或下划线！';
            let oldValue = input.value;
            let newValue = input.value;
            newValue = newValue.replace(/[^_a-zA-Z0-9\-]/g, '');
            if (input.maxLength) {
                newValue = newValue.substr(0, input.maxLength);
            }

            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue;
                input.dispatchEvent(new Event('input')); // 通知v-model更新
            }
        });
        input.addEventListener('blur', function () {
            let oldValue = input.value;
            let newValue = input.value;
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue
                input.dispatchEvent(new Event('input')) // 通知v-model更新
            }
        });
    }
});

Vue.directive('bp', {
    inserted: function (el, binding, vnode) {
        let input = vnode.elm.getElementsByTagName("input")[0];
        input.addEventListener('compositionstart', function () {
            vnode.inputLocking = true
        });
        input.addEventListener('compositionend', function () {
            vnode.inputLocking = false
            input.dispatchEvent(new Event('input'))
        });
        input.addEventListener("input", function (e) {
            if (!binding.value) binding.value = {};
            var msg = '请输入字母、数字、中划线或下划线！';
            let oldValue = input.value;
            let newValue = input.value;
            newValue = newValue.replace(/[^0-9/]/g, '');
            newValue = newValue.replace('/', '$#$').replace(/[/]/g, '').replace('$#$', '/');
            newValue = newValue.replace(eval('/^(\\d+)[/]{1}(\\d+)$/'), '$1/$2');
            if (newValue) {
                let arr = newValue.split('/')
                var sp = Number(arr[0]);
                if (sp > 300) sp = Number(arr[0].substr(0, arr[0].length - 1));
                var dp = null;
                if (arr.length > 1) {
                    var dp = Number(arr[1]);
                    if (dp > 300) dp = Number(arr[1].substr(0, arr[1].length - 1));
                    if (dp == 0) dp = "0";
                }
                newValue = sp + (dp == null ? "" : "/" + dp);
            }
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue;
                input.dispatchEvent(new Event('input')); // 通知v-model更新
            }
        });
        input.addEventListener('blur', function () {
            let oldValue = input.value;
            let newValue = input.value;
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue
                input.dispatchEvent(new Event('input')) // 通知v-model更新
            }
        });
    }
});

Vue.directive('phone', {
    inserted: function (el, binding, vnode) {
        let input = vnode.elm.getElementsByTagName("input")[0];
        input.addEventListener('compositionstart', function () {
            vnode.inputLocking = true
        });
        input.addEventListener('compositionend', function () {
            vnode.inputLocking = false
            input.dispatchEvent(new Event('input'))
        });
        input.addEventListener("input", function (e) {
            if (!binding.value) binding.value = {};
            var msg = '请输入正确的电话号码！';
            let oldValue = input.value;
            let newValue = input.value;
            newValue = newValue.replace(/[^\d\+\-]/g, '');
            if (newValue.length > 18) {
                newValue = newValue.substr(0, 18);
            }
            if (newValue.indexOf("+") > 0) {
                newValue = newValue.replace(/[\+]/g, '');
            }           
            newValue=newValue.replace(/(\-\-)/g, '-');
            
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue;
                input.dispatchEvent(new Event('input')); // 通知v-model更新
            }
        });
        input.addEventListener('blur', function () {
            let oldValue = input.value;
            let newValue = input.value;
            if (!binding.value) binding.value = {};
            var msg = '请输入正确的电话号码！';
            var mobile = /^((\+86)|(86))?\d{11}$/g;
            var tel_standard = /^(\d{4}|\d{3})-(\d{7,8})$/g;
            var tel_branch = /^(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})$/g;
            if (!mobile.test(newValue)
                && !tel_standard.test(newValue)
                && !tel_branch.test(newValue)) {
                newValue = '';
                vnode.context.$message({ type: 'warning', message: msg });
            }

            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue
                input.dispatchEvent(new Event('input')) // 通知v-model更新
            }
        });
    }
});

Vue.directive('labtest', {
    inserted: function (el, binding, vnode) {        
        let input = vnode.elm.getElementsByTagName("input")[0];
        input.addEventListener('compositionstart', function () {
            vnode.inputLocking = true
        });
        input.addEventListener('compositionend', function () {
            vnode.inputLocking = false
            input.dispatchEvent(new Event('input'))
        });
        input.addEventListener("input", function (e) {
            debugger;
            if (!binding.value) binding.value = {};
            let oldValue = input.value;
            let newValue = input.value;
            newValue = newValue.replace(/[^><0-9.\s]/g, '');
            if (input.maxLength) {
                newValue = newValue.substr(0, input.maxLength);
            }

            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue;
                input.dispatchEvent(new Event('input')); // 通知v-model更新
            }
        });
        input.addEventListener('blur', function () {
            let oldValue = input.value;
            let newValue = input.value;
            // 判断是否需要更新，避免进入死循环
            if (newValue !== oldValue) {
                input.value = newValue
                input.dispatchEvent(new Event('input')) // 通知v-model更新
            }
        });
    }
});