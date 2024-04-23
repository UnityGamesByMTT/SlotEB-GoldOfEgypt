mergeInto(LibraryManager.library, {

    CopyToClipboardAndShare: function (textToCopy) {
        const toCopy = UTF8ToString(textToCopy);
        if (navigator.clipboard && navigator.clipboard.writeText) {
            navigator.clipboard.writeText(toCopy).then(function () {
                console.debug("Copied to clipboard navigator: " + toCopy);
            }, function (error) {
                console.error("Failed to copy to clipboard navigator", error);
            });
        } else {
            const textArea = document.createElement("textarea");
            textArea.value = toCopy;

            // Avoid scrolling to bottom
            textArea.style.top = "0";
            textArea.style.left = "0";
            textArea.style.position = "fixed";

            document.body.appendChild(textArea);
            textArea.focus();
            textArea.select();

            try {
                const successful = document.execCommand('copy');
                const msg = successful ? 'successful' : 'unsuccessful';
                console.debug('Fallback: Copying text command was ' + msg);
            } catch (err) {
                console.error('Fallback: Unable to copy', err);
            }

            document.body.removeChild(textArea);
        }
       
    }
});