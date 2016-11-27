window.setCookie = setCookie = function (name, value, path) {
    document.cookie = name + "=" + escape(value) + (path ? "; path=" + path : "");
    return location.reload();
};