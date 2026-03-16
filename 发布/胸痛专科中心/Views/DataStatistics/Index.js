var app = new Vue({
    el: '#Statistic',
    mixins: [reportList],
    components: {
        'statistic-card': statisticCard,
    },
    data: function() {
        return {
            HospitalDict: Model.HospitalDict,
            statisticalType: Model.Report,
            reportList: [{ index:0, list:[]}],
            statisticalParams: {
                OrgCode: Model.DefaultOrgCode,
                StartDate: moment().subtract(5, 'months').toDate(),
                EndDate: moment().toDate(),
                StatusType: "1",
                ReportType: "1",
            },
            reportShow: {
                Measure: ['1', '2', '3']
            },
            currentIndex: 0,
            viewType: "list",
        };
    },
    methods: {
        formatReportList: function () {
            let result = [];
            if (this.statisticalType == "pci") {
                let item = { index: 0, list: []};
                for (i = 0; i < this.pciReportList.length; i++) {
                    if (item.list.length == 4) {
                        item.index = Math.floor((i - 1) / 4);
                        result.push(JSON.parse(JSON.stringify(item)));

                        item.list = []; // 清空
                    }

                    item.list.push(this.pciReportList[i]);
                }

                item.index = Math.floor((this.pciReportList.length - 1) / 4);
                result.push(JSON.parse(JSON.stringify(item)));
            } else if (this.statisticalType == "basic") {
                let newList = this.getBasicReportList();
                let item = { index: 0, list: [] };
                for (j = 0; j < newList.length; j++) {
                    if (item.list.length == 4) {
                        item.index = Math.floor((j - 1) / 4);
                        result.push(JSON.parse(JSON.stringify(item)));

                        item.list = []; // 清空
                    }

                    item.list.push(newList[j]);
                }
                item.index = Math.floor((newList.length - 1) / 4);
                result.push(JSON.parse(JSON.stringify(item)));
            }

            this.reportList = result;
        },
        setDateRange: function (num) {
            this.statisticalParams.StartDate = moment().subtract(num - 1, 'months').toDate();
            this.statisticalParams.EndDate = moment().toDate();
        },
        change: function (nindex, oindex) {
            if (oindex == -1) return;
            this.currentIndex = nindex;
            this.excuteQuery();
        },
        excuteQuery: function () {
            let _this = this;
            let p = JSON.parse(JSON.stringify(this.statisticalParams));
            for (let r in this.reportList) {
                let report = this.reportList[r];
                if (report.index == this.currentIndex) {
                    for (let l in report.list) {
                        let item = report.list[l];
                        setTimeout((function () {
                            return function () {
                                const loading = _this.$loading({
                                    target: "#card" + item.statisticName,
                                    lock: true,
                                });
                                p.Name = item.statisticName;
                                api.getReport(p).then(function (res) {
                                    if (res.data.IsSuccess) {
                                        item.data = res.data.Data.ReportView;
                                    }
                                    loading.close();
                                }).catch(function (error) {
                                    loading.close();
                                    console.log(error);
                                });
                            }
                        })(), 50);
                    }
                }
            }
        },
        showDetail: function(c) {
            let p = JSON.parse(JSON.stringify(this.statisticalParams));
            p.Name = c.statisticName;
            p.DetailTitle = c.chartTitle;
            p.Unit = c.statisticUnit;
            var detailParams = Qs.stringify(p, { arrayFormat: 'repeat' })
            window.open("/DataStatistics/Detail?" + detailParams);
        },
        showChang: function () {
            this.formatReportList();
            this.currentIndex = 0;
            this.excuteQuery();
        },
        changeView: function () {
            this.viewType = this.viewType == "chart" ? "list" : "chart";
            if (this.viewType == "chart") {
                this.excuteQuery();
            }
        },
        getBasicReportList: function() {
            let newList = [];
            for (let r in this.basicReportList) {
                let b = this.basicReportList[r];
                if (b.showType == '0' || this.reportShow.Measure.indexOf(b.showType) > -1) {
                    newList.push(JSON.parse(JSON.stringify(b)));
                }
            }
            return newList
        },
        rowsStyle: function (index) {
            var style = { 'margin-top': '5px', 'cursor': 'pointer' };
            if (index % 2 == 0) {
                return style;
            }
            style.background = 'azure';
            return style;
        }
    },
    watch: {
        statisticalParams: { // 条件发生变化是执行查询操作
            immediate: true,
            handler: function (val, oldVal) {
                this.excuteQuery();
            },
            deep: true
        },
    },
    mounted: function () {
        this.formatReportList();
        this.excuteQuery();
    },
    computed: {
        viewList: function () {
            if (this.statisticalType == "pci") return this.pciReportList;
            else if (this.statisticalType == "basic") return this.getBasicReportList();
        }
    }
})