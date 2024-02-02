using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame.Models.Data.Dtos
{
    public class UpdateUserMoneyDto
    {
        public int UserId { get; set; }
        public int AmountOfMoney { get; set; }
    }
}
