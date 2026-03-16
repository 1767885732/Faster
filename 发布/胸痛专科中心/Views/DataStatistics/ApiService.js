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
    getReport: function (params) {
        return axios.get("/DataStatistics/GetReport", {
            params: params, paramsSerializer: function (params) {
                return Qs.stringify(params, { arrayFormat: 'repeat' })
            } });
    },
    getReportDetail: function (params) {
        return axios.get("/DataStatistics/GetReportDetail", {
            params: params, paramsSerializer: function (params) {
                return Qs.stringify(params, { arrayFormat: 'repeat' })
            }
        });
    }
}