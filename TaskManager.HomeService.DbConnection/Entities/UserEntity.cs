using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core;

namespace TaskManager.HomeService.DbConnection.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }

        public TaskEntity[] Tasks { get; set; }
    }
}
