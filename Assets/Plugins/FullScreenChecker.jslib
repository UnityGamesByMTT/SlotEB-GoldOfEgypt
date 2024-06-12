var FullScreenChecker = {
    IsFullScreen: function() {
        // Check if the document is in fullscreen mode
        if (document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement || document.msFullscreenElement) {
            return 1; // true
        } else {
            return 0; // false
        }
    }
};

mergeInto(LibraryManager.library, FullScreenChecker);