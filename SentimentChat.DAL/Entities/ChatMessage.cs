using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.DAL.Entities;

public class ChatMessage
{
	public Guid Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	public string? Sentiment { get; set; }
}
