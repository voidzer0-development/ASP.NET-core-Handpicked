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
    public class ContactPersonsController : ControllerBase {
        public static readonly string endpoint = "contactpersons";

        private readonly IRepository<ContactPerson> _repo;
        private readonly IAuthentication _authentication;

        public ContactPersonsController(IRepository<ContactPerson> contactPersonRepository, IAuthentication authentication) {
            _repo = contactPersonRepository;
            _authentication = authentication;
        }

        public OkObjectResult Ok(ContactPerson model) => base.Ok(ModelToHal(model));

        public OkObjectResult Ok(IEnumerable<ContactPerson> model) => base.Ok(ModelToHal(model));

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

        private static HALResponse ModelToHal(ContactPerson model) {
            var hal = new HALResponse(model).AddLinkToSelf($"/{endpoint}/{model.Id}");

            return hal;
        }

        private static HALResponse ModelToHal(IEnumerable<ContactPerson> models) {
            return new HALResponse(new { })
                .AddLinkToSelf($"/{endpoint}")
                .AddLink(new Link("find", $"/{endpoint}/{{{nameof(ContactPerson.Id).ToLower()}}}"))
                .AddEmbeddedCollection("contactpersons", models.Select(model => ModelToHal(model)));
        }
    }
}
