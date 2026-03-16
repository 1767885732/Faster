var app = new Vue({
    el: '#Regist',
    components: {
        'aside-timeline': asideTimeline,
        'regist': regist,
        'admission': admission,
        'diagnosis': diagnosis,
        'discharge': discharge
    },
    data: {
        space_quarter: 6,
        space_half: 12,
        space_three_quater: 18,
        space_full: 24,
        gutter: 20,
        span: 6,
        activeName: 'first',
        registId: model.RegistId,
        isNew: model.IsNew,
        ehrId: model.EhrId,
        hospitalId: model.HospitalId,
        Editable: model.Editable,
        Commitable: model.Commitable,
        Printable: model.Printable,
        GenderDict: model.GenderDict,
        HospitalDict: model.HospitalDict,
        IdCardDict: model.IdCardDict,
        NationalityDict: model.NationalityDict,
        JobDict: model.JobDict,
        MarriageDict: model.MarriageDict,
        CultureDegreeDict: model.CultureDegreeDict,
        DiagnosisDict: model.DiagnosisDict,
        YesOrNoDict: model.YesOrNoDict,
        HaveOrNotDict: model.HaveOrNotDict,
        AsyncCount: 0,
        STEMI: '1',
        NSTEMI: '2',
        UA: '3',
        AD: '4',
        PE: '5',
        NACS: '6',
        NC: '7',
        OTHER: '9',
        accessSetting: { AccessUrl: [] },
        editForm:
        {
            Regist: { Id: model.RegistId, HospitalId: "", Name: "", Age: "", Gender: "", IdCareNo: "", PhoneNumber: "", DischargeTime: "", InpatientNo: "", OutpatientNo: "", Insurance: "", InsuranceNo: "", ConsciousnessType: "", TransHospitalName: "", Height: "", Weigth: "" },
            Admission: { DistressCaseDetail: [] },
            Treatment: {},
            TreatmentPci: { DelayReason: ['01'], Complication: [], CagPosition: [] },
            TreatmentPciPart: [{ HandleWay: [] }, { HandleWay: [] }, { HandleWay: [] }],
            Discharge: { Complication: [] },
            DischargeTest: {
                Troponin: { Category: "", Result: null },
                Bnp: { Category: "", Result: null },
                Tc: { Category: "", Result: null },
                Tg: { Category: "", Result: null },
                HdlC: { Category: "", Result: null },
                LdlC: { Category: "", Result: null }
            },
            DischargeDrugs: {},
            Diagnosis: { GraceRiskFactors: [], GraceVhrConditions: [], NacsType: [], NccpType: [] },
            DiagnosisTest: {
                TroponinList: [],
                Scr: { Result: null, Unit: "", Category: "" },
                Ddimer: { Result: null, Unit: "", Category: "" },
                Bnp: { Result: null, Unit: "", Category: "" },
                NtProbnp: { Result: null, Unit: "", Category: "" },
                Myo: { Result: null, Unit: "", Category: "" },
                Ckmb: { Result: null, Unit: "", Category: "" },
            },
            EcgList: [],
            MaxCategory: 0,
        },
        regionData: regionData,
        dialogVisible: false,
        CommitTips: [],
        tipVisible: false,
        tip: "",
        bindSource: null,
        gridData: null,
        options: {
            From: "0",
            OrgCode: "",
            DateRange: [moment().format('YYYY-MM-DD'), moment().format('YYYY-MM-DD')],
            DateFrom: moment().format('YYYY-MM-DD'),
            DateTo: moment().format('YYYY-MM-DD'),
            No: "",
            Name: ""
        },
    },
    methods: {
        load: function (registId) {
            var _this = this;
            const loading = this.$loading({
                lock: true,
                text: '正在加载患者病历，请您耐心等待……',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });

            api.load(registId).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.initView(_this, res.data.Data);
                    if (_this.registId) _this.refreshTimeline();
                    console.log("editForm", _this.editForm);
                }
                else {
                    _this.$message.error(res.data.Message);
                    console.log(res.data.Message);
                }
                loading.close();
            }).catch(function (error) {
                loading.close();
                console.log(error);
            });
        },
        extract: function (hospitalId, patientNo) {
            var _this = this;
            const loading = this.$loading({
                lock: true,
                text: 'Loading',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            api.loadByPatientNo(patientNo).then(function (res) {
                if (res.data.IsSuccess) {
                    var model = res.data.Data;
                    //初始化患者信息
                    _this.initView(_this, res.data.Data);
                    if (_this.registId) _this.refreshTimeline();
                    api.extract(_this.registId, hospitalId, patientNo).then(function (res) {
                        if (res.data.IsSuccess) {
                            var middle = res.data.Data;
                            //如果数据库患者信息不为空，用返回数据初始化患者信息
                            if (middle != null) {
                                _this.refreshRegist(middle.Regist);
                                _this.refreshAdmission(middle.Admission);
                                _this.refreshDiagnosis(middle.Diagnosis);
                                _this.refreshDiagnosisTest(middle.Diagnosis, middle.DiagnosisTest);
                                _this.refreshEcg(middle.EcgList);
                                _this.refreshTreatment(middle.Treatment);
                                _this.refreshTreatmentPci(middle.TreatmentPci);
                                _this.refreshDischarge(middle.Discharge);
                                _this.refreshDischargeTest(middle.DischargeTest);
                                _this.refreshDischargeDrug(middle.DischargeDrugs);
                            };
                        }
                    }).catch(function (error) {
                        console.log(error);
                    });
                }
                loading.close();
            }).catch(function (error) {
                loading.close();
                console.log(error);
            });
        },
        initView: function (_this, medical) {
            var _this = this;
            if (_this.registId == null || _this.registId == "") _this.registId = medical.Regist.Id;
            if (_this.ehrId == null || _this.ehrId == "") _this.ehrId = medical.Regist.PatientNo;
            _this.editForm.Regist = medical.Regist;
            _this.editForm.Admission = medical.Admission;
            _this.editForm.Diagnosis = medical.Diagnosis;
            _this.editForm.DiagnosisTest = medical.DiagnosisTest;
            _this.editForm.Treatment = medical.Treatment;
            _this.editForm.TreatmentPci = medical.TreatmentPci;
            _this.editForm.TreatmentPciPart = medical.TreatmentPciPart;
            _this.editForm.Discharge = medical.Discharge;
            _this.editForm.DischargeTest = medical.DischargeTest;
            _this.editForm.DischargeDrugs = medical.DischargeDrugs;
            _this.editForm.EcgList = medical.EcgList;
            _this.editForm.MaxCategory = medical.MaxCategory;

            _this.showAccessUrl();
        },
        onSave: function (isSubmit) {
            var _this = this;
            //var d = document.getElementsByClassName("el-form-item__error");
            //if (d && d.length > 0) {
            //    _this.$message.error('校验未通过，无法保存或提交');
            //    return false;
            //}
            const loading = this.$loading({
                lock: true,
                text: '正在保存患者病历，请您耐心等待……',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            var data = {
                Regist: _this.editForm.Regist,
                Admission: _this.editForm.Admission,
                Diagnosis: _this.editForm.Diagnosis,
                DiagnosisTest: _this.editForm.DiagnosisTest,
                Treatment: _this.editForm.Treatment,
                TreatmentPci: _this.editForm.TreatmentPci,
                TreatmentPciPart: _this.editForm.TreatmentPciPart,
                EcgList: _this.editForm.EcgList,
                Discharge: _this.editForm.Discharge,
                DischargeTest: _this.editForm.DischargeTest,
                DischargeDrugs: _this.editForm.DischargeDrugs
            }
            api.save(data).then(function (res) {
                if (res.data.IsSuccess) {
                    if (_this.isNew) {
                        _this.$message({
                            message: res.data.Message,
                            type: 'success',
                            onClose: function () {
                                window.location.href = "/DataManage/Regist?ehrId=&registId=" + res.data.Data;
                            }
                        });
                    }
                    else {
                        loading.close();
                        _this.refreshTimeline();
                        if (!!isSubmit && isSubmit == '1') {
                            api.commit(_this.editForm.Regist.Id).then(function (res) {
                                if (res.data.IsSuccess) {
                                    _this.editForm.Regist.IsCompleted = '1';
                                    _this.$message.success(res.data.Message);
                                } else {
                                    _this.CommitTips = res.data.Data;
                                    _this.tipVisible = true;
                                    //_this.$alert(res.data.Message, {
                                    //    dangerouslyUseHTMLString: true
                                    //});
                                    //_this.$message.error(res.data.Message);
                                }
                            }).catch(function (err) {
                                console.log(err);
                                _this.$message.error('连接服务器出错');
                            });
                        }
                        else {
                            _this.$message.success(res.data.Message);
                        }
                    }
                } else {
                    loading.close();
                    _this.$message.error(res.data.Message);
                }
            }).catch(function (err) {
                loading.close();
                console.log(err);
                _this.$message.error('连接服务器出错');
            });
        },
        // 点击绑定患者按钮
        onBind: function (bindSource) {
            // 显示绑定患者dialog
            this.dialogVisible = true;
            // 绑定患者来源为所有患者
            this.bindSource = bindSource;
            // 清空No
            this.options.No = "";

            this.options.Name = this.editForm.Regist.Name;
            this.options.OrgCode = this.editForm.Regist.HospitalId;

            if (bindSource != undefined && bindSource != null) {
                this.options.From = bindSource;

                // 门诊
                if (bindSource == "3") {
                    this.options.No = this.editForm.Admission.OutpatientNo;
                }
                    // 住院
                else if (bindSource == "0") {
                    this.options.No = this.editForm.Admission.InpatientNo;
                }
            }

            this.sourceFromChange();
        },
        onPrint: function () {
            window.open("/DataManage/ViewEmr?registId=" + this.registId);
        },
        // 改变患者来源
        sourceFromChange: function (val) {
            this.gridData = null;
        },
        // 搜索患者
        QueryIF: function () {
            var _this = this;
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
                text: '正在查询患者......',
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
        // 选中当前患者
        handleSelectionChange: function (selection) {
            this.selectedPatient = selection;
        },
        // 绑定患者
        doBind: function () {
            if (this.selectedPatient) {
                // 拉取患者
                this.selectedPatient.OrgCode = this.options.OrgCode;
                this.selectedPatient.From = this.options.From;
                var _this = this;
                axios.post("/DataManage/BindPatient", { patient: this.selectedPatient, ehrId: this.ehrId }).then(function (res) {
                    if (res.data.IsSuccess) {
                        var _data = res.data.Data;
                        model.EhrId = _data.PatientNo;
                        _this.ehrId = model.EhrId;
                        _this.editForm.Regist.PatientNo = _data.PatientNo;
                        if (_this.selectedPatient.From == "0") {
                            _this.editForm.Admission.InpatientSN = _data.UniqueNo;
                            _this.editForm.Admission.InpatientNo = _data.DisplayNo;
                        } else if (_this.selectedPatient.From == "3") {
                            _this.editForm.Admission.OutpatientSN = _data.UniqueNo;
                            _this.editForm.Admission.OutpatientNo = _data.DisplayNo;
                        }
                        _this.$message.success("绑定成功");
                        _this.dialogVisible = false;
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            } else {
                this.$message.error("请选择患者");
            }
        },
        onExtract: function () {
            var _this = this;
            var regist = _this.editForm.Regist;
            if (!regist.PatientNo) {
                _this.$message.error('需要先绑定患者，才能进行数据同步。');
                return;
            }
            const loading = this.$loading({ lock: true, text: '正在同步患者病历信息...', spinner: 'el-icon-loading', background: 'rgba(0, 0, 0, 0.7)' });
            //提取患者病历信息
            api.extract(this.registId, regist.HospitalId, regist.PatientNo).then(function (res) {
                if (res.data.IsSuccess) {
                    var middle = res.data.Data;
                    //如果数据库患者信息不为空，用返回数据初始化患者信息
                    if (middle != null) {
                        _this.refreshRegist(middle.Regist);
                        _this.refreshAdmission(middle.Admission);
                        _this.refreshDiagnosis(middle.Diagnosis);
                        _this.refreshDiagnosisTest(middle.Diagnosis, middle.DiagnosisTest);
                        _this.refreshEcg(middle.EcgList);
                        _this.refreshTreatment(middle.Treatment);
                        _this.refreshTreatmentPci(middle.TreatmentPci);
                        _this.refreshDischarge(middle.Discharge);
                        _this.refreshDischargeTest(middle.DischargeTest);
                        _this.refreshDischargeDrug(middle.DischargeDrugs);
                    };
                }
                loading.close();
            }).catch(function (error) {
                loading.close();
                console.log(error);
            });
        },
        refreshTimeline: function () {
            this.$refs.timeline.getTimeline();
        },
        showAccessUrl: function () {
            var _this = this;
            axios.get("/SystemSetting/GetAccessSetting", {}).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.accessSetting = res.data.Data;
                }
            }).catch(function (error) {
                console.log(error);
            });
        },
        formatAccessUrl: function (name, url) { // 院前电子病历
            var _this = this;
            const loading = this.$loading({
                lock: true,
                text: '正在获取调阅配置',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            api.getAccessUrl(_this.editForm.Regist.Id, escape(url)).then(function (res) {
                if (res.data.IsSuccess) {
                    var model = res.data.Data;
                    if (model && model.Url) {
                        window.open(model.Url);
                    }
                    else {
                        _this.$message.error(name + '地址获取失败！');
                    }
                }
                else {
                    _this.$message.error(res.data.Message);
                }
                loading.close();
            }).catch(function (error) {
                loading.close();
                _this.$message.error(name + '地址获取失败！');
                console.log(error);
            });

        },
        showEcg: function () {
            window.open("/ExtendView/Ecg?index=0&registId=" + this.editForm.Regist.Id);
        },
        showExamination: function () { // 检查
            window.open("/ExtendView/Examination?patientNo=" + this.editForm.Regist.PatientNo + "&org=" + this.editForm.Regist.HospitalId);
        },
        showInspection: function () {
            window.open("/ExtendView/Inspection?patientNo=" + this.editForm.Regist.PatientNo + "&org=" + this.editForm.Regist.HospitalId);
        },
        showMedication: function () {
            window.open("/ExtendView/Medication?patientNo=" + this.editForm.Regist.PatientNo + "&org=" + this.editForm.Regist.HospitalId);
        },
        //--------------------省市区-------------------
        selectedAttackAddress: function (value) {
            var _this = this;
            if (value && value[0]) {
                _this.editForm.Admission.AttackProvinceCode = value[0];
                _this.editForm.Admission.AttackProvince = CodeToText[value[0]]
            }
            if (value && value[1]) {
                _this.editForm.Admission.AttackCityCode = value[1];
                _this.editForm.Admission.AttackCity = CodeToText[value[1]]
            }
            if (value && value[2]) {
                _this.editForm.Admission.AttackAreaCode = value[2];
                _this.editForm.Admission.AttackArea = CodeToText[value[2]];
            }
        },
        //--------------用接口数据刷新画面------------
        refreshRegist: function (regist) {
            if (regist.Name) this.editForm.Regist.Name = regist.Name;
            if (regist.Age) this.editForm.Regist.Age = regist.Age;
            if (regist.Gender) this.editForm.Regist.Gender = regist.Gender;
            if (regist.IdCard) this.editForm.Regist.IdCard = regist.IdCard;
            if (regist.IdCardNo) this.editForm.Regist.IdCardNo = regist.IdCardNo;
            if (regist.PhoneNumber) this.editForm.Regist.PhoneNumber = regist.PhoneNumber;
            if (regist.Birthday) this.editForm.Regist.Birthday = moment(regist.Birthday).toDate();
            if (regist.ReceptionTime) this.editForm.Regist.ReceptionTime = moment(regist.ReceptionTime).toDate();
            if (regist.Nationality) this.editForm.Regist.Nationality = regist.Nationality;
            if (regist.Marriage) this.editForm.Regist.Marriage = regist.Marriage;
        },
        refreshAdmission: function (admission) {
            if (admission.AttackAddress) this.editForm.Admission.AttackAddress = admission.AttackAddress;
            if (admission.ComingType) this.editForm.Admission.ComingType = admission.ComingType;
            if (admission.AmbulanceDepartment) this.editForm.Admission.AmbulanceDepartment = admission.AmbulanceDepartment;
            if (admission.HelpTime) this.editForm.Admission.HelpTime = moment(admission.HelpTime).toDate();            
            //只有自行来院和院内发病才采集首次医疗接触时间
            if ((admission.ComingType == '3' || admission.ComingType == '4') && admission.FmcTime && !this.editForm.Admission.FmcTime) this.editForm.Admission.FmcTime = moment(admission.FmcTime).toDate();
            if (admission.AdmissionTime && !this.editForm.Admission.AdmissionTime) this.editForm.Admission.AdmissionTime = moment(admission.AdmissionTime).toDate();
            if (admission.ArrivedHospitalTime && !this.editForm.Admission.ArrivedHospitalTime) this.editForm.Admission.ArrivedHospitalTime = moment(admission.ArrivedHospitalTime).toDate();
            if (admission.AdmissionDoctor) this.editForm.Admission.AdmissionDoctor = admission.AdmissionDoctor;
            if (admission.ChiefComplaint) this.editForm.Admission.ChiefComplaint = admission.ChiefComplaint;
            if (admission.ConsciousnessType) this.editForm.Admission.ConsciousnessType = admission.ConsciousnessType;
            if (admission.RespirationRate) this.editForm.Admission.RespirationRate = admission.RespirationRate;
            if (admission.PulseRate) this.editForm.Admission.PulseRate = admission.PulseRate;
            if (admission.HeartRate) this.editForm.Admission.HeartRate = admission.HeartRate;
            if (admission.BloodPressure) this.editForm.Admission.BloodPressure = admission.BloodPressure;
            if (admission.Temperature) this.editForm.Admission.Temperature = admission.Temperature;

            if (admission.ArrivedEmergencyTime) this.editForm.Admission.ArrivedEmergencyTime = admission.ArrivedEmergencyTime;
            if (admission.LeaveEmergencyTime) this.editForm.Admission.LeaveEmergencyTime = admission.LeaveEmergencyTime;

            if (admission.ComingWay) this.editForm.Admission.ComingWay = admission.ComingWay;
            if (admission.IsTransHospital) this.editForm.Admission.IsTransHospital = admission.IsTransHospital;

            if (admission.IsReceiveEcg) this.editForm.Admission.IsReceiveEcg = admission.IsReceiveEcg;
            if (admission.IsRemoteEcgtran) this.editForm.Admission.IsRemoteEcgtran = admission.IsRemoteEcgtran;
            if (admission.TranTime) this.editForm.Admission.TranTime = admission.TranTime;

            if (admission.OutpatientNo) this.editForm.Admission.OutpatientNo = admission.OutpatientNo;
            if (admission.InpatientNo) this.editForm.Admission.InpatientNo = admission.InpatientNo;
        },
        refreshDiagnosis: function (diagnosis) {
            if (diagnosis.IsEcg) this.editForm.Diagnosis.IsEcg = diagnosis.IsEcg;
            if (diagnosis.EcgTime) this.editForm.Diagnosis.EcgTime = diagnosis.EcgTime;
            if (diagnosis.EcgDiagnosisTime) this.editForm.Diagnosis.EcgDiagnosisTime = diagnosis.EcgDiagnosisTime;
            if (diagnosis.IsTroponin) this.editForm.Diagnosis.IsTroponin = diagnosis.IsTroponin;
            if (diagnosis.IsScr) this.editForm.Diagnosis.IsScr = diagnosis.IsScr;
            if (diagnosis.IsDdimer) this.editForm.Diagnosis.IsDdimer = diagnosis.IsDdimer;
            if (diagnosis.IsBnp) this.editForm.Diagnosis.IsBnp = diagnosis.IsBnp;
            if (diagnosis.IsNtProbnp) this.editForm.Diagnosis.IsNtProbnp = diagnosis.IsNtProbnp;
            if (diagnosis.IsMyo) this.editForm.Diagnosis.IsMyo = diagnosis.IsMyo;
            if (diagnosis.IsCkmb) this.editForm.Diagnosis.IsCkmb = diagnosis.IsCkmb;
            if (diagnosis.DiagnosisTime) this.editForm.Diagnosis.DiagnosisTime = moment(diagnosis.DiagnosisTime).toDate();
            if (diagnosis.DiagnosisDoctor) this.editForm.Diagnosis.DiagnosisDoctor = diagnosis.DiagnosisDoctor;

            if (diagnosis.CtScanTime) this.editForm.Diagnosis.CtScanTime = moment(diagnosis.CtScanTime).toDate();
            if (diagnosis.CtReportTime) this.editForm.Diagnosis.CtReportTime = moment(diagnosis.CtReportTime).toDate();
            if (diagnosis.CheckResultTime) this.editForm.Diagnosis.CheckResultTime = moment(diagnosis.CheckResultTime).toDate();
            if (diagnosis.CduCheckTime) this.editForm.Diagnosis.CduCheckTime = moment(diagnosis.CduCheckTime).toDate();
        },
        refreshEcg: function (ecgs) {
            if (ecgs == null || ecgs.length == 0) return;
            ecgs.forEach(function (ecg) {
                ecg.CheckTime = moment(ecg.CheckTime).toDate();
                ecg.DiagnosisTime = moment(ecg.DiagnosisTime).toDate();
            });
            this.editForm.EcgList = ecgs;
        },
        refreshDiagnosisTest: function (diagnosis, diagnosisTests) {
            if (diagnosis.IsScr && diagnosisTests.Scr) this.editForm.DiagnosisTest.Scr = diagnosisTests.Scr;
            else this.editForm.DiagnosisTest.Scr.Unit = diagnosisTests.Scr.Unit;
            if (diagnosis.IsDdimer && diagnosisTests.Ddimer) this.editForm.DiagnosisTest.Ddimer = diagnosisTests.Ddimer;
            if (diagnosis.IsBnp && diagnosisTests.Bnp) this.editForm.DiagnosisTest.Bnp = diagnosisTests.Bnp;
            else this.editForm.DiagnosisTest.Bnp.Unit = diagnosisTests.Bnp.Unit;
            if (diagnosis.IsNtProbnp && diagnosisTests.NtProbnp) this.editForm.DiagnosisTest.NtProbnp = diagnosisTests.NtProbnp;
            else this.editForm.DiagnosisTest.NtProbnp.Unit = diagnosisTests.NtProbnp.Unit;
            if (diagnosis.IsMyo && diagnosisTests.Myo) this.editForm.DiagnosisTest.Myo = diagnosisTests.Myo;
            if (diagnosis.IsCkmb && diagnosisTests.Ckmb) this.editForm.DiagnosisTest.Ckmb = diagnosisTests.Ckmb;
            if (diagnosis.IsTroponin && diagnosisTests.TroponinList) {
                var ctns = this.editForm.DiagnosisTest.TroponinList;
                if (!ctns) {
                    ctns = diagnosisTests.TroponinList;
                    return;
                }
                if (!diagnosisTests.TroponinList || diagnosisTests.TroponinList.length < 1) {
                    return;
                }
                ctns.remove(function (o) {
                    return o.Source == '2' || (!o.Result && !o.SamplingTime && !o.ReportTime);
                });
                ctns = ctns.concat(diagnosisTests.TroponinList);
                ctns.sort(
                    function compare(s, t) {
                        if (s == null || s.SamplingTime == null) return 1;
                        if (t == null || t.SamplingTime == null) return -1;
                        var src = s.SamplingTime;
                        var tag = t.SamplingTime;
                        if (src < tag) {
                            return -1;
                        } else if (src > tag) {
                            return 1;
                        } else {
                            return 0;
                        }
                    }
                );
                this.editForm.DiagnosisTest.TroponinList = ctns;
            }
            
        },

        refreshTreatment: function (treatment) {

        },
        refreshTreatmentPci: function (pci) {
            if (pci.PatientArrivalTime) this.editForm.TreatmentPci.PatientArrivalTime = pci.PatientArrivalTime;
            if (pci.ActivateConduitTime) this.editForm.TreatmentPci.ActivateConduitTime = pci.ActivateConduitTime;
            if (pci.StartRadiographyTime) this.editForm.PcTreatmentPcii.StartRadiographyTime = pci.StartRadiographyTime;
            if (pci.EndRadiographyTime) this.editForm.TreatmentPci.EndRadiographyTime = pci.EndRadiographyTime;
            if (pci.AnticoagulationTime) this.editForm.TreatmentPci.AnticoagulationTime = pci.AnticoagulationTime;
            if (pci.StartPunctureTime) this.editForm.TreatmentPci.StartPunctureTime = pci.StartPunctureTime;
            if (pci.PunctureOkTime) this.editForm.TreatmentPci.PunctureOkTime = pci.PunctureOkTime;
            if (pci.BalloonExpansionTime) this.editForm.TreatmentPci.BalloonExpansionTime = pci.BalloonExpansionTime;
            if (pci.EndOperationTime) this.editForm.TreatmentPci.EndOperationTime = pci.EndOperationTime;
        },
        refreshDischarge: function (discharge) {
            if (discharge.Hod) this.editForm.Discharge.Hod = discharge.Hod;
            if (discharge.TotalCost) this.editForm.Discharge.TotalCost = discharge.TotalCost;
            if (discharge.CpDiagnosisCode) this.editForm.Discharge.CpDiagnosisCode = discharge.CpDiagnosisCode;
            if (discharge.DiagnosisTime) this.editForm.Discharge.DiagnosisTime = moment(discharge.DiagnosisTime).toDate();
            if (discharge.OutcomeCode) this.editForm.Discharge.OutcomeCode = discharge.OutcomeCode;
            if (discharge.LeaveTime) this.editForm.Discharge.LeaveTime = moment(discharge.LeaveTime).toDate();
            if (discharge.IsTroponin) this.editForm.Discharge.IsTroponin = discharge.IsTroponin;
            if (discharge.IsBnp) this.editForm.Discharge.IsBnp = discharge.IsBnp;
            if (discharge.IsTc) this.editForm.Discharge.IsTc = discharge.IsTc;
            if (discharge.IsTg) this.editForm.Discharge.IsTg = discharge.IsTg;
            if (discharge.IsHdlC) this.editForm.Discharge.IsHdlC = discharge.IsHdlC;
            if (discharge.IsLdlC) this.editForm.Discharge.IsLdlC = discharge.IsLdlC;

        },
        refreshDischargeTest: function (dischargeTest) {
            if (dischargeTest.Troponin) this.editForm.DischargeTest.Troponin = dischargeTest.Troponin;
            if (dischargeTest.Bnp) this.editForm.DischargeTest.Bnp = dischargeTest.Bnp;
            // if (dischargeTest.NtProbnp) this.editForm.DischargeTest.NtProbnp = dischargeTest.NtProbnp;

            if (dischargeTest.Tc) this.editForm.DischargeTest.Tc = dischargeTest.Tc;
            if (dischargeTest.Tg) this.editForm.DischargeTest.Tg = dischargeTest.Tg;
            if (dischargeTest.HdlC) this.editForm.DischargeTest.HdlC = dischargeTest.HdlC;
            if (dischargeTest.LdlC) this.editForm.DischargeTest.LdlC = dischargeTest.LdlC;
        },
        refreshDischargeDrug: function (dischargeDrug) {

        },
    },
    watch: {
        options: {
            handler: function (val, oldVal) {
                this.options.DateFrom = val.DateRange ? val.DateRange[0] : null;
                this.options.DateTo = val.DateRange ? val.DateRange[1] : null;
            },
            deep: true
        },
    },
    mounted: function () {
        if (this.registId) this.load(this.registId);
        else if (this.ehrId) this.extract(this.hospitalId, this.ehrId);
        else window.location.href = "/Error/Error";
    },
    computed: {
        disabledDischarge: function () {
            if (this.editForm.Admission.ComingType == '1'
                && this.editForm.Admission.IsTransHospital == '1')
                return true;
            if (this.editForm.Diagnosis.IsGiveupTreatment == '1')
                return true;
            if (this.editForm.Diagnosis.DiagnosisCode == this.NACS || this.editForm.Diagnosis.DiagnosisCode == this.NC || this.editForm.Diagnosis.DiagnosisCode == this.OTHER)
                return true;
            return false;
        },
        //----------------- 权限控制 -----------------
        showSave: function () {
            if (!this.Editable) return false;
            return (this.editForm.Regist.IsCompleted != '1' && this.editForm.Regist.IsFiled != '1');
        },
        showBind: function () {
            return this.showSave;
        },
        showSync: function () {
            return this.showSave && !StringKit.isNullOrEmpty(this.editForm.Regist.PatientNo);
        },
        showPrint: function () {
            if (!this.Printable) return false;
            return this.editForm.Regist.IsCompleted == '1';
        },
        showCommit: function () {
            if (!this.Commitable) return false;
            return (!!this.editForm.Regist.Id) && (this.editForm.Regist.IsCompleted != '1' && this.editForm.Regist.IsFiled != '1');
        },
        isBypassE: function () {
            return ((this.editForm.Admission.ComingWay == '1' || this.editForm.Admission.ComingWay == '2') && (this.editForm.Admission.IsBypassEmergency == '1'));
        },
        showPci: function () {
            return false;
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
    }
})
