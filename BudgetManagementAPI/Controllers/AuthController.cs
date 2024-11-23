using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using Microsoft.AspNetCore.Identity;
using BudgetManagementAPI.Security;
using BudgetManagementAPI.Dto;

namespace BudgetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly BudgetDBContext _context;

        public AuthController(BudgetDBContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        // GET: api/Users/5
        [HttpPost("/login")]
        public async Task<ActionResult<Boolean>> Login([FromBody] UserCredentialDTO userCredentialDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentialDTO.Username, userCredentialDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded) {
                return Unauthorized("Invalid login attempt.");
            }

            return Ok(true); // Successfully logged in
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/register")]
        public async Task<ActionResult<Boolean>> Register([FromBody] UserCredentialDTO userCredentialDTO)
        {
            
            var user = new ApplicationUser { UserName = userCredentialDTO.Username };
            var result = await _userManager.CreateAsync(user, userCredentialDTO.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors); // Returns validation errors if creation failed

            return Ok(true); // Successfully registered
        }

    }
}
