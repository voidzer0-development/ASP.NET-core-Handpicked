using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace B2Handpicked.UI.Controllers {
    [Authorize]
    public class ContactPersonsController : Controller {
        private readonly ApplicationDbContext _context;

        public ContactPersonsController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: ContactPersons
        public async Task<IActionResult> Index() {
            return View(await _context.ContactPersons.ToListAsync());
        }

        // GET: ContactPersons/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contactPerson = await _context.ContactPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPerson == null) {
                return NotFound();
            }

            return View(contactPerson);
        }

        // GET: ContactPersons/Create
        public IActionResult Create() {
            return View();
        }

        // POST: ContactPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Name,PhoneNumber")] ContactPerson contactPerson) {
            if (ModelState.IsValid) {
                _context.Add(contactPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactPerson);
        }

        // GET: ContactPersons/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contactPerson = await _context.ContactPersons.FindAsync(id);
            if (contactPerson == null) {
                return NotFound();
            }
            return View(contactPerson);
        }

        // POST: ContactPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,PhoneNumber")] ContactPerson contactPerson) {
            if (id != contactPerson.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(contactPerson);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ContactPersonExists(contactPerson.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactPerson);
        }

        // GET: ContactPersons/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contactPerson = await _context.ContactPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactPerson == null) {
                return NotFound();
            }

            return View(contactPerson);
        }

        // POST: ContactPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var contactPerson = await _context.ContactPersons.FindAsync(id);
            _context.ContactPersons.Remove(contactPerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactPersonExists(int id) {
            return _context.ContactPersons.Any(e => e.Id == id);
        }
    }
}
