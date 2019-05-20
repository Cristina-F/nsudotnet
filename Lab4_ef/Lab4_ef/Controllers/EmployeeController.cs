using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab4_ef.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebLab4_ef.Controllers {
    public class EmployeesController : Controller {
        private readonly MyContext _context;

        public EmployeesController(MyContext context) {
            _context = context;
        }

// GET: Employees 
        [Route(("/Employee"))]
        public async Task<IActionResult> Index() {
            var employees = _context.Employees.Include(e => e.Projects)
                .Select(e => new EmployeeView {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    City = e.City,
                    Sum = e.Projects.Sum(p => p.Bonus),
                    EmployeeId = e.EmployeeId,
                })
                .OrderByDescending(o => o.Sum)
                .ToList();
            return View(employees);
        }

// GET: Employees/Details/5 
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employee = await _context.Employees.Include(e => e.Projects)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

// GET: Employees/Create 
        public IActionResult Create() {
            return View();
        }

// POST: Employees/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,City")]
            Employee employee) {
            if (ModelState.IsValid) {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

// GET: Employees/Edit/5 
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> EditProject(int? id) {
            if (id == null) {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null) {
                return NotFound();
            }

            var employees = _context.Employees.Include(e => e.Projects)
                .Select(e => new EmploeeNameView() {
                    Name = e.FirstName + " " + e.LastName,
                    EmployeeId = e.EmployeeId,
                })
                .ToList();

            ViewBag.EmployeeId = new SelectList(employees, "EmployeeId", "Name");

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int id, [Bind("ProjectId,Name,EmployeeId,Bonus")]
            Project project) {
            if (id != project.ProjectId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!_context.Projects.Any(e => e.ProjectId == id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(project);
        }

// POST: Employees/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,City")]
            Employee employee) {
            if (id != employee.EmployeeId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!EmployeeExists(employee.EmployeeId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

// GET: Employees/Delete/5 
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null) {
                return NotFound();
            }

            return View(employee);
        }

// POST: Employees/Delete/5 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id) {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

// GET: Employees/Delete/5 
        public async Task<IActionResult> DeleteProject(int? id, int ? idE) {
            if (id == null) {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null) {
                return NotFound();
            }
            return View(project);
        }
        
        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjectConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            var employee = await _context.Employees.FindAsync(project.EmployeeId);
            employee.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
    
}
