using System;
using System.Linq;
using Dapper_.Data;
using Dapper_.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dapper_.Repository.IRepository;

namespace Dapper_.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IRepository<Company> _context;
        private readonly IRepository<Employee> _empRepo;

        public EmployeesController(IRepository<Company> context,
            IRepository<Employee> empRepo)
        {
            _context = context;
            _empRepo = empRepo;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        // GET: Employees
        public async Task<IActionResult> Index()
        {
              return _empRepo.GetAll() != null ? 
                          View(_empRepo.GetAll()) :
                          Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> companies = _context.GetAll().Select(item=>
            new SelectListItem()
                {
                  Text= item.Name.ToString(),
                  Value=item.CompanyId.ToString()
                }
            );
            ViewBag.CompanyList = companies;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Push()
        {
            if (ModelState.IsValid)
            {
                _empRepo.Add(Employee);
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _empRepo.GetAll() == null)
            {
                return NotFound();
            }

            var employee = await _empRepo.Find(id.GetValueOrDefault());
            if (employee == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> companies = _context.GetAll().Select(item =>
            new SelectListItem()
            {
                Text = item.Name.ToString(),
                Value = item.CompanyId.ToString()
            }
            );
            ViewBag.CompanyList = companies;

            return View(employee);
        }
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _empRepo.GetAll() == null)
            {
                return NotFound();
            }

            Employee = await _empRepo.Find(id.GetValueOrDefault());
            if (Employee == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> companies = _context.GetAll().Select(item =>
            new SelectListItem()
            {
                Text = item.Name.ToString(),
                Value = item.CompanyId.ToString()
            }
            );
            ViewBag.CompanyList = companies;
            return View(Employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _empRepo.Update(Employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _empRepo.Find(Employee.EmployeeId) !=null)
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
            return View(Employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id.HasValue)
               _empRepo.Remove(id.Value);
            return RedirectToAction(nameof(Index));
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_empRepo.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            Employee = await _empRepo.Find(id);
            if (Employee != null)
            {
                _empRepo.Remove(id);
            }           
            return RedirectToAction(nameof(Index));
        }
    }
}
