using Contacto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacto.Domain.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactCategory> ContactCategories { get; set; }
        DbSet<ContactSubCategory> ContactSubCategories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}