using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class CreateAdminCommandHandler : IRequestHandler<CreateAccountCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CreateAdminCommandHandler> _logger;

        public CreateAdminCommandHandler(UserManager<User> userManager,
                                         ILogger<CreateAdminCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userManager.Users
                    .Where(x => x.UserName == request.Email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingUser != null)
                    return Result.Failure("Такой пользователь уже существует");

                User user = new()
                {
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    Email = request.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                return result.Succeeded
                    ? Result.Success("Успешно создан")
                    : Result.Failure(result.Errors.Select(x => x.Description).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Employee account create failed with error");
                return Result.Failure("Возникли ошибки при создании сотрудника");
            }
        }
    }
}