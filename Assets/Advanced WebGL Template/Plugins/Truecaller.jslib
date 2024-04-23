var plugin = {
    OpenTab : function(url,callbackObjectName, callbackMethodName)
    {
	
		TrueCallerCallbackObjectName = UTF8ToString(callbackObjectName);
        TrueCallerCallbackMethodName = UTF8ToString(callbackMethodName);
		var e = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var r = "";
		const i = e.length;
		for (var t = 0; t < 16; t++)
			r += e.charAt(Math.floor(Math.random() * i));;
		var t=url;
		SendMessage(TrueCallerCallbackObjectName, TrueCallerCallbackMethodName, 'First_'+r.trim());
		var _android = /(android)/i.test(navigator.userAgent);
		console.debug('Fallback: Android has focus command was ' + _android);
		if(_android)
		{
			var _url = "truecallersdk://truesdk/web_verify?requestNonce=" + r.trim() + "&partnerKey=lbm3rd39349deed554c59b80fc0efa133fe42&partnerName=RummyPassion&lang=en&title=login&skipOption=useanothermethod&ctaColor=%23721B0B&loginPrefix=getstarted&type=btmsheet&btnShape=round";
			
			console.debug('Fallback: Truecaller url ' + _url);
			//window.alert('my url '+_url);
			//window.open(_url,'_blank');
			
			//window.location = _url;
			window.location.assign(
			  _url
			);
			
			const timeForTrueCallerWindow = setTimeout(function() {

			  if(window.document.hasFocus()){
				 // Truecaller app not present on the device and you redirect the user 
				 // to your alternate verification page
				console.debug('Fallback: Truecaller has focus command was ' + r);
				SendMessage(TrueCallerCallbackObjectName, TrueCallerCallbackMethodName, 'Second_'+r.trim());
				  
				var local = localStorage.getItem("isTrueCallerIntiate");
				if(local!=null)
				{
					console.debug('Fallback: Truecaller has get focus command in local ' + r);
					localStorage.removeItem("isTrueCallerIntiate");
					SendMessage(TrueCallerCallbackObjectName, TrueCallerCallbackMethodName, 'Fifth_'+r.trim());
				}
				else{
					console.debug('Fallback: Truecaller has get focus command else  local ' + r);
				}
				clearTimeout(timeForTrueCallerWindow);
			  }else{
				 // Truecaller app present on the device and the profile overlay opens
				 // The user clicks on verify & you'll receive the user's access token to fetch the profile on your 
				 // callback URL - post which, you can refresh the session at your frontend and complete the user  verification
				
				 console.debug('Fallback: Truecaller has lost focus command was ' + r);
				 SendMessage(TrueCallerCallbackObjectName, TrueCallerCallbackMethodName, 'Third_'+r.trim());
				 localStorage.setItem("isTrueCallerIntiate", 1);
			  }
			}, 600);
		}
		else{
			SendMessage(TrueCallerCallbackObjectName, TrueCallerCallbackMethodName, 'Fourth_'+r.trim());
		}
		
		
    },
	
};
mergeInto(LibraryManager.library, plugin);