using CNC.Interfaces.Config;

namespace CNC.Implementation.Config
{
    public class MagazineConfig : BaseConfig
    {
        public string PositionKeyPrefix { get; set; } = string.Empty;

        public MagazineConfig() { }

        public MagazineConfig(string filePath, string mainSection, string countKey, 
            string positionKeyPrefix) 
            : base(filePath, mainSection, countKey)
        {
            PositionKeyPrefix = positionKeyPrefix;
        }
    }
}