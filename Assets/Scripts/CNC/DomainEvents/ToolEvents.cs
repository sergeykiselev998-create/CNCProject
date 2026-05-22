using CNC.Interfaces.Events;
namespace CNC.DomainEvents
{
    public class ToolNameChanged : IEventMessage
    {
        public int ToolId { get; }
        public string NewName { get; }

        public ToolNameChanged(int toolId, string newName)
        {
            ToolId = toolId;
            NewName = newName;
        }
    }

    public class ToolRemovedFromBuffer : IEventMessage
    {
        public int ToolId { get; }

        public ToolRemovedFromBuffer(int toolId)
        {
            ToolId = toolId;
        }
    }

    public class ToolUnloadedFromMagazine : IEventMessage
    {
        public int ToolId { get; }

        public ToolUnloadedFromMagazine(int toolId)
        {
            ToolId = toolId;
        }
    }

    public class ToolLoadedToMagazine : IEventMessage
    {
        public int ToolId { get; }

        public ToolLoadedToMagazine(int toolId)
        {
            ToolId = toolId;
        }
    }

    public class ToolAddedToBuffer : IEventMessage
    {
        public int ToolId { get; }

        public ToolAddedToBuffer(int toolId)
        {
            ToolId = toolId;
        }
    }
}


