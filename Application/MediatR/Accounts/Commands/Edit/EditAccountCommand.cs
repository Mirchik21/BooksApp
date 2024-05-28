using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Accounts.Commands
{
    public class EditAccountCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
    }

    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EditAccountCommandHandler> _logger;

        public EditAccountCommandHandler(UserManager<User> userManager,
                                   ILogger<EditAccountCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userManager.Users
                    .Where(x => x.UserName == request.Email)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingUser != null)
                    return Result.Failure("Такой пользователь уже существует");

                var entity = await _userManager.FindByIdAsync(request.Id.ToString());
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.MiddleName = request.MiddleName;
                entity.Email = request.Email;
                await _userManager.UpdateAsync(entity);
                return Result.Success("Успешно изменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Employee account edit failed with error");
                return Result.Failure("Возникли ошибки при редактироние сотрудника");
            }
        }
    }
}