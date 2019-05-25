using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;

namespace B2Handpicked.LabelAPI.Authentication {
    public static class AuthenticationExtensions {
        public static string GetToken(this HttpContext context) {
            var bearerAuth = context?.Request?.Headers?["Authorization"].FirstOrDefault();
            return bearerAuth != null && bearerAuth.StartsWith("Bearer ") ? bearerAuth.Replace("Bearer ", "") : null;
        }

        public static void SetBody(this HttpResponse response, string body) {
            var bytes = Encoding.ASCII.GetBytes(body);
            response.Body.Write(bytes, 0, bytes.Length);
        }
    }
}
