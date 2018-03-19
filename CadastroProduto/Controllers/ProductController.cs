using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Repository.Mediator.ProductClass;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadastroProduto.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : AbstractBaseController
    {
        private readonly IMediator _mediatr;
        public ProductController(IMediator mediatr)
        {
            _mediatr = mediatr;

        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return GetContentByStatusCode(_mediatr.Send(new ProductMediator() { Method = "GET"}).Result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return GetContentByStatusCode(_mediatr.Send(new ProductMediator() { Method = "GET", Id=id }).Result);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]ProductViewModel value)
        {
            return GetContentByStatusCode(_mediatr.Send(new ProductMediator() { Method = "POST", ObjectView=value }).Result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductViewModel value)
        {
            return GetContentByStatusCode(_mediatr.Send(new ProductMediator() { Method = "PUT", Id=id, ObjectView=value }).Result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return GetContentByStatusCode(_mediatr.Send(new ProductMediator() { Method = "DELETE", Id=id }).Result);
        }
    }
}
