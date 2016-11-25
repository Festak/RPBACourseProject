$('body').on({ 'drop dragover dragenter': dropHandler1 }, '[data-image-creative-uploader]');
$('body').on({ 'change': regularImageUpload1 }, '#file');

function regularImageUpload1(e) {
    var file = $(this)[0],
        type = file.files[0].type.toLocaleLowerCase();
    if (type.match(/jpg/) !== null ||
       type.match(/jpeg/) !== null ||
       type.match(/png/) !== null ||
       type.match(/gif/) !== null) {

        readUploadedImage(file.files[0], '[data-image-creative]');
    }
}

function dropHandler1(e) {
    e.preventDefault();
    if (e.type === 'drop' && e.originalEvent.dataTransfer && e.originalEvent.dataTransfer.files.length) {

        var files = e.originalEvent.dataTransfer.files,
            type = files[0].type.toLocaleLowerCase();
        if (type.match(/jpg/) !== null ||
            type.match(/jpeg/) !== null ||
            type.match(/png/) !== null ||
            type.match(/gif/) !== null) {
            readUploadedImage(files[0], '[data-image-creative]');
        }

    }

    return false;
}
