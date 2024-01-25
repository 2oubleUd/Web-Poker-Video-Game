using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame.Models.Data.Dtos
{
    public class RenewTokenRequestDto
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
