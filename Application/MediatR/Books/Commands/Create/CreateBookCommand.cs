using Application.Common.Interfaces;
using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Books.Commands
{
    public class CreateBookCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishYear { get; set; }
        public string Genre { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result>
    {
        private readonly IApplicationEFContext _context;
        private readonly ILogger<CreateBookCommandHandler> _logger;

        public CreateBookCommandHandler(IApplicationEFContext context, ILogger<CreateBookCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Book
                {
                    Name = request.Name,
                    AuthorName = request.AuthorName,
                    PublishYear = request.PublishYear,
                    Genre = request.Genre
                };

                _context.Books.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success("Книга успешно создана");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Book create failed with error");
                return Result.Failure("Возникли ошибки при создании книги");
            }
        }
    }
}