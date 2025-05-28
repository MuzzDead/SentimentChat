using AutoMapper;
using SentimentChat.BLL.DTOs;
using SentimentChat.BLL.Interfaces;
using SentimentChat.DAL.Entities;
using SentimentChat.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.BLL.Services;

public class MessageService : IMessageService
{
	private readonly IMessageRepository _repository;
	private readonly IMapper _mapper;
	public MessageService(IMessageRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task CreateMessage(ChatMessageDTO messageDTO)
	{
		if (messageDTO == null)
			throw new ArgumentNullException(nameof(messageDTO));

		var message = _mapper.Map<ChatMessage>(messageDTO);
		message.Timestamp = DateTime.UtcNow;

		await _repository.CreateMessageAsync(message);
	}

	public async Task<ChatMessageDTO> GetMessage(Guid messageId)
	{
		var message = await _repository.GetMessageAsync(messageId);
		return _mapper.Map<ChatMessageDTO>(message);
	}

	public async Task<IEnumerable<ChatMessageDTO>> GetMessages()
	{
		var messages = await _repository.GetMessagesAsync();
		return _mapper.Map<IEnumerable<ChatMessageDTO>>(messages);
	}
}
