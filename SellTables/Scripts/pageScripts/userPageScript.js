function send(base64) {
    angular.element(document.getElementById('dropzone')).scope().uploadImage(base64);
}

function sendForCreative(base64) {
    document.getElementById('imageUploadInput').value = base64;
}

function clearInput() {
    document.getElementById('imageUploadInput').value = '';
}