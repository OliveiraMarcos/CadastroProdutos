using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadastroProduto.Controllers
{
    public abstract class AbstractBaseController : Controller
    {
        protected IActionResult GetContentByStatusCode(Response response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}
