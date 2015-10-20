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

setInterval(refreshData, 10000);