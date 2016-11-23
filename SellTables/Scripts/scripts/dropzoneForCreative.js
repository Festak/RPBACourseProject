$('body').on({ 'drop dragover dragenter': dropHandler }, '[data-image-creative-uploader]');
$('body').on({ 'change': regularImageUpload }, '#file');

function regularImageUpload(e) {
    var file = $(this)[0],
        type = file.files[0].type.toLocaleLowerCase();
    if (type.match(/jpg/) !== null ||
       type.match(/jpeg/) !== null ||
       type.match(/png/) !== null ||
       type.match(/gif/) !== null) {

        readUploadedImage(file.files[0]);
    }
}

function dropHandler(e) {
    e.preventDefault();

    if (e.type === 'drop' && e.originalEvent.dataTransfer && e.originalEvent.dataTransfer.files.length) {

        var files = e.originalEvent.dataTransfer.files,
            type = files[0].type.toLocaleLowerCase();
        if (type.match(/jpg/) !== null ||
            type.match(/jpeg/) !== null ||
            type.match(/png/) !== null ||
            type.match(/gif/) !== null) {
            readUploadedImage(files[0]);
        }

    }

    return false;
}

function readUploadedImage(img) {
    var reader;

    if (window.FileReader) {
        reader = new FileReader();
        reader.readAsDataURL(img);

        reader.onload = function (file) {
            if (file.target && file.target.result) {
                imageLoader(file.target.result, displayImage);
            }

        };

        reader.onerror = function () {
            throw new Error('Something went wrong!');
        };

    } else {
        throw new Error('FileReader not supported!');
    }

}

function imageLoader(src, callback) {
    var img;

    img = new Image();

    img.src = src;
    base64 = src.split(",")[1];

    img.onload = function () {
        imageResizer(img, callback);
    }
    //console.log(base64);
    sendForCreative(base64);


}

function imageResizer(img, callback) {
    var canvas = document.createElement('canvas');
    canvas.width = 200;
    canvas.height = 200;
    context = canvas.getContext('2d');
    context.drawImage(img, 0, 0, 200, 200);

    callback(canvas.toDataURL());

}

function displayImage(img) {
    $('[data-image-creative]').attr('src', img);
}
