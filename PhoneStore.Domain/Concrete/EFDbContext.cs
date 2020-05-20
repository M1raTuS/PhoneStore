using PhoneStore.Domain.Entities;
using System.Data.Entity;

namespace PhoneStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
    }
}
