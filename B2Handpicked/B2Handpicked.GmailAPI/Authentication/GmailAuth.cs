using Microsoft.AspNetCore.Http;

namespace B2Handpicked.GmailAPI.Authentication {
    public class GmailAuth : IAuthentication {
        public bool HasAccess(HttpContext context, object model) => true;
    }
}
