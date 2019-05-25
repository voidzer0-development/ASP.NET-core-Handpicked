using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2Handpicked.ApplicationServices;
using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using B2Handpicked.LabelAPI.Authentication;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace B2Handpicked.LabelAPI.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DealsController : ControllerBase {
        public static readonly string endpoint = "deals";

        private readonly IRepository<Deal> _repo;
        private readonly IAuthentication _authentication;

        public DealsController(IRepository<Deal> dealRepository, IAuthentication authentication) {
            _repo = dealRepository;
            _authentication = authentication;
        }

        public OkObjectResult Ok(Deal model) => base.Ok(ModelToHal(model));

        public OkObjectResult Ok(IEnumerable<Deal> model) => base.Ok(ModelToHal(model));

        public CreatedAtActionResult CreatedAtAction(Deal model) => base.CreatedAtAction(nameof(Get), new { id = model.Id }, ModelToHal(model));

        // GET: api/v1/*
        [HttpGet]
        public IActionResult Get() => Ok(_repo.Filter(item => _authentication.HasAccess(HttpContext, item)));

        // GET: api/v1/*/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var model = await _repo.GetById(id);
            return model == null ? NotFound() : !_authentication.HasAccess(HttpContext, model) ? (IActionResult)Unauthorized() : Ok(model);
        }

        // PUT: api/v1/*/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Deal model) {
            foreach (var error in await _repo.GetModelErrors(model))
                ModelState.AddModelError(error.Key, error.Value);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dbModel = await _repo.GetById(id);
            if (dbModel == null) return NotFound();
            if (!_authentication.HasAccess(HttpContext, dbModel)) return Unauthorized();

            model.Id = id; // Don't allow the ID to be changed
            _authentication.SetAuthentication(HttpContext, model); // Ensure that the authentication is added to this model

            try {
                await _repo.Update(model);
            } catch (KeyNotFoundException) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/v1/*
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Deal model) {
            if (model.Id != 0) ModelState.AddModelError(nameof(model.Id), "Id must be empty");

            foreach (var error in await _repo.GetModelErrors(model))
                ModelState.AddModelError(error.Key, error.Value);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _authentication.SetAuthentication(HttpContext, model);

            return await _repo.Create(model) ? (IActionResult)CreatedAtAction(model) : BadRequest();
        }

        // DELETE: api/v1/*/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var model = await _repo.GetById(id);
            return model == null ? NotFound() : await _repo.Delete(model) ? (IActionResult)Ok(model) : BadRequest();
        }

        private static HALResponse ModelToHal(Deal model) {
            var hal = new HALResponse(model).AddLinkToSelf($"/{endpoint}/{model.Id}");

            return hal;
        }

        private static HALResponse ModelToHal(IEnumerable<Deal> models) {
            return new HALResponse(new { })
                .AddLinkToSelf($"/{endpoint}")
                .AddLink(new Link("find", $"/{endpoint}/{{{nameof(Deal.Id).ToLower()}}}"))
                .AddEmbeddedCollection("deals", models.Select(model => ModelToHal(model)));
        }
    }
}
