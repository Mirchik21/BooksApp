using Application.MediatR.Accounts.Queries;
using Application.MediatR.Books.Queries;
using Domain.Entities;
using Domain.Identity;

namespace Application.Extensions
{
    public static class MappingExtension
    {
        public static AccountDto AsDto(this User entity)
        {
            return new AccountDto(
               entity.Id, entity.Email, entity.FirstName, entity.LastName, entity.MiddleName
            );
        }

        public static BookDto AsDto(this Book entity)
        {
            return new BookDto(
               entity.Id, entity.Name, entity.AuthorName, entity.PublishYear, entity.Genre);
        }
    }
}