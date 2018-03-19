using Domain.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Repository.Utils;

namespace Repository.Class
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(Contexto contexto)
            : base(contexto)
        {

        }

        public List<Order> GetOrdersByExpression(Expression<Func<Order, bool>> predicate)
        {
            return _context.Order.Include(c => c.Customer).Include(c => c.OrderItem).Where(predicate).ToList();
        }

        public void InsertOrder(Order order)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Insert(order);
                _context.SaveChanges();
                trans.Commit();

            }
            catch (Exception e)
            {
                trans.Rollback();
                throw e;
            }
        }

        public void RemoveOrder(int Id)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Order entity = GetOrdersByExpression(x => x.Id == Id).SingleOrDefault();
                _context.OrderItem.RemoveRange(entity.OrderItem);
                _context.Order.Remove(entity);
                _context.SaveChanges();
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw e;
            }
        }

        public void UpdateOrder(int Id, Order order)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Order entity = GetOrdersByExpression(x => x.Id == Id).SingleOrDefault();

                List<OrderItem> listForAdd = order.OrderItem.Except(entity.OrderItem, new CompareOrderItem()).ToList<OrderItem>();
                List<OrderItem> listForRemove = entity.OrderItem.Except(order.OrderItem, new CompareOrderItem()).ToList<OrderItem>();

                _context.OrderItem.RemoveRange(listForRemove);
                foreach (OrderItem item in listForAdd)
                {
                    item.Order = order;
                    if (item.Id > 0)
                    {
                        OrderItem itemEntity = _context.OrderItem.Find(item.Id);
                        _context.Entry(itemEntity).CurrentValues.SetValues(item);
                        _context.Entry(itemEntity).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.OrderItem.Add(item);
                    }
                }
                _context.Entry(entity).CurrentValues.SetValues(order);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw e;
            }
        }

        public void UpdateOrder(Order order)
        {
            Update(order.Id, order);
        }


    }
}
