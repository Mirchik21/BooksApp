using Application.Constants;
using Application.Extensions;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace Application.MediatR.Accounts.Queries
{
    public record GetAccountsQuery(string Email, string Name, int Page = 1) : IRequest<IPager<AccountDto>>;

    public class GetAccountQueryHandler : IRequestHandler<GetAccountsQuery, IPager<AccountDto>>
    {
        private readonly UserManager<User> _userManager;

        public GetAccountQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IPager<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(q => EF.Functions.Like(q.Email, $"%{request.Email}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(q => EF.Functions.Like(q.FirstName, $"%{request.Name}%"));
            }

            return await query.Select(x => x.AsDto())
                              .ToPagerListAsync(request.Page, Pagination.PAGE_SIZE);
        }
    }
}