using AutoMapper;
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

        public async Task CreateAsync(User user)
        {
            var userExist = GetAll().FirstOrDefault(x => x.UserName == user.UserName);

            if (userExist == null)
            {
                var newUser = _mapper.Map<UserEntity>(user);
                await _userRepository.CreateAsync(newUser);
            }
            else
            {
                throw new Exception($"UserService {user.UserName} already exist.");//???
            }
        }

        public User GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public IList<User> GetAll()
        {
            if (this._users == null) // Спросить за кеширование правильно ли я сделал
            {
                var usersFromDb = _userRepository.GetAll();
                _users = new List<User>();
                foreach (var user in usersFromDb)
                {
                    _users.Add(_mapper.Map<User>(user));
                }
            }
            return _users;
        }

        public void Delete(long id)
        {
            var user = GetAll().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                var userToEntity = _mapper.Map<UserEntity>(user);
                _userRepository.DeleteAsync(userToEntity);
            }
            else
            {
                throw new Exception($"User with {id} not found!"); //???
            }
        }
    }
}