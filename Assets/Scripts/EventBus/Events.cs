using CNC.Interfaces.Events;

public class FooEventMessage : IEventMessage
{
    public string Message { get; }

    public FooEventMessage(string message)
    {
        Message = message;
    }
}