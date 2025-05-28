using SentimentChat.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.DAL.Interfaces;

public interface IMessageRepository
{
	Task CreateMessageAsync(ChatMessage message);
	Task<ChatMessage> GetMessageAsync(Guid messageId);
	Task<IEnumerable<ChatMessage>> GetMessagesAsync();
}
