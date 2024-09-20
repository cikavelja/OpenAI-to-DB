using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.API.Services.Interfaces;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;
        public ChatController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }
        [HttpGet]
        public async Task<IActionResult> GetChatAsync(string chat)
        {
            return Ok(await _openAIService.GetSQLAsync(chat));
        }
    }
}
