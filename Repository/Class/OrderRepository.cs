using Domain.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Class
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(Contexto contexto)
            : base(contexto)
        {

        }
    }
}
