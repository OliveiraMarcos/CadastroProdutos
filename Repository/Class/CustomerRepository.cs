using Domain.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Class
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(Contexto contexto)
            :base(contexto)
        {

        }
}
}
