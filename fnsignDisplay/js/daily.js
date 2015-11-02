$(document).ready(function() {
    summit();
});

function summit() {

    var height = $(".inner").height();

    $(".inner").animate({ top: "-" + height}, 100000, "linear", slideBottom);

}

function slideBottom() {

    var height = $(".inner").height();

    $(".inner").css("top", "1300px");

    $(".inner").animate({ top: "-" + height}, 100000, "linear", cycle);
}

function cycle() {
    slideBottom();
}