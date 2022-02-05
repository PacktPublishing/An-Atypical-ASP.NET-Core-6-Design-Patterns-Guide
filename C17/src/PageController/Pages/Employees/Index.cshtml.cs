using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PageController.Data;
using PageController.Data.Models;

namespace PageController.Pages.Employees;

public class IndexModel : PageModel
{
    private readonly EmployeeDbContext _context;

    public IndexModel(EmployeeDbContext context)
    {
        _context = context;
    }

    public IList<Employee> Employee { get;set; }

    public async Task OnGetAsync()
    {
        Employee = await _context.Employees.ToListAsync();
    }
}
