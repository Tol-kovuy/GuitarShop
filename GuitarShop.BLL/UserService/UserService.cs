using AutoMapper;
using GuitarShop.BLL.Enum;
using GuitarShop.BLL.Models;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GuitarShop.BLL.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<UserEntity> _userRepository;
        private IList<User> _users;

        public UserService(
            IMapper mapper,
            IBaseRepository<UserEntity> userRepository
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
                var newUser = _mapper.Map<UserEntity>(user);
                await _userRepository.CreateAsync(newUser);
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Registration was successful!"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                }; // throw???
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var users = await GetAllAsync();
            var user = users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            if (this._users == null) // Ask for кеширование правильно ли я сделал
            {
                var usersFromDb = await _userRepository.GetAll().ToListAsync();
                _users = new List<User>();
                foreach (var user in usersFromDb)
                {
                    _users.Add(_mapper.Map<User>(user));
                }
            }
            return _users;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(long id)
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
            var userToEntity = _mapper.Map<UserEntity>(user);
            await _userRepository.DeleteAsync(userToEntity);
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK,
                Description = $"User with ID: {id} was deleted."
            };
        }
    }
}