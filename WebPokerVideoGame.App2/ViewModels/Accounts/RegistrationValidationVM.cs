using FluentValidation;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebPokerVideoGame.App2.ViewModels.Accounts
{
    public class RegistrationValidationVM : AbstractValidator<RegistrationVM>
    {
        private readonly HttpClient _httpClient;
        public RegistrationValidationVM(HttpClient httpClient)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Field 'First Name' can not be empty.")
                .MinimumLength(2).WithMessage("Your fist name length must be at leat 2.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Field 'Last Name' can not be empty.")
                .MinimumLength(2).WithMessage("Your last name length must be at leat 2.");

            RuleFor(_ => _.Email).NotEmpty()
                .EmailAddress()
                .MustAsync(async (value, cancellationToken) => await UniqueEmail(value))
                .When(_ => !string.IsNullOrEmpty(_.Email) && Regex.IsMatch(_.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase), ApplyConditionTo.CurrentValidator)
                .WithMessage("email should be unique");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password can not be empty.")
                .MinimumLength(6).WithMessage("Your password length must be at leat 6.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at leat one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at leat one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\@\!\?\*\.]+").WithMessage("Your password must contain at least one (@!? *.).");

            RuleFor(x => x.ConfirmPassword).Equal(c =>  c.Password).WithMessage("Confirm password must be equal to password");
            _httpClient = httpClient;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationVM>.CreateWithOptions((RegistrationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

        private async Task<bool> UniqueEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            try
            {
                string url = $"/api/user/unique-user-email?email={email}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<bool>(content);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
