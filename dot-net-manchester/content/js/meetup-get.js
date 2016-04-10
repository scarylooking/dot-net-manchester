(function () {
    function Meetup(item) {
        this.description = ko.observable(item.description.split("----")[0]);
        this.url = ko.observable(item.event_url);
        this.name = ko.observable(item.name);
        this.venue = ko.observable(item.venue.name);
        this.address = ko.observable(item.venue.address_1);
        
        this.attendees = ko.observable(item.yes_rsvp_count);
        this.start = ko.observable(item.time);

        this.start = {
            date: ko.observable(moment(item.time).format('dddd, Do MMMM YYYY')),
            time: ko.observable(moment(item.time).format('HH:mm'))
        }

        if (item.duration) {
            var endTime = new Date(item.time + item.duration);

            this.end = {
                date: ko.observable(moment(endTime).format('dddd, Do MMMM YYYY')),
                time: ko.observable(moment(endTime).format('HH:mm'))
            }             
        }
    }

    function MeetupViewModel() {
        var self = this;
        var url = "https://api.meetup.com/2/events?offset=0&format=json&limited_events=False&group_urlname=Manchester-NET&page=200&fields=&order=time&desc=false&status=upcoming&sig_id=65452892&sig=67587732d3ee3491b32c778b42f6fbe50908a5f3";

        self.meetups = ko.observableArray([]);
        self.loading = ko.observable(true);
        self.error = ko.observable(false);
        
        $.ajax({
            url: url,
            jsonp: "callback",
            dataType: "jsonp",
            timeout: 15000,
            success: function (data) {
                if (data.results && data.results.length > 0) {
                    for (var i = 0, iLen = data.results.length; i < iLen; i++) {
                        var item = data.results[i];

                        if (!item.announced) continue;

                        var modelItem = new Meetup(item);
                        self.meetups.push(modelItem);
                    }
                }

                self.loading(false);
            },
            error: function () {
                self.error(true);
                self.loading(false);
            }
        });
    }

    ko.applyBindings(new MeetupViewModel());
})();