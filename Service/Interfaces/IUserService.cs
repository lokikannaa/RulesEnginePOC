using RulesEnginePOC.Models;

namespace RulesEnginePOC.Service.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetAllUser();
        Task DeleteUser(int id);
        Task AddRolesToUser(int ruleId, int userId);
        Task RemoveRuleFromUser(int ruleId, int userId);
        Task<IEnumerable<Rule>> GetUserRoles(int userId);
    }

}
