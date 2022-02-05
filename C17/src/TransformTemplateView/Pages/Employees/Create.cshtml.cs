using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TransformTemplateView.Data;
using TransformTemplateView.Data.Models;

namespace TransformTemplateView.Pages.Employees;

public class CreateModel : PageModel
{
    private readonly EmployeeDbContext _context;
    public CreateModel(EmployeeDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Employee Employee { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Employees.Add(Employee);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
