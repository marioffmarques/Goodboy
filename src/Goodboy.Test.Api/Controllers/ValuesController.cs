using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Goodboy.Http;
using Goodboy.Practices.Command;
using Microsoft.AspNetCore.Mvc;

namespace Goodboy.Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
		private readonly IBus _busClient;
		private readonly ICommandHandler<CreateValueCommand> _valueCommandHandler;


		public ValuesController(IBus busclient, ICommandHandler<CreateValueCommand> valueCommandHandler)
		{
			_busClient = busclient;
			_valueCommandHandler = valueCommandHandler;
		}

		// GET api/values
		[HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {

			//var command = new CreateValueCommand("carne");
			//await _valueCommandHandler.HandleAsync(command);


			//return Accepted(new OkResponse(""));



			return new string[] { "value1", "value2" };
		}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
