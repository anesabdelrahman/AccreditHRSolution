using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR.Domain.Models;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace HR.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeController(EmployeeContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int pageNumber = 1)
        {
            SortWith(sortOrder);

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var employees = from s in _context.Employee
                            select s;


            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Department.Contains(searchString)
                                || s.Status.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "FirstName";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                employees = employees.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                employees = employees.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            var pageSize = _configuration.GetValue<int>("ViewsPaginationSettings:TotalResultsPerPage");
            return View(employees.ToPagedList(pageNumber, pageSize));
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,Email,EmployeeStatus,Department,EmployeeNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Status = employee.EmployeeStatus.ToString();
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,DateOfBirth,Department,EmployeeStatus,EmployeeNumber")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.Status = employee.EmployeeStatus.ToString();
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void SortWith(string sortOrder)
        {
            ViewData["FirstNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewData["StatusSortParm"] = sortOrder == "EmployeeStatus" ? "EmployeeStatus_desc" : "EmployeeStatus";
            ViewData["DepartmentSortParm"] = sortOrder == "Department" ? "Department_desc" : "Department";
            ViewData["DobSortParm"] = sortOrder == "DateOfBirth" ? "DateOfBirth_desc" : "DateOfBirth";
            ViewData["EmployeeNumSortParm"] = sortOrder == "EmployeeNumber" ? "EmployeeNumber_desc" : "EmployeeNumber";
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
