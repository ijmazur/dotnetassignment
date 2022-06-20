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
        // IEnumerable<Scheduler> objSchedulerList = _db.Schedulers.ToList();
        var objSchedulerList = _db.Schedulers.ToList();
        return View(objSchedulerList);
    }
    
    // GET Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    // POST Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Scheduler obj)
    {
        if (obj.Name == obj.Description.ToString())
        {
            ModelState.AddModelError("Name",
                "The description cant exactly match the name of the task, please be more insightful");
        }
        if (ModelState.IsValid)
        {
            _db.Schedulers.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Task has been created!";
            return RedirectToAction("Index");
        }
        return View();
    }
    
    // GET Update
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var scheduleFromDb = _db.Schedulers.Find(id);
        // var scheduleFromDbFirst = _db.Schedulers.FirstOrDefault(u=>u.Id==id);
        // var scheduleFromDbSingle = _db.Schedulers.SingleOrDefault(u => u.Id == id);

        if (scheduleFromDb == null)
        {
            return NotFound();
        }
        
        return View(scheduleFromDb);
    }
    
    // POST Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Scheduler obj)
    {
        if (obj.Name == obj.Description.ToString())
        {
            ModelState.AddModelError("Name",
                "The description cant exactly match the name of the task, please be more insightful");
        }
        if (ModelState.IsValid)
        {
            _db.Schedulers.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Task has been updated!";
            return RedirectToAction("Index");
        }
        return View();
    }
    
    // GET Delete
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var scheduleFromDb = _db.Schedulers.Find(id);

        if (scheduleFromDb == null)
        {
            return NotFound();
        }
        
        return View(scheduleFromDb);
    }
    
    // POST Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.Schedulers.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Schedulers.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Task has been deleted!";
        return RedirectToAction("Index");
    }
}