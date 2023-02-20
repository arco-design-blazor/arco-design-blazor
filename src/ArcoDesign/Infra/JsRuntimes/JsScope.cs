using Microsoft.JSInterop;

namespace ArcoDesign.Infra.JsRuntimes;
public class JsScope : IAsyncDisposable {
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    internal JsScope(IJSRuntime jsRuntime, string jsPath) {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", jsPath).AsTask());
    }

    public async ValueTask DisposeAsync() {
        if (moduleTask.IsValueCreated) {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    public async ValueTask<T> InvokeAsync<T>(string methodName,params object[] args) {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<T>(methodName, args);
    }

    public async ValueTask InvokeVoidAsync(string methodName, params object[] args) {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync(methodName, args);
    }

}
