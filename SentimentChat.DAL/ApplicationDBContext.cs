using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SentimentChat.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentChat.DAL;

public class ApplicationDBContext : DbContext
{
	public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
	{
	}

	// Configure model mappings
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Map ChatMessage entity to "ChatMessages" table
		modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");
	}

	// DbSet representing chat messages
	public DbSet<ChatMessage> Messages { get; set; }
}

