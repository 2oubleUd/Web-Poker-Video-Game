using PokerVideoGame.Models.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame.Models.Data.Settings
{
    public class UserRefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }
        public int UserId {  get; set; }

        public DateTime ExpirationDate { get; set; }

    }
}