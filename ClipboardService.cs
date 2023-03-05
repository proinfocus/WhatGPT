using Microsoft.JSInterop;

namespace WhatGPT;

public interface IClipboardService
{
    ValueTask ToClipboard(string text);
    ValueTask ExecuteCommand(string command, object[] args = null!);
    ValueTask<string> FromClipboard();
    ValueTask SetFocus(string id);
}

public class ClipboardService : IClipboardService
{
    private readonly IJSRuntime _jsInterop;
    public ClipboardService(IJSRuntime jsInterop) => _jsInterop = jsInterop;

    public ValueTask ExecuteCommand(string command, object[] args = null!)
        => _jsInterop.InvokeVoidAsync(command, args);
    
    public ValueTask<string> FromClipboard()
        => _jsInterop.InvokeAsync<string>("navigator.clipboard.readText");

    public ValueTask ToClipboard(string text)
        => _jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);

    public ValueTask SetFocus(string id)
        => _jsInterop.InvokeVoidAsync($"document.getElementById('{id}').focus()");
}