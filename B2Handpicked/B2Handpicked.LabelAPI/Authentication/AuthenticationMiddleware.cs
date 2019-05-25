using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace B2Handpicked.LabelAPI.Authentication {
    public class AuthenticationMiddleware {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRepository<Label> labelsRepository) {
            var token = context.GetToken();

            if (token != null && labelsRepository.GetAllAsQueryable().Any(e => e.Token == token)) {
                await _next.Invoke(context);
            } else {
                context.Response.StatusCode = 401; // Unauthorized
                context.Response.ContentType = "application/json";
                context.Response.SetBody("{\"errorCode\":401, \"errorMessage\": \"Unauthorised: invalid token\"}");
            }
        }
    }
}
