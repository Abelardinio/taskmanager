using System;

namespace TaskManager.Core
{
    public interface IProject : IProjectInfo
    {
        int Id { get; }
        int CreatorId { get; }
    }
}