using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using Microsoft.AspNetCore.Authorization;

namespace B2Handpicked.UI.Controllers {
    [Authorize]
    public class LabelsController : Controller {
        private readonly IRepository<Label> _labelsRepository;

        public LabelsController(IRepository<Label> labelsRepository) {
            _labelsRepository = labelsRepository;
        }

        // GET: Labels
        public async Task<IActionResult> Index() => View(await _labelsRepository.GetAllAsList());

        // GET: Labels/Details/5
        public async Task<IActionResult> Details(int? id) {
            var label = await _labelsRepository.GetById(id);
            if (label is null) return NotFound();

            return View(label);
        }

        // GET: Labels/Create
        public IActionResult Create() => View();

        // POST: Labels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Token,Name")] Label label) {
            if (ModelState.IsValid) {
                await _labelsRepository.Create(label);
                return RedirectToAction(nameof(Index));
            }
            return View(label);
        }

        // GET: Labels/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            var label = await _labelsRepository.GetById(id);
            if (label is null) return NotFound();

            return View(label);
        }

        // POST: Labels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Token,Name")] Label label) {
            if (id != label.Id) return NotFound();

            if (ModelState.IsValid) {
                try {
                    await _labelsRepository.Update(label);
                } catch (KeyNotFoundException) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(label);
        }

        // GET: Labels/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            var label = await _labelsRepository.GetById(id);
            if (label is null) return NotFound();

            return View(label);
        }

        // POST: Labels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            await _labelsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
