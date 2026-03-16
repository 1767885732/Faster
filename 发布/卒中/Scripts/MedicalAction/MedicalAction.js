var MecicalActionHandle = function (eventId) {
    this.eventId = eventId;
    this.medicalAction = "";
}

MecicalActionHandle.prototype.LoadMedicalAction = function () {
    if (this.medicalAction != "") {
        $(".form_tit").append(this.medicalAction);
        return;
    }

    $.ajax({
        url: "/MedicalAction/MedicalAction?eventId=" + this.eventId,
        type: "get",
        dataType: "html",
        async: false,
        success: function (html) {
            if (html.trim() != "") {
                $(".form-tit").append(html);
                this.medicalAction = html;
            }
        }
    })
}
MecicalActionHandle.prototype.Clear = function () {
    this.medicalAction = "";
}
MecicalActionHandle.prototype.Set = function (data) {
    this.medicalAction = data;
}