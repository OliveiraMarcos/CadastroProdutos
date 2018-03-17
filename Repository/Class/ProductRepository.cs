using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Class
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(Contexto contexto)
            :base(contexto)
        {

        }
    }
}
