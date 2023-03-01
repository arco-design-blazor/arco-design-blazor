window.WindowDescriptor = function (){
    return {
        InnerHeight: window.innerHeight,
        InnerWidth: window.innerHeight,
        ScreentLeft: window.screenLeft,
        ScreentTop: window.screenTop,
        ScreentX: window.screenX,
        ScreentY: window.screenY,
        Alert: function (arg) {
            window.alert(arg);
        },
        ResizeTo: function (width, height) {
            window.resizeTo(width, height);
        },
        AddEventListener: function (eventName, listener, useCapture) {
            window.addEventListener(eventName, listener, useCapture);
        },
        RemoveEventListener: function (eventName, listener, useCapture) {
            window.removeEventListener(eventName, listener, useCapture);
        }

    }
}