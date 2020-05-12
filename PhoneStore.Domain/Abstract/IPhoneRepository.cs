using PhoneStore.Domain.Entities;
using System.Collections.Generic;

namespace PhoneStore.Domain.Abstract
{
    public interface IPhoneRepository
    {
        IEnumerable<Phone> Phones { get; }
    }
}
