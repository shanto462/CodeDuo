using CodeDuo.Areas.DB.Data;
using CodeDuo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeDuo.Data;

public class CodeDuoDbContext : IdentityDbContext<CodeDuoUser>
{
    public DbSet<CodeData> CodeDatas { get; set; }
    public DbSet<CodeDataShare> CodeDataShares { get; set; }

    public CodeDuoDbContext(DbContextOptions<CodeDuoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
