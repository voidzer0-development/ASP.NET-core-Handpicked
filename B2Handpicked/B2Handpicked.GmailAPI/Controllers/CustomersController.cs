using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2Handpicked.ApplicationServices;
using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using B2Handpicked.GmailAPI.Authentication;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace B2Handpicked.GmailAPI.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase {
        public static readonly string endpoint = "customers";

        private readonly IRepository<Customer> _repo;
        private readonly IAuthentication _authentication;

        public CustomersController(IRepository<Customer> customerRepository, IAuthentication authentication) {
            _repo = customerRepository;
            _authentication = authentication;
        }

        public OkObjectResult Ok(Customer model) => base.Ok(ModelToHal(model));

        public OkObjectResult Ok(IEnumerable<Customer> model) => base.Ok(ModelToHal(model));

        // GET: api/v1/*
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "email")] string email) {
            var list = _repo.Filter(item => _authentication.HasAccess(HttpContext, item));
            if (email != null) list = list.Where(item => item.Email == email);
            return Ok(list);
        }

        // GET: api/v1/*/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var model = await _repo.GetById(id);
            return model == null ? NotFound() : !_authentication.HasAccess(HttpContext, model) ? (IActionResult)Unauthorized() : Ok(model);
        }

        private static HALResponse ModelToHal(Customer model) {
            var hal = new HALResponse(model)
                .AddLinkToSelf($"/{endpoint}/{model.Id}");
            //.AddEmbeddedCollection("invoices", model.Invoices.Select(invoice => new HALResponse(new { invoice.Id })))
            //.AddEmbeddedCollection("deals", model.Deals.Select(deal => new HALResponse(new { deal.Id })));

            if (model.LabelId != null) hal.AddLink(new Link("label", $"/{LabelsController.endpoint}/{model.LabelId}"));

            return hal;
        }

        private static HALResponse ModelToHal(IEnumerable<Customer> models) {
            return new HALResponse(new { })
                .AddLinkToSelf($"/{endpoint}")
                .AddLink(new Link("find", $"/{endpoint}/{{{nameof(Customer.Id).ToLower()}}}"))
                .AddEmbeddedCollection("customers", models.Select(model => ModelToHal(model)));
        }
    }
}
