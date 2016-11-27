$(document).ready(function () {
    $('#summernote').summernote();
});

document.getElementById("submit").onclick = function () {
    document.getElementById("Chapter_Text").value = $('#summernote').summernote('code');
    document.getElementById("Chapter_TagsString").value = takeTags();
};

function send(base64) {
    document.getElementById("Image").value = base64;
}