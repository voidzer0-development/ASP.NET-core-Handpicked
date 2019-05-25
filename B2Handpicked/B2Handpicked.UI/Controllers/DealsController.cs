using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using Microsoft.AspNetCore.Authorization;

namespace B2Handpicked.UI.Controllers {
    [Authorize]
    public class DealsController : Controller {
        private readonly IRepository<Deal> _dealRepository;

        public DealsController(IRepository<Deal> dealRepository) {
            _dealRepository = dealRepository;
        }

        // GET: Deals
        public async Task<IActionResult> Index() => View(await _dealRepository.GetAllAsList());

        // GET: Deals/Details/5
        public async Task<IActionResult> Details(int? id) {
            var deal = await _dealRepository.GetById(id);
            if (deal is null) return NotFound();

            return View(deal);
        }

        // GET: Deals/Create
        public IActionResult Create() => View();

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Value,Deadline,Percentage")] Deal deal) {
            if (ModelState.IsValid) {
                await _dealRepository.Create(deal);
                return RedirectToAction(nameof(Index));
            }
            return View(deal);
        }

        // GET: Deals/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            var deal = await _dealRepository.GetById(id);
            if (deal is null) return NotFound();

            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Value,Deadline,Percentage")] Deal deal) {
            if (id != deal.Id) return NotFound();

            if (ModelState.IsValid) {
                try {
                    await _dealRepository.Update(deal);
                } catch (KeyNotFoundException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deal);
        }

        // GET: Deals/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            var deal = await _dealRepository.GetById(id);
            if (deal is null) return NotFound();

            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _dealRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
