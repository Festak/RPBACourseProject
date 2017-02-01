$(document).ready(function () {
    $('#summernote').summernote();
});

document.getElementById("submit").onclick = function () { 
    document.getElementById("Chapter_Text").value = $('#summernote').summernote('code');
    var element = document.getElementById("ex1_value");
    console.log(element[0].value);
 
    document.getElementById("Creative_Category_Name").value = $('ex1').val();
    document.getElementById("Chapter_TagsString").value = takeTags();
};

function send(base64) {
    document.getElementById("Image").value = base64;
}

