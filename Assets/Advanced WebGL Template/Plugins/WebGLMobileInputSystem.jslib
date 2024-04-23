mergeInto(LibraryManager.library, {

    activateDialogWindow2: function (inputValue) {
        activateDialogWindow3(Pointer_stringify(inputValue))
    },

    getJsonMessage: function () {
        var s3 = s2;
        s2 = "";

        var bufferSize = lengthBytesUTF8(s3) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(s3, buffer, bufferSize);
        return buffer;
    },

    getMessage: function () {
        var var1 = hasActiveElement2;
        hasActiveElement2 = 2;

        return var1;
    },

    isMobileBrowser2: function () {
        return (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent));
    }

});