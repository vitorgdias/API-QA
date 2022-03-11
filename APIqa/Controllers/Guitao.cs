using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIqa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuitaoController : ControllerBase
    {
        public GuitaoController()
        {
        }

        [HttpGet]
        public string Get()
        {
            return "Guilherme Panisso e Vitor Galves";
        }

        [HttpPost]
        public string Post(string nome)
        { 
            if(nome == "Guilherme") {
                return "Panisso";
            }
            else if(nome == "Vitor")
            {
                return "Galves";
            }
            return "Errrooow";
        
        }

        
    }
}