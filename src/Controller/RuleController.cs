using Microsoft.AspNetCore.Mvc;
using RulesEnginePOC.Models;
using RulesEnginePOC.Service.Interfaces;

namespace RulesEnginePOC.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly IRuleService _ruleService;

        public RulesController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRules()
        {
            return Ok(await _ruleService.GetAllRules());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRule(Rule rule)
        {
            var createdRule = await _ruleService.CreateRule(rule);
            return Ok(createdRule);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRule(int id)
        {
            var rule = await _ruleService.GetRule(id);
            if (rule == null)
            {
                return NotFound();
            }
            return Ok(rule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            await _ruleService.DeleteRule(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRule(Rule rule)
        {
            var updatedRule = await _ruleService.UpdateRule(rule);
            if (updatedRule == null)
            {
                return NotFound();
            }
            return Ok(updatedRule);
        }
    }
}
