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

namespace Repository.Mediator.ProductClass
{
    public class ProductHandler : IRequestHandler<ProductMediator, Response>
    {
        private readonly IGenericRepository<Product> _rep;
        private readonly IMapper _mapper;

        public ProductHandler(IGenericRepository<Product> rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public Task<Response> Handle(ProductMediator request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            
            switch (request.Method)
            {
                case "GET":
                    if (request.Id > 0)
                    {
                        Product Product = _rep.GetOne(request.Id);
                        response.Result = _mapper.Map<Product, ProductViewModel>(Product);
                    }
                    else
                    {
                        List<Product> Products = _rep.GetAll(a => a.Id > 0);
                        response.Result = _mapper.Map<List<Product>, List<ProductViewModel>>(Products);
                    }
                    response.Message = "Sucesso!";
                    response.StatusCode = 200;
                    break;
                case "PUT":
                case "POST":
                    var resultValidator = new ProductValidator().Validate(request.ObjectView);
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
                            _rep.Update(request.Id, _mapper.Map<ProductViewModel, Product>(request.ObjectView));
                            response.Message = "Atualizado com Sucesso!";
                            response.StatusCode = 204;
                        }
                        else
                        {
                            _rep.Insert(_mapper.Map<ProductViewModel, Product>(request.ObjectView));
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
