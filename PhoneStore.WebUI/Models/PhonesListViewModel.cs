using PhoneStore.Domain.Entities;
using System.Collections.Generic;

namespace PhoneStore.WebUI.Models
{
    public class PhonesListViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}