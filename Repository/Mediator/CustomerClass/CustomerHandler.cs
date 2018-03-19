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

namespace Repository.Mediator.CustomerClass
{
    public class CustomerHandler : IRequestHandler<CustomerMediator, Response>
    {
        private readonly IGenericRepository<Customer> _rep;
        private readonly IMapper _mapper;

        public CustomerHandler(IGenericRepository<Customer> rep, IMapper mapper)
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
                        Customer Customer = _rep.GetOne(request.Id);
                        response.Result = _mapper.Map<Customer, CustomerViewModel>(Customer);
                    }
                    else
                    {
                        List<Customer> Customers = _rep.GetAll(a => a.Id > 0);
                        response.Result = _mapper.Map<List<Customer>, List<CustomerViewModel>>(Customers);
                    }
                    response.Message = "Sucesso!";
                    response.StatusCode = 200;
                    break;
                case "PUT":
                case "POST":
                    var resultValidator = new CustomerValidator().Validate(request.ObjectView);
                    if (resultValidator.Errors.Count > 0)
                    {
                        response.Rason = resultValidator.Errors;
                        response.Message = "Falha de Validação.";
                        response.StatusCode = 422;
                        response.HasError = true;
                    }
                    else
                    {
                        if (request.Id > 0)
                        {
                            _rep.Update(request.Id, _mapper.Map<CustomerViewModel, Customer>(request.ObjectView));
                            response.Message = "Atualizado com Sucesso!";
                            response.StatusCode = 204;
                        }
                        else
                        {
                            _rep.Insert(_mapper.Map<CustomerViewModel, Customer>(request.ObjectView));
                            response.Message = "Criado com Sucesso!";
                            response.StatusCode = 201;
                        }
                    }
                    break;
                case "DELETE":
                    _rep.Remove(request.Id);
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
