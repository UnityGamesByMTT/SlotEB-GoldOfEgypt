var CookieManager = {
    GetAuthToken: function(cookieName) {
        var name = cookieName + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i].trim();
            if (c.indexOf(name) == 0) {
                var value = c.substring(name.length, c.length);
                var lengthBytes = lengthBytesUTF8(value) + 1;
                var stringOnHeap = _malloc(lengthBytes);
                stringToUTF8(value, stringOnHeap, lengthBytes);
                return stringOnHeap;
            }
        }
        return null;
    },
    FreeAuthToken: function(ptr) {
        _free(ptr);
    }
};

mergeInto(LibraryManager.library, CookieManager);
