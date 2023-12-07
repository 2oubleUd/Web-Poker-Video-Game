using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public string? Surname { get; set; }
        public int AccountBalance { get; set; }
        public string? Email { get; set; }

        // to do: add Login and Password properties for loging and registering players
        //public string? Login { get; set; }
        //public string? Password { get; set; }

        public List<GameHistory>? PlayerGameHistory { get; set; } = new List<GameHistory>();
    }
}
