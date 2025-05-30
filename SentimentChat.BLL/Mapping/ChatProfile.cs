using AutoMapper;
using SentimentChat.BLL.DTOs;
using SentimentChat.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.BLL.Mapping;

// AutoMapper profile for mapping between models and DTOs
public class ChatProfile : Profile
{
	public ChatProfile()
	{
		CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();
		CreateMap<ChatMessage, CreateMessageDTO>().ReverseMap();
	}
}
