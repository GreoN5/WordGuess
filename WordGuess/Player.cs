using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordGuess
{
	public class Player
	{
		[Key]
		public int PlayerID { get; set; }

		[Column (TypeName = "varchar(50)")]
		public string Username { get; set; }

		[Column (TypeName = "varchar(50)")]
		public string Password { get; set; }

		[Column(TypeName = "decimal(7,1)")]
		public decimal Points { get; set; }
	}
}
