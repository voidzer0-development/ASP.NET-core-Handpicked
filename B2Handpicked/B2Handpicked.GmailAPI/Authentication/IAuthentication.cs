using Microsoft.AspNetCore.Http;

namespace B2Handpicked.GmailAPI.Authentication {
    public interface IAuthentication {
        bool HasAccess(HttpContext context, object model);
    }
}
