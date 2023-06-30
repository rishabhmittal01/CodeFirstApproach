using CodeFirstApproach.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace CodeFirstApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UsersController(UserContext usercontext)
        {
                _userContext = usercontext;
        }

        [HttpGet]
        [Route("Extract")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_userContext.Users == null) 
            {
                return NotFound();      
            }
            return await _userContext.Users.ToListAsync();
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int id)
        {
            if(_userContext.Users == null) 
            {
                return NotFound();
            }
            var user = await _userContext.Users.FirstOrDefaultAsync();
            if(user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _userContext.Entry(user).State = EntityState.Modified;
            try 
            {
                await _userContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)  
            {
                if(!BrandAvailable(id)) 
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
            }
            return Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if(_userContext.Users==null)
            {
                return NotFound();
            }
            var user= await _userContext.Users.FindAsync(id);
            if(user==null)
            {
                return NotFound();
                12]
            }
            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();
            return Ok(user);
        }
        private bool BrandAvailable(int id)
        {
            return (_userContext.Users?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}
