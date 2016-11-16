$(document).ready(function () {
    $('body').scrollspy({ target: ".col-md-4", offset: 50 });
    $("#myScroll a").on('click', function (event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {
                window.location.hash = hash;
            });
        }
    });
});