using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadastroProduto.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IMapper _mapper;
        private IOrderRepository _rep;


        public OrderController(IMapper mapper, IOrderRepository rep)
        {
            this._mapper = mapper;
            this._rep = rep;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public OrderViewModel Get(int id)
        {
            Order o = this._rep.GetOne(id);
            return this._mapper.Map<Order, OrderViewModel>(o);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
