var statisticCard = {
    template: "\
    <el-card style='margin: 5px; height:calc((100vh - 140px) / 2);' shadow='hover'>\
        <div slot='header'>\
            <p><span class=''>{{chartOrder}}</span> {{chartTitle}}</p>\
        </div>\
        <div v-bind:id='chartDivId' style='height:calc((100vh - 330px) / 2);width:calc((100vw - 400px) / 2)'></div>\
    </el-card>\
    ",
    props: {
        chartOrder: {
            type: String,
        },
        chartTitle: {
            type: String,
        },
        chartSubtitle: {
            type: String,
        },
        chartUnit: {
            type: String,
        },
        reportType: {
            type: String,
        },
        chartData: {
            required: true,
        }
    },
    methods: {
        showChart: function () {
            let _this = this;
            if (this.chartData) {
                var series = [];
                for (var s in this.chartData.series) {
                    series.push({
                        name: this.chartData.series[s].name,
                        type: 'line',
                        data: this.chartData.series[s].data,
                        label: {
                            normal: {
                                show: true,
                            }
                        },
                        smooth: true
                    });
                }

                var chart = echarts.init(document.getElementById(this.chartDivId));
                chart.setOption({
                    title: {
                        subtext: this.chartSubtitle,
                        x: 'center'
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    xAxis: {
                        type: 'category',
                        data: this.chartData.categories,
                        axisLabel: {
                            interval: 0,
                            rotate: this.reportType == "2" ? 30 : 0,
                            formatter: function (value, index) {
                                var year = value.substring(0, 4);
                                var week = value.substring(4, 6);
                                return year + "年" + week + (_this.reportType == "2" ? "周" : "月");
                            }
                        }
                    },
                    yAxis: {
                        name: this.chartUnit,
                        type: 'value'
                    },
                    series: series
                });
            }
            else {

            }
        }
    },
    watch: {
        chartData: {
            handler: function (val, oldVal) {
                this.showChart();
            },
            deep: true
        },
    },
    computed: {
        chartDivId: function () {
            return this.chartSubtitle + 'chartDiv';
        }
    }
}