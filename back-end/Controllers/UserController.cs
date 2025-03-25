using todo.Models.Users;
using todo.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PasswordService _passwordService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserContext _usersContext;
        public UsersController(UserContext usersContext, PasswordService passwordService, JwtTokenService jwtTokenService)
        {
            _usersContext = usersContext;
            _passwordService = passwordService;
            _jwtTokenService = jwtTokenService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    if(_usersContext.Users == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _usersContext.Users.ToListAsync();
        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            if (_usersContext.Users == null)
            {
                return NotFound();
            }

            var PasswordHash = _passwordService.VerifyPassword(loginRequest.Password);
            var user = await _usersContext.Users
                .Where(u => u.Email == loginRequest.Email && u.Password == PasswordHash)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Usuário ou senha inválidos.");
            }

            string token = _jwtTokenService.GenerateJwtToken(user.Name, user.Id);

            return Ok(new { token = token });
        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateUser(User user)
        {
            var PasswordHash = _passwordService.GeneratePasswordHash(user.Password);
            user.Password = PasswordHash;
            _usersContext.Users.Add(user);
            var row = await _usersContext.SaveChangesAsync();

            return Ok(row);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _usersContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _usersContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (_usersContext.Users == null)
            {
                return NotFound();
            }
            var user = await _usersContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Removed = true;
            await _usersContext.SaveChangesAsync();
            return Ok();
        }
    }
}
