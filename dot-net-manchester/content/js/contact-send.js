(function () {


    function ContactViewModel() {
        var self = this;

        self.email = ko.observable();
        self.message = ko.observable();
        self.name = ko.observable();
        self.reCaptchaToken = "";

        $("#sendButton").on("click", function () {
            self.reCaptchaToken = grecaptcha.getResponse();

            $.post("/contact", ko.toJS(self), function (data) {
                console.log(data)
            });
        });      
    }

    ko.applyBindings(new ContactViewModel());
})();