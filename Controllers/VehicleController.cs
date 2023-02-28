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
    public class VehicleController : ControllerBase
    {
        private readonly ACE42023Context db;
        public VehicleController(ACE42023Context _db){
            db = _db;
        }
        
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult getVehicles(){
            return Ok(db.Vehicles);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult getVehicles(int id){
            Vehicle vehicle = db.Vehicles.FirstOrDefault(x => x.VehicleId == id);
            if(vehicle == null){
                return BadRequest(new {Message = "Vehicles not found!"});
            }
            return Ok(vehicle);
        }        
        [HttpPost]     
        [Route("api/[controller]")]
        public async Task<IActionResult> AddVehicles([FromBody] Vehicle? v){
           
            if(ModelState.IsValid){
                db.Vehicles.Add(v);
                await db.SaveChangesAsync();
                return Ok(v);
            }
            else{
                return BadRequest(new {Message = "Please Provide details in valid form!"});
            }
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult> UpdateVehicles(int id,[FromBody] Vehicle? vehicle){
            Vehicle v = await db.Vehicles.FindAsync(id);
            if(v == null){
                return BadRequest(new {Message = "Vehicle not found!"});
            }
                v.ModelName = vehicle.ModelName;
                v.RegistrationNumber = vehicle.RegistrationNumber;
                v.DailyRent = vehicle.DailyRent;
                v.VehicleType = vehicle.VehicleType;

                db.Vehicles.Update(v);
                await db.SaveChangesAsync();
            return Ok(v);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteVehicles(int id){
            try{
                       List<Review> reviews = db.Reviews.Where(x => x.VehicleId == id).ToList();
                        List<TransactionVrm> transactionlist = db.TransactionVrms.Where(x => x.VehicleId == id).ToList();
                        Vehicle vehicle = db.Vehicles.FirstOrDefault(x => x.VehicleId == id);
                        foreach(TransactionVrm transaction in transactionlist){
                            db.TransactionVrms.Remove(transaction);
                        }
                        foreach(Review review in reviews){
                            db.Reviews.Remove(review);
                        }
                        db.SaveChanges();
                        db.Vehicles.Remove(vehicle);
                        db.SaveChanges();
                        return Ok("Vehicle Deleted from database");
            }catch{
                return BadRequest(new {Message = "Vehicle not found!!!!!"});
            }
            
        }
       
       [HttpPost]
       [Route("api/[controller]/Search")]
        public async Task<ActionResult> search([FromBody]string keyword){
            var result = db.Vehicles.Where(x => x.ModelName.Contains(keyword)).Select(x => x).ToList();
            return Ok(result);
        }

    }
}