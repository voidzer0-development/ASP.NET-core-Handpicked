using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using Halcyon.HAL;
using B2Handpicked.ApplicationServices;
using B2Handpicked.GmailAPI.Authentication;
using System.Linq;

namespace B2Handpicked.GmailAPI.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase {
        public static readonly string endpoint = "invoices";

        private readonly IRepository<Invoice> _repo;
        private readonly IAuthentication _authentication;

        public InvoicesController(IRepository<Invoice> invoiceRepository, IAuthentication authentication) {
            _repo = invoiceRepository;
            _authentication = authentication;
        }

        public OkObjectResult Ok(Invoice model) => base.Ok(ModelToHal(model));

        public OkObjectResult Ok(IEnumerable<Invoice> model) => base.Ok(ModelToHal(model));

        // GET: api/v1/*
        [HttpGet]
        public IActionResult Get() => Ok(_repo.Filter(item => _authentication.HasAccess(HttpContext, item)));

        // GET: api/v1/*/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var model = await _repo.GetById(id);
            return model == null ? NotFound() : !_authentication.HasAccess(HttpContext, model) ? (IActionResult) Unauthorized() : Ok(model);
        }

        private static HALResponse ModelToHal(Invoice model) {
            var hal = new HALResponse(model).AddLinkToSelf($"/{endpoint}/{model.Id}");
            if (model.CustomerId != null) hal.AddLink(new Link("customer", $"/{CustomersController.endpoint}/{model.CustomerId}"));
            if (model.LabelId != null) hal.AddLink(new Link("label", $"/{LabelsController.endpoint}/{model.LabelId}"));
            return hal;
        }

        private static HALResponse ModelToHal(IEnumerable<Invoice> models) {
            return new HALResponse(new { })
                .AddLinkToSelf($"/{endpoint}")
                .AddLink(new Link("find", $"/{endpoint}/{{{nameof(Invoice.Id).ToLower()}}}"))
                .AddEmbeddedCollection("invoices", models.Select(model => ModelToHal(model)));
        }
    }
}