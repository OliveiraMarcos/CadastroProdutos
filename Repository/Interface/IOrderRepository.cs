using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> GetOrdersByExpression(Expression<Func<Order, bool>> predicate);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void UpdateOrder(int Id, Order order);
        void RemoveOrder(int Id);

    }
}
