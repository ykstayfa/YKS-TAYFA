
namespace YKSTayfa.Services;

public interface IPushService
{
    Task RegisterAsync();
    Task UnregisterAsync();
    bool IsRegistered { get; }
}

public class PushService : IPushService
{
    public bool IsRegistered { get; private set; }

    public Task RegisterAsync() { IsRegistered = true; return Task.CompletedTask; }
    public Task UnregisterAsync() { IsRegistered = false; return Task.CompletedTask; }
}
