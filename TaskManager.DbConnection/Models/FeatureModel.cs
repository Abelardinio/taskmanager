using TaskManager.Core;

namespace TaskManager.DbConnection.Models
{
    public class FeatureModel : IFeatureModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
    }
}
