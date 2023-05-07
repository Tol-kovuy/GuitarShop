using AutoMapper;
using GuitarShop.BLL.Enum;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GuitarShop.BLL.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<DAL.Entities.User> _userRepository;
        private IList<User> _users;

        public UserService(
            IMapper mapper,
            IBaseRepository<DAL.Entities.User> userRepository
            )
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> CreateAsync(User user)
        {
            try
            {
                var allUsers = await GetAllAsync();
                var userExist = allUsers.FirstOrDefault(u => u.UserName == user.UserName);

                if (userExist != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = $"UserService {user.UserName} already exist.",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                var newUser = _mapper.Map<DAL.Entities.User>(user);
                await _userRepository.CreateAsync(newUser);
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Registration was successful!"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByIdAsync(long id)
        {
            var users = await GetAllAsync();
            var user = users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            var _users = await _userRepository.GetAll().ToListAsync();

            return _users;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var allUsers = await GetAllAsync();
                var user = allUsers.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        StatusCode = StatusCode.UserNotFound,
                        Description = $"No user with ID: {id}."
                    };

                }
                var userToEntity = _mapper.Map<DAL.Entities.User>(user);
                await _userRepository.DeleteAsync(userToEntity);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = $"User with ID: {id} was deleted."
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByNameAsync(string name)
        {

            try
            {
                var users = await GetAllAsync();
                return users.FirstOrDefault(user => user.UserName == name);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}