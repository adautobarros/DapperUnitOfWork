using Store.Data.Repository;
using Store.Domain.Contracts;
using Store.Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Store.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection;

        public UnitOfWork()
        {
            _connection = new SqlConnection("~connectionString");
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return new CustomerRepository(_transaction);
            }
        }

        public void Commit()
        {
            _transaction.Commit();
            _transaction = _connection.BeginTransaction();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
