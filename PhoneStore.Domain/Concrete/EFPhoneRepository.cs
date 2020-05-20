using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Entities;
using System.Collections.Generic;

namespace PhoneStore.Domain.Concrete
{
    public class EFPhoneRepository : IPhoneRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Phone> Phones
        {
            get { return context.Phones; }
        }
    }
}
