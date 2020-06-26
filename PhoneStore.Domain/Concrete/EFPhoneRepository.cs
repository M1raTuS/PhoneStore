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
        public void SavePhone(Phone phone)
        {
            if (phone.PhoneId == 0)
            {
                context.Phones.Add(phone);
            }
            else
            {
                Phone dbEntry = context.Phones.Find(phone.PhoneId);
                if (dbEntry != null)
                {
                    dbEntry.Name = phone.Name;
                    dbEntry.Description = phone.Description;
                    dbEntry.Price = phone.Price;
                    dbEntry.Category = phone.Category;
                }
            }
            context.SaveChanges();
        }
    }
}
