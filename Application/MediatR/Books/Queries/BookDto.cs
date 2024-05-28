namespace Application.MediatR.Books.Queries
{
    public record BookDto(Guid Id, string Name, string AuthorName, DateTime PublishYear, string Genre);
}