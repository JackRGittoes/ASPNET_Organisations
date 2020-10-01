using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganisationsDetail.Models;

namespace OrganisationsDetail.Controllers
{
    public class OrganisationController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Organisation Organisation { get; set; }
        public OrganisationController(ApplicationDbContext db)
        {
            _db = db;
        }

  
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int? id)
        {
            Organisation = new Organisation();
            if (id == null)
            {
                
                return View(Organisation);
            }
            
            Organisation = _db.Organisations.FirstOrDefault(u => u.Id == id);
            if (Organisation == null)
            {
                return NotFound();
            }

            return View(Organisation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                if (Organisation.Id == 0)
                {
                    // create
                    _db.Organisations.Add(Organisation);

                }
                else
                {
                    _db.Organisations.Update(Organisation);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Organisation);
        }

        #region APICalls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           return Json(new { data = await _db.Organisations.ToListAsync() });
            

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var orgFromDb = await _db.Organisations.FirstOrDefaultAsync(u => u.Id == id);
            if (orgFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Organisations.Remove(orgFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
