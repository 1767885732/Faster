var reprotPercentDatatable = {
    template: "\
    <table class='basic-table'>\
        <tr>\
            <td>{{reportType=='1' ? '月份':'周数'}}</td>\
            <td v-for='d in reportData' v-bind:key='d.TimeExpress' style='cursor:pointer;color:#72ACE3' v-on:click='getDetailReport(d.TimeExpress)'>{{d.TimeExpress}}</td>\
	    </tr>\
    	<tr v-if='!customTable || customTable.length == 0'>\
        <td>百分比</td>\
            <td v-for='(d,index) in reportData' v-bind:key='index'>\
                {{ d.Percent }}%\
            </td>\
	    </tr>\
        <tr v-if='!customTable || customTable.length == 0'>\
            <td>数量</td>\
            <td v-for='(d,index) in reportData' v-bind:key='index'>\
			    {{ d.CurrentCount }}\
		    </td>\
	    </tr>\
        <tr v-if='!customTable || customTable.length == 0'>\
            <td>总数</td>\
            <td v-for='(d,index) in reportData' v-bind:key='index'>\
			    {{ d.Total }}\
		    </td>\
	    </tr>\
    </table>\
    ",
    props: {
        reportData: {
            type: Array,
            default: function () { return [] }
        },
        customTable: {
            type: Array,
            default: function () { return [] }
        },
        reportType: {
            type: String,
            default: "1"
        },
    },
    methods: {
        getDetailReport: function (selected) {
            this.$emit('get', selected);
        }
    }
}