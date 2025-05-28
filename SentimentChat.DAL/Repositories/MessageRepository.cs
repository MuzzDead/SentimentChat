using Microsoft.EntityFrameworkCore;
using SentimentChat.DAL.Entities;
using SentimentChat.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.DAL.Repositories;

public class MessageRepository : IMessageRepository
{
	private readonly ApplicationDBContext _context;
	public MessageRepository(ApplicationDBContext contecxt)
	{
		_context = contecxt;
	}

	public async Task CreateMessageAsync(ChatMessage message)
	{
		if (message == null) throw new ArgumentNullException(nameof(message));

		await _context.Messages.AddAsync(message);
		await _context.SaveChangesAsync();
	}

	public async Task<ChatMessage> GetMessageAsync(Guid messageId)
	{
		return await _context.Messages
			.FirstOrDefaultAsync(m => m.Id == messageId);
	}

	public async Task<IEnumerable<ChatMessage>> GetMessagesAsync()
	{
		return await _context.Messages
			.OrderBy(m => m.Timestamp)
			.ToListAsync();
	}
}
