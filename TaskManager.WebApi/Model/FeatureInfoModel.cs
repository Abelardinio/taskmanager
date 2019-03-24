using TaskManager.Core;

namespace TaskManager.WebApi.Model
{
    public class FeatureInfoModel : IFeatureInfo
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}