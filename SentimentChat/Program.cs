using Microsoft.EntityFrameworkCore;
using SentimentChat.BLL.Interfaces;
using SentimentChat.BLL.Mapping;
using SentimentChat.BLL.Services;
using SentimentChat.DAL;
using SentimentChat.DAL.Interfaces;
using SentimentChat.DAL.Repositories;
using SentimentChat.Hubs;
using System;

namespace SentimentChat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// DB
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationDBContext>(options =>
				options.UseSqlServer(connectionString));

			// AutoMapper
			builder.Services.AddAutoMapper(typeof(ChatProfile));

			// Repositories & Services
			builder.Services.AddScoped<IMessageRepository, MessageRepository>();
			builder.Services.AddScoped<IMessageService, MessageService>();

			// SignalR
			builder.Services.AddSignalR()
				.AddAzureSignalR(builder.Configuration["Azure:SignalR:ConnectionString"]);


			// CORS
			var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", policy =>
				{
					policy.WithOrigins(allowedOrigins)
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials();
				});
			});


			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "SentimentChat API",
					Version = "v1"
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "SentimentChat API v1");
					c.RoutePrefix = string.Empty;
				});
			}

			app.UseHttpsRedirection();

			app.UseCors("AllowFrontend");

			app.UseAuthorization();

			app.UseAzureSignalR(routes =>
			{
				routes.MapHub<ChatHub>("/chathub");
			});

			app.MapControllers();

			app.Run();
		}
	}
}
