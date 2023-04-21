using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace HttpServerWorkshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private static readonly Dictionary<Guid, User> _users = new Dictionary<Guid, User>
        {
            [Guid.NewGuid()] = new User { Name = "Alice" },
            [Guid.NewGuid()] = new User { Name = "Bob" },
            [Guid.NewGuid()] = new User { Name = "Charlie" },
        };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(Guid id)
        {
            if (_users.ContainsKey(id))
            {
                return _users[id];
            }

            return NotFound(id);
        }

        [HttpPost]
        public ActionResult<Guid> AddUser(string name)
        {
            var newUser = new User { Name = name };
            var guid = Guid.NewGuid();
            _users.Add(guid, newUser);

            return Ok(guid);
        }

        [HttpPut("{id}")]
        public ActionResult<Guid> UpdateUser(Guid id, string user)
        {
            Request.Headers.TryGetValue("user", out StringValues username);
            _logger.LogInformation($"Username: [{username}]");

            if (_users.ContainsKey(id))
            {
                _users[id].Name = user;
                return Ok(id);
            }

            return NotFound(id);
        }

        [HttpDelete("{id}")]
        public ActionResult<Guid> DeleteUser(Guid id)
        {
            if (_users.ContainsKey(id))
            {
                _users.Remove(id);
                return Ok(id);
            }

            return NotFound(id);
        }

    }
}
