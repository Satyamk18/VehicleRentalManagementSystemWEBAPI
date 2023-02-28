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
    public class TransactionController : ControllerBase{
        private readonly ACE42023Context db;
        public TransactionController(ACE42023Context _db){
            db = _db;
        }
        
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult Transactions(int id){
            // ViewBag.username = HttpContext.Session.GetString("Username");
            // if(ViewBag.username == null){
            //     return RedirectToAction("Login","Account");
            // }
            //TransactionVrm transaction = db.TransactionVrms.FirstOrDefault(x => x.TransactionId == id);
           // int userid = Convert.ToInt32(HttpContext.Session.GetString("Userid"));
           
            Customer customer = db.Customers.FirstOrDefault(x => x.Userid == id);
            List<TransactionVrm> list = new List<TransactionVrm>();
            foreach(TransactionVrm t in db.TransactionVrms){
                  if(t.Userid == customer.Userid){
                     list.Add(t);
                  }
            }
            return Ok(list);
        }
        // [HttpGet]
        // [Route("api/getTransactionById/{id}")]
        // public IActionResult Transactions(int id){
        //     // ViewBag.username = HttpContext.Session.GetString("Username");
        //     // if(ViewBag.username == null){
        //     //     return RedirectToAction("Login","Account");
        //     // }
        //    // int userid = Convert.ToInt32(HttpContext.Session.GetString("Userid"));
            
        //     // Customer customer = db.Customers.FirstOrDefault(x => x.Userid == userid);
        //     // List<TransactionVrm> list = new List<TransactionVrm>();
        //     // foreach(TransactionVrm t in db.TransactionVrms){
        //     //       if(t.Userid == customer.Userid){
        //     //          list.Add(t);
        //     //       }
        //     // }
        //     try{
        //             TransactionVrm transaction = db.TransactionVrms.FirstOrDefault(x => x.TransactionId == id);
        //             return Ok(transaction);
        //     }catch{
        //             return Ok("Transaction not found");
        //     }
        // }

        [HttpPost]
        [Route("api/[controller]/RentVehicle/{vehicleid}")]
        public IActionResult RentVehicle(int vehicleid , int userid){
            try{
                TransactionVrm transaction = new TransactionVrm();
                Vehicle vehicle = db.Vehicles.FirstOrDefault(x => x.VehicleId == vehicleid);
                transaction.VehicleId = vehicle.VehicleId;
                //transaction.Userid = Convert.ToInt32(HttpContext.Session.GetString("Userid"));
                transaction.Userid = userid;

                transaction.RentalStartDate = DateTime.Now;
                vehicle.IsAvailable = false;
                db.TransactionVrms.Add(transaction);
                db.SaveChanges();
                return Ok(transaction);
            }catch{
                return Ok("Error");
            }
        }
        [HttpPost]
        [Route("api/[controller]/Return")]
        public IActionResult ReturnVehicle(int transactionid,int userid){
              TransactionVrm transaction = db.TransactionVrms.FirstOrDefault(x => x.TransactionId == transactionid);
              Vehicle vehicle = db.Vehicles.FirstOrDefault(x => x.VehicleId == transaction.VehicleId);
              transaction.RentalEndDate = DateTime.Now;
              TimeSpan diff= DateTime.Now.Subtract(transaction.RentalStartDate);
              int diffindays = diff.Days;
              if(diffindays == 0){
                 diffindays = 1;
              }
              int rentalrate = diffindays*(vehicle.DailyRent);
              transaction.RentalRate = rentalrate;
              vehicle.IsAvailable = true;
              db.TransactionVrms.Update(transaction);
              //ViewBag.transaction = transaction;
              db.Vehicles.Update(vehicle);
              db.SaveChanges();
              return Ok(transaction);
              //return View(transaction);
        }

    }
}