using ViewModels.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService = new StudentService();

        public async Task<IActionResult> Index()
        {
            // Get data from the data store
            var students = await _studentService.ReadAllAsync();

            // Create the ViewModel, based on the data
            var viewModel = new StudentListViewModel
            {
                Students = students.Select(student => new StudentListItemViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    ClassCount = student.Classes.Count()
                })
            };

            // Return the View
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel model)
        {
            // Create the student
            await _studentService.CreateAsync(model.Name);

            // Redirect back to the list
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.ReadOneAsync(id);
            var viewModel = new EditStudentViewModel
            {
                Id = student.Id,
                Form = new StudentFormViewModel
                {
                    Name = student.Name,
                },
                Classes = student.Classes.Select(x => x.Name)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentViewModel model)
        {
            // Update the student
            await _studentService.UpdateAsync(model.Id, model.Form.Name);

            // Redirect back to the list
            return RedirectToAction(nameof(Index));
        }
    }
}
