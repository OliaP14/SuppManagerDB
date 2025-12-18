using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using SuppManagerDB.WebApp.Models;

namespace SuppManagerDB.WebApp.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(
            ISupplierManager manager,
            IMapper mapper,
            ILogger<SuppliersController> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        //  Read
        [AllowAnonymous]
        public IActionResult Index()
        {
            var suppliers = _manager.GetAll();   //  метод
            return View(suppliers);
        }

        // Create (GET)
        [Authorize(Roles = "SupplierManager")] //[Authorize]
        public IActionResult Create()
        {
            return View(new EditSupplierModel());
        }

        // Create (POST)

        [Authorize(Roles = "SupplierManager")] //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditSupplierModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var supplier = _mapper.Map<Supplier>(model);
                _manager.Create(supplier);   //  метод
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supplier");
                ModelState.AddModelError("", "Error creating supplier");
                return View(model);
            }
        }

        // Edit
        [Authorize(Roles = "SupplierManager")]
        public IActionResult Edit(int id)
        {
            //  У BL НЕМАЄ GetById — беремо з GetAll()
            var supplier = _manager.GetAll().FirstOrDefault(s => s.SupplierID == id);
            if (supplier == null) return NotFound();

            return View(_mapper.Map<EditSupplierModel>(supplier));
        }

        [Authorize(Roles = "SupplierManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditSupplierModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                model.SupplierId = id;
                _manager.Update(_mapper.Map<Supplier>(model));   // 
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating supplier");
                ModelState.AddModelError("", "Error updating supplier");
                return View(model);
            }
        }
        // Details
        [AllowAnonymous]
        // або [Authorize] — якщо тільки залогінені
        public IActionResult Details(int id)
        {
            var supplier = _manager.GetAll()
                .FirstOrDefault(s => s.SupplierID == id);

            if (supplier == null)
                return NotFound();

            var model = _mapper.Map<SupplierDetailsModel>(supplier);
            return View(model);
        }


        // Delete
        // GET: Suppliers/Delete/5
        [Authorize(Roles = "SupplierManager")]
        public IActionResult Delete(int id)
        {
            var supplier = _manager.GetAll()
                .FirstOrDefault(s => s.SupplierID == id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [Authorize(Roles = "SupplierManager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _manager.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
