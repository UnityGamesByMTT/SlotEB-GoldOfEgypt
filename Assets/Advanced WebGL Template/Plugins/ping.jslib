mergeInto(LibraryManager.library, {

    ping: function (address) {
    var ws = new WebSocket(Pointer_stringify(address));
    ws.onopen = function () {
      ws.send(Date.now());
    };
    ws.onmessage = function (message) {
      SendMessage("MyObject", "Receive", Date.now() - message.data);
      ws.close();
    };
  }
});