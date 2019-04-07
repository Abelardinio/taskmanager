using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Resources;
using TaskManager.Core.DataAccessors;
using TaskManager.Core.DataProviders;
using TaskManager.Core.Exceptions;

namespace TaskManager.AuthService.Data.DataProviders
{
    public class LoginDataProvider : ILoginDataProvider
    {
        private readonly IUsersDataAccessor _usersDataAccessor;
        private readonly IHashCreator _hashCreator;
        private readonly ITokenProvider _tokenProvider;

        public LoginDataProvider(
            IUsersDataAccessor usersDataAccessor, 
            IHashCreator hashCreator, 
            ITokenProvider tokenProvider)
        {
            _usersDataAccessor = usersDataAccessor;
            _hashCreator = hashCreator;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _usersDataAccessor.GetLoginInfo().FirstOrDefaultAsync(x => x.Username == username);

            if (user != null && string.IsNullOrEmpty(user.Password))
            {
                await _usersDataAccessor.SetPasswordAsync(user.Id, _hashCreator.Create(username, password, user.PasswordSalt));
                return _tokenProvider.Get(user);
            }

            if (user == null || user.Password != _hashCreator.Create(username, password, user.PasswordSalt))
            {
                throw new AuthException(ErrorMessages.Login_AuthError);
            }

            return _tokenProvider.Get(user);
        }
    }
}