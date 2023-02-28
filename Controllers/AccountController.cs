using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using firstwebapi.Models;


namespace firstwebapi.Controllers
{
    [ApiController]
//    [Route("[controller]")]
    public class AccountController : ControllerBase{
        private readonly ACE42023Context db;
        public AccountController(ACE42023Context _db){
            db = _db;
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public IActionResult Login([FromBody] Customer? user){
            var Username = user.Username;
            var Password = user.Password;
            if(Username == null || Password == null){
                return BadRequest(new {Message = "Username and Password are Required!"});
            } 
            var result = (from i in db.Customers
                          where i.Username == Username && i.Password == Password
                          select i).SingleOrDefault();
                          
            if(result != null){
                return Ok(result);
            }
            else{
                return Unauthorized(new {Message = "Invalid Credentials!"});
            }                        
        }

        [HttpPost]
        [Route("api/[controller]/Register")]
        public async Task<ActionResult> Register(Customer? customer){
            if(ModelState.IsValid){
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return Ok(customer);
            }else{
                return BadRequest("Please Provide details in valid form!");
            }
        }
        //logout
        // [HttpPost]
        // [Route("api/[controller]/Logout")] 
        // public IActionResult Logout(){
        //     // HttpContext.Session.Clear();
        //     // return RedirectToAction("Login", "Account");
        //     return Ok();
        // }
        
    }
}