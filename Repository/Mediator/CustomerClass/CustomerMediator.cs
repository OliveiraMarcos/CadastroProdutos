using Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mediator.CustomerClass
{
    public class CustomerMediator : BaseMediator<CustomerViewModel>, IRequest<Response>
    {
        
    }
}
