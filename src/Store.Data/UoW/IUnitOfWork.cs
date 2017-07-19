using Store.Domain.Contracts;
using Store.Domain.Models;
using System;

namespace Store.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }

        void Commit();
        void Rollback();
    }
}
