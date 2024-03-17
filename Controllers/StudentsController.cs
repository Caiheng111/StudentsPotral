using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Models;
using StudentPortalWeb.Models.Entities;

namespace StudentPortalWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentsViewModel viewModel)
        {
            var student = new Students
            {

                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,

            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

          return  RedirectToAction("List", "Students");


        }

        [HttpGet]
        public async Task<IActionResult> List()
        {

            var students = await dbContext.Students.ToListAsync();

            return View(students);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

        var student= await dbContext.Students.FindAsync(id);

            return View(student);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Students viewModel)
        {

            var student = await dbContext.Students.FindAsync(viewModel.Id);
            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
                return RedirectToAction("List", "Students");
               
            }

            return View(student);

        }




        [HttpPost]
        public async Task<IActionResult> Delete(Students viewModel)
        {

            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (student is not null)
            {

                dbContext.Students.Remove(viewModel);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }
    }




}
