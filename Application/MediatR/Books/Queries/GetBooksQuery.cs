using Application.Common.Interfaces;
using Application.Constants;
using Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P.Pager;

namespace Application.MediatR.Books.Queries
{
    public record GetBooksQuery(string Name, string AuthorName, DateTime PublishYear, int Page = 1) : IRequest<IPager<BookDto>>;

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IPager<BookDto>>
    {
        private readonly IApplicationEFContext _context;

        public GetBooksQueryHandler(IApplicationEFContext context)
        {
            _context = context;
        }

        public async Task<IPager<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.Books.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                entities = entities.Where(q => EF.Functions.Like(q.Name, $"%{request.Name}%"));
            }
            if (!string.IsNullOrWhiteSpace(request.AuthorName))
            {
                entities = entities.Where(q => EF.Functions.Like(q.AuthorName, $"%{request.AuthorName}%"));
            }
            if (request.PublishYear != default)
            {
                entities = entities.Where(x => x.PublishYear == request.PublishYear);
            }

            return await entities.Select(x => x.AsDto())
                              .ToPagerListAsync(request.Page, Pagination.PAGE_SIZE);
        }
    }
}