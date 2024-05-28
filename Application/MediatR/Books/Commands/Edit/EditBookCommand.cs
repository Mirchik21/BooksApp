using Application.Common.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Books.Commands
{
    public class EditBookCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishYear { get; set; }
        public string Genre { get; set; }
    }

    public class EditBookCommandHandler : IRequestHandler<EditBookCommand, Result>
    {
        private readonly IApplicationEFContext _context;
        private readonly ILogger<EditBookCommandHandler> _logger;

        public EditBookCommandHandler(IApplicationEFContext context, ILogger<EditBookCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
                entity.Name = request.Name;
                entity.AuthorName = request.AuthorName;
                entity.PublishYear = request.PublishYear;
                entity.Genre = request.Genre;

                _context.Books.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success("Редактирование книги успешно");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Book edit failed with error");
                return Result.Failure("Возникли ошибки при редактировании книги");
            }
        }
    }
}