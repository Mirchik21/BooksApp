using Application.Common.Interfaces;
using Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MediatR.Books.Queries
{
    public record GetBookByIdQuery(Guid Id) : IRequest<BookDto>;

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IApplicationEFContext _context;

        public GetBookByIdQueryHandler(IApplicationEFContext context)
        {
            _context = context;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
            return entity.AsDto();
        }
    }
}