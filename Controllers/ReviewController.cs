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
    //[Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ACE42023Context db;
        public ReviewController(ACE42023Context _db){
            db = _db;
        }

        //getreviews by vehicleid
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult getReviewsById(int id){
            var reviews = db.Reviews.Where(x => x.VehicleId == id).ToList();
            return Ok(reviews);
        }   

        [HttpPost]
        [Route("api/[controller]/submitReview")]
        public async Task<IActionResult> ReviewSubmit([FromBody]Review? rev){
            // ViewBag.username = HttpContext.Session.GetString("Username");
            // if(ViewBag.username == null){
            //     return RedirectToAction("Login","Account");
            // }
            // rev.Userid = Convert.ToInt32(HttpContext.Session.GetString("Userid"));
             
           if(ModelState.IsValid){
                db.Reviews.Add(rev);
                await db.SaveChangesAsync();
                return Ok(rev);
            }
            else{
                // return Ok("Client side error");
                return BadRequest(new {Message = "Please Provide details in valid form!"});
            }
        }
    }
}