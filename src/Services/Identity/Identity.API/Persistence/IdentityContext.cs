using Identity.API.Models;

namespace Identity.API.Persistence;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
	public IdentityContext(DbContextOptions options) : base(options)
	{
	}

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<ApplicationUser>().HasData(InitialUsers);
		base.OnModelCreating(builder);
	}

	private ApplicationUser[] InitialUsers
	{
		get
		{
			PasswordHasher<ApplicationUser> hasher = new();

			var user1 = new ApplicationUser() { UserName = "johndoe", FirstName = "John", LastName = "Doe" };
			user1.PasswordHash = hasher.HashPassword(user1, "1");

			var user2 = new ApplicationUser() { UserName = "richardroe", FirstName = "Richard", LastName = "Roe" };
			user2.PasswordHash = hasher.HashPassword(user2, "2");

			return [
				user1,
				user2
			];
		}
	}
}
