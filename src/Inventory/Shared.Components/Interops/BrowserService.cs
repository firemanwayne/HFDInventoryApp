using Microsoft.JSInterop;

namespace Inventory.Shared.Components;

public class BrowserService : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public BrowserService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Inventory.Shared.Components/browserServiceInterop.js").AsTask());
    }

    public async ValueTask<BrowserDimension> GetDimensions()
    {
        var module = await moduleTask.Value;

        var dim = await module.InvokeAsync<BrowserDimension>("getDimensions");

        return dim;
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}

public class BrowserDimension
{
    public int Width { get; set; }
    public int Height { get; set; }
}