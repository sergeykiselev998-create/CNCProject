using System;
using CNC.Interfaces.Events;
using Reflex.Attributes;
using UnityEngine;

public class Example : MonoBehaviour
{
    [Inject]
    private IEventBus _eventBus;
    
    private IDisposable _disposable;
    private IDisposable _disposable1;
    private IDisposable _disposable2;
    
    [ContextMenu("Subscribe")]
    public void Subscribe()
    {
        _disposable = _eventBus.Subscribe<FooEventMessage>(OnToolChanged1, 1);
        _disposable2 = _eventBus.Subscribe<FooEventMessage>(OnToolChanged2, 2);
        _disposable1 = _eventBus.Subscribe<FooEventMessage>(OnToolChanged, 0);
    }

    [ContextMenu("Publish")]
    public void Publish()
    {
        _eventBus.Publish(new FooEventMessage("Message"));
    }

    private void OnToolChanged(FooEventMessage evt)
    {
        Debug.Log($"Priority 0: {evt.Message}");
    }
    
    private void OnToolChanged1(FooEventMessage evt)
    {
        Debug.Log($"Priority 1: {evt.Message}");
    }
    private void OnToolChanged2(FooEventMessage evt)
    {
        Debug.Log($"Priority 2: {evt.Message}");
    }
    
    private void OnDestroy()
    {
        _disposable?.Dispose();
        _disposable1?.Dispose();
        _disposable2?.Dispose();
    }
}