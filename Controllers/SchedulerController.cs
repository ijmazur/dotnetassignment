using dotnet.Data;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

public class SchedulerController : Controller
{
    private readonly ApplicationDbContext _db;

    public SchedulerController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    public IActionResult Index()
    {
        IEnumerable<Scheduler> objSchedulerList = _db.Schedulers.ToList();
        return View(objSchedulerList);
    }
    
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Scheduler obj)
    {
        if (obj.Name == obj.Description.ToString())
        {
            ModelState.AddModelError("Name", "The description cant exactl match the name of the task, please be more insightful");
        }
        
        if (ModelState.IsValid)
        {
            _db.Schedulers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View();
    }
}