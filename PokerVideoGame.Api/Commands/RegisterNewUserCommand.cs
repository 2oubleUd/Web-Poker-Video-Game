using MediatR;
using PokerVideoGame.Models.Data.Entites;

namespace PokerVideoGame.Api.Commands
{
    public class RegisterNewUserCommand : IRequest<(bool IsUserRegistered, string Message)>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterNewUserCommand(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
