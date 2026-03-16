var app = new Vue({
    el: '#setting',
    data: function () {
        return {
            currentMenuIndex: 1,
            accessSetting: { PreHospitalUrl: "", EmergencyUrl: "", AccessUrl: [] },
            generalSetting: { MatchComingTypeEnabled: false, UploadType: "" },
            otherSetting: { AcsDrugDose: "", AcsDrugType: "", AspirinDose: "", NetHospital: [] },
            workstationSetting: { Enabled: false, AreaMapping: [] },
            AcsDrugTypeDict: [],
            UserNamesDict: [],
            AnticoagulationUnitDict: [],
            DeptNamesDict: [],
            CommitStatusDict: [{ Key: "1", Value: "填报中" },
                           { Key: "2", Value: "提交审核" },
                           { Key: "3", Value: "已审核" },
                           { Key: "4", Value: "已存档" }],
            regionData: regionData,
        }
    },
    methods: {
        onMenuSelected: function (key, keyPath) {
            this.currentMenuIndex = key;
        },
        getAccessSetting: function () {
            var _this = this;
            axios.get("/SystemSetting/GetAccessSetting", {}).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.accessSetting = res.data.Data;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        setAccessSetting: function () {
            var _this = this;
            console.log(this.accessSetting)
            axios.post("/SystemSetting/SetAccessSetting", { setting: this.accessSetting }).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.$message.success("保存成功");
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        getGeneralSetting: function () {
            var _this = this;
            axios.get("/SystemSetting/GetGeneralSetting", {}).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.generalSetting = res.data.Data.Setting;
                    _this.UserNamesDict = res.data.Data.UserNamesDict;
                    _this.DeptNamesDict = res.data.Data.DeptNamesDict;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        setGeneralSetting: function () {
            var _this = this;
            console.log(this.generalSetting)
            axios.post("/SystemSetting/SetGeneralSetting", { setting: this.generalSetting }).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.$message.success("保存成功");
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        getOtherSetting: function () {
            var _this = this;
            axios.get("/SystemSetting/GetOtherSetting", {}).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.otherSetting = res.data.Data.Setting;
                    _this.AcsDrugTypeDict = res.data.Data.AcsDrugTypeDict;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        setOtherSetting: function () {
            var _this = this;
            console.log(this.otherSetting)
            axios.post("/SystemSetting/SetOtherSetting", { setting: this.otherSetting }).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.$message.success("保存成功");
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        getWorkStationSetting: function () {
            var _this = this;
            axios.get("/SystemSetting/GetWorkStationSetting", {}).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.workstationSetting = res.data.Data.Setting;
                    _this.UserNamesDict = res.data.Data.UserNamesDict;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        setWorkStationSetting: function () {
            var _this = this;
            console.log(this.workstationSetting)
            axios.post("/SystemSetting/SetWorkStationSetting", { setting: this.workstationSetting }).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.$message.success("保存成功");
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        addAreaMapping: function () {
            this.workstationSetting.AreaMapping.push({ AreaName: null, AreaCode: null, User: null });
        },
        removeAreaMapping: function (index) {
            this.workstationSetting.AreaMapping.splice(index, 1);
        },
        addNetHospital: function () {
            this.otherSetting.NetHospital.push({ Name: null });
        },
        removeNetHospital: function (index) {
            this.otherSetting.NetHospital.splice(index, 1);
        },
        addAnticoagulation: function () {
            this.otherSetting.Anticoagulation.push({ Unit: null });
        },
        removeAnticoagulation: function (index) {
            this.otherSetting.Anticoagulation.splice(index, 1);
        },
        addAccessUrl: function () {
            this.accessSetting.AccessUrl.push({ Name: null, Url: null });
        },
        removeAccessUrl: function (index) {
            this.accessSetting.AccessUrl.splice(index, 1);
        },
        queryUserNames: function (queryString, cb) {
            var userNames = this.UserNames;
            var results = queryString ? userNames.filter(function (name) {
                return (name.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            }) : userNames;
            // 调用 callback 返回建议列表的数据
            cb(results);
        },
        queryDeptNames: function (queryString, cb) {
            var deptNames = this.DeptNames;
            var results = queryString ? deptNames.filter(function (name) {
                return (name.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            }) : deptNames;
            // 调用 callback 返回建议列表的数据
            cb(results);
        },
        selectedAttackAddress: function (value) {
            var _this = this;
            if (value && value[0]) {
                _this.otherSetting.AttackProvinceCode = value[0];
                _this.otherSetting.AttackProvince = CodeToText[value[0]]
            }
            if (value && value[1]) {
                _this.otherSetting.AttackCityCode = value[1];
                _this.otherSetting.AttackCity = CodeToText[value[1]]
            }
            if (value && value[2]) {
                _this.otherSetting.AttackAreaCode = value[2];
                _this.otherSetting.AttackArea = CodeToText[value[2]];
            }
        },
        showHint: function (type) {
            if (type == 'ws-hospital-mapping') {

            }
        }
    },
    mounted: function () {
        this.getAccessSetting();
        this.getGeneralSetting();
        this.getWorkStationSetting();
        this.getOtherSetting();
    },
    computed: {
        UserNames: function () {
            var userNames = [];
            if (this.UserNamesDict == null) return userNames;
            this.UserNamesDict.forEach(function (n) {
                userNames.push({ "value": n.LoginName });
            });
            return userNames;
        },
        DeptNames: function () {
            var admissionDepts = [];
            if (this.DeptNamesDict == null) return admissionDepts;
            this.DeptNamesDict.forEach(function (n) {
                admissionDepts.push({ "value": n.Name });
            });
            return admissionDepts;
        },
        selectedOptions: {
            get: function () {
                return [this.otherSetting.AttackProvinceCode, this.otherSetting.AttackCityCode, this.otherSetting.AttackAreaCode];
            },
            set: function (val) {

            }
        },
    }
});