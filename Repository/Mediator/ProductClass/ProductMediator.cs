using Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mediator.ProductClass
{
    public class ProductMediator : BaseMediator<ProductViewModel>, IRequest<Response>
    {
    }
}
