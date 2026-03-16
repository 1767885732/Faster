var extendMixin = {
    filters: {
        emptyReplace: function (value) {
            if (!value) return '--';
            return value;
        },
        dateFmt: function (value) {
            if (!value) return null;
            return moment(value).format('YYYY-MM-DD HH:mm:ss');
        }
    }
}