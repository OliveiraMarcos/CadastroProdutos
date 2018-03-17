using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Class
{
    public class OrderItemRepository:GenericRepository<OrderItem>
    {
        public OrderItemRepository(Contexto contexto)
            : base(contexto)
        {

        }
    }
}
