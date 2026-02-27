using Microsoft.EntityFrameworkCore;
using Sales.Application.Contracts.Repositories;
using Sales.Domain.Orders.Entities;
using Sales.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructure.Repositories
{
    public class EfOrderRepository : IOrderRepository
    {
        private readonly SalesDbContext _context;

        public EfOrderRepository(SalesDbContext context)
        {
            _context = context;
        }

        public Order GetById(long id)
        {
            return _context.Orders.Include(order => order.Items).AsNoTracking().FirstOrDefault(order => order.Id == id);
        }

        public List<Order> GetAll()
        {
            return _context.Orders.Include(order => order.Items).AsNoTracking().ToList();
        }

        public void Insert(Order order)
        {
            _context.Orders.Add(order);

            _context.SaveChanges();
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);

            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);

            _context.SaveChanges();
        }
    }
}
