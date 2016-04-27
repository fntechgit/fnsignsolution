$.getScript("/js/jquery.cookie.js", function () {});

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

            if (data.d.event_id == 0) {

                //console.log('EventID Lost');

                var cookie_id = $.cookie("FNSIGN_EventID");

                // if Session["event_id"] Is Null check for the cookie
                $.ajax({
                    type: "POST",
                    url: "/display.asmx/loginAgain",
                    data: "{'event_id': '" + cookie_id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, status) {
                        refreshData();
                    }
                });

            } else {

                $("#session_type").text(data.d.event_type);
                $("#session_title").text(data.d.name);

                if (data.d.name.length > 30) {
                    $("#session_title").attr("class", "session-type");
                } else {
                    $("#session_title").attr("class", "session-type-big");
                }

                $("#start_time").text(data.d.event_start);

                next();

                background();

                twitter();

            }
        }
    });

}

function twitter() {

    $(".twitter").show();
    $(".announcement").hide();

    $(".twitter").animate({ left: "-1080" }, 500, "swing", slideRight);
}

function announcements() {
    // check for announcement
    terminal_id = $("#terminal_id").val();
    template_id = $("#template_id").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/get_message",
        data: "{'template_id': " + template_id + ", 'terminal_id': " + terminal_id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            if (data.d.id > 0) {
                // render the announcement
                $("#message_title").text(data.d.title);
                $("#message").text(data.d.message);

                if (data.d.pic != null) {
                    $("#message_img").html(data.d.pic);
                }

                $(".twitter").hide();
                $(".announcement").show();

            } else {
                twitter();
            }
        }
    });
}


var $mq = $(".ticker");

function refreshNews() {

    var template_id = $("#template_id").val();
    var terminal_id = $("#terminal_id").val();

    $.ajax({
        type: "POST",
        url: "/display.asmx/get_message",
        data: "{'template_id': " + template_id + ", 'terminal_id': " + terminal_id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {

            console.log("Length: " + data.d.length);

            if (data.d.id < 1) {

                $mq.hide();
                showMarquee();

            } else {

                $mq.show();
                $mq.marquee('destroy');
                $mq.html(data.d.message);
                $mq.marquee({ duration: 10000 });
            }
        }
    });
}

$mq.bind('finished', showMarquee);

function showMarquee() {
    refreshNews();
}

function slideRight() {

    tweet();

    $(".twitter").css("left", "1580px");

    setTimeout(enterRight, 1000);
}

function enterRight() {
    $(".twitter").animate({ left: "40px" }, 500, "swing");
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

            if (data.d.name.length > 90) {
                $("#next_session").attr("class", "next-session-small");
            } else {
                $("#next_session").attr("class", "next-session");
            }

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

//setInterval(refreshData, 10000);