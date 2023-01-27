using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // GET: api/<TransactionController>
        [HttpPost("Trans")]
        public IActionResult Trans (ML.Transaction transaction)
        {
            ML.Result result = BL.Transaction.Trans(transaction);  //se ingresa al metodo
            if (result.Correct)
            {
                return Ok(transaction);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET api/<TransactionController>/5
        [HttpGet("GetByAccount")]
        public IActionResult GetAccount(string account)
        {
            ML.Transaction transaction = new ML.Transaction();
            ML.Result result = new ML.Result();

            result = BL.Transaction.GetAccount(account); //se ingresa al metodo

            if (result.Correct)
            {
                return Ok(result); 
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<TransactionController>/5
        [HttpGet("FromAccount")]
        public IActionResult FromAccount(string fromaccount)
        {
            ML.Transaction transaction = new ML.Transaction();
            ML.Result result = new ML.Result();

            result = BL.Transaction.FromAccount(fromaccount);  //se ingresa al metodo

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<TransactionController>/5
        [HttpGet("ToAccount")]
        public IActionResult ToAccount(string toaccount)
        {
            ML.Transaction transaction = new ML.Transaction();
            ML.Result result = new ML.Result();

            result = BL.Transaction.ToAccount(toaccount);  //se ingresa al metodo

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
