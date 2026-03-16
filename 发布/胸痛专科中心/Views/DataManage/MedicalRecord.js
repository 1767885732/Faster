var app = new Vue({
    el: '#medicalrecord',
    components: {
        'aside-timeline': asideTimeline,
    },
    data: {
        space_quarter: 6,
        space_half: 12,
        space_three_quater: 18,
        space_full: 24,
        gutter: 20,
        span: 6,
        emr: model,
        registId: model.RegistId,
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
        DiagnosisDict: model.DiagnosisDict,
        AttackZoneDict: model.AttackZoneDict,
        InsuranceDict: model.InsuranceDict,
        SenseDict: model.SenseDict,
        DistressCaseDict: model.DistressCaseDict,
        DistressCaseDetailDict: model.DistressCaseDetailDict,
        DiagnosisDict: model.DiagnosisDict,
        TransDiagnosisDict: model.TransDiagnosisDict,
        TransferTypeDict: model.TransferTypeDict,
        RemoteEcgtranWayDict: model.RemoteEcgtranWayDict,
        ComingDict: model.ComingDict,
        AmbulanceDepartmentDict: model.AmbulanceDepartmentDict,
        BypassEmergencyDict: model.BypassEmergencyDict,
        ThrombolyticScreeningDict: model.ThrombolyticScreeningDict,
        FaThrombolyticPlacegDict: model.FaThrombolyticPlacegDict,
        AcsDrugTypeDict: model.AcsDrugTypeDict,
        TransferDestinationDict: model.TransferDestinationDict,
        ThrombolyticDrugTypeDict: model.ThrombolyticDrugTypeDict,
        ThrombolyticDrugCodeDict: model.ThrombolyticDrugCodeDict,
        PciPlaceDict: model.PciPlaceDict,
        DiagnosisDict: model.DiagnosisDict,
        KillipLevelDict: model.KillipLevelDict,
        StemiReperfusionStrategyDict: model.StemiReperfusionStrategyDict,
        NstemiReperfusionStrategyDict: model.NstemiReperfusionStrategyDict,
        PerfusionMeasureDict: model.PerfusionMeasureDict,
        GraceEstimateDict: model.GraceEstimateDict,
        GraceHighriskConditionDict: model.GraceHighriskConditionDict,
        GraceHighriskLaminationDict: model.GraceHighriskLaminationDict,
        NyhaDict: model.NyhaDict,
        InterlayerTypeDict: model.InterlayerTypeDict,
        TreatmentStrategyDict: model.TreatmentStrategyDict,
        HighriskLaminationDict: model.HighriskLaminationDict,
        NacsTypeDict: model.NacsTypeDict,
        NccpTypeDict: model.NccpTypeDict,
        TroponinDict: model.TroponinDict,
        CtnUnitDict: model.CtnUnitDict,
        PositiveOrNotDict: model.PositiveOrNotDict,
        AcsDrugTypeDict: model.AcsDrugTypeDict,
        AnticoagulationDrugDict: model.AnticoagulationDrugDict,
        IhThrombolyticPlaceDict: model.IhThrombolyticPlaceDict,
        ThrombolyticDrugTypeDict: model.ThrombolyticDrugTypeDict,
        ThrombolyticDrugCodeDict: model.ThrombolyticDrugCodeDict,
        RemoteEcgtranWayDict: model.RemoteEcgtranWayDict,
        MyoUnitDict: model.MyoUnitDict,
        DdimerUnitDict: model.DdimerUnitDict,
        CkmbUnitDict: model.CkmbUnitDict,
        ConsultationTypeDict: model.ConsultationTypeDict,
        GraceRehazardLevelDict: model.GraceRehazardLevelDict,
        HandleWayDict: model.HandleWayDict,
        ReperfusionMeasureRsDict: model.ReperfusionMeasureRsDict,
        ThrombolyticScreeningDict: model.ThrombolyticScreeningDict,
        PciTransferTypeDict: model.PciTransferTypeDict,
        TreatmentMeasuresDict: model.TreatmentMeasuresDict,
        NcHandleWay: model.NcHandleWay,
        RouteDict: model.RouteDict,
        NarrowLevelDict: model.NarrowLevelDict,
        TimiLevelDict: model.TimiLevelDict,
        IntracavityImageDict: model.IntracavityImageDict,
        FunctionTestDict: model.FunctionTestDict,
        BracketNumDict: model.BracketNumDict,
        TreatmentPciComplicationDict: model.TreatmentPciComplicationDict,
        DelayReasonDict: model.DelayReasonDict,
        CagPositionDict: model.CagPositionDict,
        PciHandleWayDict: model.PciHandleWayDict,
        BracketTypeDict: model.BracketTypeDict,
        DischargeDiagnosisDict: model.DischargeDiagnosisDict,
        BnpTypeDict: model.BnpTypeDict,
        DischargeComplicationDict: model.DischargeComplicationDict,
        CardiovRiskFactorsDict: model.CardiovRiskFactorsDict,
        PatientOutcomeDict: model.PatientOutcomeDict,
        TreatmentResultDict: model.TreatmentResultDict,
        DaptDict: model.DaptDict,
        AceiorarbDict: model.AceiorarbDict,
        StatinsDict: model.StatinsDict,
        RetardantDict: model.RetardantDict,
        DeathCauseDict: model.DeathCauseDict,
        NacsTypeDict: model.NacsTypeDict,
        NccpTypeDict: model.NccpTypeDict,
        TroponinDict: model.TroponinDict,
        AfTypeDict: model.AfTypeDict,
        ChfNyhaDict: model.ChfNyhaDict,
        CvdTypeDict: model.CvdTypeDict,
        SmokingSituationDict: model.SmokingSituationDict,
        DischargeEducationDict: model.DischargeEducationDict,
        RateCountDict: model.RateCountDict,
        RateTimeDict: model.RateTimeDict,
        patient:
        {
            Regist: { HospitalId: "", Name: "", Age: "", Gender: "", IdCareNo: "", PhoneNumber: "", DischargeTime: "", InpatientNo: "", OutpatientNo: "", Insurance: "", InsuranceNo: "", ConsciousnessType: "", TransHospitalName: "", Height: "", Weigth: "" },
            Admission: { DistressCaseDetail: [] },
            Treatment: {},
            TreatmentPci: { DelayReason: ['01'], Complication: [], CagPosition: [] },
            TreatmentPciPart: [{ HandleWay: [] }, { HandleWay: [] }, { HandleWay: [] }],
            Discharge: { Complication: [] },
            DischargeTest: {
                Bnp: { Result: null, Unit: "", Category: "" },
                HdlC: { Result: null, Unit: "", Category: "" },
                LdlC: { Result: null, Unit: "", Category: "" },
                Tc: { Result: null, Unit: "", Category: "" },
                Tg: { Result: null, Unit: "", Category: "" },
                Troponin: { Result: null, Unit: "", Category: "" },
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
        STEMI: '1',
        NSTEMI: '2',
        UA: '3',
        AD: '4',
        PE: '5',
        NACS: '6',
        NC: '7',
        OTHER: '9',
        ZJPCI: '1',
        RS: '2',
        ZQJR: '3',
        CABG: '4',
        ZYPCI: '5',
        JJJR: '21',
        TFH: '22',
        BJPCI: '21',
        RSHJR: '22',
    },
    methods: {
        load: function (registId) {
            var _this = this;
            var count = 0;
            const loading = this.$loading({
                lock: true,
                text: 'Loading',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            api.load(registId).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.initView(_this, res.data.Data);
                    console.log("patient", _this.patient);
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
        initView: function (_this, medical) {
            _this.patient.Regist = _this.formatRegistDict(medical.Regist);
            _this.patient.Admission = _this.formatAdmissionDict(medical.Admission);
            _this.patient.Diagnosis = _this.formatDiagnosisDict(medical.Diagnosis);
            _this.patient.DiagnosisTest = medical.DiagnosisTest;
            _this.patient.Treatment = _this.formatTreatmentDict(medical.Treatment);
            _this.patient.TreatmentPci = _this.formatTreatmentPciDict(medical.TreatmentPci);
            _this.patient.TreatmentPciPart = medical.TreatmentPciPart;
            for (var i = 0; i < _this.patient.TreatmentPciPart.length; i++) {
                _this.patient.TreatmentPciPart[i] = _this.formatTreatmentPciPartDict(_this.patient.TreatmentPciPart[i]);
            }
            _this.patient.Discharge = _this.formatDischargeDict(medical.Discharge);
            _this.patient.DischargeTest = _this.formatDischargeTestDict(medical.DischargeTest);
            _this.patient.DischargeDrugs = _this.formatDischargeDrugsDict(medical.DischargeDrugs);
            _this.patient.EcgList = medical.EcgList;
            _this.patient.MaxCategory = medical.MaxCategory;
        },
        showPosition: function (position) {
            var _this = this;
            return JSON.stringify(_this.patient.TreatmentPci.CagPosition).indexOf(position) >= 0;
        },
        //--------------------字典格式化-------------------
        formatSingleDict: function (key, dict) {
            try {
                for (var i = 0; i < dict.length; i++) {
                    if (key == dict[i].Key) {
                        return dict[i].Value;
                    }
                }
                return key;
            }
            catch (err) {
                console.log(err);
                console.log("key:", key);
                return "";
            }
        },
        formatMultiDict: function (keyArr, dict, name) {
            try {
                var result = "";
                for (var i = 0; i < keyArr.length; i++) {
                    for (var j = 0; j < dict.length; j++) {
                        if (keyArr[i] == dict[j].Key) {
                            result += '|' + dict[j].Value;
                        }
                    }
                }

                return result.length > 0 ? result.slice(1) : result;
            }
            catch (err) {
                console.log(err);
                console.log("name:", name);
                console.log("keyArr:", keyArr);
                console.log("dict:", dict);
                return "";
            }
        },
        formatRegistDict: function (regist) {
            if (regist.Gender) regist.Gender = this.formatSingleDict(regist.Gender, this.GenderDict);
            if (regist.Nationality) regist.Nationality = this.formatSingleDict(regist.Nationality, this.NationalityDict);
            if (regist.IdCard) regist.IdCard = this.formatSingleDict(regist.IdCard, this.IdCardDict);
            if (regist.Job) regist.Job = this.formatSingleDict(regist.Job, this.JobDict);
            if (regist.Marriage) regist.Marriage = this.formatSingleDict(regist.Marriage, this.MarriageDict);
            if (regist.CultureDegree) regist.CultureDegree = this.formatSingleDict(regist.CultureDegree, this.CultureDegreeDict);
            return regist;
        },
        formatAdmissionDict: function (d) {
            if (d.ConsciousnessType) d.ConsciousnessType = this.formatSingleDict(d.ConsciousnessType, this.SenseDict);
            if (d.AttackZone) d.AttackZone = this.formatSingleDict(d.AttackZone, this.AttackZoneDict);
            if (d.DistressCase) d.DistressCase = this.formatSingleDict(d.DistressCase, this.DistressCaseDict);
            if (d.ComingType) d.ComingType = this.formatSingleDict(d.ComingType, this.ComingDict);
            if (d.AmbulanceDepartment) d.AmbulanceDepartment = this.formatSingleDict(d.AmbulanceDepartment, this.AmbulanceDepartmentDict);
            if (d.IsTransHospital) d.IsTransHospital = this.formatSingleDict(d.IsTransHospital, this.YesOrNoDict);
            if (d.IsRsDirect) d.IsRsDirect = this.formatSingleDict(d.IsRsDirect, this.YesOrNoDict);
            if (d.ZcyyType) d.ZcyyType = this.formatSingleDict(d.ZcyyType, this.TransferTypeDict);
            if (d.Insurance) d.Insurance = this.formatSingleDict(d.Insurance, this.InsuranceDict);
            if (d.RsScreen) d.RsScreen = this.formatSingleDict(d.RsScreen, this.ThrombolyticScreeningDict);
            if (d.IsRs) d.IsRs = this.formatSingleDict(d.IsRs, this.HaveOrNotDict);
            if (d.RsPlace) d.RsPlace = this.formatSingleDict(d.RsPlace, this.FaThrombolyticPlacegDict);
            if (d.RsDrugType) d.RsDrugType = this.formatSingleDict(d.RsDrugType, this.ThrombolyticDrugTypeDict);
            if (d.RsDrugCode) d.RsDrugCode = this.formatSingleDict(d.RsDrugCode, this.ThrombolyticDrugCodeDict);
            if (d.PciPlace) d.PciPlace = this.formatSingleDict(d.PciPlace, this.PciPlaceDict);
            if (d.IsRsRecanalization) d.IsRsRecanalization = this.formatSingleDict(d.IsRsRecanalization, this.YesOrNoDict);
            if (d.IsRemedyPci) d.IsRemedyPci = this.formatSingleDict(d.IsRemedyPci, this.YesOrNoDict);
            if (d.IsStrickenInsurance) d.IsStrickenInsurance = this.formatSingleDict(d.IsStrickenInsurance, this.YesOrNoDict);
            if (d.DistressCaseDetail) d.DistressCaseDetail = this.formatMultiDict(d.DistressCaseDetail, this.DistressCaseDetailDict, "DistressCaseDetail");
            return d;
        },
        formatDiagnosisDict: function (d) {
            if (d.IsEcg) d.IsEcg = this.formatSingleDict(d.IsEcg, this.YesOrNoDict);
            if (d.EcgTransmission) d.EcgTransmission = this.formatSingleDict(d.EcgTransmission, this.YesOrNoDict);
            if (d.IsGiveupTreatment) d.IsGiveupTreatment = this.formatSingleDict(d.IsGiveupTreatment, this.YesOrNoDict);
            if (d.IsBypassEmergency) d.IsBypassEmergency = this.formatSingleDict(d.IsBypassEmergency, this.YesOrNoDict);
            if (d.IsBypassCcu) d.IsBypassCcu = this.formatSingleDict(d.IsBypassCcu, this.YesOrNoDict);
            if (d.EcgTransmissionType) d.EcgTransmissionType = this.formatSingleDict(d.EcgTransmissionType, this.RemoteEcgtranWayDict);
            if (d.ConsultationType) d.ConsultationType = this.formatSingleDict(d.ConsultationType, this.ConsultationTypeDict);
            if (d.KillipLevel) d.KillipLevel = this.formatSingleDict(d.KillipLevel, this.KillipLevelDict);
            if (d.GraceRiskFactors) d.GraceRiskFactors = this.formatMultiDict(d.GraceRiskFactors, this.GraceEstimateDict, "GraceEstimateDict");
            if (d.GraceHazardLevel) d.GraceHazardLevel = this.formatSingleDict(d.GraceHazardLevel, this.GraceHighriskLaminationDict);
            if (d.GraceRehazardLevel) d.GraceRehazardLevel = this.formatSingleDict(d.GraceRehazardLevel, this.GraceRehazardLevelDict);
            //if (d.DiagnosisCode) d.DiagnosisCode = this.formatSingleDict(d.DiagnosisCode, this.DiagnosisDict);
            if (d.NccpType) d.NccpType = this.formatMultiDict(d.NccpType, this.NccpTypeDict, "NccpType");
            if (d.NacsType) d.NacsType = this.formatMultiDict(d.NacsType, this.NacsTypeDict, "NacsType");
            if (d.HandleWay) d.HandleWay = this.formatMultiDict(d.HandleWay, this.HandleWayDict);
            if (d.GraceVhrConditions) d.GraceVhrConditions = this.formatMultiDict(d.GraceVhrConditions, this.GraceHighriskConditionDict, "GraceVhrConditions");
            return d;
        },
        formatTreatmentDict: function (d) {
            if (d.IsAntiplatelet) d.IsAntiplatelet = this.formatSingleDict(d.IsAntiplatelet, this.YesOrNoDict);
            if (d.IsAnticoagulation) d.IsAnticoagulation = this.formatSingleDict(d.IsAnticoagulation, this.YesOrNoDict);
            if (d.IsStatin) d.IsStatin = this.formatSingleDict(d.IsStatin, this.YesOrNoDict);
            if (d.IsBetaBlocker) d.IsBetaBlocker = this.formatSingleDict(d.IsBetaBlocker, this.YesOrNoDict);
            if (d.IsRsRecanalization) d.IsRsRecanalization = this.formatSingleDict(d.IsRsRecanalization, this.YesOrNoDict);
            if (d.IsRsDirect) d.IsRsDirect = this.formatSingleDict(d.IsRsDirect, this.HaveOrNotDict);
            //if (d.IsReperfusion) d.IsReperfusion = this.formatSingleDict(d.IsReperfusion, this.HaveOrNotDict);
            if (d.AnticoagulationDrug) d.AnticoagulationDrug = this.formatSingleDict(d.AnticoagulationDrug, this.AnticoagulationDrugDict);
            if (d.InterlayerType) d.InterlayerType = this.formatSingleDict(d.InterlayerType, this.InterlayerTypeDict);
            //if (d.Strategy) d.Strategy = this.formatSingleDict(d.Strategy, this.TreatmentStrategyDict);
            if (d.ReperfusionMeasureRs) d.ReperfusionMeasureRs = this.formatSingleDict(d.ReperfusionMeasureRs, this.ReperfusionMeasureRsDict);
            if (d.RiskLevel) d.RiskLevel = this.formatSingleDict(d.RiskLevel, this.HighriskLaminationDict);
            if (d.RsPlace) d.RsPlace = this.formatSingleDict(d.RsPlace, this.IhThrombolyticPlaceDict);
            if (d.RsDrugType) d.RsDrugType = this.formatSingleDict(d.RsDrugType, this.ThrombolyticDrugTypeDict);
            if (d.RsDrugCode) d.RsDrugCode = this.formatSingleDict(d.RsDrugCode, this.ThrombolyticDrugCodeDict);
            //if (d.ReperfusionMeasure) d.ReperfusionMeasure = this.formatSingleDict(d.ReperfusionMeasure, this.PerfusionMeasureDict);
            return d;
        },
        formatTreatmentPciDict: function (d) {
            if (d.AnticoagulationDrug) d.AnticoagulationDrug = this.formatSingleDict(d.AnticoagulationDrug, this.AnticoagulationDrugDict);
            if (d.Approach) d.Approach = this.formatSingleDict(d.Approach, this.RouteDict);
            if (d.IntracavityImage) d.IntracavityImage = this.formatSingleDict(d.IntracavityImage, this.IntracavityImageDict);
            if (d.FunctionTest) d.FunctionTest = this.formatSingleDict(d.FunctionTest, this.FunctionTestDict);
            if (d.IsIabp) d.IsIabp = this.formatSingleDict(d.IsIabp, this.YesOrNoDict);
            if (d.IsPacemaker) d.IsPacemaker = this.formatSingleDict(d.IsPacemaker, this.YesOrNoDict);
            if (d.IsEcmo) d.IsEcmo = this.formatSingleDict(d.IsEcmo, this.YesOrNoDict);
            if (d.IsAuxiliaryDevice) d.IsAuxiliaryDevice = this.formatSingleDict(d.IsAuxiliaryDevice, this.YesOrNoDict);
            if (d.Complication) d.Complication = this.formatMultiDict(d.Complication, this.TreatmentPciComplicationDict, "Complication");
            if (d.DelayReason) d.DelayReason = this.formatMultiDict(d.DelayReason, this.DelayReasonDict, "DelayReason");
            return d;
        },
        formatTreatmentPciPartDict: function (d) {
            if (d.NarrowLevel) d.NarrowLevel = this.formatSingleDict(d.NarrowLevel, this.NarrowLevelDict);
            if (d.TimiLevel) d.TimiLevel = this.formatSingleDict(d.TimiLevel, this.TimiLevelDict);
            if (d.BracketThrombus) d.BracketThrombus = this.formatSingleDict(d.BracketThrombus, this.YesOrNoDict);
            if (d.BranchingSick) d.BranchingSick = this.formatSingleDict(d.BranchingSick, this.YesOrNoDict);
            if (d.IsCto) d.IsCto = this.formatSingleDict(d.IsCto, this.YesOrNoDict);
            if (d.IsCalcifiedLesions) d.IsCalcifiedLesions = this.formatSingleDict(d.IsCalcifiedLesions, this.YesOrNoDict);
            if (d.IsCulpritLesion) d.IsCulpritLesion = this.formatSingleDict(d.IsCulpritLesion, this.YesOrNoDict);
            if (d.TimiLevelPost) d.TimiLevelPost = this.formatSingleDict(d.TimiLevelPost, this.TimiLevelDict);
            if (d.BracketCount) d.BracketCount = this.formatSingleDict(d.BracketCount, this.BracketNumDict);
            if (d.BracketType) d.BracketType = this.formatSingleDict(d.BracketType, this.BracketTypeDict);
            if (d.HandleWay) d.HandleWay = this.formatMultiDict(d.HandleWay, this.PciHandleWayDict, "HandleWay");
            return d;
        },
        formatDischargeDict: function (d) {
            if (d.IsHeartFailure) d.IsHeartFailure = this.formatSingleDict(d.IsHeartFailure, this.YesOrNoDict);
            if (d.IsHypertension) d.IsHypertension = this.formatSingleDict(d.IsHypertension, this.YesOrNoDict);
            if (d.IsHyperlipidemia) d.IsHyperlipidemia = this.formatSingleDict(d.IsHyperlipidemia, this.YesOrNoDict);
            if (d.IsDiabetes) d.IsDiabetes = this.formatSingleDict(d.IsDiabetes, this.YesOrNoDict);
            if (d.IsObesity) d.IsObesity = this.formatSingleDict(d.IsObesity, this.YesOrNoDict);
            if (d.IsCvdHr) d.IsCvdHr = this.formatSingleDict(d.IsCvdHr, this.YesOrNoDict);
            if (d.IsSmoking) d.IsSmoking = this.formatSingleDict(d.IsSmoking, this.YesOrNoDict);
            if (d.IsChd) d.IsChd = this.formatSingleDict(d.IsChd, this.YesOrNoDict);
            if (d.IsChdHor) d.IsChdHor = this.formatSingleDict(d.IsChdHor, this.YesOrNoDict);
            if (d.IsAf) d.IsAf = this.formatSingleDict(d.IsAf, this.YesOrNoDict);
            if (d.IsChf) d.IsChf = this.formatSingleDict(d.IsChf, this.YesOrNoDict);
            if (d.IsCvd) d.IsCvd = this.formatSingleDict(d.IsCvd, this.YesOrNoDict);
            if (d.IsVhd) d.IsVhd = this.formatSingleDict(d.IsVhd, this.YesOrNoDict);
            if (d.IsPad) d.IsPad = this.formatSingleDict(d.IsPad, this.YesOrNoDict);
            if (d.IsAneurysm) d.IsAneurysm = this.formatSingleDict(d.IsAneurysm, this.YesOrNoDict);
            if (d.IsCopd) d.IsCopd = this.formatSingleDict(d.IsCopd, this.YesOrNoDict);
            if (d.IsCkd) d.IsCkd = this.formatSingleDict(d.IsCkd, this.YesOrNoDict);
            if (d.IsAnemia) d.IsAnemia = this.formatSingleDict(d.IsAnemia, this.YesOrNoDict);
            if (d.IsPepticUlcer) d.IsPepticUlcer = this.formatSingleDict(d.IsPepticUlcer, this.YesOrNoDict);
            if (d.IsThyroidDysfunction) d.IsThyroidDysfunction = this.formatSingleDict(d.IsThyroidDysfunction, this.YesOrNoDict);
            if (d.IsMalignantTumor) d.IsMalignantTumor = this.formatSingleDict(d.IsMalignantTumor, this.YesOrNoDict);
            if (d.IsTroponin) d.IsTroponin = this.formatSingleDict(d.IsTroponin, this.YesOrNoDict);
            if (d.IsBnp) d.IsBnp = this.formatSingleDict(d.IsBnp, this.YesOrNoDict);
            if (d.IsTc) d.IsTc = this.formatSingleDict(d.IsTc, this.YesOrNoDict);
            if (d.IsTg) d.IsTg = this.formatSingleDict(d.IsTg, this.YesOrNoDict);
            if (d.IsHdlC) d.IsHdlC = this.formatSingleDict(d.IsHdlC, this.YesOrNoDict);
            if (d.IsLdlC) d.IsLdlC = this.formatSingleDict(d.IsLdlC, this.YesOrNoDict);
            if (d.IsUcc) d.IsUcc = this.formatSingleDict(d.IsUcc, this.YesOrNoDict);
            if (d.IsVentricularAneurysm) d.IsVentricularAneurysm = this.formatSingleDict(d.IsVentricularAneurysm, this.YesOrNoDict);
            if (d.IsRwma) d.IsRwma = this.formatSingleDict(d.IsRwma, this.YesOrNoDict);
            if (d.IsAntiplatelet) d.IsAntiplatelet = this.formatSingleDict(d.IsAntiplatelet, this.YesOrNoDict);
            if (d.IsAceiOrArb) d.IsAceiOrArb = this.formatSingleDict(d.IsAceiOrArb, this.YesOrNoDict);
            if (d.IsLipidRegulation) d.IsLipidRegulation = this.formatSingleDict(d.IsLipidRegulation, this.YesOrNoDict);
            if (d.IsBetaBlocker) d.IsBetaBlocker = this.formatSingleDict(d.IsBetaBlocker, this.YesOrNoDict);
            if (d.IsNetHospital) d.IsNetHospital = this.formatSingleDict(d.IsNetHospital, this.YesOrNoDict);
            //if (d.IsTransPci) d.IsTransPci = this.formatSingleDict(d.IsTransPci, this.YesOrNoDict);
            if (d.EcgTransmission) d.EcgTransmission = this.formatSingleDict(d.EcgTransmission, this.YesOrNoDict);
            if (d.EcgTransmissionType) d.EcgTransmissionType = this.formatSingleDict(d.EcgTransmissionType, this.RemoteEcgtranWayDict);
            if (d.IsDirectToCr) d.IsDirectToCr = this.formatSingleDict(d.IsDirectToCr, this.YesOrNoDict);
            if (d.SmokingSituation) d.SmokingSituation = this.formatSingleDict(d.SmokingSituation, this.SmokingSituationDict);
            if (d.AfType) d.AfType = this.formatSingleDict(d.AfType, this.AfTypeDict);
            if (d.ChfNyha) d.ChfNyha = this.formatSingleDict(d.ChfNyha, this.ChfNyhaDict);
            if (d.CvdType) d.CvdType = this.formatSingleDict(d.CvdType, this.CvdTypeDict);
            if (d.TreatmentResult) d.TreatmentResult = this.formatSingleDict(d.TreatmentResult, this.TreatmentResultDict);
            if (d.DeathCause) d.DeathCause = this.formatSingleDict(d.DeathCause, this.DeathCauseDict);
            if (d.NacsType) d.NacsType = this.formatMultiDict(d.NacsType, this.NacsTypeDict, "NacsType");
            if (d.NccpType) d.NccpType = this.formatMultiDict(d.NccpType, this.NccpTypeDict, "NccpType");
            if (d.Complication) d.Complication = this.formatMultiDict(d.Complication, this.DischargeComplicationDict, "Complication");
            if (d.Education) d.Education = this.formatMultiDict(d.Education, this.DischargeEducationDict, "Education");
            return d;
        },
        formatDischargeTestDict: function (d) {
            if (d.Troponin && d.Troponin.Category) d.Troponin.Category = this.formatSingleDict(d.Troponin.Category, this.TroponinDict);
            if (d.Bnp && d.Bnp.Category) d.Bnp.Category = this.formatSingleDict(d.Bnp.Category, this.BnpTypeDict);
            return d;
        },
        formatDischargeDrugsDict: function (d) {
            for (var i = 0; i < d.Dapt.length; i++) {
                d.Dapt[i].Code = this.formatSingleDict(d.Dapt[i].Code, this.DaptDict);
                d.Dapt[i].RateTime = this.formatSingleDict(d.Dapt[i].RateTime, this.RateTimeDict);
                d.Dapt[i].RateCount = this.formatSingleDict(d.Dapt[i].RateCount, this.RateCountDict);
            }
            for (var i = 0; i < d.Aceiorarb.length; i++) {
                d.Aceiorarb[i].Code = this.formatSingleDict(d.Aceiorarb[i].Code, this.AceiorarbDict);
                d.Aceiorarb[i].RateTime = this.formatSingleDict(d.Aceiorarb[i].RateTime, this.RateTimeDict);
                d.Aceiorarb[i].RateCount = this.formatSingleDict(d.Aceiorarb[i].RateCount, this.RateCountDict);
            }
            for (var i = 0; i < d.Statins.length; i++) {
                d.Statins[i].Code = this.formatSingleDict(d.Statins[i].Code, this.StatinsDict);
                d.Statins[i].RateTime = this.formatSingleDict(d.Statins[i].RateTime, this.RateTimeDict);
                d.Statins[i].RateCount = this.formatSingleDict(d.Statins[i].RateCount, this.RateCountDict);
            }
            for (var i = 0; i < d.Retardant.length; i++) {
                d.Retardant[i].Code = this.formatSingleDict(d.Retardant[i].Code, this.RetardantDict);
                d.Retardant[i].RateTime = this.formatSingleDict(d.Retardant[i].RateTime, this.RateTimeDict);
                d.Retardant[i].RateCount = this.formatSingleDict(d.Retardant[i].RateCount, this.RateCountDict);
            }
            return d;
        },
        //--------------------转归模块-------------------
        IsNacs: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.Diagnosis) return false;
            return this.patient.Discharge.Diagnosis == '6';
        },
        IsNccp: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.Diagnosis) return false;
            return this.patient.Discharge.Diagnosis == '7';
        },
        IsCpc: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.Diagnosis) return false;
            return this.patient.Discharge.Diagnosis == '1'
                || this.patient.Discharge.Diagnosis == '2'
                || this.patient.Discharge.Diagnosis == '3';
        },
        IsAcs: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.Diagnosis) return false;
            return this.patient.Discharge.Diagnosis == '1'
                || this.patient.Discharge.Diagnosis == '2'
                || this.patient.Discharge.Diagnosis == '3'
                || this.patient.Discharge.Diagnosis == '4'
                || this.patient.Discharge.Diagnosis == '5';
        },
        IsLeave: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.OutcomeCode) return false;
            return this.patient.Discharge.OutcomeCode == '1';
        },
        IsTransferHospital: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.OutcomeCode) return false;
            return this.patient.Discharge.OutcomeCode == '2';
        },
        IsTransPci: function () {
            if (!this.IsTransferHospital()) return false;
            return this.patient.Discharge.IsTransPci == '1';
        },
        IsTransferDept: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.OutcomeCode) return false;
            return this.patient.Discharge.OutcomeCode == '3';
        },
        IsDeath: function () {
            if (this.patient.Discharge == null
                || !this.patient.Discharge.OutcomeCode) return false;
            return this.patient.Discharge.OutcomeCode == '4';
        },
        IsEducation: function () {
            if (this.patient.Discharge == null || !this.patient.Discharge.OutcomeCode) return false;
            return (this.patient.Discharge.OutcomeCode == '1' || this.patient.Discharge.OutcomeCode == '2' || this.patient.Discharge.OutcomeCode == '3');
        },
        print: function () {
            var _this = this;
            const loading = _this.$loading({
                lock: true,
                text: '正在努力准备......',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            document.getElementById("printButton").style.display = "none";
            // 这个dom元素是要导出pdf的div容器
            var element = document.querySelector("#medicalrecord");
            var width = element.clientWidth;
            var height = element.clientHeight;
            //要将 canvas 的宽高设置成容器宽高的 2 倍
            var canvas = document.createElement("canvas");

            html2canvas(element, {
                width: width,
                height: height,
                canvas: canvas
            }).then(function (canvas) {
                //未生成pdf的html页面高度
                var leftHeight = canvas.height;

                var a4Width = 595.28
                var a4Height = 841.89
                //一页pdf显示html页面生成的canvas高度;
                var a4HeightRef = Math.floor(canvas.width / a4Width * a4Height);

                //pdf页面偏移
                var position = 0;

                var pageData = canvas.toDataURL('image/jpeg', 1.0);

                var pdf = new jsPDF('x', 'pt', 'a4');
                var index = 1,
                    canvas1 = document.createElement('canvas'),
                    height;
                pdf.setDisplayMode('fullwidth', 'continuous', 'FullScreen');

                var pdfName = _this.patient.Regist.Name + '病历打印.pdf';
                function createImpl(canvas) {
                    if (leftHeight > 0) {
                        index++;

                        var checkCount = 0;
                        if (leftHeight > a4HeightRef) {
                            var i = position + a4HeightRef;
                            for (i = position + a4HeightRef; i >= position; i--) {
                                var isWrite = true
                                for (var j = 0; j < canvas.width; j++) {
                                    var c = canvas.getContext('2d').getImageData(j, i, 1, 1).data

                                    if (c[0] != 0xff || c[1] != 0xff || c[2] != 0xff) {
                                        isWrite = false
                                        break
                                    }
                                }
                                if (isWrite) {
                                    checkCount++
                                    if (checkCount >= 10) {
                                        break
                                    }
                                } else {
                                    checkCount = 0
                                }
                            }
                            height = Math.round(i - position) || Math.min(leftHeight, a4HeightRef);
                            if (height <= 0) {
                                height = a4HeightRef;
                            }
                        } else {
                            height = leftHeight;
                        }

                        canvas1.width = canvas.width;
                        canvas1.height = height;

                        // console.log(index, 'height:', height, 'pos', position);

                        var ctx = canvas1.getContext('2d');
                        ctx.drawImage(canvas, 0, position, canvas.width, height, 0, 0, canvas.width, height);

                        var pageHeight = Math.round(a4Width / canvas.width * height);
                        // pdf.setPageSize(null, pageHeight)
                        if (position != 0) {
                            pdf.addPage();
                        }
                        pdf.addImage(canvas1.toDataURL('image/jpeg', 1.0), 'JPEG', 0, 0, a4Width, a4Width / canvas1.width * height);
                        leftHeight -= height;
                        position += height;
                        //$('.pdfProgress').text(index + 1);
                        //$('.pdfTotal').text(index + Math.ceil(leftHeight / a4HeightRef));
                        if (leftHeight > 0) {
                            createImpl(canvas);
                        } else {
                            loading.close();
                            if (_this.IEVersion() > 0) {
                                pdf.save(pdfName);
                            }
                            else {
                                var doc = pdf.output('datauri');
                                var PDFData = doc.replace("data:application/pdf;filename=generated.pdf;base64,", "");
                                sessionStorage.setItem("_imgUrl", PDFData);
                                window.location = "../Scripts/extension/pdfjs/web/viewer.html";
                            }
                            //$('.pdfTip').remove();
                        }
                    }
                }

                //当内容未超过pdf一页显示的范围，无需分页
                if (leftHeight < a4HeightRef) {
                    pdf.addImage(pageData, 'JPEG', 0, 0, a4Width, a4Width / canvas.width * leftHeight);
                    loading.close();
                    if (_this.IEVersion() > 0) {
                        pdf.save(pdfName);
                    }
                    else {
                        var doc = pdf.output('datauri');
                        var PDFData = doc.replace("data:application/pdf;filename=generated.pdf;base64,", "");
                        sessionStorage.setItem("_imgUrl", PDFData);
                        window.location = "../Scripts/extension/pdfjs/web/viewer.html";
                    }
                } else {
                    try {
                        pdf.deletePage(0);
                        //$('.pdfTip').show();
                        //$('.pdfTotal').text(index + Math.ceil(leftHeight / a4HeightRef));
                        createImpl(canvas);
                    } catch (err) {
                        console.log(err);
                    }
                }
            });
            document.getElementById("printButton").style.display = "inline";
        },
        IEVersion: function () {
            var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
            var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
            var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
            var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
            if (isIE) {
                var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
                reIE.test(userAgent);
                var fIEVersion = parseFloat(RegExp["$1"]);
                if (fIEVersion == 7) {
                    return 7;
                } else if (fIEVersion == 8) {
                    return 8;
                } else if (fIEVersion == 9) {
                    return 9;
                } else if (fIEVersion == 10) {
                    return 10;
                } else {
                    return 6;//IE版本<=7
                }
            } else if (isEdge) {
                return 99;//edge
            } else if (isIE11) {
                return 11; //IE11  
            } else {
                return -1;//不是ie浏览器
            }
        }
    },
    mounted: function () {
        this.load(this.registId);
    },
    computed: {
        // 直接PCI
        showZJPCI: function () {
            var _this = this;
            if (!_this.showReperfusion) return false;
            return _this.patient.Treatment.IsReperfusion == '1' && _this.patient.Treatment.ReperfusionMeasure == _this.ZJPCI;
        },
        // 溶栓
        showRS: function () {
            var _this = this;
            var stemiRs = _this.showReperfusion && _this.patient.Treatment.IsReperfusion == '1' && _this.patient.Treatment.ReperfusionMeasure == _this.RS;
            var peRs = _this.patient.Diagnosis.DiagnosisCode == _this.PE;
            return stemiRs || peRs;
        },
        // 补救PCI
        showBJPCI: function () {
            var _this = this;
            return _this.showRS && _this.patient.Treatment.ReperfusionMeasureRs == _this.BJPCI;
        },
        // 溶栓后介入
        showRSHJR: function () {
            var _this = this;
            return _this.showRS && _this.patient.Treatment.ReperfusionMeasureRs == _this.RSHJR;
        },
        // 择期介入
        showZQJR: function () {
            var _this = this;
            return _this.showReperfusion && _this.patient.Treatment.IsReperfusion == '1' && _this.patient.Treatment.ReperfusionMeasure == _this.ZQJR;
        },
        // CABG
        showCABG: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.STEMI && _this.patient.Treatment.IsReperfusion == '1' && _this.patient.Treatment.ReperfusionMeasure == _this.CABG;
        },
        // 转运PCI
        showZYPCI: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.STEMI && _this.patient.Treatment.IsReperfusion == '1' && _this.patient.Treatment.ReperfusionMeasure == _this.ZYPCI;
        },
        // 紧急介入
        showJJJR: function () {
            var _this = this;
            if (_this.patient.Diagnosis.DiagnosisCode != _this.NSTEMI
                && _this.patient.Diagnosis.DiagnosisCode != _this.UA)
                return false;
            if (_this.patient.Diagnosis.GraceRehazardLevel == _this.GRL_ToSTEMI) return false;
            return _this.patient.Treatment.Strategy == '2' && _this.patient.Treatment.InvasiveStrategy == _this.JJJR;
        },
        // 24H内介入治疗
        show24H: function () {
            var _this = this;
            var nstemi24H = _this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI && _this.patient.Treatment.Strategy == '2' && _this.patient.Treatment.InvasiveStrategy == _this.TFH;
            var ua24H = _this.patient.Diagnosis.DiagnosisCode == _this.UA && _this.patient.Treatment.Strategy == '2' && _this.patient.Treatment.InvasiveStrategy == _this.TFH;
            return nstemi24H || ua24H;
        },
        // 心功能分级
        showKillip: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.STEMI || _this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI || _this.patient.Diagnosis.DiagnosisCode == _this.UA;
        },
        // 绕行急诊 & 绕行CCU
        showIsBypass: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.STEMI || _this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI || _this.patient.Diagnosis.DiagnosisCode == _this.UA;
        },
        // 药物
        showDrugs: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.STEMI || _this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI || _this.patient.Diagnosis.DiagnosisCode == _this.UA;
        },
        // Grace评估
        showGrace: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI || _this.patient.Diagnosis.DiagnosisCode == _this.UA;
        },
        // 再次危险分层
        showGraceRehazardLevel: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI || _this.patient.Diagnosis.DiagnosisCode == _this.UA;
        },
        showReperfusion: function () {
            var _this = this;
            if (_this.patient.Diagnosis.DiagnosisCode == _this.STEMI) return true;
            if (!_this.patient.Diagnosis.GraceRehazardLevel) return false;
            if (_this.patient.Diagnosis.DiagnosisCode == _this.NSTEMI
                || _this.patient.Diagnosis.DiagnosisCode == _this.UA)
                return _this.patient.Diagnosis.GraceRehazardLevel == _this.GRL_ToSTEMI;
            return false;
        },
        // 处理策略
        showStrategy: function () {
            var _this = this;
            if (_this.patient.Diagnosis.DiagnosisCode != _this.NSTEMI
                && _this.patient.Diagnosis.DiagnosisCode != _this.UA)
                return false;
            return _this.patient.Diagnosis.GraceRehazardLevel != _this.GRL_ToSTEMI;
        },
        // 急诊CT
        showIsEct: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.AD || _this.patient.Diagnosis.DiagnosisCode == _this.PE;
        },
        // 急诊CT
        showEct: function () {
            var _this = this;
            return _this.showIsEct && _this.patient.Diagnosis.IsEct == '1';
        },
        // 彩超
        showIsCdu: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.AD;
        },
        // 彩超
        showCdu: function () {
            var _this = this;
            return _this.showIsCdu && _this.patient.Diagnosis.IsCdu == '1';
        },
        // MRI
        showIsMri: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.AD;
        },
        // 危险分层
        showRiskLamination: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.PE;
        },
        // 非ACS心源性胸痛类型
        showNacsType: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.NACS;
        },
        // 其它非心源性胸痛
        showNccpType: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.NC;
        },
        // 处理措施
        showHandleWay: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.NACS || _this.patient.Diagnosis.DiagnosisCode == _this.NC || _this.patient.Diagnosis.DiagnosisCode == _this.OTHER;
        },
        // 心外科
        showCsc: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.AD;
        },
        showInterlayerType: function () {
            var _this = this;
            return _this.patient.Diagnosis.DiagnosisCode == _this.AD;
        },
        // 导管室
        showDGS: function () {
            var _this = this;
            return _this.showZJPCI || _this.showBJPCI || _this.showRSHJR || (_this.showZYPCI && _this.patient.Treatment.PciTransferType == '2') || _this.showJJJR;
        },
        showJRSS: function () {
            var _this = this;
            return _this.showZJPCI || _this.showJJJR;
        },
        D2W: function () {
            var _this = this;
            var hasValue = false;
            if (_this.patient.Admission.ArrivedHospitalTime != null && _this.patient.TreatmentPci.CagPosition != null && _this.patient.TreatmentPci.CagPosition.length > 0) {
                for (var i = 0; i < _this.patient.TreatmentPciPart.length; i++) {
                    var part = _this.patient.TreatmentPciPart[i];
                    if (JSON.stringify(_this.patient.TreatmentPci.CagPosition).indexOf(part.PartId) >= 0) {
                        if (part.IsPci == "1" && part.GwPassageTime != null) {
                            hasValue = true;
                            return moment(part.GwPassageTime).diff(moment(_this.patient.Admission.ArrivedHospitalTime), 'minute');
                        }
                    }
                }
            }

            if (!hasValue) {
                return null;
            }
        },
        DiagnosisDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Diagnosis.DiagnosisCode, _this.DiagnosisDict);
        },
        DischargeDiagnosisDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Discharge.Diagnosis, _this.DischargeDiagnosisDict);
        },
        StrategyDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Treatment.Strategy, _this.TreatmentStrategyDict);
        },
        InvasiveStrategyDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Treatment.InvasiveStrategy, _this.NstemiReperfusionStrategyDict);
        },
        ReperfusionMeasureDesc: function () {
            var _this = this;
            return _this.formatMultiDict(_this.patient.Treatment.NoReperfusionMeasure, _this.PerfusionMeasureDict);
        },
        SetmiReperfusionMeasureDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Treatment.ReperfusionMeasure, _this.StemiReperfusionStrategyDict);
        },
        RsScreenDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Treatment.RsScreen, _this.ThrombolyticScreeningDict);
        },
        PciTransferTypeDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Treatment.PciTransferType, _this.PciTransferTypeDict);
        },
        CagPositionDesc: function () {
            var _this = this;
            return _this.formatMultiDict(_this.patient.TreatmentPci.CagPosition, _this.CagPositionDict);
        },
        OutcomeCodeDesc: function () {
            var _this = this;
            return _this.formatSingleDict(_this.patient.Discharge.OutcomeCode, _this.PatientOutcomeDict);
        },
    }
})
