using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core;

namespace TaskManager.DbConnection.Entities
{
    [Table("Features")]
    public class FeatureEntity : IFeature
    {
        public FeatureEntity() { }

        public FeatureEntity(IFeatureInfo info)
        {
            Name = info.Name;
            Description = info.Description;
            ProjectId = info.ProjectId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public List<TaskEntity> Tasks { get; set; }
    }
}