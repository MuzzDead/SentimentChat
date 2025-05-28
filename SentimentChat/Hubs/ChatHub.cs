using Microsoft.AspNetCore.SignalR;
using SentimentChat.BLL.DTOs;
using SentimentChat.BLL.Interfaces;
using SentimentChat.DAL.Entities;

namespace SentimentChat.Hubs;

public class ChatHub : Hub
{
	private readonly IMessageService _service;
	public ChatHub(IMessageService service)
	{
		_service = service;
	}

	public async Task SendMessage(ChatMessageDTO message)
	{
		await _service.CreateMessage(message);

		await Clients.All.SendAsync("ReceiveMessage", message.Username, message.Message, DateTime.UtcNow);
	}
}
