using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationEFContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<User> AspNetUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void SetEntityState(object entity, EntityState entityState);

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}