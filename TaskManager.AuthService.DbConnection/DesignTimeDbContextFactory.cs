using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.AuthService.DbConnection
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            return new Context("_", false);
        }
    }
}