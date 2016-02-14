$.ajax({
    url: "https://api.meetup.com/2/events?offset=0&format=json&limited_events=False&group_urlname=Manchester-NET&page=200&fields=&order=time&desc=false&status=upcoming&sig_id=65452892&sig=67587732d3ee3491b32c778b42f6fbe50908a5f3",
    jsonp: "callback",
    dataType: "jsonp",
    timeout: 15000,
    success: function (response) {
        if (response.results.length > 0) {
            var meetingData = response.results[0];

            $("#loadingEvent").hide();

            $("#eventTitle").text(meetingData.name);
            $("#eventText").html(meetingData.description);
            $("#bookingButton").attr("href", meetingData.event_url).attr("disabled", false);
            $("#eventLocationName").text(meetingData.venue.name);
            $("#eventLocationAddress").text(meetingData.venue.address_1);
            $("#eventAttendees").text(meetingData.yes_rsvp_count);

            var startDateTime = new Date(meetingData.time);
            var endDateTime = new Date(meetingData.time + meetingData.duration);

            $("#eventDate").text(startDateTime.toString("dddd ddS MMMM yyyy"));
            $("#eventStartTime").text(startDateTime.toString("HH:mm"));
            $("#eventEndTime").text(endDateTime.toString("HH:mm"));

            $("#eventDetails").show();
        }
        else {
            $("#loadingEvent").hide();
            $("#noEvent").show();
        }
    },
    error: function () {
        $("#loadingEvent").hide();
        $("#eventError").show();
    }
});