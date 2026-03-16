var app = new Vue({
    el: '#EmergencyWS',
    components: {

    },
    data: {
        space_quarter: 6,
        space_half: 12,
        space_three_quater: 18,
        space_full: 24,
        gutter: 20,
        span: 6,
        activeName: 'first',
        userId: model.UserId,
        registId: model.RegistId,
        patient: model.Patient,
        timeline: model.Timeline,
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
        DistressCaseDict: model.DistressCaseDict,
        AcsDrugTypeDict: model.AcsDrugTypeDict,
        KillipLevelDict: model.KillipLevelDict,
        ComingDict: model.ComingDict,
        rules: {
            //'Age': [
            //    { pattern: /^[1-9]\d*$/, message: '请输入正确的年龄' }
            //],
            'PhoneNumber': [
                { pattern: /(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}/, message: '请输入正确的联系电话' }
            ],
            'IdCardNo': [
                { pattern: /(^\d{15}$)|(^\d{17}([0-9]|X|x)$)/, message: '请输入正确的身份证' }
            ],
            'AspirinDose': [
                { pattern: /^([1-9]\d*|0)(\.\d{1,2})?$/, message: '请输入正确的给药量' }
            ],
            'AcsDrugDose': [
                { pattern: /^([1-9]\d*|0)(\.\d{1,2})?$/, message: '请输入正确的给药量' }
            ],
            'IsNoticeImcd': [
                { required: true, message: '请选择是否通知心内科会诊', trigger: 'change' }
            ],
            'DiagnosisType': [
                { required: true, message: '请选择诊断类型', trigger: 'change' }
            ],
        }
    },
    methods: {
        onSave: function (isSubmit) {
            var _this = this;
            //var d = document.getElementsByClassName("el-form-item__error");
            //if (d && d.length > 0) {
            //    _this.$message('校验未通过，无法保存或提交');
            //    return false;
            //}
            this.$refs['registForm'].validate(function(valid){
                if (valid) {
                    const loading = _this.$loading({
                        lock: true,
                        text: '正在保存患者信息，请您耐性等候……',
                        spinner: 'el-icon-loading',
                        background: 'rgba(0, 0, 0, 0.7)'
                    });
                    api.push(_this.userId, _this.patient).then(function (res) {
                        if (res.data.IsSuccess) {
                            loading.close();
                            _this.$message.success("患者保存成功。");
                            //_this.refreshTimeline();
                        } else {
                            loading.close();
                            _this.$message.error(res.data.Message);
                        }
                    }).catch(function (err) {
                        loading.close();
                        console.log(err);
                        _this.$message.error('连接服务器出错！');
                    });
                } else {
                    _this.$message.error('校验未通过，无法保存或提交');
                    return false;
                }
            });

        },
        refreshTimeline: function () {
            this.$refs.timeline.getTimeline();
        },
        formatModel: function (model) {
            if (!!model.AttackTime) model.AttackTime = moment(model.AttackTime).toDate();
            if (!!model.ArrivedHospitalTime) model.ArrivedHospitalTime = moment(model.ArrivedHospitalTime).toDate();
            if (!!model.AdmissionTime) model.AdmissionTime = moment(model.AdmissionTime).toDate();
            if (!!model.EcgTime) model.EcgTime = moment(model.EcgTime).toDate();
            if (!!model.FmcTime) model.FmcTime = moment(model.FmcTime).toDate();
            if (!!model.EcgDiagnosisTime) model.EcgDiagnosisTime = moment(model.EcgDiagnosisTime).toDate();
            if (!!model.ConsultationNotifyTime) model.NoticeImcdTime = moment(model.NoticeImcdTime).toDate();
            if (!!model.AspirinTime) model.AcsDeliveryTime = moment(model.AcsDeliveryTime).toDate();
            if (!!model.DiagnosisTime) model.DiagnosisTime = moment(model.DiagnosisTime).toDate();
            return model;
        },
        now: function (name) {
            var _this = this;
            if (name == "AspirinTime") {
                _this.patient.AspirinTime = moment().toDate();
            } else if (name == "ConsultationNotifyTime") {
                _this.patient.ConsultationNotifyTime = moment().toDate();
            } else if (name == "DiagnosisTime") {
                _this.patient.DiagnosisTime = moment().toDate();
            }
        },
        onDateSelected: function (val) {
            formatDateTime(val);
        }
    },
    mounted: function () {
        var _this = this;
        _this.patient = _this.formatModel(_this.patient);
    },
    computed: {
        timenodes: function () {
            var nodes = [];
            if (this.timeline.Nodes) {
                this.timeline.Nodes.forEach(function (n) {
                    nodes.push({ name: n.Name, time: moment(n.Value).format('YYYY-MM-DD HH:mm:ss') })
                });
            }
            return nodes;
        }
    }
})
