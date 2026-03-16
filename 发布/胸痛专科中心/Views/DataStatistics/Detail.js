var app = new Vue({
    el: '#report',
    mixins: [reportList],
    components: {
        'reprot-datatable': reprotDatatable,
        'reprot-percent-datatable': reprotPercentDatatable
    },
    data: function () {
        return {
            detailModel: Model,
            reportInfo: null,
            reportTable: "",
            detailTable: "",
            reportData: [],
            reportPercentData: []
        };
    },
    methods: {
        showChart: function(data) {
            if (data) {
                var _this = this;

                var series = [];
                for (var s in data.series) {
                    series.push({
                        name: data.series[s].name,
                        type: 'line',
                        data: data.series[s].data,
                        label: {
                            normal: {
                                show: true,
                            }
                        },
                        smooth: true
                    });
                }

                var chart = echarts.init(document.getElementById("chart_main"));
                chart.setOption({
                    title: {
                        text: this.detailModel.DetailTitle,
                        x: 'center'
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    toolbox: {
                        show: true,
                        itemSize: 20,
                        itemGap: 30,
                        //orient:'vertical',
                        feature: {
                            dataZoom: {
                                yAxisIndex: 'none'
                            },
                            magicType: { type: ['line', 'bar'] },
                            saveAsImage: {
                                show: true,
                                excludeComponents: ['toolbox'],
                                pixelRatio: 2
                            },
                            restore: {},
                        },
                        right:30,
                    },
                    xAxis: {
                        type: 'category',
                        data: data.categories,
                        axisLabel: {
                            interval: 0,
                            rotate: this.detailModel.ReportType == "2" ? 30 : 0,
                            formatter: function (value, index) {
                                var year = value.substring(0, 4);
                                var week = value.substring(4, 6);
                                return year + "年" + week + (_this.detailModel.ReportType == "2" ? "周" : "月");
                            }
                        }
                    },
                    yAxis: {
                        name: this.detailModel.Unit,
                        type: 'value'
                    },
                    series: series
                });
            }
        },
        getDetailReport: function (selectedTime) {
            var _this = this;
            let p = JSON.parse(JSON.stringify(this.detailModel));
            p.SelectedTime = selectedTime;
            api.getReportDetail(p).then(function (res) {
                if (res.data.IsSuccess) {
                    if (_this.detailModel.Unit == "%") _this.ShowPercentDetailTable(res.data.Data, selectedTime);
                    else _this.ShowDetailTable(res.data.Data, selectedTime);
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        ShowDetailTable: function (data, selectedTime) {
            var effectiveCount = 0;
            var invalidCount = 0;
            for (var item in data) {
                if (data[item].IsValid == 'FALSE') {
                    invalidCount++;
                }
                else {
                    effectiveCount++;
                }
            }
            var table = "<table class='basic-table'>";
            table += "<tr >";
            table += "<td  >" + selectedTime + "</td> ";
            if (effectiveCount > 0) { table += "<td  colspan='" + effectiveCount + "'></td> "; }
            if (invalidCount > 0) { table += "<td style='color:red;font-weight:bold;'  colspan='" + invalidCount + "'>无效数据</td> "; }

            table += "</tr>";
            table += "<tr>";
            table += "<th  style='width: 15%;'>姓名</th> ";
            for (var o in data) {
                if (o >= effectiveCount) { table += "<th><a style='color:red;font-weight:bold;' data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></th>"; }
                else {
                    table += "<th><a data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></th>";
                }
            }
            table += "</tr>";
            table += "<tr>";
            table += "<td   style='width: 15%;'>" + this.reportInfo.statisticDetail + "</td> ";
            for (var o in data) {
                table += "<td >" + (data[o].IsValid == 'TRUE' ? data[o].Result : "") + "</td>";
            }
            table += "</tr>";
            table += "</table > ";
            this.detailTable = table;

        },
        ShowPercentDetailTable: function (data, selectedTime) {
            var effectiveCount = 0;
            var invalidCount = 0;
            for (var item in data) {
                if (data[item].IsValid == 'FALSE') {
                    invalidCount++;
                }
                else {
                    effectiveCount++;
                }
            }
            var table = "<table class='basic-table'>";
            table += "<tr >";
            table += "<td  >" + selectedTime + "</td> ";
            if (effectiveCount > 0) { table += "<td  colspan='" + effectiveCount + "'>有效数据</td> "; }
            if (invalidCount > 0) {
                table += "<td style='color:red;font-weight:bold;' colspan='" + invalidCount + "'>无效数据</td> ";
            }

            table += "</tr>";
            table += "<tr>";
            table += "<td  style='width: 15%;'>姓名</td> ";
            for (var o in data) {
                if (o >= effectiveCount) { table += "<td><a style='color:red;font-weight:bold;'  data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></td>"; }
                else { table += "<td><a data-registerid='" + data[o].ID + "'>" + data[o].Name + "</a></td>"; }
            }
            table += "</tr>";
            table += "<tr>";
            table += "<td  style='width: 15%;'>" + this.reportInfo.statisticDetail + "</td> ";
            for (var o in data) {
                table += "<td >" + (data[o].Result != null ? data[o].Result : "") + "</td>";
            }

            table += "</tr></table>";
            this.detailTable = table;
        }
    },
    mounted: function () {
        // 获取当前报表的信息
        let list = this.detailModel.Report == 'pci' ? this.pciReportList: this.basicReportList;
        for (let r in list) {
            if (list[r].statisticName == this.detailModel.Name)
                this.reportInfo = list[r];
        }

        var _this = this;
        api.getReport(this.detailModel).then(function (res) {
            if (res.data.IsSuccess) {
                _this.showChart(res.data.Data.ReportView)
                if (_this.detailModel.Unit == "%") {
                    _this.reportPercentData = res.data.Data.Data;
                    console.log(_this.reportPercentData)
                }
                else {
                    _this.reportData = res.data.Data.Data;
                }
            }
        }).catch(function (error) {
            console.log(error);
        });
    }
})