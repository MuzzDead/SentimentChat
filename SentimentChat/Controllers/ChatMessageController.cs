using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentChat.BLL.DTOs;
using SentimentChat.BLL.Interfaces;
using SentimentChat.DAL.Entities;

namespace SentimentChat.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatMessageController : ControllerBase
	{
		private readonly IMessageService _service;
		public ChatMessageController(IMessageService service)
		{
			_service = service;
		}

		[HttpGet("{messageId:guid}")]
		public async Task<ActionResult<ChatMessageDTO>> GetMessage(Guid messageId)
		{
			var message = await _service.GetMessage(messageId);
			if (message == null) 
				return NotFound();

			return Ok(message);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetMessages()
		{
			var messages = await _service.GetMessages();
			return Ok(messages);
		}

		[HttpPost]
		public async Task<ActionResult> PostMessage([FromBody] ChatMessageDTO message)
		{
			await _service.CreateMessage(message);
			return Ok(new { Message = "Message created successfully" });
		}
	}
}
