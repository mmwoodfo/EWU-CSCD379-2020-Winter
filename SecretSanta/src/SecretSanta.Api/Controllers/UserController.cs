using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService UserService {get;}

        public UserController(IUserService userService)
        {
            UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: api/User
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            List<User> users = await UserService.FetchAllAsync();
            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Get(int id)
        {
            if(await UserService.FetchByIdAsync(id) is { } user)
            {
                return Ok(user);
            }
            return NotFound();
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User value)
        {
            //TODO
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            //TODO
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           //TODO
        }
    }
}
