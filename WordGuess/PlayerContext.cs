using Microsoft.EntityFrameworkCore;

namespace WordGuess
{
	public class PlayerContext : DbContext
	{
		public DbSet<Player> Players { get; set; }

		public PlayerContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-OI3QR52\\SQLEXPRESS;Database=Players;Trusted_Connection=True;");
		}
	}
}
