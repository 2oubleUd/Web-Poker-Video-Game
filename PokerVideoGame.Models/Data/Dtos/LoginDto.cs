using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PokerVideoGame.Models.Data.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
