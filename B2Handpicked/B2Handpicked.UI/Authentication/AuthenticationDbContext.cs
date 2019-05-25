using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B2Handpicked.UI.Authentication {
    public class AuthenticationDbContext : IdentityDbContext<IdentityUser> {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }
    }
}
