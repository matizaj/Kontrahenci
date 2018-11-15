using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrahenci.Models
{
    public class CustomerListViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
