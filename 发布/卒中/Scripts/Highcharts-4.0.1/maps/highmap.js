$(function () {

    //Highcharts.setOptions({
    //    lang: {
    //        drillUpText: "返回 > {series.name}"
    //    }
    //});
    var mapGeoJSON = Highcharts.maps['countries/cn/custom/cn-all-china'];
    var data = Highcharts.geojson(mapGeoJSON), small = $('#container').width() < 400;
    // 给城市设置随机数据
    $.each(data, function (i) {
        this.drilldown = this.properties['drill-key'];
        this.value = i;
    });
    function getPoint(e) {
        console.log(e.point.name);
    }
    function getShow(e) {
        alert(1);
    }
    //初始化地图
    $('#container').highcharts('Map', {
        chart: {
            spacingBottom: 30,

            events: {
                drilldown: function (e) {
                    if (e.point.name == "Jiangsu" || e.point.name == "Fujian" || e.point.name == "Guangdong" || e.point.name == "Hubei" || e.point.name == "Heilongjiang") {
                        window.location = "Hospital?gbcode=" + e.point.properties["postal-code"];
                    }
                },
                drillup: function () {
                    this.setTitle(null, { text: '中国' });
                }
            }
        },
        tooltip: {
            enabled: false
        },
        credits: {
            enabled: false
        },
        title: {
            enabled: false,
            text: ''
        },

        legend: small ? {} : {
            enabled: false,
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle'
        },
        //colorAxis: {
        //    min: 0,
        //    //minColor: '#E6E7E8',
        //    maxColor: '#005645',
        //    labels: {
        //        style: {
        //            "color": "red", "fontWeight": "bold"
        //        }
        //    }
        //},
        mapNavigation: {
            enabled: false,
            buttonOptions: {
                verticalAlign: 'bottom'
            }
        },
        //plotOptions: {
        //    map: {
        //        states: {
        //            hover: {
        //                color: '#EEDD66'
        //            }
        //        }
        //    }
        //},
        series: [{
            data: data,
            name:"中国",
            dataLabels: {
                enabled: true,
                borderWidth: 6,                                            //设置边框线宽
                borderColor: 'black',                                            //设置边框颜色
                shape: 'circle',                                                       //设置边框形状
                backgroundColor:'black',                                   //设置填充颜色
                formatter: function () {
                    var cnname = "";
                    if (this.point.name == "Jiangsu" || this.point.name == "Fujian" || this.point.name == "Guangdong" || this.point.name == "Hubei" || this.point.name == "Heilongjiang") {
                        cnname = this.point.properties["cn-name"];
                    }
                    return cnname;
                }
            }
        }],
        drilldown: {
            activeDataLabelStyle: {
                color: '#FFFFFF',
                textDecoration: 'none',
                textShadow: '0 0 3px #000000'
            },
            drillUpButton: {
                relativeTo: 'spacingBox',
                position: {
                    x: 0,
                    y: 60
                }
            }
        }
    });
});

var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
var base64DecodeChars = new Array(
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
    52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
    -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
    -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);


function base64decode(str) {
    var c1, c2, c3, c4;
    var i, len, out;

    len = str.length;
    i = 0;
    out = "";
    while (i < len) {
        /* c1 */
        do {
            c1 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
        } while (i < len && c1 == -1);
        if (c1 == -1)
            break;

        /* c2 */
        do {
            c2 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
        } while (i < len && c2 == -1);
        if (c2 == -1)
            break;

        out += String.fromCharCode((c1 << 2) | ((c2 & 0x30) >> 4));

        /* c3 */
        do {
            c3 = str.charCodeAt(i++) & 0xff;
            if (c3 == 61)
                return out;
            c3 = base64DecodeChars[c3];
        } while (i < len && c3 == -1);
        if (c3 == -1)
            break;

        out += String.fromCharCode(((c2 & 0XF) << 4) | ((c3 & 0x3C) >> 2));

        /* c4 */
        do {
            c4 = str.charCodeAt(i++) & 0xff;
            if (c4 == 61)
                return out;
            c4 = base64DecodeChars[c4];
        } while (i < len && c4 == -1);
        if (c4 == -1)
            break;
        out += String.fromCharCode(((c3 & 0x03) << 6) | c4);
    }
    return out;
}