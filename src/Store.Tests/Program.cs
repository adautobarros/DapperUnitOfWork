using Store.Data.UoW;
using Store.Domain.Models;
using System;

namespace Store.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var uow = new UnitOfWork())
            {
                uow.Customers.Add(new Customer { Name = "Fabio" });
                uow.Customers.Add(new Customer { Name = "Maria" });
                uow.Customers.Add(new Customer { Name = "Ana" });

                uow.Rollback();

                uow.Customers.Add(new Customer { Name = "Luiz" });
                uow.Customers.Add(new Customer { Name = "Matheus" });
                uow.Customers.Add(new Customer { Name = "Rosana" });

                uow.Commit();

                var customers = uow.Customers.GetAll();

                foreach (var customer in customers)
                    Console.WriteLine(customer.Name);
            }

            Console.ReadKey();
        }
    }
}