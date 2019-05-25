using Microsoft.AspNetCore.Http;

namespace B2Handpicked.LabelAPI.Authentication {
    public interface IAuthentication {
        bool HasAccess(HttpContext context, object model);
        void SetAuthentication(HttpContext context, object model);
    }
}
