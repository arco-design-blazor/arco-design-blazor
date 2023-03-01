using Microsoft.JSInterop;

namespace ArcoDesign.Core;
internal class Js {
    private readonly IJSRuntime jsRuntime;

    public Js(IJSRuntime jsRuntime) {
        this.jsRuntime = jsRuntime;
    }

    public JsScope CreateScope(string jsPath) {
        if (jsRuntime == null) {
            throw new TypeInitializationException(nameof(Js), new Exception("JsRuntime初始化异常"));
        }
        return new JsScope(jsRuntime, jsPath);
    }
}
