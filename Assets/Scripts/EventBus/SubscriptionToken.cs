using System;

public class SubscriptionToken : IDisposable
{
    private readonly Action _unsubscribeAction;

    public SubscriptionToken(Action unsubscribeAction)
    {
        _unsubscribeAction = unsubscribeAction;
    }

    public void Dispose()
    {
        _unsubscribeAction?.Invoke();
    }
}