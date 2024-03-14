using Microsoft.AspNetCore.Mvc;
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
            return View();

        }
    }
}
