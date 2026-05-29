using CNC.Implementation.Tool;
using CNC.Interfaces.Tool;

namespace CNC.Implementation.ToolPanel.Repositories
{
    public class TurningToolRepository : ToolRepository<ITurningTool>
    {
        public TurningToolRepository(ExternalToolRepository<ITurningTool> externalRepository, AdditionalToolRepository<ITurningTool> additionalRepository) 
            : base(externalRepository, additionalRepository)
        {
        }
        
        protected override ITurningTool CreateEmptyTool()
        {
            return new TurningTool(
                id: -1,
                toolName: string.Empty,
                cutterType: 0//,
               // shiftX: 0,
                //  shiftY: 0
            );
        }
    }
}