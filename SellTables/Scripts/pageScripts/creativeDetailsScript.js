var swiper = new Swiper('.swiper-container', {
    pagination: '.swiper-pagination',
    paginationClickable: true,
    nextButton: '.swiper-button-next',
    prevButton: '.swiper-button-prev',
    spaceBetween: 30,
    grabCursor: true,
    autoHeight: true,
    debugger: true
});

var $vid = document.getElementById('bgvid');

function calculateNewSize() {
    var videoWidth,
        videoHeight,
        videoAspectRatio = 16 / 9,
        windowWidth = window.innerWidth,
        windowHeight = window.innerHeight,
        windowAspectRatio = window.innerWidth / window.innerHeight;

    if (windowAspectRatio < videoAspectRatio) {
        videoHeight = windowHeight;
        videoWidth = Math.ceil(videoHeight * videoAspectRatio);
    } else {
        videoWidth = windowWidth;
        videoHeight = videoWidth / videoAspectRatio;
    }


    $vid.style.width = videoWidth + 'px';
    $vid.style.height = videoHeight + 'px';
}

window.onresize = calculateNewSize;
calculateNewSize();