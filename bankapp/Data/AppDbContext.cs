using bankapp.Models;
using bankapp.Models.member;
using bankapp.Models.NewAccount;
using Microsoft.EntityFrameworkCore;

namespace bankapp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Member> members { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<Municipality> municipality { get; set; }
        public DbSet<Ward> ward { get; set; }
        public DbSet<Tole> tole { get; set; }
        public DbSet<AccountCreate> accountCreate { get; set; }
        public DbSet<AccountType> accountType { get; set; }

        public DbSet<WithdrawAccount> withdrawAccount { get; set; }


    }
}
