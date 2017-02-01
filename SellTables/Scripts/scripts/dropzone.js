$('body').on({ 'drop dragover dragenter': dropHandler }, '[data-image-uploader]');
$('body').on({ 'change': regularImageUpload }, '#file');

function regularImageUpload(e) {
    var file = $(this)[0],
        type = file.files[0].type.toLocaleLowerCase();
    if (type.match(/jpg/) !== null ||
       type.match(/jpeg/) !== null ||
       type.match(/png/) !== null ||
       type.match(/gif/) !== null) {

        readUploadedImage(file.files[0], '[data-image]');
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
            readUploadedImage(files[0], '[data-image]');
        }

    }

    return false;
}

function readUploadedImage(img, targetUpload) {
    var reader;

    if (window.FileReader) {
        reader = new FileReader();
        reader.readAsDataURL(img);

        reader.onload = function (file) {
            if (file.target && file.target.result) {
                imageLoader(file.target.result, displayImage, targetUpload);
            }

        };

        reader.onerror = function () {
            throw new Error('Something went wrong!');
        };

    } else {
        throw new Error('FileReader not supported!');
    }

}

function imageLoader(src, callback, targetUpload) {
    var img;

    img = new Image();

    img.src = src;
    base64 = src.split(",")[1];

    img.onload = function () {
        imageResizer(img, callback, targetUpload);
    }
    if (targetUpload == '[data-image]') {
        send(base64);
    }
    if (targetUpload == '[data-image-creative]') {
        sendForCreative(base64);
    }
    

}

function imageResizer(img, callback, targetUpload) {

    var canvas = document.createElement('canvas');
    canvas.width = 200;
    canvas.height = 200;
    context = canvas.getContext('2d');
    context.drawImage(img, 0, 0, 200, 200);

    callback(canvas.toDataURL(), targetUpload);

}

function displayImage(img, targetUpload) {
    $(targetUpload).attr('src', img);
}

//-----------------------
$(function () {
    $('body').on('dragover', function () {
        $(this).addClass('hover');
        $('#dropzone').addClass('activate');
    });

    $('body').on('dragleave', function () {
        $(this).removeClass('hover');
        $('#dropzone').removeClass('activate');
    });

    $('#dropzone').on('dragover', function () {
        $(this).addClass('hover');
    });

    $('#dropzone').on('dragleave', function () {
        $(this).removeClass('hover');
    });

    $('#dropzone input').on('change', function (e) {
        var file = this.files[0];

        $('#dropzone').removeClass('hover');

        if (this.accept && $.inArray(file.type, this.accept.split(/, ?/)) == -1) {
            return alert('File type not allowed.');
        }

        $('#dropzone').addClass('dropped');
        $('#dropzone img').remove();

        if ((/^image\/(gif|png|jpeg)$/i).test(file.type)) {
            var reader = new FileReader(file);

            reader.readAsDataURL(file);

            reader.onload = function (e) {
                var data = e.target.result,
                    $img = $('<img />').attr('src', data).fadeIn();

                $('#dropzone div').html($img);
            };
        } else {
            var ext = file.name.split('.').pop();

            $('#dropzone div').html(ext);
        }
    });
});