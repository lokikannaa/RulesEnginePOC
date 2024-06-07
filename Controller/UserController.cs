using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RulesEnginePOC.Models;
using RulesEnginePOC.Service.Interfaces;

namespace RulesEnginePOC.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRuleService _ruleService;

        public UsersController(IUserService userService, IRuleService ruleService)
        {
            _userService = userService;
            this._ruleService = ruleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _userService.GetAllUser();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        [HttpPost("{userId}/rules/{ruleId}")]
        public async Task<IActionResult> AddRuleToUser(int userId, int ruleId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            var rule = await _ruleService.GetRule(ruleId);
            if (rule == null)
            {
                return NotFound();
            }

            await _userService.AddRolesToUser(ruleId, userId);

            return Ok();
        }

        [HttpDelete("{userId}/rules/{ruleId}")]
        public async Task<IActionResult> RemoveRuleFromUser(int userId, int ruleId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            var rule = await _ruleService.GetRule(ruleId);
            if (rule == null)
            {
                return NotFound();
            }

            await _userService.RemoveRuleFromUser(ruleId, userId);

            return Ok();
        }

        [HttpGet("{userId}/rules")]
        public async Task<IActionResult> GetUserRules(int userId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userService.GetUserRoles(userId);

            return Ok(roles);
        }

    }
}
