using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly AccountContext _context;
        public AccountController(AccountContext context)
        {
            _context = context;
        }
        // tim toan bo account
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAll()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }
        // lay account 
        [HttpGet("id")]
        public async Task<ActionResult<List<Account>>> GetAccount(int id)
        {
            var acc = await _context.Accounts.FindAsync(id);
            if (acc == null)
            {
                return BadRequest("Account not found.");
            }
            return Ok(acc);
        }
        // them account
        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return Ok(await _context.Accounts.ToListAsync());
        }
        // cap nhat account
        [HttpPut("id")]
        public async Task<ActionResult<List<Account>>> UpdateAccount(Account request)
        {
            var acc = await _context.Accounts.FindAsync(request.Id);
            if (acc == null)
            {
                return BadRequest("Account not found.");
            }

            acc.Name = request.Name;
            acc.FirstName = request.FirstName;
            acc.LastName = request.LastName;
            acc.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.ToListAsync());
        }
        // Xoa account
        [HttpDelete("id")]
        public async Task<ActionResult<List<Account>>> DeleteAccount(int id)
        {
            var acc = await _context.Accounts.FindAsync(id);
            if (acc == null)
            {
                return BadRequest("Account not found.");
            }
            _context.Accounts.Remove(acc);

            await _context.SaveChangesAsync();
            
            return Ok(await _context.Accounts.ToListAsync());
        }
    }
}
