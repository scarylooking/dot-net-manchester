var model = {};


function ContactViewModel() {
    var self = this

    self.emailAddress = ko.observable("");
    self.message = ko.observable("");
    self.name = ko.observable("");

    self.status = {
        sent: ko.observable(false),
        emailAddress: ko.observable(true),
        name: ko.observable(true),
        message: ko.observable(true),
        recaptcha: ko.observable(true),
        failed: ko.observable(false)
    }

    self.status.errorInForm = ko.computed(function () {
        var self = this;
        return !self.status.emailAddress() ||
            !self.status.name() ||
            !self.status.message() ||
            !self.status.recaptcha();
    }, this)

    self.recaptchaToken = "";
};

function SetModelState(stateData) {
    ko.mapping.fromJS(stateData, {}, model.status);
}

$(document).ready(function () {

    model = new ContactViewModel();

    ko.applyBindings(model);

    $("#sendButton").on("click", function () {
        model.recaptchaToken = grecaptcha.getResponse();

        grecaptcha.reset();

        $.ajax({
            type: "POST",
            url: "/contact",
            data: ko.toJS(model),
            success: function (data) {
                SetModelState(data);
                console.log(data);
            },
            error: function (response) {
                SetModelState(response.responseJSON);
            }
        });
    });
});