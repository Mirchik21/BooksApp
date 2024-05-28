using Application.Models;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Accounts.Commands
{
    public record DeleteAccountCommand(Guid Id) : IRequest<Result>;

    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DeleteAccountHandler> _logger;

        public DeleteAccountHandler(UserManager<User> userManager,
                                   ILogger<DeleteAccountHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _userManager.FindByIdAsync(request.Id.ToString());
                await _userManager.DeleteAsync(entity);
                return Result.Success("Сотрудник успешно удален");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Employee account delete failed with error");
                return Result.Failure("Возникли ошибки при удалении сотрудника");
            }
        }
    }
}