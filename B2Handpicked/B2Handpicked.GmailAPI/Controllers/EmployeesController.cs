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
    public class EmployeesController : ControllerBase {
        public static readonly string endpoint = "employees";

        private readonly IRepository<Employee> _repo;
        private readonly IAuthentication _authentication;

        public EmployeesController(IRepository<Employee> employeeRepository, IAuthentication authentication) {
            _repo = employeeRepository;
            _authentication = authentication;
        }

        public OkObjectResult Ok(Employee model) => base.Ok(ModelToHal(model));

        public OkObjectResult Ok(IEnumerable<Employee> model) => base.Ok(ModelToHal(model));

        public CreatedAtActionResult CreatedAtAction(Employee model) => base.CreatedAtAction(nameof(Get), new { id = model.Id }, ModelToHal(model));

        // GET: api/v1/*
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "email")] string email) {
            var list = _repo.Filter(item => _authentication.HasAccess(HttpContext, item));
            if (email != null) list = list.Where(item => item.Gmail == email);
            return Ok(list);
        }

        // GET: api/v1/*/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var model = await _repo.GetById(id);
            return model == null ? NotFound() : !_authentication.HasAccess(HttpContext, model) ? (IActionResult)Unauthorized() : Ok(model);
        }

        private static HALResponse ModelToHal(Employee model) {
            var hal = new HALResponse(model).AddLinkToSelf($"/{endpoint}/{model.Id}");

            return hal;
        }

        private static HALResponse ModelToHal(IEnumerable<Employee> models) {
            return new HALResponse(new { })
                .AddLinkToSelf($"/{endpoint}")
                .AddLink(new Link("find", $"/{endpoint}/{{{nameof(Employee.Id).ToLower()}}}"))
                .AddEmbeddedCollection("employees", models.Select(model => ModelToHal(model)));
        }
    }
}
