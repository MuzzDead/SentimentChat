using AutoMapper;
using Microsoft.Extensions.Azure;
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
	private readonly ISentimentService _sentimentService;
	public MessageService(IMessageRepository repository, IMapper mapper, ISentimentService sentimentService)
	{
		_sentimentService = sentimentService;
		_repository = repository;
		_mapper = mapper;
	}

	// Creates a new message with sentiment analysis
	public async Task CreateMessage(CreateMessageDTO messageDTO)
	{
		if (messageDTO == null)
			throw new ArgumentNullException(nameof(messageDTO));

		var message = _mapper.Map<ChatMessage>(messageDTO);
		message.Timestamp = DateTime.UtcNow;

		// Perform sentiment analysis on message text
		var sentiment = await _sentimentService.AnalyzeSentimentAsync(message.Message);
		message.Sentiment = sentiment;

		await _repository.CreateMessageAsync(message);
	}

	// Gets a single message by ID
	public async Task<ChatMessageDTO> GetMessage(Guid messageId)
	{
		var message = await _repository.GetMessageAsync(messageId);
		return _mapper.Map<ChatMessageDTO>(message);
	}

	// Gets all messages
	public async Task<IEnumerable<ChatMessageDTO>> GetMessages()
	{
		var messages = await _repository.GetMessagesAsync();
		return _mapper.Map<IEnumerable<ChatMessageDTO>>(messages);
	}
}
