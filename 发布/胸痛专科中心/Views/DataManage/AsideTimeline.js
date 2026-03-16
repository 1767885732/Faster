var asideTimeline = {
    template: "\
    <div style='padding:20px; height:calc(100vh - 130px);'>\
        <el-scrollbar style='height:100%;'>\
            <el-timeline :reverse='reverse'>\
                <el-timeline-item placement='top'\
                    v-for='(activity, index) in timenodes'\
                    v-bind:key='index'\
                    v-bind:timestamp='activity.time'\
                    color='#409EFF'>\
                    <div>\
                        <h4>{{activity.name}}</h4>\
                        <p v-for='(d,i) in activity.description' v-bind:class='d.color'>{{d.desc}}</p>\
                    </div>\
                </el-timeline-item>\
            </el-timeline>\
        </el-scrollbar>\
    </div>\
    ",
    props: {
        id: {
            type: String,
            required: true
        },
    },
    data: function () {
        return {
            reverse: false,
            timeline: {
                Nodes: []
            }
        };
    },
    created: function () {
        this.getTimeline();
    },
    methods: {
        getTimeline: function () {
            var _this = this;
            if (!_this.id) return;
            api.getTimeline(_this.id).then(function (res) {
                if (res.data.IsSuccess) {
                    _this.timeline = res.data.Data.Timeline;
                }
            }).catch(function (error) {
                console.log(error);
            });
        }
    },
    computed: {
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
}