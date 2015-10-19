function refreshData() {

    // refresh the data

    location = $("#location_sched").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/current",
        data: "{'location': '" + location + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            $("#session_type").text(data.d.session_type);
            $("#session_title").text(data.d.name);
            $("#session_time").text(data.d.session_start + ' to ' + data.d.session_end);

            next();

        }
    });

}

function next() {

    location = $("#location_sched").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/next",
        data: "{'location': '" + location + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            $("#next_session").text('NEXT: ' + data.d.name);

        }
    });
}

setInterval(refreshData, 6000);