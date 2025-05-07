using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities.IdentityEntities;


namespace Shaghaf.Infrastructure.IdentityData
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        //DI
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        } 

    }
}
