using Homework_19.Data;
using Homework_19.Domain;
using Homework_19.Models;

namespace Homework_19.Services;

public interface IUserService
{
    User? Login(LoginUser model);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User? Login(LoginUser loginModel)
    {
        if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            return null;
        
        var user = _context.Users
            .FirstOrDefault(u => u.Username == loginModel.Username && 
                                u.Password == loginModel.Password);

        
        return user;
    }
}