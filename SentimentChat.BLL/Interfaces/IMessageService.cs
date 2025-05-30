using SentimentChat.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.BLL.Interfaces;

public interface IMessageService
{
	Task CreateMessage(CreateMessageDTO messageDTO);
	Task<ChatMessageDTO> GetMessage(Guid messageId);
	Task<IEnumerable<ChatMessageDTO>> GetMessages();
}
