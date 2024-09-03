using AirlineDAL.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineDAL.Repository
{
    public class UserRepository
    {
        private readonly AirlineDbContext _airlineContext;
        public UserRepository()
        {
            _airlineContext = new();
        }

        public async Task<bool> CheckUserNameExistRepositoryAsync(string userName)
        {
            return await _airlineContext.User.AnyAsync(x => x.UserName == userName);
        }

        public async Task<bool> CheckPasswordExistRepositoryAsync(string userName, string password)
        {
            return await _airlineContext.User.AnyAsync(x => x.UserName == userName && x.Password == password);
        }
    }
}
