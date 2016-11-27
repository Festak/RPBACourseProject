$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
        angular.element($('#scroller')).scope().load();
    }
});