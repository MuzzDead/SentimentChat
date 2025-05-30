using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using SentimentChat.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.BLL.Services;

public class SentimentService : ISentimentService
{
	private readonly TextAnalyticsClient _client;
	public SentimentService(IConfiguration configuration)
	{
		var endpoint = new Uri(configuration["AzureCognitive:Endpoint"]);
		var key = new AzureKeyCredential(configuration["AzureCognitive:Key"]);
		_client = new TextAnalyticsClient(endpoint, key);
	}
	public async Task<string> AnalyzeSentimentAsync(string message)
	{
		var result = await _client.AnalyzeSentimentAsync(message);
		return result.Value.Sentiment.ToString(); // (Positive/Neutral/Negative)
	}
}
