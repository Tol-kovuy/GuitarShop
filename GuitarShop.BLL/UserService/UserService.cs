using AutoMapper;
using GuitarShop.BLL.Enum;
using GuitarShop.BLL.Exceptions;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GuitarShop.BLL.UserService;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<User> _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IMapper mapper,
        IBaseRepository<User> userRepository,
        ILogger<UserService> logger)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task CreateAsync(User user)
    {
        try
        {
            var userExist = await _userRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (userExist != null)
            {
                throw new UserException($"UserService {user.UserName} already exist");
            }

            await _userRepository.CreateAsync(userExist);
        }
        catch (UserException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
    }

    public IQueryable<User> GetAll()
    {
        var users = _userRepository.GetAll();
        return users;
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var user = await _userRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task DeleteAsync(long id)
    {
        try
        {
            var user = await _userRepository.GetAll().SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new UserException($"No user with ID: {id}.");
            }
            var userToEntity = _mapper.Map<DAL.Entities.User>(user);
            await _userRepository.DeleteAsync(user);
        }
        catch (UserException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
    }

    public User GetByName(string name)
    {
        try
        {
            var user = _userRepository.GetAll().SingleOrDefault(user => user.UserName == name);
            return user;
        }
        catch (UserException ex)
        {
            _logger.LogError(ex, "Error", ex.Message);
            throw;
        }
    }
}