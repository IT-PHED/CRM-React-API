using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Auth.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
        .WithMessage("Email or Staff ID is required.")
        .Must(emailOrId =>
        {
            if (emailOrId.Contains('@'))
            {
                return emailOrId.EndsWith("@phed.com.ng", StringComparison.OrdinalIgnoreCase);
            }

            // Option 2: Numeric staff ID (5–10 digits)
            if (Regex.IsMatch(emailOrId, @"^\d{5,10}$"))
            {
                return true;
            }

            if (Regex.IsMatch(emailOrId, @"^FT\d+$", RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        })
        .WithMessage("Value must be a valid @phed.com.ng email, a numeric Staff ID (5-10 digits), or start with 'FT' followed by numbers (e.g. FT12345).");

        RuleFor(c => c.Password).NotEmpty().WithMessage("password is required");
    }
}
