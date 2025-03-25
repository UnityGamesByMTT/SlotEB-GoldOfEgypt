mergeInto(LibraryManager.library, {
    SendLogToReactNative: function (messagePtr) {
        var message = UTF8ToString(messagePtr);
        // console.log('jslib fun : ' + message);
        if (window.ReactNativeWebView) {
          window.ReactNativeWebView.postMessage(message);
        } 
    },

    SendPostMessage: function(messagePtr) {
      var message = UTF8ToString(messagePtr);
      console.log('SendReactPostMessage, message sent: ' + message);
      if(window.ReactNativeWebView){
        if(message == "authToken"){
          var injectedObjectJson = window.ReactNativeWebView.injectedObjectJson();
          var injectedObj = JSON.parse(injectedObjectJson);

          window.ReactNativeWebView.postMessage('Injected obj : ' + injectedObjectJson);
          
          var combinedData = JSON.stringify({
              socketURL: injectedObj.socketURL.trim(),
              cookie: injectedObj.token.trim(),
              nameSpace: injectedObj.nameSpace ? injectedObj.nameSpace.trim() : ""
          });

          if (typeof SendMessage === 'function') {
            SendMessage('SocketManager', 'ReceiveAuthToken', combinedData);
          }
        }
        window.ReactNativeWebView.postMessage(message);
      }
      else if(window.parent){
        if(message == "authToken"){
          window.addEventListener('message', function(event){
            if(event.data.type === 'authToken'){
              var combinedData = JSON.stringify({
                  cookie: event.data.cookie,
                  socketURL: event.data.socketURL,
                  nameSpace: event.data && event.data.nameSpace ? event.data.nameSpace : ''
              }); 

              if (typeof SendMessage === 'function') {
                SendMessage('SocketManager', 'ReceiveAuthToken', combinedData);
              }
              else{
                console.log('SendMessage is not a func');
              }
            }
          });
        }
        window.parent.postMessage(message, "*");
      }
    }
});
