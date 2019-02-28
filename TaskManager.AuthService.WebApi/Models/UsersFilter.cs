using System.Linq;
using TaskManager.Core;

namespace TaskManager.AuthService.WebApi.Models
{
    public class UsersFilter : IUnsortableFilter<IUser>
    {
        public string Value { get; set; }

        public IQueryable<IUser> Filter(IQueryable<IUser> input)
        {
            return input.Where(x =>
                string.IsNullOrEmpty(Value) ||
                x.Username.Contains(Value) ||
                x.FirstName.Contains(Value) ||
                x.LastName.Contains(Value) ||
                x.Email.Contains(Value));
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}