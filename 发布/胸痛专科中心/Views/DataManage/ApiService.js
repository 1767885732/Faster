axios.interceptors.response.use(function (response) {
    return response;
}, function (error) {
    if (590 === error.response.status) {
        window.location = '/Account/Login';
    } else {
        return Promise.reject(error);
    }
});
axios.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

var api = {
    //--------------get--------------//    
    load: function (id) {
        return axios.get("/DataManage/LoadMedical?registId=" + id, {});
    },
    loadByPatientNo: function (patientNo) {
        return axios.get("/DataManage/LoadMedicalByPatientNo?patientNo=" + patientNo, {});
    },
    extract: function (registId, hospitalId, patientNo) {
        return axios.get("/DataManage/ExtractMedical?registId=" + registId + "&hospitalId=" + hospitalId + "&patientNo=" + patientNo, {});
    },
    getRegist: function (id) {
        return axios.get("/DataManage/GetRegist?registId=" + id, {});
    },
    getTimeline: function (id) {
        return axios.get("/DataManage/GetTimeline?id=" + id, {});
    },
    getAdmission: function (id) {
        return axios.get("/DataManage/GetAdmission?registId=" + id, {});
    },
    getDiagnosis: function (id) {
        return axios.get("/DataManage/GetDiagnosis?registId=" + id, {});
    },

    getEcg: function (id) {
        return axios.get("/DataManage/GetEcg?registId=" + id, {});
    },
    getEcgList: function (param) {
        return axios.post("/DataManage/GetEcgList", { param: param});
    },
    bindEcg: function (param) {
        return axios.post("/DataManage/BindEcg", { param: param });
    },
    getFile: function (id) {
        return axios.get("/DataManage/GetFile?registId=" + id, {});
    },
    getTreatmentPci: function (id) {
        return axios.get("/DataManage/GetTreatmentPci?registId=" + id, {});
    },
    getTreatmentPciPart: function (id) {
        return axios.get("/DataManage/GetTreatmentPciPart?registId=" + id, {});
    },
    getTreatment: function (id) {
        return axios.get("/DataManage/getTreatment?registId=" + id, {});
    },
    getDischarge: function (id) {
        return axios.get("/DataManage/GetDischarge?registId=" + id, {});
    },
    getDischargeTest: function (id) {
        return axios.get("/DataManage/GetDischargeTest?registId=" + id, {});
    },
    getDischargeDrugs: function (id) {
        return axios.get("/DataManage/GetDischargeDrugs?registId=" + id, {});
    },
    
    getAccessUrl: function (id, url) {
        return axios.get("/ExtendView/GetAccessUrl?registId=" + id + "&url=" + url, {});
    },
    getAccessSetting: function (id) {
        return axios.get("/SystemSetting/GetAccessSetting", {});
    },
    pullData: function (ehrId, hospitalId) {
        return axios.get("/DataManage/ExtractPatient?patientNo=" + ehrId + "&hospitalId=" + hospitalId, {});
    },
    //--------------post---------------//
    save: function (model) {
        return axios.post("/DataManage/Save", {
            model: model
        });
    },
    extractRegist: function (hospitalId, registId, patientNo) {
        var url = "/DataManage/ExtractRegist?hospitalId={0}&registId={1}&patientNo={2}".format(hospitalId, registId, patientNo);
        return axios.get(url, {});
    },
    extractAdmission: function (hospitalId, registId, patientNo) {
        var url = "/DataManage/ExtractAdmission?hospitalId={0}&registId={1}&patientNo={2}".format(hospitalId, registId, patientNo);
        return axios.get(url, {});
    },
    extractPci: function (hospitalId, registId, patientNo) {
        var url = "/DataManage/ExtractPci?hospitalId={0}&registId={1}&patientNo={2}".format(hospitalId, registId, patientNo);
        return axios.get(url, {});
    },
    extractDischarge: function (hospitalId, registId, patientNo) {
        var url = "/DataManage/ExtractDischarge?hospitalId={0}&registId={1}&patientNo={2}".format(hospitalId, registId, patientNo);
        return axios.get(url, {});
    },
    validate: function (id) {
        return axios.post("/DataManage/Validate", {
            registId: id,
        });
    },
    commit: function (id) {
        return axios.post("/DataManage/Commit", {
            registId: id,
        });
    },
    push: function (userId, emergency) {
        return axios.post("/WorkStation/PushEmergency", {
            user: userId,
            emergency: emergency
        });
    },
    queryList: function (url, params) {
        return axios.get(url, { params: params });
    }
}