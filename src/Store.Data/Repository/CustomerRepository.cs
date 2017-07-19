using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Store.Domain.Contracts;
using Store.Domain.Models;

namespace Store.Data.Repository
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IDbTransaction _transaction;

        public CustomerRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _transaction.Connection.Query<Customer>(
                "SELECT Id, Name FROM Customer ORDER By Name", transaction: _transaction)
                .ToList();
        }

        public Customer Get(int id)
        {
            return _transaction.Connection.Query<Customer>(
                "SELECT Id, Name FROM Customer WERE Id = @id",
                new { id = id }, _transaction).FirstOrDefault();
        }

        public IEnumerable<Customer> GetByName(string name)
        {
            return _transaction.Connection.Query<Customer>(
              "SELECT Id, Name FROM Customer WERE name = @name",
              new { name = name }, _transaction).ToList();
        }

        public void Add(Customer obj)
        {
            obj.Id = _transaction.Connection.ExecuteScalar<int>(
              "INSERT INTO Customer (Name) VALUES (@name)",
              new { name = obj.Name },
              _transaction);
        }

        public void Update(Customer obj)
        {
            _transaction.Connection.Query<Customer>(
              "UPDATE Customer SET Name = @name WHERE Id = @id)",
              new { name = obj.Name, id = obj.Id },
              _transaction);
        }

        public void Remove(Customer obj)
        {
            _transaction.Connection.Query<Customer>(
               "DELETE FROM Customer WHERE Id = @id)",
               new { id = obj.Id },
               _transaction);
        }
    }
}
