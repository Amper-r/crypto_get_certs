var host_name = "com.certs.native";
var port = null;

function connectToNative() {
    console.log('Connecting to native host: ' + host_name);
    port = chrome.runtime.connectNative(host_name);
}

function sendNativeMessage(msg) {
    message = msg;
    console.log('Sending message to native app: ' + JSON.stringify(message));
    port.postMessage(message);
    console.log('Sent message to native app: ' + msg);
}


function onDisconnected() {
    console.log(chrome.runtime.lastError);
    console.log('disconnected from native app.');
    port = null;
}

function unicodeToChar(text) {
    return text.replace(/\\u[\dA-F]{4}/gi, 
           function (match) {
                return String.fromCharCode(parseInt(match.replace(/\\u/g, ''), 16));
           });
 }

chrome.runtime.onMessageExternal.addListener(
    function(request, sender, sendResponse) {
        blacklistedWebsite = 'http : / / yourdomain . com /';
        if (sender.url == blacklistedWebsite)
        return;
        if (request.task) {
            console.log(request.task);
            if(request.task == "get_certs_list"){
                connectToNative();
                sendNativeMessage([{task: "get_certs",data: null}]);
                port.onMessage.addListener(function(msg){
                    sendResponse(JSON.parse(msg.data));
                });
                port = null;
            }
        }   
    }
);