using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Account account)
        {
            ML.Result result = BL.Account.Add(account);  //se ingresa al metodo
            if (result.Correct)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
        }
        // GET: api/<AccountController>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            ML.Account account = new ML.Account();
            ML.Result result = new ML.Result();
            result = BL.Account.GetAll();  //se ingresa al metodo

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<AccountController>/5
        [HttpGet("GetByOwner")]
        public IActionResult GetByOwner(string owner)
        {
            ML.Account account = new ML.Account();
            ML.Result result = new ML.Result();

            result = BL.Account.GetByOwner(owner);  //se ingresa al metodo

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        
        // GET api/<AccountController>/5
        [HttpGet("Balance")]
        public IActionResult Balance(string account)
        {
            ML.Account balance = new ML.Account();
            ML.Result result = new ML.Result();

            result = BL.Account.Balance(account);  //se ingresa al metodo

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}