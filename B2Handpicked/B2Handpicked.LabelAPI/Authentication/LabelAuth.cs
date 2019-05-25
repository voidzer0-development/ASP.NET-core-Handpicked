using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace B2Handpicked.LabelAPI.Authentication {
    public class LabelAuth : IAuthentication {
        private readonly IRepository<Label> _labelRepository;

        public LabelAuth(IRepository<Label> labelRepository) {
            _labelRepository = labelRepository;
        }

        private Label GetLabel(HttpContext context) => _labelRepository.GetAllAsQueryable().FirstOrDefault(item => item.Token == context.GetToken());

        public bool HasAccess(HttpContext context, object model) {
            if (model is Invoice invoice)
                return GetLabel(context)?.InvoiceIds?.Any(id => invoice.LabelId == id) ?? false;
            if (model is Customer customer)
                return GetLabel(context)?.CustomerIds?.Any(id => customer.LabelId == id) ?? false;
            if (model is Deal deal)
                return GetLabel(context)?.DealIds?.Any(id => deal.LabelId == id) ?? false;
            if (model is Employee employee)
                return GetLabel(context)?.EmployeeIds?.Any(id => employee.LabelId == id) ?? false;
            if (model is ContactPerson contactPerson)
                return GetLabel(context)?.ContactPersonIds?.Any(id => contactPerson.LabelId == id) ?? false;

            return false;
        }

        public void SetAuthentication(HttpContext context, object model) {
            if (model is Invoice invoice)
                invoice.LabelId = GetLabel(context)?.Id;
            if (model is Customer customer)
                customer.LabelId = GetLabel(context)?.Id;
            if (model is Employee employee)
                employee.LabelId = GetLabel(context)?.Id;
        }
    }
}
