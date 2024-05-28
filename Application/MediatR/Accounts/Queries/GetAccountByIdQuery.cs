using Application.Extensions;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.MediatR.Accounts.Queries
{
    public record GetAccountByIdQuery(Guid Id) : IRequest<AccountDto>;

    public class GetAccountByHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
    {
        private readonly UserManager<User> _userManager;

        public GetAccountByHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByIdAsync(request.Id.ToString());
            return applicationUser switch
            {
                null => null,
                _ => applicationUser.AsDto()
            };
        }
    }
}