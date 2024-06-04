using Microsoft.EntityFrameworkCore;
using RulesEnginePOC;
using RulesEnginePOC.Models;
using RulesEnginePOC.Service;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<User> CreateUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetUser(int id) => await _dbContext.Users.FindAsync(id);

    public async Task<IEnumerable<User>> GetAllUser() => await _dbContext.Users.ToListAsync();

    public async Task AddRolesToUser(int ruleId, int userId)
    {
        var userRule = await _dbContext.UserRules
                                      .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RuleId == ruleId);
        if (userRule == null)
        {
            var newUserRule = new UserRule
            {
                UserId = userId,
                RuleId = ruleId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _dbContext.UserRules.Add(newUserRule);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveRuleFromUser(int ruleId, int userId)
    {
        var userRule = await _dbContext.UserRules
                                       .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RuleId == ruleId);
        if (userRule != null)
        {
            _dbContext.UserRules.Remove(userRule);
            await _dbContext.SaveChangesAsync();
        }
    }
    
    public async Task<IEnumerable<Rule>> GetUserRoles(int userId)
    {
        var userRules =  await _dbContext.UserRules.Include(ur => ur.Rule).Where(ur => ur.UserId == userId).ToListAsync();
        return userRules.Select(ur => ur.Rule);
    }

}
