namespace Application.MediatR.Accounts.Queries
{
    public record AccountDto(Guid Id, string Email, string FirstName, string LastName, string MiddleName)
    {
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}