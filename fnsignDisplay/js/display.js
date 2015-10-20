function refreshData() {

    // refresh the data

    location_id = $("#location_sched").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/current",
        data: "{'location': '" + location_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            $("#session_type").text(data.d.event_type);
            $("#session_title").text(data.d.name);
            $("#start_time").text(data.d.event_start);

            next();

            background();

            tweet();
        }
    });

}

function next() {

    location_id = $("#location_sched").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/next",
        data: "{'location': '" + location_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            $("#next_session").text(data.d.event_start + ': ' + data.d.name);

        }
    });
}

function background() {

    terminal_id = $("#terminal_id").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/template",
        data: "{'terminal': " + terminal_id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            $('#thebody').css('background-image', 'url("' + data.d + '")');

        }
    });
}

function tweet() {
    $.ajax({
        type: "POST",
        url: "/display.asmx/random",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            $("#twitpic").attr('src', data.d.profilepic);
            $("#fullname").text(data.d.full_name);
            $("#username").text(data.d.username);
            $("#tweet").text(data.d.description);

            if (data.d.source != null) {

                console.log(data.d.source);

                $("#twitimg").html('<img src="' + data.d.source + '" />');
            } else {
                $("#twitimg").text('');
            }
        }
    });
}

setInterval(refreshData, 10000);