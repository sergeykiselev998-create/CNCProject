using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public class MillingToolRepository : ToolRepository<IMillingTool>
    {
        public MillingToolRepository(ExternalToolRepository<IMillingTool> externalRepository, AdditionalToolRepository<IMillingTool> additionalRepository) 
            : base(externalRepository, additionalRepository)
        {
        }
        
        protected override IMillingTool CreateEmptyTool()
        {
            return new MillingTool(
                id: -1,
                toolName: string.Empty,
                diameter: 0,
                cutterType: 0
            );
        }

    }
}