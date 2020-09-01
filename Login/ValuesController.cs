using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Login
{
    [Route("api/")]
    public class ValuesController : Controller
    {
        private readonly UserContext _context;

        public ValuesController(UserContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (!_context.Users.Any(x => x.Username == user.Username))
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetUser", new { id = user.Id }, (UserResponse)user);
            return new User { Username = user.Username, Id = user.Id, Auth_token = user.Auth_token };
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
