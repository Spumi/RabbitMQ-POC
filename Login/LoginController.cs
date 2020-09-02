using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitSender;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Login
{
    [Route("/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserContext _context;

        public LoginController(UserContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            Send.SendMessage(JsonSerializer.Serialize(_context.Users.ToListAsync()), "hello");
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            bool auth = false;
            User loginUser = null;
             foreach (User _user in _context.Users)
            {
                if (user.Username == _user.Username && user.Password == _user.Password)
                {
                    auth = true;
                    loginUser = _user;
                    _user.GenerateAuthToken();
                    _context.SaveChanges();
                    break;
                }
            }
            //_context.Users.Add(user);
            _context.SaveChangesAsync();
            if (auth)
            {
                return new JsonResult(loginUser);
            }
            else
                return new JsonResult(new User { Username = null, Id = 0, Auth_token = null });
        }

        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            if (!_context.Users.Any(x => x.Username == user.Username))
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
            //return CreatedAtAction("GetUser", new { id = user.Id }, (UserResponse)user);
            return new User { Username = user.Username, Id = user.Id, Auth_token = user.Auth_token };
        }

    }
}
