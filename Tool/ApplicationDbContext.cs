using Microsoft.EntityFrameworkCore;
using Test.ViewModels;

namespace Test.Tool;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // 替换为你的实际连接字符串
            optionsBuilder.UseSqlServer("Server=.;Database=test;User Id=sa;Password=12315;Encrypt=False;TrustServerCertificate=True");
        }
    }
}