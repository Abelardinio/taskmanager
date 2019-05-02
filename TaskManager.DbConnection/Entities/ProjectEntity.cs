using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core;

namespace TaskManager.DbConnection.Entities
{
    [Table("Projects")]
    public class ProjectEntity : IProject
    {
        public ProjectEntity() { }
        public ProjectEntity(int userId, IProjectInfo info)
        {
            Name = info.Name;
            Description = info.Description;
            CreatorId = userId;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }

        public List<FeatureEntity> Features { get; set; }
    }
}