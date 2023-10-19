using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace financasSimples.Identity.Context;

public class IdentityDataContext : IdentityDbContext
{

    public IdentityDataContext()
    {
        
    }

    public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) 
    {

    }

}