using AutoMapper;
using Domain.ViewModels;
using Domain.Entity;
using MediatR;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.ValidatorViewModel;
using System.Linq;
using FluentValidation.Results;

namespace Repository.Mediator.OrderClass
{
    public class CustomerHandler : IRequestHandler<CustomerMediator, Response>
    {
        private readonly IOrderRepository _rep;
        private readonly IMapper _mapper;

        public CustomerHandler(IOrderRepository rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public Task<Response> Handle(CustomerMediator request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            
            switch (request.Method)
            {
                case "GET":
                    if (request.Id > 0)
                    {
                        Order order = _rep.GetOrdersByExpression(c => c.Id==request.Id).FirstOrDefault();
                        response.Result = _mapper.Map<Order, OrderViewModel>(order);
                    }
                    else
                    {
                        List<Order> orders = _rep.GetOrdersByExpression(a => a.Id > 0);
                        response.Result = _mapper.Map<List<Order>, List<OrderViewModel>>(orders);
                    }
                    response.Message = "Sucesso!";
                    response.StatusCode = 200;
                    break;
                case "PUT":
                case "POST":
                    List<ValidationFailure> errors = new OrderValidator().Validate(request.ObjectView).Errors as List<ValidationFailure>;
                    foreach (OrderItemViewModel item in request.ObjectView.OrderItem)
                    {
                        errors.AddRange(new OrderItemValidator().Validate(item).Errors);
                    }

                    if (errors.Count > 0)
                    {
                        response.Rason = errors;
                        response.Message = "Falha de Validação.";
                        response.StatusCode = 422;
                        response.HasError = true;
                    }
                    else
                    {
                        if (request.Id > 0)
                        {
                            _rep.UpdateOrder(request.Id, _mapper.Map<OrderViewModel, Order>(request.ObjectView));
                            response.Message = "Atualizado com Sucesso!";
                            response.StatusCode = 204;
                        }
                        else
                        {
                            _rep.InsertOrder(_mapper.Map<OrderViewModel, Order>(request.ObjectView));
                            response.Message = "Criado com Sucesso!";
                            response.StatusCode = 201;
                        }
                    }
                    break;
                case "DELETE":
                    _rep.RemoveOrder(request.Id);
                    response.Message = "Removido com Sucesso!";
                    response.StatusCode = 204;
                    break;
                default:
                    response.Message = "Recurso não encontrado!";
                    response.StatusCode = 404;
                    response.HasError = true;
                    break;
            }
            return Task.FromResult(response);
        }
    }
}
