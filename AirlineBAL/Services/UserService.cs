using AirlineDAL.Repository;

namespace AirlineBAL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new();
        }

        public async Task<bool> CheckUserNameExistService(string userName)
        {
            return await _userRepository.CheckUserNameExistRepositoryAsync(userName);
        }

        public async Task<bool> CheckPasswordExistService(string userName, string password)
        {
            return await _userRepository.CheckPasswordExistRepositoryAsync(userName, password);
        }
    }
}
