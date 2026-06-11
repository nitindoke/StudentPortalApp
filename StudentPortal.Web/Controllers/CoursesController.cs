using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CoursesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel vm)
        {
            var course = new Course
            {
                Id = Guid.NewGuid().ToString(),
                Title = vm.Title,
                Description = vm.Description,
                Credits = vm.Credits
            };

            await _db.Course.AddAsync(course);
            await _db.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var courses = await _db.Course.ToListAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var course = await _db.Course.FindAsync(id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course model)
        {
            var course = await _db.Course.FindAsync(model.Id);
            if (course != null)
            {
                course.Title = model.Title;
                course.Description = model.Description;
                course.Credits = model.Credits;

                await _db.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Course vm)
        {
            var course = await _db.Course
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == vm.Id);

            if (course != null)
            {
                _db.Course.Remove(vm);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
