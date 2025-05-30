using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.BLL.Interfaces
{
	public interface ISentimentService
	{
		Task<string> AnalyzeSentimentAsync(string message);
	}
}
