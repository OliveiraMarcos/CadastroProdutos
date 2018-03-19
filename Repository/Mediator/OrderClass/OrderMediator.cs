using Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mediator.OrderClass
{
    public class CustomerMediator : BaseMediator<OrderViewModel>, IRequest<Response>
    {
    }
}
