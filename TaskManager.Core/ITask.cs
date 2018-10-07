using System;
using System.Linq.Expressions;

namespace TaskManager.Core
{
    public interface ITask : ITaskInfo
    {
        int Id { get; }

        TaskStatus Status { get; }
    }
}