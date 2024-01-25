using FluentValidation;

namespace WebPokerVideoGame.App2.ViewModels.Accounts
{
    public class LoginValidationVM : AbstractValidator<LoginVM>
    {
        public LoginValidationVM() 
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Field 'Email' can not be empty.")
                .EmailAddress().WithMessage("Provided email is not valid");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password can not be empty.")
                .MinimumLength(6).WithMessage("Invalid password")
                .MaximumLength(16).WithMessage("Invalid password")
                .Matches(@"[A-Z]+").WithMessage("Invalid password")
                .Matches(@"[a-z]+").WithMessage("Invalid password")
                .Matches(@"[0-9]+").WithMessage("Invalid password")
                .Matches(@"[\@\!\?\*\.]+").WithMessage("Invalid password");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LoginVM>.CreateWithOptions((LoginVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }
}
