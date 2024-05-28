using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Books.Commands
{
    public record DeleteBookCommand(Guid Id) : IRequest<Result>;

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IApplicationEFContext _context;
        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(IApplicationEFContext context, ILogger<DeleteBookCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
                _context.Books.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success("Книга успешно удалена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Book delete failed with error");
                return Result.Failure("Возникли ошибки при удалении книги");
            }
        }
    }
}