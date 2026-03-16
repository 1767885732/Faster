//debugger;
var app = new Vue({
    el: '#index',
    data: {
        //权限配置
        model: Model,
        Initializable: Model.Initializable,//初始化
        Queryable: Model.Queryable,//查询
        Additable: Model.Additable,  //新增
        Viewable: Model.Viewable,  //查看
        Editable: Model.Editable,  //编辑
        Deletable: Model.Deletable,  //删除        
        Archivable: Model.Archivable,  //归档
        ArchivableAll: Model.ArchivableAll,  //归档
        Printable: Model.Printable, // 打印
        Exportable: Model.Exportable, // 导出
        Reportable: Model.Reportable, //上报

        GenderDict: Model.GenderDict,
        HospitalDict: Model.HospitalDict,
        ComingTypeDict: Model.ComingTypeDict,
        DiagnosisDict: Model.DiagnosisDict,
        DischargeDiagnosisDict: Model.DischargeDiagnosisDict,
        OutcomeDict: Model.OutcomeDict,
        YesOrNoDict: Model.YesOrNoDict,
        DefaultOrgCode: Model.DefaultOrgCode,
        // 查询条件
        searchParams: {
            page: 1,
            rows: 20,
            Name: "",
            DiagnosisType: "",
            ComingType: "",
            OrgCode: Model.DefaultOrgCode,
            DateRange: ["", ""],
            StartDate: '',
            EndDate: '',
            Type: "0",
        },
        advSearchParams: {
            page: 1,
            rows: 20,
            Name: "",
            Gender: "",
            IsGiveup: "",
            DiagnosisType: "",
            DischargeDiagnosisType: "",
            ComingType: "",
            OutcomeType: "",
            OrgCode: "",
            StartDate: '',
            EndDate: '',
            Type: "0",
        },
        dataTotal: 0,
        tableData: null,
        countList: [],
        multipleSelection: [],
        dialogVisible: false,
        timelineVisible: false,
        exportVisible: false,
        advSearchVisible: false,
        tipVisible: false,
        tipUplpadVisible: false,
        CommitTips: [],
        UploadTips: [],
        gridData: null,
        options: {
            From: "0",
            OrgCode: Model.DefaultOrgCode,
            DateRange: [moment().format('YYYY-MM-DD'), moment().format('YYYY-MM-DD')],
            DateFrom: moment().format('YYYY-MM-DD'),
            DateTo: moment().format('YYYY-MM-DD'),
            No: "",
            Name: ""
        },
        selectedPatient: null,
        timeline: { Nodes: [] },
        timelineHeader: { OutpatientNo: "", InpatientNo: "" },
        timelineHeaderExt: {},
        exportModel: {
            Name: "",
            DiagnosisType: "",
            DateRange: ["", ""],
            Start: "",
            End: "",
        },
        pickerOptions1: {
            disabledDate: function (time) {
                return time.getTime() > Date.now();
            },
            shortcuts: [{
                text: '今天',
                onClick: function (picker) {
                    picker.$emit('pick', new Date());
                }
            }, {
                text: '昨天',
                onClick: function (picker) {
                    const date = new Date();
                    date.setTime(date.getTime() - 3600 * 1000 * 24);
                    picker.$emit('pick', date);
                }
            }, {
                text: '一周前',
                onClick: function (picker) {
                    const date = new Date();
                    date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
                    picker.$emit('pick', date);
                }
            }]
        },
    },
    methods: {
        searchClick: function () {
            var _this = this;
            axios.get("/DataManage/Query", { params: this.searchParams }).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.tableData = res.data.Data.rows;
                    _this.dataTotal = res.data.Data.total;
                    _this.countList = res.data.Data.countList;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        advSearchClick: function () {
            //debugger;
            this.advSearchVisible = true;
            // 查询条件
            this.advSearchParams.page = this.searchParams.page;
            this.advSearchParams.rows = this.searchParams.rows;
            this.advSearchParams.Name = this.searchParams.Name;
            this.advSearchParams.DiagnosisType = this.searchParams.DiagnosisType;
            this.advSearchParams.ComingType = this.searchParams.ComingType;
            this.advSearchParams.OrgCode = this.searchParams.OrgCode;
            this.advSearchParams.DateRange = this.searchParams.DateRange;
            this.advSearchParams.StartDate = this.searchParams.StartDate;
            this.advSearchParams.EndDate = this.searchParams.EndDate;
            this.advSearchParams.Type = this.searchParams.Type;
        },
        doAdvSearch: function () {
            var _this = this;
            axios.get("/DataManage/Query", { params: this.advSearchParams }).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.tableData = res.data.Data.rows;
                    _this.dataTotal = res.data.Data.total;
                    _this.countList = res.data.Data.countList;
                    _this.advSearchVisible = false;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        timeFormat: function (row, column, cellValue, index) {
            if (cellValue) {
                var date = new moment(cellValue);
                return date.format("YYYY-MM-DD HH:mm");
            }
            return cellValue;
        },
        formatTime: function (value) {
            if (value) {
                var date = new moment(value);
                return date.format("YYYY-MM-DD HH:mm");
            }
            return value;
        },
        calcIntegrity: function (val) {
            if (val < 50) {
                return "exception"
            }
            else if (val < 80) {
                return "warning"
            }
            else if (val < 100) {
                return null
            } else if (val == 100) {
                return "success";
            }
        },
        handleSizeChange: function (pageSize) {
            this.searchParams.page = 1;
            this.searchParams.rows = pageSize;

            this.advSearchParams.page = 1;
            this.advSearchParams.rows = pageSize;
            this.searchClick();
        },
        handleCurrentChange: function (pageIndex) {
            this.searchParams.page = pageIndex;

            this.advSearchParams.page = pageIndex;
            this.searchClick();
        },
        handleView: function (index, row) {
            window.open("/DataManage/ViewEmr?registId=" + row.Id)
        },
        handleEdit: function (index, row) {
            window.open("/DataManage/Regist?registId=" + row.Id + "&ehrId=", "_blank")
        },
        handleDelete: function (index, row) {
            var _this = this;
            this.$confirm('此操作将删除该患者【' + row.Name + '】, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                axios.post("/DataManage/Delete", { registId: row.Id }).then(function (res) {
                    if (res.data.IsSuccess) {
                        _this.searchClick();
                        _this.$message({
                            type: 'success',
                            message: '删除成功!'
                        });
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            }).catch(function () {
            });
        },
        handleTimeline: function (index, row) {
            var _this = this;
            api.getTimeline(row.Id).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.timeline = res.data.Data.Timeline;
                    _this.timelineHeader = res.data.Data.Regist;
                    _this.timelineHeaderExt = res.data.Data.Diagnosis;
                    _this.timelineHeader.ReceptionTime = DateKit.toFormatDate(_this.timelineHeader.ReceptionTime);
                    _this.timelineHeader.OutpatientNo = res.data.Data.Admission.OutpatientNo;
                    _this.timelineHeader.InpatientNo = res.data.Data.Admission.InpatientNo;
                    var diagnosis = _this.DiagnosisDict.filter(function (x) {
                        return x.Key == _this.timelineHeader.Diagnosis;
                    });
                    _this.timelineHeader.Diagnosis = (diagnosis && diagnosis.length > 0) ? diagnosis[0].Value : "";
                    _this.timelineVisible = true;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        tabClick: function (tab, event) {
            this.searchParams.page = 1;
            this.advSearchParams.page = 1;
            this.searchParams.Type = tab.name;
            this.advSearchParams.Type = tab.name;
            this.searchClick();
        },
        addPatient: function () {
            this.dialogVisible = true;
        },
        handleMultipleSelection: function (val) {
            this.multipleSelection = val;
        },
        deletePatients: function () {
            if (this.multipleSelection.length > 0) {
                for (var i in this.multipleSelection) {
                    if (this.multipleSelection[i].IsFiled == "1") {
                        this.$message({
                            message: '选择的患者中包含已归档患者，无法删除。',
                            type: 'warning'
                        });
                        return;
                    }
                }

                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });

                var _this = this;
                this.$confirm('此操作将删除所选患者, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在删除患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/DeletePatients", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            _this.$message({
                                type: 'success',
                                message: '患者病历删除成功!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        deletePatientsHard: function () {
            if (this.multipleSelection.length > 0) {
                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });

                var _this = this;
                this.$confirm('此操作将删除所选患者无法恢复, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在删除患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/DeletePatientsHard", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            _this.$message({
                                type: 'success',
                                message: '患者病历删除成功!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        restorePatients: function () {
            if (this.multipleSelection.length > 0) {
                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });

                var _this = this;
                this.$confirm('此操作将恢复所选患者, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在恢复患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/RestorePatients", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            _this.$message({
                                type: 'success',
                                message: '患者病历恢复成功!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        refreshTimeline: function () {
            var _this = this;
            this.$confirm('此操作将更新所有患者时间轴, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                const loading = _this.$loading({
                    lock: true,
                    text: '正在初始化时间轴及完整度，请您耐心等待……',
                    spinner: 'el-icon-loading',
                    background: 'rgba(0, 0, 0, 0.7)'
                });
                axios.post("/DataManage/RefreshTimeline", {}).then(function (res) {
                    if (res.data.IsSuccess) {
                        _this.$message({
                            type: 'success',
                            message: '时间轴初始化完成!'
                        });
                    }
                    loading.close();
                }).catch(function (error) {
                    console.log(error);
                    loading.close();
                });
            }).catch(function () {
            });
        },
        undoCompletedPatients: function () {
            if (this.multipleSelection.length > 0) {
                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });

                var _this = this;
                this.$confirm('此操作将撤回已提交患者病历, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在撤回已提交患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/UndoCompletedPatients", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            _this.$message({
                                type: 'success',
                                message: '已提交患者病历撤回成功!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        archivedPatients: function () {
            if (this.multipleSelection.length > 0) {
                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });

                var _this = this;
                this.$confirm('此操作将归档所选患者, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在归档患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/ArchivedPatients", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            _this.$message({
                                type: 'success',
                                message: '患者病历归档成功!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        archivedPatientsAll: function () {
            var _this = this;
            this.$confirm('将归档所有已填报完成患者, 是否继续?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                const loading = _this.$loading({
                    lock: true,
                    text: '正在归档患者病历，请您耐心等待……',
                    spinner: 'el-icon-loading',
                    background: 'rgba(0, 0, 0, 0.7)'
                });
                axios.post("/DataManage/ArchivedPatientsAll", {}).then(function (res) {
                    if (res.data.IsSuccess) {
                        _this.searchClick();
                        _this.$message({
                            type: 'success',
                            message: '患者病历归档成功!'
                        });
                    }
                    loading.close();
                }).catch(function (error) {
                    loading.close();
                    console.log(error);
                });
            }).catch(function () {
            });
        },
        undoArchivedPatients: function () {
            if (this.multipleSelection.length > 0) {
                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });

                var _this = this;
                this.$confirm('此操作将撤销归档所选患者, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在撤销已归档患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/UndoArchivedPatients", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            _this.$message({
                                type: 'success',
                                message: '已归档患者病历撤销成功!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        uploadToCPA: function () {
            if (this.multipleSelection.length > 0) {
                var ids = this.multipleSelection.map(function (s) {
                    return s.Id;
                });
                this.UploadTips = this.multipleSelection.map(function (s) {
                    return { Id: s.Id, Name: s.Name };
                });
                var _this = this;

                this.$confirm('将上报所选患者病历到胸痛联盟, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(function () {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在上报患者病历，请您耐心等待……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    axios.post("/DataManage/UploadToCPA", { registIds: ids }).then(function (res) {
                        if (res.data.IsSuccess) {
                            _this.searchClick();
                            if (res.data.Data.Item1 == "0") {
                                if (res.data.Data.Item2) {
                                    _this.$message.success("患者病历上报成功!");
                                } else {
                                    _this.$message.error("患者病历上报失败!");
                                }
                            }
                            else if (res.data.Data.Item1 == "1") {
                                _this.tipUplpadVisible = true;
                                for (var i = 0; i < _this.UploadTips.length; i++) {
                                    _this.UploadTips[i].index = i + 1;
                                    _this.UploadTips[i].UploadStatus = res.data.Data.Item3[_this.UploadTips[i].Id].Key;
                                    _this.UploadTips[i].UploadStatusDesc = res.data.Data.Item3[_this.UploadTips[i].Id].Key ? "患者病历上报成功" : "患者病历上报失败";
                                    _this.UploadTips[i].Message = res.data.Data.Item3[_this.UploadTips[i].Id].Value.replace(/;/g, "\n").replace(/"/g, "");
                                }
                            }
                        }
                        else {
                            _this.searchClick();
                            _this.$message({
                                type: 'error',
                                message: '患者病历上报失败!'
                            });
                        }
                        loading.close();
                    }).catch(function (error) {
                        loading.close();
                        console.log(error);
                    });
                }).catch(function () {
                });
            }
        },
        sourceFromChange: function (val) {
            this.options.No = "";
            this.gridData = null;
        },
        QueryIF: function () {
            var _this = this;
            if (!this.options.OrgCode) {
                this.$message.error("请选择患者就诊医院！");
                return;
            }
            if (this.options.DateFrom == '' || this.options.DateTo == '') {
                this.$message.error("请输入查询时间范围！");
                return;
            }
            if (moment(this.options.DateTo) < moment(this.options.DateFrom)) {
                this.$message.error("开始时间不能小于结束时间！");
                return;
            }
            if (moment(this.options.DateTo).diff(moment(this.options.DateFrom), "days") > 1) {
                this.$message.error("查询时间范围不能超过2天！");
                return;
            }
            const loading = this.$loading({
                lock: true,
                text: '正在查询患者，请您耐心等待……',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            axios.get("/DataManage/QueryIF", { params: this.options }).then(function (res) {
                loading.close();
                if (res.data.IsSuccess) {
                    _this.gridData = res.data.Data;
                }
            }).catch(function (error) {
                loading.close();
                console.log(error);
            });
        },
        handleSelectionChange: function (selection) {
            this.selectedPatient = selection;
        },
        exportData: function () {
            this.exportVisible = true;
        },
        resetForm: function (formName) {
            this.$refs[formName].resetFields();
        },
        doExport: function () {
            const loading = this.$loading({
                lock: true,
                text: '正在导出患者病历，请您耐心等待……',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            var _this = this;
            axios.post("/DataManage/Export", { model: this.exportModel }).then(function (res) {
                loading.close();
                if (res.data.IsSuccess) {
                    window.location = "/DataManage/Download?file=" + res.data.Data;
                    _this.exportVisible = false;
                }
            }).catch(function (error) {
                loading.close();
                console.log(error);
            });
        },
        doAdd: function () {
            if (!this.options.OrgCode) {
                this.$message.error("请选择患者就诊医院！");
                return;
            }
            if (this.selectedPatient) {
                // 拉取患者
                this.selectedPatient.OrgCode = this.options.OrgCode;
                this.selectedPatient.From = this.options.From;
                var _this = this;
                axios.post("/DataManage/SyncPatient", { patient: this.selectedPatient }).then(function (res) {
                    if (res.data.IsSuccess) {
                        var ehrId = res.data.Data;
                        _this.dialogVisible = false;
                        window.open("/DataManage/Regist?registId=&ehrId=" + ehrId + "&hospitalId=" + _this.options.OrgCode, "_blank");
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            } else {
                this.$message.error("请选择患者");
            }
        },
        newPatient: function () {
            this.dialogVisible = false;
            window.open("/DataManage/CreateBlank", "_blank");
        },
        validateResult: function (id) {
            let _this = this;
            api.validate(id).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.$message.success("该患者病历已填报完整");
                } else {
                    _this.CommitTips = res.data.Data;
                    _this.tipVisible = true;
                }
            }).catch(function (err) {
                console.log(err);
                _this.$message.error('连接服务器出错');
            });
        },
        //显示控制
        //显示归档按钮
        showAdd: function () {
            if (!this.Additable) return false;
            return this.searchParams.Type == '0' || this.searchParams.Type == '1';
        },
        showEdit: function (row) {
            if (!this.Editable) return false;
            if (this.searchParams.Type == '0') {
                return row.IsCompleted != "1" && row.IsFiled != "1";
            }
            return this.searchParams.Type == '1';
        },
        showRefreshTimeline: function () {
            return this.Initializable && this.searchParams.Type == '1';
        },
        showView: function (row) {
            if (!this.Viewable) return false;
            if (this.searchParams.Type == '0') {
                return row.IsCompleted == "1" || row.IsFiled == "1";
            }
            return this.searchParams.Type == '2' || this.searchParams.Type == '3' || this.searchParams.Type == '4' || this.searchParams.Type == '5';
        },
        //显示归档按钮
        showArchive: function () {
            if (!this.Archivable) return false;
            return this.searchParams.Type == '2';
        },
        //显示归档按钮
        showArchiveAll: function () {
            if (!this.ArchivableAll) return false;
            return this.searchParams.Type == '2';
        },
        showRevokeArchive: function () {
            if (!this.Archivable) return false;
            return this.searchParams.Type == '3';
        },
        showDelete: function (row) {
            if (!this.Deletable) return false;
            if (row && this.searchParams.Type == '0') {
                return row.IsFiled != "1";
            }
            return this.searchParams.Type != '2' && this.searchParams.Type != '3' && this.searchParams.Type != '4' && this.searchParams.Type != '5';
        },
        showRestore: function () {
            if (!this.Deletable) return false;
            return this.searchParams.Type == '5';
        },
        showDeleteHard: function () {
            if (!this.Deletable) return false;
            return this.searchParams.Type == '5';
        },
        showExport: function () {
            if (!this.Exportable) return false;
            return this.searchParams.Type == '2' || this.searchParams.Type == '3' || this.searchParams.Type == '4';
        },
        showReport: function () {
            if (!this.Reportable) return false;
            return this.searchParams.Type == '2' || this.searchParams.Type == '3' || this.searchParams.Type == '4';
        },
    },
    watch: {
        //searchParams: {
        //    handler: function (val, oldVal) {
        //        this.searchParams.StartDate = val.DateRange ? val.DateRange[0] : null;
        //        this.searchParams.EndDate = val.DateRange ? val.DateRange[1] : null;
        //    },
        //    deep: true
        //},
        options: {
            handler: function (val, oldVal) {
                this.options.DateFrom = val.DateRange ? val.DateRange[0] : null;
                this.options.DateTo = val.DateRange ? val.DateRange[1] : null;
            },
            deep: true
        },
        exportModel: {
            handler: function (val, oldVal) {
                this.exportModel.Start = val.DateRange ? val.DateRange[0] : null;
                this.exportModel.End = val.DateRange ? val.DateRange[1] : null;
            },
            deep: true
        },
    },
    mounted: function () {
        this.searchClick(); // 初始化查询列表
    },
    computed: {
        countData: function () {
            var result = { Item1: 0, Item2: 0, Item3: 0, Item4: 0, Item5: 0, Item6: 0 };
            if (this.countList.length == 6) {
                result.Item1 = this.countList[0];
                result.Item2 = this.countList[1];
                result.Item3 = this.countList[2];
                result.Item4 = this.countList[3];
                result.Item5 = this.countList[4];
                result.Item6 = this.countList[5];
            }
            return result;
        },
        tableHeight: function () {
            var size = window.innerHeight - 192;
            return size;
        },
        showData: function () {
            var data = {
                noInput: "请输入住院号",
                noCol: "住院号",
                timeCol: "入院时间"
            };

            switch (this.options.From) {
                case "0":
                    data.noInput = "请输入住院号";
                    data.noCol = "住院号";
                    data.timeCol = "入院时间";
                    break;
                case "1":
                    data.noInput = "";
                    data.noCol = "";
                    data.timeCol = "接诊时间";
                    break;
                case "2":
                    data.noInput = "请输入主诉（或诊断）";
                    data.noCol = "急诊号";
                    data.timeCol = "分诊时间";
                    break;
                case "3":
                    data.noInput = "请输入门诊号";
                    data.noCol = "门诊号";
                    data.timeCol = "挂号时间";
                    break;
            }

            return data;
        },
        timenodes: function () {
            var nodes = [];
            if (this.timeline.Nodes) {
                this.timeline.Nodes.forEach(function (n) {
                    var description = new Array();
                    if (n.Description) {
                        var arr = n.Description.split("~");
                        if (arr.length > 0) {
                            for (var i = 0; i < arr.length; i++) {
                                var color = "timeline_" + arr[i].split("|")[0];
                                var desc = arr[i].split("|")[1];
                                description.push({ desc: desc, color: color });
                            }
                        }
                    }
                    nodes.push({ name: n.Name, time: moment(n.Value).format('YYYY-MM-DD HH:mm'), description: description })
                });
            }
            return nodes;
        }
    }
})
