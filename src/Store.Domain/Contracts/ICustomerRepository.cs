using Store.Domain.Models;
using System.Collections.Generic;

namespace Store.Domain.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetByName(string name);
    }
}
