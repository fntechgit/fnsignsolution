
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