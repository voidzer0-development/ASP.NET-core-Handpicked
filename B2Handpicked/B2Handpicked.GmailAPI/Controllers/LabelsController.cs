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
    public class LabelsController : ControllerBase {
        public static readonly string endpoint = "labels";

        private readonly IRepository<Label> _repo;
        private readonly IAuthentication _authentication;

        public LabelsController(IRepository<Label> labelRepository, IAuthentication authentication) {
            _repo = labelRepository;
            _authentication = authentication;
        }

        public OkObjectResult Ok(Label model) => base.Ok(ModelToHal(model));

        public OkObjectResult Ok(IEnumerable<Label> model) => base.Ok(ModelToHal(model));

        // GET: api/v1/*
        [HttpGet]
        public IActionResult Get() => Ok(_repo.Filter(item => _authentication.HasAccess(HttpContext, item)));

        // GET: api/v1/*/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var model = await _repo.GetById(id);
            return model == null ? NotFound() : !_authentication.HasAccess(HttpContext, model) ? (IActionResult) Unauthorized() : Ok(model);
        }

        private static HALResponse ModelToHal(Label model) {
            var hal = new HALResponse(model).AddLinkToSelf($"/{endpoint}/{model.Id}");
            return hal;
        }

        private static HALResponse ModelToHal(IEnumerable<Label> models) {
            return new HALResponse(new { })
                .AddLinkToSelf($"/{endpoint}")
                .AddLink(new Link("find", $"/{endpoint}/{{{nameof(Label.Id).ToLower()}}}"))
                .AddEmbeddedCollection("labels", models.Select(model => ModelToHal(model)));
        }
    }
}